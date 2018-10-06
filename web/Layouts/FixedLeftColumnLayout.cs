using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OwinFramework.Pages.Core.Attributes;

namespace Website.Layouts
{
    //------------------------------------------------------------------------------------
    // Two column layout with left column fixed width

    [IsRegion("fixedLeft_Left")]
    internal class FixedLeftColumnLeftRegion : ContentElement { }

    [IsRegion("fixedLeft_Right")]
    internal class FixedLeftColumnMiddleRegion : ContentElement { }

    [UsesRegion("left", "fixedLeft_Left")]
    [UsesRegion("right", "fixedLeft_Right")]
    internal class FixedLeftColumnLayout : ContentElement { }
}