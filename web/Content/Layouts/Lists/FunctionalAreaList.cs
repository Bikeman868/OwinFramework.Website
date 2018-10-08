﻿using OwinFramework.Pages.Core.Attributes;

namespace Website.Content.Layouts.Lists
{
    [IsLayout("functional_area__list", "title,list")]
    [UsesRegion("title","blank")]
    [UsesRegion("list", "functional_area__list")]
    [RegionComponent("title", "functional_area__list_heading")] // TODO: When RegionHtml is implemented use here
    internal class FunctionalAreaList: ContentElement
    {
    }
}