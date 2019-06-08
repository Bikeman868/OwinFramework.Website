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
    [IsDataProvider(typeof(SiteMap.FunctionalArea))]
    [SuppliesData(typeof(SiteMap.Document))]
    public class FunctionalArea : DataProvider
    {
        private readonly Regex _urlRegex = new Regex(
            "/content/area/([^/]*)", 
            RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);

        public FunctionalArea(IDataProviderDependenciesFactory dependencies)
            : base(dependencies)
        {
        }

        protected override void Supply(
            IRenderContext renderContext, 
            IDataContext dataContext, 
            IDataDependency dependency)
        {
            SiteMap.FunctionalArea functionalArea = null;
            SiteMap.Document document = null;

            if (dependency != null && dependency.DataType == typeof(SiteMap.FunctionalArea))
            {
                functionalArea = GetFunctionalArea(renderContext);

                if (functionalArea != null)
                    document = functionalArea.Document;
            }
            
            dataContext.Set(functionalArea);
            dataContext.Set(document);
        }

        private SiteMap.FunctionalArea GetFunctionalArea(IRenderContext renderContext)
        {
            var match = _urlRegex.Match(renderContext.OwinContext.Request.Path.Value);
            if (match.Success)
            {
                var areaName = match.Groups[1].Value;
                return SiteMap.Instance.FunctionalAreas.FirstOrDefault(a => string.Equals(a.Identifier, areaName, StringComparison.InvariantCultureIgnoreCase));
            }
            return null;
        }
    }
}