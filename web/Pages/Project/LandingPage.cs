using OwinFramework.Pages.Core.Attributes;
using OwinFramework.Pages.Core.Enums;
using Website.Content;
using Website.Content.Layouts.PageStructure;
using Website.PageBase;

namespace Website.Pages.Project
{
    [IsLayout("projectLandingPage_RightColumn", "panel1,panel2")]
    [LayoutRegion("panel1", "blank")]
    [LayoutRegion("panel2", "blank")]
    [RegionLayout("panel1", "functional_area__list")]
    [RegionLayout("panel2", "project__list")]
    internal class LandingPageRightColumnLayout : FixedRightColumnLayout { }

    [IsLayout("projectLanding", "right,left")]
    [RegionComponent("left", "content__template")]
    [RegionLayout("right", "projectLandingPage_RightColumn")]
    internal class LandingPageLayout : FixedRightColumnLayout { }

    [IsPage("projectLanding")]
    [Route("/content/project/**", Methods.Get, Priority = -80)]
    [Route("/", Methods.Get)]
    [PageTitle("OWIN Framework")]
    [RegionLayout("body", "projectLanding")]
    public class LandingPage: NavigationMasterPage{ }
}