using OwinFramework.Pages.Core.Attributes;
using Website.Navigation;

namespace Website.Content.Regions.Lists
{
    [IsRegion("repository__list")]
    [Container("ul")] // Wraps the list in a 'ul' element
    [Repeat(typeof(SiteMap.Repository), null, null)] // Gets a list of repositories and repeats the region for each repository
    [UsesComponent("repository__list_item")] // Draws the repository as an 'li' element
    internal class RepositoryList: ContentElement{}
}