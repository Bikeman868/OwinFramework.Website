﻿using OwinFramework.Pages.Core.Attributes;
using OwinFramework.Pages.Core.Enums;
using OwinFramework.Pages.Core.Interfaces.Builder;
using OwinFramework.Pages.Core.Interfaces.Runtime;
using OwinFramework.Pages.Html.Elements;

namespace Website.Components
{
    [IsComponent("page__head")]
    [PartOf("application")]
    public class PageHead: Component
    {
        public PageHead(IComponentDependenciesFactory dependencies) : base(dependencies)
        {
            PageAreas = new [] { PageArea.Head, PageArea.Body };
        }

        public override IWriteResult WritePageArea(IRenderContext context, PageArea pageArea)
        {
            if (pageArea == PageArea.Head)
            {
                context.Html.WriteElementLine("meta", null, "name", "viewport", "content", "width=device-width, initial-scale=1.0");
                context.Html.WriteElementLine("meta", null, "name", "google-site-verification", "content", "PjvggteEimLNbI9ZWPiY9tm8iDszMPWjgoYmmVLrfp0");
                context.Html.WriteElementLine("link", null, "rel", "stylesheet", "type", "text/css", "href", "/assets/styles/app{_v_}.css");
            }
            else if (pageArea == PageArea.Body)
            {
                context.Html.WriteOpenTag("a", "class", Package.NamespaceName + "_page-head", "href", "/");
                context.Html.WriteLine();
                context.Html.WriteElementLine("h1", "Owin Framework");
                context.Html.WriteElementLine("p", "An open architecture for interoperable middleware");
                context.Html.WriteCloseTag("a");
                context.Html.WriteLine();
            }

            return base.WritePageArea(context, pageArea);
        }
    }
}