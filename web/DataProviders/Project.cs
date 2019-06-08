using System;
using System.Linq;
using System.Text.RegularExpressions;
using OwinFramework.Pages.Core.Attributes;
using OwinFramework.Pages.Core.Extensions;
using OwinFramework.Pages.Core.Interfaces.Builder;
using OwinFramework.Pages.Core.Interfaces.DataModel;
using OwinFramework.Pages.Core.Interfaces.Runtime;
using OwinFramework.Pages.Framework.DataModel;
using Website.Navigation;

namespace Website.DataProviders
{
    [IsDataProvider(typeof(SiteMap.Project))]
    [SuppliesData(typeof(SiteMap.Repository))]
    [SuppliesData(typeof(SiteMap.Document))]
    public class Project : DataProvider
    {
        private readonly Regex _urlRegex = new Regex(
            "/content/project/([^/]*)", 
            RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);

        public Project(IDataProviderDependenciesFactory dependencies)
            : base(dependencies)
        {
        }

        protected override void Supply(
            IRenderContext renderContext, 
            IDataContext dataContext, 
            IDataDependency dependency)
        {
            SiteMap.Project project = null;
            SiteMap.Document document = null;
            SiteMap.Repository repository = null;

            if (dependency != null && dependency.DataType == typeof(SiteMap.Project))
            {
                project = GetProject(renderContext);
                if (project != null)
                {
                    document = project.Document;
                    repository = project.Repository;
                }
            }

            dataContext.Set(project);
            dataContext.Set(document);
            dataContext.Set(repository);
        }

        private SiteMap.Project GetProject(IRenderContext renderContext)
        {
            var match = _urlRegex.Match(renderContext.OwinContext.Request.Path.Value);
            if (match.Success)
            {
                var projectName = match.Groups[1].Value;
                return SiteMap.Instance.Projects.FirstOrDefault(p => string.Equals(p.ProjectName, projectName, StringComparison.InvariantCultureIgnoreCase));
            }
            return null;
        }
    }
}