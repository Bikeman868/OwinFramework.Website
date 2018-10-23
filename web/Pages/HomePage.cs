using OwinFramework.Pages.Core.Attributes;
using OwinFramework.Pages.Core.Enums;
using Website.Content;
using Website.Content.Layouts.PageStructure;
using Website.PageBase;

namespace Website.Pages
{
    [IsComponent("homePage_Content")]
    [RenderHtml("home-page-right-column", "<img src='https://openclipart.org/download/293843/under-construction_geek_woman.svg' style='padding: 5vh; max-width: 50vw; max-height: 50vh;'/>")]
    internal class HomePageContent : ContentElement { }

    [IsLayout("homePage_RightColumn", "panel1,panel2")]
    [LayoutRegion("panel1", "blank")]
    [LayoutRegion("panel2", "blank")]
    [RegionLayout("panel1", "functional_area__list")]
    [RegionLayout("panel2", "project__list")]
    internal class HomePageRightColumnLayout : FixedRightColumnLayout { }

    [IsLayout("home", "right,left")]
    [RegionComponent("left", "homePage_Content")]
    [RegionLayout("right", "homePage_RightColumn")]
    internal class HomePageLayout : FixedRightColumnLayout { }

    [IsPage("home", "/home")]
    [Route("/home", Methods.Get, Priority = 100)]
    [Route("/", Methods.Get, Priority = 100)]
    [PageTitle("OWIN Framework Home")]
    [RegionLayout("body", "home")]
    public class HomePage: NavigationMasterPage{ }
}