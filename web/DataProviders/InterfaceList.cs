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
    [IsDataProvider(typeof(IList<SiteMap.InterfaceDefinition>))]
    public class InterfaceList : DataProvider
    {
        private readonly IList<SiteMap.InterfaceDefinition> _interfaces;

        public InterfaceList(IDataProviderDependenciesFactory dependencies)
            : base(dependencies)
        {
            _interfaces = new List<SiteMap.InterfaceDefinition>();
        }

        protected override void Supply(
            IRenderContext renderContext, 
            IDataContext dataContext, 
            IDataDependency dependency)
        {
            if (dependency == null)
                return;

            if (dependency.DataType == typeof(IList<SiteMap.InterfaceDefinition>))
            {
                dataContext.Set(_interfaces);
                return;
            }

            throw new Exception(GetType().DisplayName() + " can not supply " +
                dependency.DataType.DisplayName());
        }
    }
}