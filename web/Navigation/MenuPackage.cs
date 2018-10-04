using System.Collections.Generic;
using OwinFramework.Pages.Core.Attributes;
using OwinFramework.Pages.Core.Enums;
using OwinFramework.Pages.Core.Interfaces;
using OwinFramework.Pages.Core.Interfaces.Builder;
using OwinFramework.Pages.Core.Interfaces.Runtime;
using OwinFramework.Pages.Html.Elements;
using OwinFramework.Pages.Html.Runtime;

namespace Website.Navigation
{
    [IsPackage("menu")]
    public class MenuPackage : OwinFramework.Pages.Framework.Runtime.Package
    {
        public MenuPackage(IPackageDependenciesFactory dependencies)
            : base(dependencies) { }

        public class MenuItem
        {
            public string Name { get; set; }
            public string Url { get; set; }
            public string Target { get; set; }
            public IList<MenuItem> SubMenu { get; set; }
        }

        private class MenuItemComponent : Component
        {
            public MenuItemComponent(IComponentDependenciesFactory dependencies) 
                : base(dependencies) 
            {
                PageAreas = new []{ PageArea.Body };
            }

            public override IWriteResult WritePageArea(
                IRenderContext context, 
                PageArea pageArea)
            {
                if (pageArea == PageArea.Body)
                {
                    var menuItem = context.Data.Get<MenuItem>();
                    var url = string.IsNullOrEmpty(menuItem.Url) ? "javascript:void(0);" : menuItem.Url;
                    var attributes = string.IsNullOrEmpty(menuItem.Target)
                        ? new[] { "href", url }
                        : new[] { "href", url, "target", menuItem.Target };
                    context.Html.WriteElementLine("a", menuItem.Name, attributes);
                }
                return WriteResult.Continue();
            }
        }

        [DeployCss("ul.{ns}_menu", "list-style-type: none; overflow: hidden; white-space: nowrap;", 1)]
        [DeployCss("li.{ns}_option", "display: inline-block;", 2)]
        [DeployCss("li.{ns}_option a, a.{ns}_option", "display: inline-block; text-decoration: none;", 3)]
        [DeployCss("div.{ns}_dropdown", "display: none; position: absolute; overflow: hidden; z-index: 1;", 4)]
        [DeployCss("div.{ns}_dropdown a", "text-decoration: none; display: block; text-align: left", 5)]
        [DeployCss("li.{ns}_option:hover div.{ns}_dropdown", "display: block;", 6)]
        public class MenuStyles
        { }

        [DeployCss("ul.{ns}_menu", "margin: 0; padding: 0; background-color: #333", 1)]
        [DeployCss("li.{ns}_option a", "color: white; text-align: center; padding: 14px 16px;", 2)]
        [DeployCss("li.{ns}_option a:hover, li.{ns}_menu-option:hover a.{ns}_menu-option", "background-color: red", 3)]
        [DeployCss("div.{ns}_dropdown a:hover", "background-color: #f1f1f1;", 4)]
        [DeployCss("div.{ns}_dropdown", "background-color: #f9f9f9; min-width: 160px; box-shadow: 0px 8px 16px 0px rgba(0,0,0,0.2);", 5)]
        [DeployCss("div.{ns}_dropdown a", "color: black; padding: 12px 16px;", 6)]
        public class MenuStyle1
        { }

        public override IPackage Build(IFluentBuilder builder)
        {
            // This component outputs CSS that makes the menu work as a menu
            builder.BuildUpComponent(new MenuStyles())
                .Name("menuStyles")
                .Build();

            // This component outputs CSS that defines the menu appearence
            builder.BuildUpComponent(new MenuStyle1())
                .Name("menuStyle1")
                .NeedsComponent("menuStyles")
                .Build();

            // This component displays a main menu item
            var mainMenuItemComponent = builder.BuildUpComponent(
                new MenuItemComponent(Dependencies.ComponentDependenciesFactory))
                .BindTo<MenuItem>()
                .Build();

            // This component displays a submenu item
            var subMenuItemComponent = builder.BuildUpComponent(
                new MenuItemComponent(Dependencies.ComponentDependenciesFactory))
                .BindTo<MenuItem>("submenu")
                .Build();

            // This data provider extracts sub-menu items from the current menu item
            // using fluent syntax. No custom class is needed in this case
            var subMenuDataProvider = builder.BuildUpDataProvider()
                .BindTo<MenuItem>()
                .Provides<IList<MenuItem>>((rc, dc, d) => 
                    {
                        var menuItem = dc.Get<MenuItem>();
                        dc.Set(menuItem.SubMenu, "submenu");
                    },
                    "submenu")
                .Build();

            // This region is a container for the options on the main menu
            var mainMenuItemRegion = builder.BuildUpRegion()
                .BindTo<MenuItem>()
                .Tag("div")
                .Component(mainMenuItemComponent)
                .Build();

            // This region is a container for the drop down menu items. It
            // renders one menu item component for each menu item in the sub-menu
            var dropDownMenuRegion = builder.BuildUpRegion()
                .Tag("div")
                .ClassNames("{ns}_dropdown")
                .DataProvider(subMenuDataProvider)
                .ForEach<MenuItem>("submenu", null, null, "submenu")
                .Component(subMenuItemComponent)
                .Build();

            // This layout defines the main menu option and the sub-menu that
            // drops down wen the main menu option is tapped
            var menuOptionLayout = builder.BuildUpLayout()
                .Tag("li")
                .ClassNames("{ns}_option")
                .RegionNesting("head,submenu")
                .Region("head", mainMenuItemRegion)
                .Region("submenu", dropDownMenuRegion)
                .Build();

            // This region is the whole menu structure with top level menu 
            // options and sub-menus beneath each option
            builder.BuildUpRegion()
                .Name("menu")
                .Tag("ul")
                .NeedsComponent("menuStyles")
                .ClassNames("{ns}_menu")
                .ForEach<MenuItem>()
                .Layout(menuOptionLayout)
                .Build();

            return this;
        }

    }
}
