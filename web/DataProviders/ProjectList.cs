using System;
using System.Collections.Generic;
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
    [IsDataProvider(typeof(IList<SiteMap.Project>))]
    public class ProjectList : DataProvider
    {
        private readonly IList<SiteMap.Project> _projects;

        private readonly Regex _repositoryRegex = new Regex(
            "/content/repository/([^/]*)",
            RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);

        private readonly Regex _areaRegex = new Regex(
            "/content/area/([^/]*)",
            RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);

        public ProjectList(IDataProviderDependenciesFactory dependencies)
            : base(dependencies)
        {
            _projects = SiteMap.Instance.Projects.ToList();
        }

        protected override void Supply(
            IRenderContext renderContext, 
            IDataContext dataContext, 
            IDataDependency dependency)
        {
            if (dependency == null)
                return;

            if (dependency.DataType == typeof(IList<SiteMap.Project>))
            {
                var path = renderContext.OwinContext.Request.Path.Value;

                var repositoryMatch = _repositoryRegex.Match(path);
                var areaMatch = _areaRegex.Match(path);

                if (repositoryMatch.Success)
                {
                    var repositoryName = repositoryMatch.Groups[1].Value;
                    var projects = _projects
                        .Where(p => string.Equals(p.Repository.GitHubRepositoryName, repositoryName, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                    dataContext.Set<IList<SiteMap.Project>>(projects);
                }
                else if (areaMatch.Success)
                {
                    var areaName = areaMatch.Groups[1].Value;
                    var projects = _projects
                        .Where(p => string.Equals(p.FunctionalArea.Identifier, areaName, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                    dataContext.Set<IList<SiteMap.Project>>(projects);
                }
                else
                {
                    dataContext.Set(_projects);
                }
                return;
            }

            throw new Exception(GetType().DisplayName() + " can not supply " +
                dependency.DataType.DisplayName());
        }
    }
}