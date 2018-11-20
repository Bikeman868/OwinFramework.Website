using OwinFramework.Pages.Core.Attributes;
using OwinFramework.Pages.Core.Enums;
using Website.Content;
using Website.Content.Layouts.PageStructure;
using Website.PageBase;

namespace Website.Pages.Project
{
    [IsLayout("projectLandingPage_Header", "title,detail")]
    [RegionTemplate("detail", "/data/project")]
    internal class LandingPageHeaderLayout : DocumentHeadLayout { }

    [IsLayout("projectLandingPage_LeftColumn", "header,body")]
    [LayoutRegion("header", "layouts:null")]
    [LayoutRegion("body", "layouts:null")]
    [RegionLayout("header", "projectLandingPage_Header")]
    [RegionComponent("body", "content__template")]
    internal class LandingPageLeftColumnLayout : ContentElement { }

    [IsLayout("projectLandingPage_RightColumn", "panel1")]
    [LayoutRegion("panel1", "layouts:null")]
    [RegionLayout("panel1", "interface__list")]
    internal class LandingPageRightColumnLayout : ContentElement { }

    [IsLayout("projectLanding", "right,left")]
    [RegionLayout("left", "projectLandingPage_LeftColumn")]
    [RegionLayout("right", "projectLandingPage_RightColumn")]
    internal class LandingPageLayout : FixedRightColumnLayout { }

    [IsPage("projectLanding")]
    [Route("/content/project/**", Methods.Get, Priority = -80)]
    [PageTitle("OWIN Framework Project")]
    [RegionLayout("body", "projectLanding")]
    public class LandingPage: NavigationMasterPage{ }
}