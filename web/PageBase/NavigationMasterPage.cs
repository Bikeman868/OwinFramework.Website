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
    [UsesComponent("page__head")]
    internal class TitleRegion : NavigationElement { }

    [IsLayout("header_bar", "hamburger,title")]
    [ZoneRegion("hamburger", "menu:mobile_menu")]
    [ZoneRegion("title", "title")]
    [NeedsComponent("menu:menuStyle1")]
    internal class HeaderBarLayout : NavigationElement { }

    [IsRegion("header_bar")]
    [Container("div", "{ns}_header_bar_region")]
    [UsesLayout("header_bar")]
    internal class HeaderBarRegion : NavigationElement { }

    [IsLayout("header", "header-bar,menu")]
    [ZoneRegion("header-bar", "header_bar")]
    [ZoneRegion("menu", "menu:desktop_menu")]
    [NeedsComponent("menu:menuStyle1")]
    internal class HeaderLayout : NavigationElement { }

    [IsRegion("header")]
    [Container("div", "{ns}_header-region")]
    [UsesLayout("header")]
    internal class HeaderRegion : NavigationElement { }

    //------------------------------------------------------------------------------------
    // Footer - is the same design on all pages

    [IsComponent("footer")]
    [RenderHtml("footer.standard", "<p class='{ns}_footer'>Copyright 2014-2020</p>")]
    internal class FooterComponent : NavigationElement { }

    [IsRegion("footer")]
    [Container("div", "{ns}_footer-region")]
    [UsesComponent("footer")]
    internal class MainFooterRegion : NavigationElement { }

    //------------------------------------------------------------------------------------
    // Sitemap index - contains an index of all pages on the website

    [IsRegion("index")]
    [Container("div", "{ns}_sitemap-region")]
    [UsesComponent("sitemap_index")]
    internal class IndexRegion: ContentElement { }

    //------------------------------------------------------------------------------------
    // Body - the body region has a different layout on each page

    [IsRegion("body")]
    [Container("div", "{ns}_body-region")]
    internal class BodyRegion: ContentElement { }

    //------------------------------------------------------------------------------------
    // Base page for most regular pages on the website

    [IsLayout("navigationPage", "header,(index,body),footer")]
    [Container("div", "{ns}_page")]
    [ChildContainer("div", "{ns}_body")]
    [ZoneRegion("header", "header")]
    [ZoneRegion("index", "index")]
    [ZoneRegion("body", "body")]
    [ZoneRegion("footer", "footer")]
    [ZoneLayout("header", "header")]
    [ZoneComponent("footer", "footer")]
    internal class NavigationPageLayout : NavigationElement { }

    [UsesLayout("navigationPage")]
    [DeployedAs("content")]
    public class NavigationMasterPage : BlankMasterPage
    {
    }
}