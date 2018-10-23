using OwinFramework.Pages.Core.Attributes;
using OwinFramework.Pages.Core.Enums;
using Website.Content;
using Website.Content.Layouts.PageStructure;
using Website.PageBase;

namespace Website.Pages.FunctionalArea
{
    [IsLayout("areaLandingPage_RightColumn", "panel1,panel2")]
    [LayoutRegion("panel1", "blank")]
    [LayoutRegion("panel2", "blank")]
    [RegionLayout("panel1", "functional_area__list")]
    [RegionLayout("panel2", "project__list")]
    internal class LandingPageRightColumnLayout : FixedRightColumnLayout { }

    [IsLayout("areaLanding", "right,left")]
    [RegionComponent("left", "content__template")]
    [RegionLayout("right", "areaLandingPage_RightColumn")]
    internal class LandingPageLayout : FixedRightColumnLayout { }

    [IsPage("areaLanding")]
    [Route("/content/area/**", Methods.Get, Priority = -80)]
    [Route("/", Methods.Get)]
    [PageTitle("OWIN Framework")]
    [RegionLayout("body", "areaLanding")]
    public class LandingPage: NavigationMasterPage{ }
}