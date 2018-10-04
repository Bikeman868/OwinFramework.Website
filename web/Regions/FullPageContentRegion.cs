using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OwinFramework.Pages.Core.Attributes;

namespace Website.Regions
{
    [IsRegion("fullPageContent")]
    [PartOf("application")]
    [DeployedAs("content")]
    [Style("min-height: 400px;")]
    [Container("div")]
    internal class TitleRegion { }
}