using OwinFramework.Pages.Core.Attributes;
using OwinFramework.Pages.Core.Enums;
using Website.Content;
using Website.Content.Layouts.PageStructure;
using Website.PageBase;

namespace Website.Pages
{
    [IsLayout("contentPage_RightColumn", "panel1,panel2")]
    [LayoutRegion("panel1", "layouts:null")]
    [LayoutRegion("panel2", "layouts:null")]
    [RegionLayout("panel1", "functional_area__list")]
    [RegionLayout("panel2", "project__list")]
    internal class ContentPageRightColumnLayout : FixedRightColumnLayout { }

    [IsLayout("content", "right,left")]
    [RegionComponent("left", "content__template")]
    [RegionLayout("right", "contentPage_RightColumn")]
    internal class ContentPageLayout : FixedRightColumnLayout { }

    [IsPage("content")]
    [Route("/content/**", Method.Get, Priority = -100)]
    [PageTitle("OWIN Framework")]
    [RegionLayout("body", "content")]
    public class ContentPage: NavigationMasterPage{ }
}