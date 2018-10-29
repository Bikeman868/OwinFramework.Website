using OwinFramework.Pages.Core.Attributes;
using Website.Navigation;

namespace Website.Content.Regions.Lists
{
    [IsRegion("functional_area__list")]
    [PartOf("application")]
    [Container("ul")] // Wraps the list in a 'ul' element
    [Repeat(typeof(SiteMap.FunctionalArea), null, null)] // Gets a list of functional areas and repeats the region for each functional area
    [NeedsData("functional_area__document")] // Extracts the 'Document' from each functional area
    [UsesComponent("document__list_item")] // Draws the document as an 'li' element
    internal class FunctionalAreaList : ContentElement{}
}