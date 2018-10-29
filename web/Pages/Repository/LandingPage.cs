using OwinFramework.Pages.Core.Attributes;
using OwinFramework.Pages.Core.Enums;
using Website.Content;
using Website.Content.Layouts.PageStructure;
using Website.PageBase;

namespace Website.Pages.Repository
{
    [IsLayout("repositoryLandingPage_Header", "title,detail")]
    [RegionTemplate("detail", "/data/repository")]
    internal class LandingPageHeaderLayout : DocumentHeadLayout { }

    [IsLayout("repositoryLandingPage_LeftColumn", "header,body")]
    [LayoutRegion("header", "layouts:null")]
    [LayoutRegion("body", "layouts:null")]
    [RegionLayout("header", "repositoryLandingPage_Header")]
    [RegionComponent("body", "content__template")]
    internal class LandingPageLeftColumnLayout : ContentElement { }

    [IsLayout("repositoryLandingPage_RightColumn", "panel1,panel2")]
    [LayoutRegion("panel1", "layouts:null")]
    [LayoutRegion("panel2", "layouts:null")]
    [RegionLayout("panel1", "functional_area__list")]
    [RegionLayout("panel2", "repository__list")]
    internal class LandingPageRightColumnLayout : FixedRightColumnLayout { }

    [IsLayout("repositoryLanding", "right,left")]
    [RegionLayout("left", "repositoryLandingPage_LeftColumn")]
    [RegionLayout("right", "repositoryLandingPage_RightColumn")]
    internal class LandingPageLayout : FixedRightColumnLayout { }

    [IsPage("repositoryLanding")]
    [Route("/content/repository/**", Methods.Get, Priority = -80)]
    [PageTitle("GitHub Repository")]
    [RegionLayout("body", "repositoryLanding")]
    public class LandingPage: NavigationMasterPage{ }
}