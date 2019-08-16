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
    [IsDataProvider(typeof(SiteMap.InterfaceDefinition))]
    [SuppliesData(typeof(SiteMap.Document))]
    public class InterfaceDefinition : DataProvider
    {
        private readonly Regex _urlRegex = new Regex(
            "/content/interface/([^/]*)", 
            RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);

        public InterfaceDefinition(IDataProviderDependenciesFactory dependencies)
            : base(dependencies)
        {
        }

        protected override void Supply(
            IRenderContext renderContext, 
            IDataContext dataContext, 
            IDataDependency dependency)
        {
            SiteMap.InterfaceDefinition interfaceDefinition = null;
            SiteMap.Document document = null;

            if (dependency != null && dependency.DataType == typeof(SiteMap.InterfaceDefinition))
            {
                interfaceDefinition = GetInterfaceDefinition(renderContext);

                if (interfaceDefinition != null)
                    document = interfaceDefinition.Document;
            }

            dataContext.Set(interfaceDefinition);
            dataContext.Set(document);
        }

        private SiteMap.InterfaceDefinition GetInterfaceDefinition(IRenderContext renderContext)
        {
            if (renderContext.OwinContext == null) return null;

            var match = _urlRegex.Match(renderContext.OwinContext.Request.Path.Value);
            if (match.Success)
            {
                var interfaceName = match.Groups[1].Value;
                return SiteMap.Instance.Interfaces.FirstOrDefault(i => string.Equals(i.FullyQualifiedName, interfaceName, StringComparison.InvariantCultureIgnoreCase));
            }
            return null;
        }
    }
}