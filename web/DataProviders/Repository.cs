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
    [IsDataProvider(typeof(SiteMap.Repository))]
    [SuppliesData(typeof(SiteMap.RepositoryOwner))]
    public class Repository : DataProvider
    {
        private readonly Regex _urlRegex = new Regex(
            "/content/repository/([^/]*)", 
            RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);

        public Repository(IDataProviderDependenciesFactory dependencies)
            : base(dependencies)
        {
        }

        protected override void Supply(
            IRenderContext renderContext, 
            IDataContext dataContext, 
            IDataDependency dependency)
        {
            if (dependency == null || dependency.DataType != typeof(SiteMap.Repository)) return;

            SiteMap.Repository repository = null;
            SiteMap.RepositoryOwner owner = null;

            repository = GetRepository(renderContext);
            if (repository != null)
                owner = repository.Owner;

            dataContext.Set(repository);
            dataContext.Set(owner);
        }

        private SiteMap.Repository GetRepository(IRenderContext renderContext)
        {
            if (renderContext.OwinContext == null) return null;

            var match = _urlRegex.Match(renderContext.OwinContext.Request.Path.Value);
            if (match.Success)
            {
                var repositoryName = match.Groups[1].Value;
                return SiteMap.Instance.Repositories.FirstOrDefault(r => string.Equals(r.GitHubRepositoryName, repositoryName, StringComparison.InvariantCultureIgnoreCase));
            }
            return null;
        }
    }
}