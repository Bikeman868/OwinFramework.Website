using OwinFramework.Pages.Core.Attributes;
using Website.Content;

namespace Website.PageBase
{
    /*
     * This is the base page for all regular pages on the website. It provides a standard
     * header, navigation menu and footer, but does not specify the page content of the
     * 'body' region of the page layout, which should be specified for each page on the 
     * website.
     * */

    [PartOf("application")]
    [DeployedAs("navigation")]
    internal class NavigationElement { }

    //------------------------------------------------------------------------------------
    // Header - is the same on all pages

    [IsRegion("title")]
    [UsesComponent("pageHead")]
    internal class TitleRegion : NavigationElement { }

    [IsLayout("header_bar", "hamburger,title")]
    [LayoutRegion("hamburger", "menu:mobile_menu")]
    [LayoutRegion("title", "title")]
    [NeedsComponent("menu:menuStyle1")]
    internal class HeaderBarLayout : NavigationElement { }

    [IsRegion("header_bar")]
    [Container("div", "{ns}_header_bar_region")]
    [UsesLayout("header_bar")]
    internal class HeaderBarRegion : NavigationElement { }

    [IsLayout("header", "header-bar,menu")]
    [LayoutRegion("header-bar", "header_bar")]
    [LayoutRegion("menu", "menu:desktop_menu")]
    [NeedsComponent("menu:menuStyle1")]
    internal class HeaderLayout : NavigationElement { }

    [IsRegion("header")]
    [Container("div", "{ns}_header-region")]
    [UsesLayout("header")]
    internal class HeaderRegion : NavigationElement { }

    //------------------------------------------------------------------------------------
    // Footer - is the same design on all pages

    [IsComponent("footer")]
    [RenderHtml("footer.standard", "<p class='{ns}_footer'>Copyright 2018</p>")]
    internal class FooterComponent : NavigationElement { }

    [IsRegion("footer")]
    [Container("div", "{ns}_footer-region")]
    [UsesComponent("footer")]
    internal class MainFooterRegion : NavigationElement { }

    //------------------------------------------------------------------------------------
    // Body - the body region has a different layout on each page

    [IsRegion("body")]
    [Container("div", "{ns}_body-region")]
    internal class BodyRegion: ContentElement { }

    //------------------------------------------------------------------------------------
    // Base page for most regular pages on the website

    [IsLayout("navigationPage", "header,body,footer")]
    [Container("div", "{ns}_page")]
    [LayoutRegion("header", "header")]
    [LayoutRegion("body", "body")]
    [LayoutRegion("footer", "footer")]
    [RegionLayout("header", "header")]
    [RegionComponent("footer", "footer")]
    internal class NavigationPageLayout : NavigationElement { }

    [UsesLayout("navigationPage")]
    [DeployedAs("content")]
    public class NavigationMasterPage : BlankMasterPage
    {
    }
}