using OwinFramework.Pages.Core.Attributes;
using OwinFramework.Pages.Core.Enums;
using Website.Content;
using Website.Content.Layouts.PageStructure;
using Website.PageBase;

namespace Website.Pages.FunctionalArea
{
    [IsComponent("areaLandingPage_Content")]
    [RenderHtml("areaLanding-page-right-column", "<h1 style='padding:50px;'>This area landing page needs and author, interested?</h1>")]
    internal class LandingPageContent : ContentElement { }

    [IsLayout("areaLandingPage_RightColumn", "panel1,panel2")]
    [LayoutRegion("panel1", "blank")]
    [LayoutRegion("panel2", "blank")]
    [RegionLayout("panel1", "functional_area__list")]
    [RegionLayout("panel2", "project__list")]
    internal class LandingPageRightColumnLayout : FixedRightColumnLayout { }

    [IsLayout("areaLanding", "right,left")]
    [RegionComponent("left", "areaLandingPage_Content")]
    [RegionLayout("right", "areaLandingPage_RightColumn")]
    internal class LandingPageLayout : FixedRightColumnLayout { }

    [IsPage("areaLanding")]
    [Route("/content/area/**", Methods.Get, Priority = 200)]
    [Route("/", Methods.Get)]
    [PageTitle("OWIN Framework")]
    [RegionLayout("body", "areaLanding")]
    public class LandingPage: NavigationMasterPage{ }
}