using OwinFramework.Pages.Core.Attributes;

namespace Website.Layouts.PageStructure
{
    //------------------------------------------------------------------------------------
    // Two column layout with right column fixed width

    [IsRegion("fixedRight_Left")]
    [Container("div", "{ns}_left")]
    internal class FixedRightColumnLeftRegion : ContentElement { }

    [IsRegion("fixedRight_Right")]
    [Container("div", "{ns}_right")]
    internal class FixedRightColumnMiddleRegion : ContentElement { }

    [UsesRegion("left", "fixedRight_Left")]
    [UsesRegion("right", "fixedRight_Right")]
    [Container("div", "{ns}_fixed-right-layout")]
    internal class FixedRightColumnLayout : ContentElement { }
}