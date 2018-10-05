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
    // Css for the navigation elements

    [IsComponent("navigationStyles")]
    [PartOf("application")]
    [DeployedAs("content")]
    [DeployCss("div.{ns}_header-region", "height: 150px; width:100%; background: gray; color: whitesmoke; clear: both;", 1)]
    [DeployCss("div.{ns}_header-region h1", "font-size: 3em; padding: 15px 0px 0px 30px; margin: 0; letter-spacing: 1px;", 2)]
    [DeployCss("div.{ns}_header-region p", "font-size: 0.8em; padding: 0px 0px 0px 50px; margin: 13px;", 2)]
    [DeployCss("div.{ns}_footer-region", "height: 50px; width:100%; padding:5px; background: gray; color: whitesmoke;", 3)]
    [DeployCss(".navigation", "font-family: sans-serif")]
    internal class NavigationStylesComponent { }

    //------------------------------------------------------------------------------------
    // Header

    [IsComponent("title")]
    [PartOf("application")]
    [DeployedAs("navigation")]
    [RenderHtml("heading.main", 1, "<h1 style='font-size: 3em; padding: 15px 0px 0px 30px; margin: 0; letter-spacing: 1px; font-family: sans-serif;'>Owin Framework</h1>")] // BUG - style should be handled by CSS
    [RenderHtml("heading.sub", 2, "<p style='font-size: 0.8em; padding: 0px 0px 0px 50px; margin: 13px;'>An open architecture for interoperable middleware</p>")] // BUG - style should be handled by CSS
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
    [Style("height: 150px; width:100%; background: gray; color: whitesmoke; clear: both;")] // BUG - should be handled by the ContainerAttribute
    [UsesLayout("header")]
    internal class HeaderRegion { }

    //------------------------------------------------------------------------------------
    // Footer

    [IsComponent("footer")]
    [PartOf("application")]
    [DeployedAs("navigation")]
    [DeployCss("p.{ns}_footer", "font-weight:bold; font-size:9pt; text-align: right;")]
    [RenderHtml("footer.standard", "<p class='{ns}_footer'>Copyright 2018</p>")]
    internal class FooterComponent { }

    [IsRegion("footer")]
    [PartOf("application")]
    [DeployedAs("navigation")]
    [Container("div", "{ns}_footer-region")]
    [Style("height: 50px; width:100%; padding:5px; background: gray; color: whitesmoke;")] // BUG - should be handled by the ContainerAttribute
    [UsesComponent("footer")]
    internal class MainFooterRegion { }

    //------------------------------------------------------------------------------------
    // Page layout

    [IsRegion("body")]
    [PartOf("application")]
    [DeployedAs("navigation")]
    internal class DummyBody_RemoveLater { }

    [IsLayout("navigationPage", "header,body,footer)")]
    [PartOf("application")]
    [DeployedAs("navigation")]
    [UsesRegion("header", "header")]
    [UsesRegion("body", "body")] // BUG - this should be ommited
    [UsesRegion("footer", "footer")]
    [RegionLayout("header", "header")]
    [RegionComponent("footer", "footer")]
    internal class NavigationPageLayout { }

    [UsesLayout("navigationPage")]
    [NeedsComponent("navigationStyles")]
    public class NavigationMasterPage : BlankMasterPage
    {
    }
}