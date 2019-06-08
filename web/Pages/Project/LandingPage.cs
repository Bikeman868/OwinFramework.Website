using OwinFramework.Pages.Core.Attributes;
using OwinFramework.Pages.Core.Enums;
using OwinFramework.Pages.Html.Elements;
using Website.Content;
using Website.Content.Layouts.PageStructure;
using Website.PageBase;

namespace Website.Pages.Project
{
    [IsLayout("projectLandingPage_Header", "title,detail")]
    [ZoneTemplate("detail", "/data/project")]
    internal class LandingPageHeaderLayout : DocumentHeadLayout { }

    [IsLayout("projectLandingPage_LeftColumn", "header,body")]
    [ZoneRegion("header", "layouts:null")]
    [ZoneRegion("body", "layouts:null")]
    [ZoneLayout("header", "projectLandingPage_Header")]
    [ZoneComponent("body", "content__template")]
    internal class LandingPageLeftColumnLayout : ContentElement { }

    [IsLayout("projectLandingPage_RightColumn", "panel1")]
    [ZoneRegion("panel1", "layouts:null")]
    [ZoneLayout("panel1", "interface__list")]
    internal class LandingPageRightColumnLayout : ContentElement { }

    [IsLayout("projectLanding", "right,left")]
    [ZoneLayout("left", "projectLandingPage_LeftColumn")]
    [ZoneLayout("right", "projectLandingPage_RightColumn")]
    internal class LandingPageLayout : FixedRightColumnLayout { }

    [IsPage("projectLanding")]
    [Route("/content/project/**", Method.Get, Priority = -80)]
    [PageTitle("OWIN Framework Project")]
    [ZoneLayout("body", "projectLanding")]
    public class LandingPage: NavigationMasterPage{ }
}