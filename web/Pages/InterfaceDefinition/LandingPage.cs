using OwinFramework.Pages.Core.Attributes;
using OwinFramework.Pages.Core.Enums;
using Website.Content;
using Website.Content.Layouts.PageStructure;
using Website.PageBase;

namespace Website.Pages.InterfaceDefinition
{
    [IsLayout("interfaceLandingPage_Header", "title,detail")]
    [ZoneTemplate("detail", "/data/interfacedefinition")]
    internal class LandingPageHeaderLayout : DocumentHeadLayout { }

    [IsLayout("interfaceLandingPage_LeftColumn", "header,body")]
    [ZoneRegion("header", "layouts:null")]
    [ZoneRegion("body", "layouts:null")]
    [ZoneLayout("header", "interfaceLandingPage_Header")]
    [ZoneComponent("body", "content__template")]
    internal class LandingPageLeftColumnLayout : ContentElement { }
    
    [IsLayout("interfaceLanding", "right,left")]
    [ZoneLayout("left", "interfaceLandingPage_LeftColumn")]
    internal class LandingPageLayout : FixedRightColumnLayout { }

    [IsPage("interfaceLanding")]
    [Route("/content/interface/**", Method.Get, Priority = -80)]
    [PageTitle("OWIN Framework Interface")]
    [ZoneLayout("body", "interfaceLanding")]
    public class LandingPage: NavigationMasterPage{ }
}