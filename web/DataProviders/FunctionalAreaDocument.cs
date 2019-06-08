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
    [IsDataProvider("functional_area__document", typeof(SiteMap.Document))]
    [NeedsData(typeof(SiteMap.FunctionalArea))]
    public class FunctionalAreaDocument : DataProvider
    {
        public FunctionalAreaDocument(IDataProviderDependenciesFactory dependencies)
            : base(dependencies)
        {
        }

        protected override void Supply(
            IRenderContext renderContext, 
            IDataContext dataContext, 
            IDataDependency dependency)
        {
            SiteMap.Document document = null;

            if (dependency != null && dependency.DataType == typeof (SiteMap.Document))
            {
                var functionalArea = dataContext.Get<SiteMap.FunctionalArea>();
                if (functionalArea != null)
                    document = functionalArea.Document;
            }

            dataContext.Set(document);
        }
    }
}