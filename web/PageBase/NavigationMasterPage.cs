using OwinFramework.Pages.Core.Attributes;

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

    [IsComponent("title")]
    [RenderHtml("heading.main", 1, "<h1>Owin Framework</h1>")]
    [RenderHtml("heading.sub", 2, "<p>An open architecture for interoperable middleware</p>")]
    internal class TitleComponent : NavigationElement { }

    [IsRegion("title")]
    [UsesComponent("title")]
    internal class TitleRegion : NavigationElement { }

    [IsLayout("header", "title,menu")]
    [UsesRegion("title", "title")]
    [UsesRegion("menu", "menu:menu")]
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
    [PartOf("application")]
    [DeployedAs("content")]
    [Container("div", "{ns}_body-region")]
    internal class BodyRegion { }

    //------------------------------------------------------------------------------------
    // Base page for most regular pages on the website

    [IsLayout("navigationPage", "header,body,footer")]
    [Container("div", "{ns}_page")]
    [UsesRegion("header", "header")]
    [UsesRegion("body", "body")]
    [UsesRegion("footer", "footer")]
    [RegionLayout("header", "header")]
    [RegionComponent("footer", "footer")]
    internal class NavigationPageLayout : NavigationElement { }

    [UsesLayout("navigationPage")]
    [DeployedAs("content")]
    [NeedsComponent("pageHead")]
    public class NavigationMasterPage : BlankMasterPage
    {
    }
}