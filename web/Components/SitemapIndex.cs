using OwinFramework.Pages.Core.Attributes;
using OwinFramework.Pages.Core.Enums;
using OwinFramework.Pages.Core.Interfaces.Builder;
using OwinFramework.Pages.Core.Interfaces.Runtime;
using OwinFramework.Pages.Html.Elements;
using OwinFramework.Pages.Html.Runtime;
using OwinFramework.Pages.Standard;
using System;
using System.Collections.Generic;

namespace Website.Components.Project
{
    [IsComponent("sitemap_index")]
    [NeedsData(typeof(IList<MenuPackage.MenuItem>), "sitemap")]
    [PartOf("application")]
    public class Index: Component
    {
        public Index(IComponentDependenciesFactory dependencies) 
            : base(dependencies)
        {
            PageAreas = new PageArea[] { PageArea.Body, PageArea.Initialization };
        }

        public override IWriteResult WritePageArea(IRenderContext context, PageArea pageArea)
        {
            if (pageArea == PageArea.Body)
            {
                var menuItems = context.Data.Get<IList<MenuPackage.MenuItem>>("sitemap");
                WriteMenu(context, menuItems, 0);
            }
            else if (pageArea == PageArea.Initialization)
            {
                var className = Package.NamespaceName + "_this-page";
                context.Html.WriteScriptOpen();
                context.Html.WriteLine("[].forEach.call(document.getElementsByClassName('" + className + "'), function(e){ e.scrollIntoView({block: 'center'}); });");
                context.Html.WriteScriptClose();
            }
           return WriteResult.Continue();
        }

        private void WriteMenu(IRenderContext context, IList<MenuPackage.MenuItem> menuItems, int depth)
        {
            if (menuItems == null) return;

            context.Html.WriteOpenTag("ul", "class", Package.NamespaceName + "_sitemap-section");
            context.Html.WriteLine();

            foreach (var menuItem in menuItems)
            {
                var hasSubmenu = menuItem.SubMenu != null && menuItem.SubMenu.Length > 0;
                var isThisPage = string.Equals(context.OwinContext.Request.Path.Value, menuItem.Url, StringComparison.OrdinalIgnoreCase);

                var listItemClasses = Package.NamespaceName + "_list-item " + Package.NamespaceName + (hasSubmenu ? "_sitemap-item" : "_sitemap-leaf");
                var anchorClasses = Package.NamespaceName + "_sitemap-link";

                if (isThisPage)
                {
                    if (hasSubmenu)
                        anchorClasses += " " + Package.NamespaceName + "_this-page";
                    else
                        listItemClasses += " " + Package.NamespaceName + "_this-page";
                }

                context.Html.WriteOpenTag("li", "class", listItemClasses);

                if (string.IsNullOrEmpty(menuItem.Url))
                {
                    context.Html.WriteElement(depth == 0 ? "h3" : "h4", menuItem.Name);
                }
                else
                {
                    context.Html.WriteElement("a", menuItem.Name, "href", menuItem.Url, "class", anchorClasses);
                }

                WriteMenu(context, menuItem.SubMenu, depth + 1);

                context.Html.WriteCloseTag("li");
                context.Html.WriteLine();
            }

            context.Html.WriteCloseTag("ul");
            context.Html.WriteLine();
        }
    }
}