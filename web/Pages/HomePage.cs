using OwinFramework.Pages.Core.Attributes;
using OwinFramework.Pages.Core.Enums;
using Website.Content;
using Website.Layouts;
using Website.Layouts.PageStructure;
using Website.PageBase;

namespace Website.Pages
{
    [IsComponent("homePage_Content")]
    [RenderHtml("home-page-right-column", "<h1 style='padding:20px;'>This website is under construction.</h1>")]
    internal class HomePageContent : ContentElement { }

    [IsLayout("homePage_RightColumn", "panel1,panel2")]
    [UsesRegion("panel1", "blank")]
    [UsesRegion("panel2", "blank")]
    [RegionLayout("panel1", "functional_area__list")]
    [RegionLayout("panel2", "project__list")]
    internal class HomePageRightColumnLayout : FixedRightColumnLayout { }

    [IsLayout("home", "right,left")]
    [RegionComponent("left", "homePage_Content")]
    [RegionLayout("right", "homePage_RightColumn")]
    internal class HomePageLayout : FixedRightColumnLayout { }

    [IsPage("home", "/home")]
    [Route("/home", Methods.Get)]
    [Route("/", Methods.Get)]
    [PageTitle("OWIN Framework Home")]
    [RegionLayout("body", "home")]
    public class HomePage: NavigationMasterPage{ }
}