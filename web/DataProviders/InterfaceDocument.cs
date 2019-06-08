using System;
using OwinFramework.Pages.Core.Attributes;
using OwinFramework.Pages.Core.Extensions;
using OwinFramework.Pages.Core.Interfaces.Builder;
using OwinFramework.Pages.Core.Interfaces.DataModel;
using OwinFramework.Pages.Core.Interfaces.Runtime;
using OwinFramework.Pages.Framework.DataModel;
using Website.Navigation;

namespace Website.DataProviders
{
    [IsDataProvider("interface__document", typeof(SiteMap.Document))]
    [NeedsData(typeof(SiteMap.InterfaceDefinition))]
    public class InterfaceDocument : DataProvider
    {
        public InterfaceDocument(IDataProviderDependenciesFactory dependencies)
            : base(dependencies)
        {
        }

        protected override void Supply(
            IRenderContext renderContext, 
            IDataContext dataContext, 
            IDataDependency dependency)
        {
            SiteMap.Document document = null;

            if (dependency != null && dependency.DataType == typeof(SiteMap.Document))
            {
                var interfaceDefinition = dataContext.Get<SiteMap.InterfaceDefinition>();
                if (interfaceDefinition != null)
                    document = interfaceDefinition.Document;
            }

            dataContext.Set(document);
        }
    }
}