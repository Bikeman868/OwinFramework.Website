using OwinFramework.Pages.Core.Attributes;

namespace Website.Pages
{
    //------------------------------------------------------------------------------------
    // Default page styles

    [IsComponent("defaultStyles")]
    [PartOf("application")]
    [DeployedAs("content")]
    [DeployCss("p", "font-size:11pt;")]
    [DeployCss("h1", "font-size:16pt;")]
    [DeployCss("h2", "font-size:14pt;")]
    [DeployCss("h3", "font-size:12pt;")]
    internal class DefaultStylesComponent { }

    //------------------------------------------------------------------------------------
    // Header - is the same on all pages

    [IsComponent("title")]
    [PartOf("application")]
    [DeployedAs("navigation")]
    [RenderHtml("heading.main", "<h1>OWIN Framework</h1>")]
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
    [Style("height: 90px; width:100%; padding:10px; background: gray; color: whitesmoke; clear: both;")]
    [UsesLayout("header")]
    internal class HeaderRegion { }

    //------------------------------------------------------------------------------------
    // Footer - is the same design on all pages

    [IsComponent("footer")]
    [PartOf("application")]
    [DeployedAs("navigation")]
    [DeployCss("p.{ns}_footer", "font-weight:bold; font-size:9pt;")]
    [RenderHtml("footer.standard", "<p class='{ns}_footer'>Copyright 2018</p>")]
    internal class FooterComponent { }

    [IsRegion("footer")]
    [PartOf("application")]
    [DeployedAs("navigation")]
    [Style("height: 50px; width:100%; padding:5px; background: gray; color: whitesmoke;")]
    [UsesComponent("footer")]
    internal class MainFooterRegion { }

    //------------------------------------------------------------------------------------
    // Body - the body has a different layout on each page

    [IsRegion("body")]
    [PartOf("application")]
    [DeployedAs("content")]
    internal class BodyRegion { }

    //------------------------------------------------------------------------------------
    // Base page for most regular pages on the website

    [IsLayout("navigationPage", "header,body,footer)")]
    [PartOf("application")]
    [DeployedAs("navigation")]
    [UsesRegion("header", "header")]
    [UsesRegion("body", "body")]
    [UsesRegion("footer", "footer")]
    [RegionLayout("header", "header")]
    [RegionComponent("footer", "footer")]
    internal class NavigationPageLayout { }

    [NeedsComponent("defaultStyles")]
    [UsesLayout("navigationPage")]
    public class NavigationMasterPage : BlankMasterPage
    {
    }
}