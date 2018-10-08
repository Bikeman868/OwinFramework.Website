﻿using OwinFramework.Pages.Core.Attributes;
using Website.Navigation;

namespace Website.Regions.Content
{
    [IsRegion("functional_area__list")]
    [PartOf("application")]
    [Container("ul")] // Wraps the list in a 'ul' element
    [Repeat(typeof(Sitemap.FunctionalArea))] // Gets a list of functional areas and repeats the region for each functional area
    [NeedsData("functional_area__document")] // Extracts the 'Document' from each functional area
    [UsesComponent("document__list_item")] // Draws the document as an 'li' element
    public class FunctionalAreaList
    {
    }

    [IsRegion("functional_area__icons")]
    [PartOf("application")]
    [Container("ul")] // Wraps the list in a 'ul' element
    [Repeat(typeof(Sitemap.FunctionalArea), null, "li")] // Gets a list of functional areas and repeats the region for each functional area. Wraps each region in 'li'
    [NeedsData("functional_area__document")] // Extracts the 'Document' from each functional area
    [UsesComponent("document__icon")] // Draws the document icon as an image
    public class FunctionalAreaIcons
    {
    }
}