using OwinFramework.Pages.Core.Attributes;
using OwinFramework.Pages.Core.Enums;

namespace Website.Pages
{
    [IsComponent("homePageContent")]
    [PartOf("application")]
    [DeployedAs("content")]
    [RenderHtml("home-page-content", "<div style='min-height: 800px;'><h1 style='padding:20px;'>This website is under construction.</h1></div>")]
    internal class HomePageContent { }

    [IsPage("home")]
    [Route("/home", Methods.Get)]
    [Route("/", Methods.Get)]
    [PageTitle("OWIN Framework Home")]
    [UsesRegion("body", "fullPageContent")]
    [RegionComponent("body", "homePageContent")]
    public class HomePage: NavigationMasterPage{ }
}