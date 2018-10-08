using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OwinFramework.Pages.Core.Attributes;

namespace Website.Layouts.Content
{
    [IsLayout("functional_area__list", "title,list")]
    [PartOf("application")]
    [UsesRegion("title","component")]
    [UsesRegion("list", "functional_area__list")]
    [RegionComponent("title", "functional_area__list_heading")]
    public class FunctionalAreaList
    {
    }
}