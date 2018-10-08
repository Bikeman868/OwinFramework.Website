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
    [IsDataProvider("functional_area__document", typeof(Sitemap.Document))]
    [NeedsData(typeof(Sitemap.FunctionalArea))]
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
            if (dependency == null)
                return;

            if (dependency.DataType == typeof(Sitemap.Document))
            {
                var functionalArea = dataContext.Get<Sitemap.FunctionalArea>();
                if (functionalArea != null)
                    dataContext.Set(functionalArea.Document);
                return;
            }

            throw new Exception(GetType().DisplayName() + " can not supply " +
                dependency.DataType.DisplayName());
        }
    }
}