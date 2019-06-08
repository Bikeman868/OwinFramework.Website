using OwinFramework.Pages.Core.Attributes;
using OwinFramework.Pages.Core.Enums;
using Website.Content;
using Website.Content.Layouts.PageStructure;
using Website.PageBase;

namespace Website.Pages
{
    [IsLayout("contentPage_RightColumn", "panel1,panel2")]
    [ZoneRegion("panel1", "layouts:null")]
    [ZoneRegion("panel2", "layouts:null")]
    [ZoneLayout("panel1", "functional_area__list")]
    [ZoneLayout("panel2", "project__list")]
    internal class ContentPageRightColumnLayout : FixedRightColumnLayout { }

    [IsLayout("content", "right,left")]
    [ZoneComponent("left", "content__template")]
    [ZoneLayout("right", "contentPage_RightColumn")]
    internal class ContentPageLayout : FixedRightColumnLayout { }

    [IsPage("content")]
    [Route("/content/**", Method.Get, Priority = -100)]
    [PageTitle("OWIN Framework")]
    [ZoneLayout("body", "content")]
    public class ContentPage: NavigationMasterPage{ }
}