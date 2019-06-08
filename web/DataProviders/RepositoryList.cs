using System;
using System.Collections.Generic;
using System.Linq;
using OwinFramework.Pages.Core.Attributes;
using OwinFramework.Pages.Core.Extensions;
using OwinFramework.Pages.Core.Interfaces.Builder;
using OwinFramework.Pages.Core.Interfaces.DataModel;
using OwinFramework.Pages.Core.Interfaces.Runtime;
using OwinFramework.Pages.Framework.DataModel;
using Website.Navigation;

namespace Website.DataProviders
{
    [IsDataProvider(typeof(IList<SiteMap.Repository>))]
    public class RepositoryList : DataProvider
    {
        private readonly IList<SiteMap.Repository> _repositories;

        public RepositoryList(IDataProviderDependenciesFactory dependencies)
            : base(dependencies)
        {
            _repositories = SiteMap.Instance.Repositories.ToList();
        }

        protected override void Supply(
            IRenderContext renderContext, 
            IDataContext dataContext, 
            IDataDependency dependency)
        {
            dataContext.Set(_repositories);
        }
    }
}