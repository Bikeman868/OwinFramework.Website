﻿using System;
using System.IO;
using System.Reflection;
using Microsoft.Owin;
using Ioc.Modules;
using Ninject;
using Urchin.Client.Sources;
using Owin;
using OwinFramework.Builder;
using OwinFramework.Interfaces.Builder;
using OwinFramework.Interfaces.Utility;
using OwinFramework.Less;
using OwinFramework.Pages.DebugMiddleware;
using OwinFramework.Pages.Standard;
using OwinFramework.StaticFiles;
using OwinFramework.NotFound;
using OwinFramework.ExceptionReporter;
using OwinFramework.OutputCache;
using OwinFramework.Pages.Core;
using OwinFramework.Pages.Core.Interfaces.Builder;
using OwinFramework.Pages.Core.Interfaces.Managers;
using Website.Navigation;
using OwinFramework.Versioning;

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
#if DEBUG
            var configFile = new FileInfo(hostingEnvironment.MapPath("config.debug.json"));
#else
            var configFile = new FileInfo(hostingEnvironment.MapPath("config.json"));
#endif
            _configurationFileSource = ninject.Get<FileSource>().Initialize(configFile, TimeSpan.FromSeconds(5));

            var config = ninject.Get<IConfiguration>();

            var pipelineBuilder = ninject.Get<IBuilder>().EnableTracing();

            pipelineBuilder.Register(ninject.Get<ExceptionReporterMiddleware>()).ConfigureWith(config, "/middleware/exceptionReporter").RunFirst();
            pipelineBuilder.Register(ninject.Get<OutputCacheMiddleware>()).ConfigureWith(config, "/middleware/outputCache").As("OutputCache");
            pipelineBuilder.Register(ninject.Get<VersioningMiddleware>()).ConfigureWith(config, "/middleware/versioning").As("Versioning").RunAfter("OutputCache");
            pipelineBuilder.Register(ninject.Get<LessMiddleware>()).ConfigureWith(config, "/middleware/less").As("Less").RunAfter("Versioning");
            pipelineBuilder.Register(ninject.Get<StaticFilesMiddleware>()).ConfigureWith(config, "/middleware/staticFiles/assets").As("StaticFiles").RunAfter("Less");
            pipelineBuilder.Register(ninject.Get<DebugInfoMiddleware>()).ConfigureWith(config, "/middleware/debugInfo").As("DebugInfo");
            pipelineBuilder.Register(ninject.Get<PagesMiddleware>()).ConfigureWith(config, "/middleware/pages").RunAfter("StaticFiles").RunAfter("DebugInfo");
            pipelineBuilder.Register(ninject.Get<NotFoundMiddleware>()).ConfigureWith(config, "/middleware/notFound").RunLast();

            app.UseBuilder(pipelineBuilder);

            var fluentBuilder = ninject.Get<IFluentBuilder>();

            ninject.Get<OwinFramework.Pages.Framework.BuildEngine>().Install(fluentBuilder);
            ninject.Get<OwinFramework.Pages.Html.BuildEngine>().Install(fluentBuilder);

            fluentBuilder.RegisterPackage(ninject.Get<MenuPackage>(), "menu");
            fluentBuilder.RegisterPackage(ninject.Get<LayoutsPackage>(), "layouts");
            fluentBuilder.RegisterPackage(ninject.Get<TextEffectsPackage>(), "text");

            fluentBuilder.Register(Assembly.GetExecutingAssembly(), t => ninject.Get(t));

            LoadTemplates(ninject);

            var nameManager = ninject.Get<INameManager>();
            nameManager.Bind();
        }

        private void LoadTemplates(StandardKernel ninject)
        {
            var markdownParser = ninject.Get<OwinFramework.Pages.Html.Templates.MarkdownParser>();
            var asIsParser = ninject.Get<OwinFramework.Pages.Html.Templates.AsIsParser>();
            var mustacheParser = ninject.Get<OwinFramework.Pages.Html.Templates.MustacheParser>();

            var uriLoader = ninject.Get<OwinFramework.Pages.Html.Templates.UriLoader>();
            uriLoader.ReloadEvery(TimeSpan.FromHours(12));

            foreach (var project in SiteMap.Instance.Projects)
            {
                var repository = project.Repository;
                var repositoryName = repository.GitHubRepositoryName;
                var ownerName = repository.Owner.GitHubAccountName;

                var uri = new Uri("https://raw.githubusercontent.com/" + ownerName + "/" + repositoryName + "/master/" + project.ProjectName + "/readme.md");
                var templatePath = "/content/project/" + project.ProjectName + "/readme";
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
                var templatePath = "/content/repository/" + repositoryName + "/readme";
                try
                {
                    uriLoader.LoadUri(uri, markdownParser, templatePath);
                }
                catch { }
            }

            var fileLoader = ninject.Get<OwinFramework.Pages.Html.Templates.FileSystemLoader>();
#if DEBUG
            fileLoader.ReloadEvery(TimeSpan.FromSeconds(5));
#endif

            fileLoader.Load(markdownParser, p => p.Value.EndsWith(".md"));
            fileLoader.Load(asIsParser, p => p.Value.EndsWith(".html"));
            fileLoader.Load(mustacheParser, p => p.Value.EndsWith(".mustache"));
        }
    }
}