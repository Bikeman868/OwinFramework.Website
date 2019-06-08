using OwinFramework.Pages.Core.Attributes;
using OwinFramework.Pages.Core.Enums;
using Website.Content;
using Website.Content.Layouts.PageStructure;
using Website.PageBase;

namespace Website.Pages.Repository
{
    [IsLayout("repositoryLandingPage_Header", "title,detail")]
    [ZoneTemplate("detail", "/data/repository")]
    internal class LandingPageHeaderLayout : DocumentHeadLayout { }

    [IsLayout("repositoryLandingPage_LeftColumn", "header,body")]
    [ZoneRegion("header", "layouts:null")]
    [ZoneRegion("body", "layouts:null")]
    [ZoneLayout("header", "repositoryLandingPage_Header")]
    [ZoneComponent("body", "content__template")]
    internal class LandingPageLeftColumnLayout : ContentElement { }

    [IsLayout("repositoryLandingPage_RightColumn", "panel1")]
    [ZoneRegion("panel1", "layouts:null")]
    [ZoneLayout("panel1", "project__list")]
    internal class LandingPageRightColumnLayout : FixedRightColumnLayout { }

    [IsLayout("repositoryLanding", "right,left")]
    [ZoneLayout("left", "repositoryLandingPage_LeftColumn")]
    [ZoneLayout("right", "repositoryLandingPage_RightColumn")]
    internal class LandingPageLayout : FixedRightColumnLayout { }

    [IsPage("repositoryLanding")]
    [Route("/content/repository/**", Method.Get, Priority = -80)]
    [PageTitle("GitHub Repository")]
    [ZoneLayout("body", "repositoryLanding")]
    public class LandingPage: NavigationMasterPage{ }
}