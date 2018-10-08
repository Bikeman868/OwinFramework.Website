using OwinFramework.Pages.Core.Attributes;
using OwinFramework.Pages.Core.Enums;
using Website.Layouts;
using Website.Layouts.PageStructure;
using Website.PageBase;

namespace Website.Pages
{
    [IsComponent("homePage_Content")]
    [RenderHtml("home-page-right-column", "<h1 style='padding:20px;'>This website is under construction.</h1>")]
    internal class HomePageMiddleColumn : ContentElement { }

    [IsLayout("home", "right,left")]
    [RegionComponent("left", "homePage_Content")]
    [RegionLayout("right", "functional_area__list")]
    internal class HomePageLayout : FixedRightColumnLayout { }

    [IsPage("home", "/home")]
    [Route("/home", Methods.Get)]
    [Route("/", Methods.Get)]
    [PageTitle("OWIN Framework Home")]
    [RegionLayout("body", "home")]
    public class HomePage: NavigationMasterPage{ }
}