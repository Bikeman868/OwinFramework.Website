using OwinFramework.Pages.Core.Attributes;
using OwinFramework.Pages.Core.Enums;

namespace Website.Pages
{
    //------------------------------------------------------------------------------------
    // Base classes

    [PartOf("application")]
    [DeployedAs("content")]
    internal class ContentElement { }

    //------------------------------------------------------------------------------------
    // Two column layout with left column fixed width

    [IsRegion("fixedLeftLeft")]
    internal class FixedLeftColumnLeftRegion : ContentElement { }

    [IsRegion("fixedLeftMiddle")]
    internal class FixedLeftColumnMiddleRegion : ContentElement { }

    [UsesRegion("left", "fixedLeftLeft")]
    [UsesRegion("middle", "fixedLeftMiddle")]
    internal class FixedLeftColumnLayout : ContentElement { }

    //------------------------------------------------------------------------------------
    // Two column layout with right column fixed width

    [IsRegion("fixedRightMiddle")]
    internal class FixedRightColumnLeftRegion : ContentElement { }

    [IsRegion("fixedRightRight")]
    internal class FixedRightColumnMiddleRegion : ContentElement { }

    [UsesRegion("middle", "fixedRightMiddle")]
    [UsesRegion("right", "fixedRightRight")]
    internal class FixedRightColumnLayout : ContentElement { }

    //------------------------------------------------------------------------------------
    // Home page

    [IsComponent("homePageLeft")]
    [RenderHtml("home-page-content", "<h2 style='padding:20px;'>Left column.</h2>")]
    internal class HomePageLeftColumn : ContentElement { }

    [IsComponent("homePageMiddle")]
    [RenderHtml("home-page-content", "<div style='min-height: 800px;'><h1 style='padding:20px;'>This website is under construction.</h1></div>")]
    internal class HomePageMiddleColumn : ContentElement { }

    [IsLayout("home", "left,middle")]
    [RegionComponent("left", "homePageLeft")]
    [RegionComponent("middle", "homePageMiddle")]
    internal class HomePageLayout : FixedLeftColumnLayout { }

    [IsPage("home")]
    [Route("/home", Methods.Get)]
    [Route("/", Methods.Get)]
    [PageTitle("OWIN Framework Home")]
    [RegionLayout("body", "home")]
    public class HomePage: NavigationMasterPage{ }
}