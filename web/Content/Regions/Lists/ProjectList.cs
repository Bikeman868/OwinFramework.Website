using OwinFramework.Pages.Core.Attributes;
using Website.Content;
using Website.Navigation;

namespace Website.Regions.Content
{
    [IsRegion("project__list")]
    [Container("ul")] // Wraps the list in a 'ul' element
    [Repeat(typeof(Sitemap.Project), null, null)] // Gets a list of projects and repeats the region for each project
    [UsesComponent("project__list_item")] // Draws the project as an 'li' element
    internal class ProjectList: ContentElement{}

    [IsRegion("project__icons")]
    [Container("ul")] // Wraps the list in a 'ul' element
    [Repeat(typeof(Sitemap.Project), null, "li")] // Gets a list of projects and repeats the region for each project. Wraps each region in 'li'
    [NeedsData("project__document")] // Extracts the 'Document' from each project
    [UsesComponent("document__icon")] // Draws the document icon as an image
    internal class ProjectIcons : ContentElement{}
}