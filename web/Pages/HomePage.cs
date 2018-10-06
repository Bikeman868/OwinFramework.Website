using OwinFramework.Pages.Core.Attributes;
using OwinFramework.Pages.Core.Enums;
using Website.Layouts;
using Website.PageBase;

namespace Website.Pages
{
    [IsComponent("homePage_Left")]
    [RenderHtml("home-page-left-column", "<div style='min-height: 600px;'></div>")]
    internal class HomePageLeftColumn : ContentElement { }

    [IsComponent("homePage_Right")]
    [RenderHtml("home-page-right-column", "<h1 style='padding:20px;'>This website is under construction.</h1>")]
    internal class HomePageMiddleColumn : ContentElement { }

    [IsLayout("home", "left,right")]
    [RegionComponent("left", "homePage_Left")]
    [RegionComponent("right", "homePage_Right")]
    internal class HomePageLayout : FixedLeftColumnLayout { }

    [IsPage("home")]
    [Route("/home", Methods.Get)]
    [Route("/", Methods.Get)]
    [PageTitle("OWIN Framework Home")]
    [RegionLayout("body", "home")]
    public class HomePage: NavigationMasterPage{ }
}