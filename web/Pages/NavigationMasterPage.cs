using OwinFramework.Pages.Core.Attributes;

namespace Website.Pages
{
    /*
     * This is the base page for all regular pages on the website. It provides a standard
     * header, navigation menu and footer, but does not specify the page content of the
     * 'body' region of the page layout, which should be specified for each page on the 
     * website.
     * */

    //------------------------------------------------------------------------------------
    // Header

    [IsComponent("title")]
    [PartOf("application")]
    [DeployedAs("navigation")]
    [RenderHtml("heading.main", 1, "<h1>Owin Framework</h1>")]
    [RenderHtml("heading.sub", 2, "<p>An open architecture for interoperable middleware</p>")]
    internal class TitleComponent { }

    [IsRegion("title")]
    [PartOf("application")]
    [DeployedAs("navigation")]
    [UsesComponent("title")]
    internal class TitleRegion { }

    [IsLayout("header", "title,menu")]
    [PartOf("application")]
    [DeployedAs("navigation")]
    [UsesRegion("title", "title")]
    [UsesRegion("menu", "menu:menu")]
    [NeedsComponent("menu:menuStyle1")]
    internal class HeaderLayout { }

    [IsRegion("header")]
    [PartOf("application")]
    [DeployedAs("navigation")]
    [Container("div", "{ns}_header-region")]
    [UsesLayout("header")]
    internal class HeaderRegion { }

    //------------------------------------------------------------------------------------
    // Footer

    [IsComponent("footer")]
    [PartOf("application")]
    [DeployedAs("navigation")]
    [RenderHtml("footer.standard", "<p class='{ns}_footer'>Copyright 2018</p>")]
    internal class FooterComponent { }

    [IsRegion("footer")]
    [PartOf("application")]
    [DeployedAs("navigation")]
    [Container("div", "{ns}_footer-region")]
    [UsesComponent("footer")]
    internal class MainFooterRegion { }

    //------------------------------------------------------------------------------------
    // Page layout

    [IsLayout("navigationPage", "header,body,footer)")]
    [PartOf("application")]
    [DeployedAs("navigation")]
    [UsesRegion("header", "header")]
    [UsesRegion("footer", "footer")]
    [RegionLayout("header", "header")]
    [RegionComponent("footer", "footer")]
    internal class NavigationPageLayout { }

    [UsesLayout("navigationPage")]
    [NeedsComponent("assetReferences")]
    public class NavigationMasterPage : BlankMasterPage
    {
    }
}