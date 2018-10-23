using System;
using System.IO;
using System.Reflection;
using Ioc.Modules;
using Microsoft.Owin;
using Ninject;
using Ninject.Modules;
using Owin;
using OwinFramework.Builder;
using OwinFramework.Interfaces.Builder;
using OwinFramework.Interfaces.Utility;
using OwinFramework.Less;
using OwinFramework.Pages.DebugMiddleware;
using OwinFramework.Pages.Standard;
using OwinFramework.StaticFiles;
using Urchin.Client.Sources;
using OwinFramework.Pages.Core;
using OwinFramework.Pages.Core.Interfaces.Builder;
using OwinFramework.Pages.Core.Interfaces.Managers;
using Website.Navigation;

[assembly: OwinStartup(typeof(Website.Startup))]

namespace Website
{
    public class Startup
    {
        private static IDisposable _configurationFileSource;

        public void Configuration(IAppBuilder app)
        {
            var packageLocator = new PackageLocator()
                .ProbeBinFolderAssemblies()
                .Add(Assembly.GetExecutingAssembly());

            var ninject = new StandardKernel(new Ioc.Modules.Ninject.Module(packageLocator));

            var hostingEnvironment = ninject.Get<IHostingEnvironment>();
            var configFile = new FileInfo(hostingEnvironment.MapPath("config.json"));
            _configurationFileSource = ninject.Get<FileSource>().Initialize(configFile, TimeSpan.FromSeconds(5));

            var config = ninject.Get<IConfiguration>();

            var pipelineBuilder = ninject.Get<IBuilder>().EnableTracing();

            pipelineBuilder.Register(ninject.Get<PagesMiddleware>()).ConfigureWith(config, "/middleware/pages").RunAfter("StaticFiles").RunAfter("DebugInfo");
            pipelineBuilder.Register(ninject.Get<DebugInfoMiddleware>()).ConfigureWith(config, "/middleware/debugInfo").As("DebugInfo");
            pipelineBuilder.Register(ninject.Get<LessMiddleware>()).ConfigureWith(config, "/middleware/less").As("Less");
            pipelineBuilder.Register(ninject.Get<StaticFilesMiddleware>()).ConfigureWith(config, "/middleware/staticFiles/assets").As("StaticFiles").RunAfter("Less");
            pipelineBuilder.Register(ninject.Get<OwinFramework.NotFound.NotFoundMiddleware>()).ConfigureWith(config, "/middleware/notFound").RunLast();
            pipelineBuilder.Register(ninject.Get<OwinFramework.ExceptionReporter.ExceptionReporterMiddleware>()).ConfigureWith(config, "/middleware/exceptionReporter").RunFirst();

            app.UseBuilder(pipelineBuilder);

            var fluentBuilder = ninject.Get<IFluentBuilder>();

            ninject.Get<OwinFramework.Pages.Framework.BuildEngine>().Install(fluentBuilder);
            ninject.Get<OwinFramework.Pages.Html.BuildEngine>().Install(fluentBuilder);

            fluentBuilder.Register(ninject.Get<MenuPackage>(), "menu");
            fluentBuilder.Register(ninject.Get<LayoutsPackage>(), "layout");
            fluentBuilder.Register(Assembly.GetExecutingAssembly(), t => ninject.Get(t));

            LoadTemplates(ninject);

            var nameManager = ninject.Get<INameManager>();
            nameManager.Bind();
        }

        private void LoadTemplates(StandardKernel ninject)
        {
            var uriLoader = ninject.Get<OwinFramework.Pages.Html.Templates.UriLoader>();
            uriLoader.ReloadInterval = TimeSpan.FromHours(6);

            var markdownParser = ninject.Get<OwinFramework.Pages.Html.Templates.MarkdownParser>();

            foreach (var project in SiteMap.Instance.Projects)
            {
                var repository = project.Repository;
                var repositoryName = repository.GitHubRepositoryName;
                var ownerName = repository.Owner.GitHubAccountName;

                var uri = new Uri("https://raw.githubusercontent.com/" + ownerName + "/" + repositoryName + "/master/" + project.ProjectName + "/readme.md");
                var templatePath = "/project/" + project.ProjectName.Replace('.', '_') + "/overview"; // TODO leave . in after bug fix
                try
                {
                    uriLoader.LoadUri(uri, markdownParser, templatePath);
                }
                catch { }
            }

            foreach (var repository in SiteMap.Instance.Repositories)
            {
                var repositoryName = repository.GitHubRepositoryName;
                var ownerName = repository.Owner.GitHubAccountName;

                var uri = new Uri("https://raw.githubusercontent.com/" + ownerName + "/" + repositoryName + "/master/readme.md");
                var templatePath = "/repository/" + repositoryName.Replace('.', '_') + "/landing"; // TODO leave . in after bug fix
                try
                {
                    uriLoader.LoadUri(uri, markdownParser, templatePath);
                }
                catch { }
            }
        }
    }
}