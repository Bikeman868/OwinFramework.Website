using OwinFramework.Pages.Core.Attributes;
using OwinFramework.Pages.Core.Enums;
using Website.Content;
using Website.Content.Layouts.PageStructure;
using Website.PageBase;

namespace Website.Pages.Project
{
    [IsComponent("projectLandingPage_Content")]
    [RenderHtml("projectLanding-page-right-column", "<h1 style='padding:50px;'>This project landing page needs and author, interested?</h1>")]
    internal class ProjectLandingPageContent : ContentElement { }

    [IsLayout("projectLandingPage_RightColumn", "panel1,panel2")]
    [UsesRegion("panel1", "blank")]
    [UsesRegion("panel2", "blank")]
    [RegionLayout("panel1", "functional_area__list")]
    [RegionLayout("panel2", "project__list")]
    internal class LandingPageRightColumnLayout : FixedRightColumnLayout { }

    [IsLayout("projectLanding", "right,left")]
    [RegionComponent("left", "projectLandingPage_Content")]
    [RegionLayout("right", "projectLandingPage_RightColumn")]
    internal class LandingPageLayout : FixedRightColumnLayout { }

    [IsPage("projectLanding")]
    [Route("/content/project/**", Methods.Get, Priority = 200)]
    [Route("/", Methods.Get)]
    [PageTitle("OWIN Framework")]
    [RegionLayout("body", "projectLanding")]
    public class LandingPage: NavigationMasterPage{ }
}