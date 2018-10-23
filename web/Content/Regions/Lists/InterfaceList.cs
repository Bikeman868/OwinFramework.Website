using OwinFramework.Pages.Core.Attributes;
using Website.Navigation;

namespace Website.Content.Regions.Lists
{
    [IsRegion("interface__list")]
    [Container("ul")] // Wraps the list in a 'ul' element
    [Repeat(typeof(SiteMap.InterfaceDefinition), null, null)] // Gets a list of interfaces and repeats the region for each interface
    [UsesComponent("interface__list_item")] // Draws the project as an 'li' element
    internal class InterfaceList: ContentElement{}
}