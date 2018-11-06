using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OwinFramework.Pages.Core.Attributes;

namespace Website.Content.Regions
{
    [IsRegion("content__document")]
    [Container("div", "{ns}_content-document")]
    internal class ContentRegion: ContentElement
    {
    }
}