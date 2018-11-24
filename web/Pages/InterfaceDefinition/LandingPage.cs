using OwinFramework.Pages.Core.Attributes;
using OwinFramework.Pages.Core.Enums;
using Website.Content;
using Website.Content.Layouts.PageStructure;
using Website.PageBase;

namespace Website.Pages.InterfaceDefinition
{
    [IsLayout("interfaceLandingPage_Header", "title,detail")]
    [RegionTemplate("detail", "/data/interfacedefinition")]
    internal class LandingPageHeaderLayout : DocumentHeadLayout { }

    [IsLayout("interfaceLandingPage_LeftColumn", "header,body")]
    [LayoutRegion("header", "layouts:null")]
    [LayoutRegion("body", "layouts:null")]
    [RegionLayout("header", "interfaceLandingPage_Header")]
    [RegionComponent("body", "content__template")]
    internal class LandingPageLeftColumnLayout : ContentElement { }
    
    [IsLayout("interfaceLanding", "right,left")]
    [RegionLayout("left", "interfaceLandingPage_LeftColumn")]
    internal class LandingPageLayout : FixedRightColumnLayout { }

    [IsPage("interfaceLanding")]
    [Route("/content/interface/**", Methods.Get, Priority = -80)]
    [PageTitle("OWIN Framework Interface")]
    [RegionLayout("body", "interfaceLanding")]
    public class LandingPage: NavigationMasterPage{ }
}