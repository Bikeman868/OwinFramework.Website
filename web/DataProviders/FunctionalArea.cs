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
    [IsDataProvider(typeof(SiteMap.FunctionalArea))]
    public class FunctionalArea : DataProvider
    {
        private readonly Regex _urlRegex = new Regex("/content/area/([^/]*)", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);

        public FunctionalArea(IDataProviderDependenciesFactory dependencies)
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

            if (dependency.DataType == typeof(SiteMap.FunctionalArea))
            {
                var match = _urlRegex.Match(renderContext.OwinContext.Request.Path.Value);
                if (match.Success)
                {
                    var area = match.Groups[1].Value;
                    dataContext.Set(SiteMap.Instance.FunctionalAreas.FirstOrDefault(a => string.Equals(a.Identifier, area)));
                }
                return;
            }

            throw new Exception(GetType().DisplayName() + " can not supply " +
                dependency.DataType.DisplayName());
        }
    }
}