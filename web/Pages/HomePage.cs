using OwinFramework.Pages.Core.Attributes;
using OwinFramework.Pages.Core.Enums;
using Website.Content;
using Website.Content.Layouts.PageStructure;
using Website.PageBase;

namespace Website.Pages
{
    [IsLayout("homePage_LeftColumn", "main")]
    [ZoneRegion("main", "content__document")]
    [ZoneTemplate("main", "/content/general/home")]
    internal class HomePageLeftColumnLayout : ContentElement { }

    [IsLayout("homePage_RightColumn", "panel1,panel2")]
    [ZoneRegion("panel1", "layouts:null")]
    [ZoneRegion("panel2", "layouts:null")]
    [ZoneLayout("panel1", "functional_area__list")]
    [ZoneLayout("panel2", "project__list")]
    internal class HomePageRightColumnLayout : ContentElement { }

    [IsLayout("home", "right,left")]
    [ZoneLayout("left", "homePage_LeftColumn")]
    [ZoneLayout("right", "homePage_RightColumn")]
    internal class HomePageLayout : FixedRightColumnLayout { }

    [IsPage("home", "/home")]
    [Route("/home", Method.Get, Priority = 100)]
    [Route("/", Method.Get, Priority = 100)]
    [PageTitle("OWIN Framework Home")]
    [ZoneLayout("body", "home")]
    public class HomePage: NavigationMasterPage{ }
}