using OwinFramework.Pages.Core.Attributes;
using OwinFramework.Pages.Core.Enums;
using Website.Content;
using Website.Content.Layouts.PageStructure;
using Website.PageBase;

namespace Website.Pages.Repository
{
    [IsLayout("repositoryLandingPage_RightColumn", "panel1,panel2")]
    [LayoutRegion("panel1", "layouts:null")]
    [LayoutRegion("panel2", "layouts:null")]
    [RegionLayout("panel1", "functional_area__list")]
    [RegionLayout("panel2", "project__list")]
    internal class LandingPageRightColumnLayout : FixedRightColumnLayout { }

    [IsLayout("repositoryLanding", "right,left")]
    [RegionComponent("left", "content__template")]
    [RegionLayout("right", "repositoryLandingPage_RightColumn")]
    internal class LandingPageLayout : FixedRightColumnLayout { }

    [IsPage("repositoryLanding")]
    [Route("/content/repository/**", Methods.Get, Priority = -80)]
    [PageTitle("GitHub Repository")]
    [RegionLayout("body", "repositoryLanding")]
    public class LandingPage: NavigationMasterPage{ }
}