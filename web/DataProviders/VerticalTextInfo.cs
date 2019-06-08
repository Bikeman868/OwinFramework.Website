using System;
using System.Text.RegularExpressions;
using OwinFramework.Pages.Core.Attributes;
using OwinFramework.Pages.Core.Extensions;
using OwinFramework.Pages.Core.Interfaces.Builder;
using OwinFramework.Pages.Core.Interfaces.DataModel;
using OwinFramework.Pages.Core.Interfaces.Runtime;
using OwinFramework.Pages.Framework.DataModel;
using OwinFramework.Pages.Standard;

namespace Website.DataProviders
{
    [IsDataProvider(typeof(TextEffectsPackage.VerticalText))]
    public class VerticalTextInfo : DataProvider
    {
        private readonly Regex _urlRegex = new Regex(
            "/content/([^/]*)", 
            RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);

        public VerticalTextInfo(IDataProviderDependenciesFactory dependencies)
            : base(dependencies)
        {
        }

        protected override void Supply(
            IRenderContext renderContext, 
            IDataContext dataContext, 
            IDataDependency dependency)
        {
            if (dependency == null)
            {
                dataContext.Set<TextEffectsPackage.VerticalText>(null);
            }
            else if (dependency.DataType == typeof(TextEffectsPackage.VerticalText))
            {
                TextEffectsPackage.VerticalText verticalTextInfo = null;
                var area = GetContentArea(renderContext);
                if (area != null)
                {
                    area = char.ToUpper(area[0]) + area.Substring(1);
                    verticalTextInfo = new TextEffectsPackage.VerticalText(area, 60, 160)
                    {
                        Background = null,
                        TextStyle = "font: 30px serif; fill: white;"
                    };
                }
                dataContext.Set(verticalTextInfo);
            }
            else throw new Exception(GetType().DisplayName() + " can not supply " +
                dependency.DataType.DisplayName());
        }

        private string GetContentArea(IRenderContext renderContext)
        {
            var match = _urlRegex.Match(renderContext.OwinContext.Request.Path.Value);
            if (match.Success)
                return match.Groups[1].Value;

            return null;
        }
    }
}