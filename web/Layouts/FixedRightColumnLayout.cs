using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OwinFramework.Pages.Core.Attributes;

namespace Website.Layouts
{
    //------------------------------------------------------------------------------------
    // Two column layout with right column fixed width

    [IsRegion("fixedRight_Left")]
    internal class FixedRightColumnLeftRegion : ContentElement { }

    [IsRegion("fixedRight_Right")]
    internal class FixedRightColumnMiddleRegion : ContentElement { }

    [UsesRegion("middle", "fixedRight_Left")]
    [UsesRegion("right", "fixedRight_Right")]
    internal class FixedRightColumnLayout : ContentElement { }
}