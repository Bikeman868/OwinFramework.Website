﻿using OwinFramework.Pages.Core.Attributes;

namespace Website.Content.Layouts.Lists
{
    [IsLayout("interface__list", "title,list")]
    [PartOf("application")]
    [UsesRegion("title","blank")]
    [UsesRegion("list", "interface__list")]
    [RegionComponent("title", "interface__list_heading")] // TODO: When RegionHtml is implemented use here
    internal class InterfaceList : ContentElement
    {
    }
}