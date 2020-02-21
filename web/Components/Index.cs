using OwinFramework.Pages.Core.Attributes;
using OwinFramework.Pages.Core.Enums;
using OwinFramework.Pages.Core.Interfaces.Builder;
using OwinFramework.Pages.Core.Interfaces.Runtime;
using OwinFramework.Pages.Html.Elements;
using OwinFramework.Pages.Html.Runtime;
using OwinFramework.Pages.Standard;
using System.Collections.Generic;

namespace Website.Components.Project
{
    [IsComponent("index")]
    [NeedsData(typeof(IList<MenuPackage.MenuItem>), "sitemap")]
    [PartOf("application")]
    public class Index: Component
    {
        public Index(IComponentDependenciesFactory dependencies) 
            : base(dependencies)
        {
        }

        public override IWriteResult WritePageArea(IRenderContext context, PageArea pageArea)
        {
           if (pageArea == PageArea.Body)
           {
               var menuItems  = context.Data.Get<IList<MenuPackage.MenuItem>>("sitemap");
               WriteMenu(context, menuItems);
           }
           return WriteResult.Continue();
        }

        private void WriteMenu(IRenderContext context, IList<MenuPackage.MenuItem> menuItems)
        {
            if (menuItems == null) return;

            context.Html.WriteOpenTag("ul", "class", Package.NamespaceName + "_list-item " + Package.NamespaceName + "_sitemap-section");
            context.Html.WriteLine();

            foreach (var menuItem in menuItems)
            {
                context.Html.WriteOpenTag("li", "class", Package.NamespaceName + "_list-item " + Package.NamespaceName + "_sitemap-item");

                if (string.IsNullOrEmpty(menuItem.Url))
                {
                    context.Html.WriteElement("span", menuItem.Name);
                }
                else
                {
                    context.Html.WriteElement("a", menuItem.Name, "href", menuItem.Url);
                }

                WriteMenu(context, menuItem.SubMenu);

                context.Html.WriteCloseTag("li");
                context.Html.WriteLine();
            }

            context.Html.WriteCloseTag("ul");
            context.Html.WriteLine();
        }
    }
}