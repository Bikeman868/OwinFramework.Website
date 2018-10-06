using OwinFramework.Pages.Core.Attributes;
using OwinFramework.Pages.Core.Enums;
using Website.Layouts;
using Website.PageBase;

namespace Website.Pages
{
    [IsComponent("homePage_Fixed")]
    [RenderHtml("home-page-fixed-column", "<div style='min-height: 600px;'></div>")]
    internal class HomePageLeftColumn : ContentElement { }

    [IsComponent("homePage_Content")]
    [RenderHtml("home-page-right-column", "<h1 style='padding:20px;'>This website is under construction.</h1>")]
    internal class HomePageMiddleColumn : ContentElement { }

    [IsLayout("home", "right,left")]
    [RegionComponent("left", "homePage_Content")]
    [RegionComponent("right", "homePage_Fixed")]
    internal class HomePageLayout : FixedRightColumnLayout { }

    [IsPage("home", "/home")]
    [Route("/home", Methods.Get)]
    [Route("/", Methods.Get)]
    [PageTitle("OWIN Framework Home")]
    [RegionLayout("body", "home")]
    public class HomePage: NavigationMasterPage{ }
}