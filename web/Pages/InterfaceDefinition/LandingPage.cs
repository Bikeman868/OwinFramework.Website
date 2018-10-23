using OwinFramework.Pages.Core.Attributes;
using OwinFramework.Pages.Core.Enums;
using Website.Content;
using Website.Content.Layouts.PageStructure;
using Website.PageBase;

namespace Website.Pages.InterfaceDefinition
{
    [IsLayout("interfaceLandingPage_RightColumn", "panel1,panel2")]
    [LayoutRegion("panel1", "blank")]
    [LayoutRegion("panel2", "blank")]
    [RegionLayout("panel1", "functional_area__list")]
    [RegionLayout("panel2", "interface__list")]
    internal class LandingPageRightColumnLayout : FixedRightColumnLayout { }

    [IsLayout("interfaceLanding", "right,left")]
    [RegionComponent("left", "content__template")]
    [RegionLayout("right", "interfaceLandingPage_RightColumn")]
    internal class LandingPageLayout : FixedRightColumnLayout { }

    [IsPage("interfaceLanding")]
    [Route("/content/interface/**", Methods.Get, Priority = -80)]
    [Route("/", Methods.Get)]
    [PageTitle("OWIN Framework")]
    [RegionLayout("body", "interfaceLanding")]
    public class LandingPage: NavigationMasterPage{ }
}