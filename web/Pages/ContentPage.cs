using OwinFramework.Pages.Core.Attributes;
using OwinFramework.Pages.Core.Enums;
using Website.Content;
using Website.Layouts.PageStructure;
using Website.PageBase;

namespace Website.Pages
{
    [IsComponent("contentPage_Content")]
    [RenderHtml("content-page-right-column", "<h1 style='padding:50px;'>This page needs and author, interested?</h1>")]
    internal class ContentPageContent : ContentElement { }

    [IsLayout("contentPage_RightColumn", "panel1,panel2")]
    [UsesRegion("panel1", "blank")]
    [UsesRegion("panel2", "blank")]
    [RegionLayout("panel1", "functional_area__list")]
    [RegionLayout("panel2", "project__list")]
    internal class ContentPageRightColumnLayout : FixedRightColumnLayout { }

    [IsLayout("content", "right,left")]
    [RegionComponent("left", "contentPage_Content")]
    [RegionLayout("right", "contentPage_RightColumn")]
    internal class ContentPageLayout : FixedRightColumnLayout { }

    [IsPage("content")]
    [Route("/content/**", Methods.Get)]
    [Route("/", Methods.Get)]
    [PageTitle("OWIN Framework")]
    [RegionLayout("body", "content")]
    public class ContentPage: NavigationMasterPage{ }
}