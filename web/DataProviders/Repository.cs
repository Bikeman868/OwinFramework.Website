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
            if (dependency == null)
                return;

            if (dependency.DataType == typeof(SiteMap.Repository))
            {
                var repository = GetRepository(renderContext);
                if (repository != null)
                {
                    dataContext.Set(repository);
                    dataContext.Set(repository.Owner);
                }
                return;
            }

            throw new Exception(GetType().DisplayName() + " can not supply " +
                dependency.DataType.DisplayName());
        }

        private SiteMap.Repository GetRepository(IRenderContext renderContext)
        {
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