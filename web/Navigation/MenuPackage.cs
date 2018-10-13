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
    /// <summary>
    /// This package defines a region that will render a horizontal menu bar
    /// with drop-down sub-menus using pure CSS and no JavaScript. The menu
    /// skin is a separate component that you can add to your page, or you
    /// can style the menu your way.
    /// </summary>
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

        private class MobileHeaderComponent : Component
        {
            public MobileHeaderComponent(IComponentDependenciesFactory dependencies)
                : base(dependencies)
            {
                PageAreas = new[] { PageArea.Body };
            }

            public override IWriteResult WritePageArea(
                IRenderContext context,
                PageArea pageArea)
            {
                if (pageArea == PageArea.Body)
                {
                    var menuItem = context.Data.Get<MenuItem>();
                    context.Html.WriteElementLine("li", menuItem.Name, "class", Package.NamespaceName + "_mb_heading");
                }
                return WriteResult.Continue();
            }
        }

        private class HamburgerButtonComponent : Component
        {
            public HamburgerButtonComponent(IComponentDependenciesFactory dependencies)
                : base(dependencies)
            {
                PageAreas = new[] { PageArea.Body };
            }

            public override IWriteResult WritePageArea(
                IRenderContext context,
                PageArea pageArea)
            {
                if (pageArea == PageArea.Body)
                {
                    var ns = Package.NamespaceName;
                    context.Html.WriteOpenTag("input", true, "type", "checkbox", "class", ns + "_mb_hamburger_button", "id", ns + "_mb_hamburger_button");
                    context.Html.WriteLine();
                    context.Html.WriteOpenTag("label", "for", ns + "_mb_hamburger_button", "class", ns + "_mb_hamburger_icon");
                    context.Html.WriteElementLine("div", null, "class", ns + "_mb_hamburger_icon " + ns + "_mb_hamburger_icon_1");
                    context.Html.WriteElementLine("div", null, "class", ns + "_mb_hamburger_icon " + ns + "_mb_hamburger_icon_2");
                    context.Html.WriteElementLine("div", null, "class", ns + "_mb_hamburger_icon " + ns + "_mb_hamburger_icon_3");
                    context.Html.WriteCloseTag("label");
                }
                return WriteResult.Continue();
            }
        }

        // Desktop menu behavior
        [DeployCss("ul.{ns}_dt_menu", "list-style-type: none; overflow: hidden; white-space: nowrap;", 1)]
        [DeployCss("li.{ns}_dt_option", "display: inline-block;", 2)]
        [DeployCss("li.{ns}_dt_option a, a.{ns}_dt_option", "display: inline-block; text-decoration: none;", 3)]
        [DeployCss("div.{ns}_dt_dropdown", "display: none; position: absolute; overflow: hidden; z-index: 1;", 4)]
        [DeployCss("div.{ns}_dt_dropdown a", "text-decoration: none; display: block; text-align: left", 5)]
        [DeployCss("li.{ns}_dt_option:hover div.{ns}_dt_dropdown", "display: block;", 6)]

        // Hamburger button behavior
        [DeployCss("input[type=checkbox].{ns}_mb_hamburger_button", "display: none;", 20)]
        [DeployCss("label.{ns}_mb_hamburger_button", "transition: all 0.3s; cursor: pointer; ", 21)]
        [DeployCss("div.{ns}_mb_hamburger_icon", "transition: all 0.3s; position: relative; float: left; width: 100%;", 22)]
        [DeployCss("input[type=checkbox].{ns}_mb_hamburger_button:checked + label > .{ns}_mb_hamburger_icon_1", "transition: all 0.3s; transform: rotate(135deg);", 23)]
        [DeployCss("input[type=checkbox].{ns}_mb_hamburger_button:checked + label > .{ns}_mb_hamburger_icon_2", "transition: all 0.3s; opacity: 0;", 23)]
        [DeployCss("input[type=checkbox].{ns}_mb_hamburger_button:checked + label > .{ns}_mb_hamburger_icon_3", "transition: all 0.3s; transform: rotate(-135deg);", 23)]

        // Slideout menu behavior
        [DeployCss("ul.{ns}_mb_slideout", "position: absolute;", 24)]
        [DeployCss("input[type=checkbox].{ns}_mb_hamburger_button:checked ~ ul.{ns}_mb_slideout", "transform: translateX(0);", 25)]
        public class MenuStyles
        { }

        // Desktop menu appearence
        [DeployCss("ul.{ns}_dt_menu", "margin: 0; padding: 0; background-color: #333", 1)]
        [DeployCss("li.{ns}_dt_option a", "color: white; text-align: center; padding: 14px 16px; font-family: sans-serif; letter-spacing: 1px;", 2)]
        [DeployCss("li.{ns}_dt_option a:hover, li.{ns}_dt_menu-option:hover a.{ns}_dt_menu-option", "background-color: red", 3)]
        [DeployCss("div.{ns}_dt_dropdown a:hover", "background-color: #f1f1f1;", 4)]
        [DeployCss("div.{ns}_dt_dropdown", "background-color: #f9f9f9; min-width: 160px; box-shadow: 0px 8px 16px 0px rgba(0,0,0,0.2);", 5)]
        [DeployCss("div.{ns}_dt_dropdown a", "color: black; padding: 12px 16px; font-family: sans-serif;", 6)]

        // Hamburger button appearence
        [DeployCss("div.{ns}_mb_menu", "height: 50px; width: 70px; float: left;", 21)]
        [DeployCss("div.{ns}_mb_hamburger_icon", "height: 3px; margin-top: 6px; background-color: white;", 21)]
        [DeployCss("label.{ns}_mb_hamburger_icon", "position: absolute; z-index: 99; left: 30px; top: 3vw; margin-top: 8px; width: 30px; height: 33px;", 23)]
        [DeployCss("input[type=checkbox].{ns}_mb_hamburger_button:checked + label > .{ns}_mb_hamburger_icon_1", "margin-top: 16px;", 23)]
        [DeployCss("input[type=checkbox].{ns}_mb_hamburger_button:checked + label > .{ns}_mb_hamburger_icon_3", "margin-top: -12px;", 23)]

        // Slideout menu appearence
        [DeployCss("ul.{ns}_mb_slideout", "left: 10px; top:44px; margin-top: 5.9vw; width: 250px; transform: translateX(-300px); transition: transform 250ms ease-in-out; background-color: #f9f9f9; box-shadow: 0px 8px 16px 0px rgba(0,0,0,0.2); padding: 0; font-family: sans-serif; letter-spacing: 1px;", 24)]
        [DeployCss("ul.{ns}_mb_slideout li", "padding: 6px; border-bottom: 1px solid rgba(255, 255, 255, 0.25); font-size: 14px;", 24)]
        [DeployCss("li.{ns}_mb_heading", "color: whitesmoke; background-color: black;", 24)]
        [DeployCss("li.{ns}_mb_heading span", "display: block; font-size: 10px;", 24)]
        [DeployCss("li.{ns}_mb_option", "color: black; cursor: pointer;", 24)]
        [DeployCss("li.{ns}_mb_option:hover", "background-color: #f1f1f1;", 24)]
        [DeployCss("li.{ns}_mb_option a", "color: black;", 24)]
        [DeployCss("li.{ns}_mb_option a:visited", "color: black;", 24)]
        public class MenuStyle1
        { }

        public override IPackage Build(IFluentBuilder builder)
        {
            /******************************************************************
             * 
             * These things are shared between the desktop and mobile menus
             * 
             ******************************************************************/

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
            // using fluent syntax.
            var subMenuDataProvider = builder.BuildUpDataProvider()
                .BindTo<MenuItem>()
                .Provides<IList<MenuItem>>((rc, dc, d) => 
                    {
                        var menuItem = dc.Get<MenuItem>();
                        dc.Set(menuItem.SubMenu, "submenu");
                    },
                    "submenu")
                .Build();

            /******************************************************************
             * 
             * These things are for the desktop menu only
             * 
             ******************************************************************/

            // This region is a container for the options on the desktop menu
            var desktopMenuItemRegion = builder.BuildUpRegion()
                .BindTo<MenuItem>()
                .Tag("div")
                .Component(mainMenuItemComponent)
                .Build();

            // This region is a container for the desktop drop down menu items. It
            // renders one menu item component for each menu item in the sub-menu
            var desktopDropDownMenuRegion = builder.BuildUpRegion()
                .Tag("div")
                .ClassNames("{ns}_dt_dropdown")
                .ForEach<MenuItem>("submenu", null, null, "submenu")
                .Component(subMenuItemComponent)
                .Build();

            // This layout defines the desktop menu option and the sub-menu that
            // drops down when the main menu option is tapped or hovered
            var desktopOptionLayout = builder.BuildUpLayout()
                .Tag("li")
                .ClassNames("{ns}_dt_option")
                .RegionNesting("head,submenu")
                .Region("head", desktopMenuItemRegion)
                .Region("submenu", desktopDropDownMenuRegion)
                .DataProvider(subMenuDataProvider)
                .Build();

            // This region is the whole desktop menu structure with top level menu 
            // options and sub-menus beneath each option. This is the region you
            // need to add to a layout in your page to display a desktop menu.
            builder.BuildUpRegion()
                .Name("desktop_menu")
                .Tag("ul")
                .NeedsComponent("menuStyles")
                .ClassNames("{ns}_dt_menu")
                .ForEach<MenuItem>("", "", "", "desktop")
                .Layout(desktopOptionLayout)
                .Build();

            /******************************************************************
             * 
             * These things are for the mobile menu only
             * 
             ******************************************************************/

            var mobileHamburgerButtonComponent = builder.BuildUpComponent(                
                new HamburgerButtonComponent(Dependencies.ComponentDependenciesFactory))
                .Build();

            // This component displays a main menu item
            var mobileHeaderComponent = builder.BuildUpComponent(
                new MobileHeaderComponent(Dependencies.ComponentDependenciesFactory))
                .BindTo<MenuItem>()
                .Build();

            var mobileMenuHead = builder.BuildUpRegion()
                .Tag("")
                .Component(mobileHeaderComponent)
                .Build();

            var mobileSubMenu = builder.BuildUpRegion()
                .Tag("")
                .Component(subMenuItemComponent)
                .ForEach<MenuItem>("submenu", "li", null, "submenu", "{ns}_mb_option")
                .Build();

            var mobileMenuOptionLayout = builder.BuildUpLayout()
                .Tag("")
                .RegionNesting("head,subMenu")
                .Region("head", mobileMenuHead)
                .Region("subMenu", mobileSubMenu)
                .DataProvider(subMenuDataProvider)
                .Build();

            var mobileHamburgerRegion = builder.BuildUpRegion()
                .Tag("")
                .Component(mobileHamburgerButtonComponent)
                .Build();

            var mobileSlideoutRegion = builder.BuildUpRegion()
                .Tag("ul")
                .ClassNames("{ns}_mb_slideout")
                .ForEach<MenuItem>("", "", "", "mobile")
                .Layout(mobileMenuOptionLayout)
                .Build();

            var mobileMenuLayout = builder.BuildUpLayout()
                .RegionNesting("hamburger,slideout")
                .Region("hamburger", mobileHamburgerRegion)
                .Region("slideout", mobileSlideoutRegion)
                .Build();

            // This region is the whole mobile menu structure with a hamburger
            // button that opens and closes a slide-out sidebar menu.
            builder.BuildUpRegion()
                .Name("mobile_menu")
                .NeedsComponent("menuStyles")
                .ClassNames("{ns}_mb_menu")
                .Layout(mobileMenuLayout)
                .Build();

            return this;
        }

    }
}
