using OwinFramework.Pages.Core.Attributes;
using OwinFramework.Pages.Core.Enums;
using Website.Content;
using Website.Content.Layouts.PageStructure;
using Website.PageBase;

namespace Website.Pages.FunctionalArea
{
    [IsLayout("areaLandingPage_Header", "title,detail")]
    [ZoneTemplate("detail", "/data/area")]
    internal class LandingPageHeaderLayout : DocumentHeadLayout { }

    [IsLayout("areaLandingPage_LeftColumn", "header,body")]
    [ZoneRegion("header", "layouts:null")]
    [ZoneRegion("body", "layouts:null")]
    [ZoneLayout("header", "areaLandingPage_Header")]
    [ZoneComponent("body", "content__template")]
    internal class LandingPageLeftColumnLayout : ContentElement { }

    [IsLayout("areaLandingPage_RightColumn", "panel1")]
    [ZoneRegion("panel1", "layouts:null")]
    [ZoneLayout("panel1", "project__list")]
    internal class LandingPageRightColumnLayout : FixedRightColumnLayout { }

    [IsLayout("areaLanding", "right,left")]
    [ZoneLayout("left", "areaLandingPage_LeftColumn")]
    [ZoneLayout("right", "areaLandingPage_RightColumn")]
    internal class LandingPageLayout : FixedRightColumnLayout { }

    [IsPage("areaLanding")]
    [Route("/content/area/**", Method.Get, Priority = -80)]
    [PageTitle("OWIN Framework Area")]
    [ZoneLayout("body", "areaLanding")]
    public class LandingPage: NavigationMasterPage{ }
}