using OwinFramework.Pages.Core.Attributes;

namespace Website.Content.Layouts.PageStructure
{
    //------------------------------------------------------------------------------------
    // Two column layout with left column fixed width

    [IsRegion("fixedLeft_Left")]
    [Container("div", "{ns}_left")]
    internal class FixedLeftColumnLeftRegion : ContentElement { }

    [IsRegion("fixedLeft_Right")]
    [Container("div", "{ns}_right")]
    internal class FixedLeftColumnMiddleRegion : ContentElement { }

    [LayoutRegion("left", "fixedLeft_Left")]
    [LayoutRegion("right", "fixedLeft_Right")]
    [Container("div", "{ns}_fixed-left-layout")]
    internal class FixedLeftColumnLayout : ContentElement { }
}