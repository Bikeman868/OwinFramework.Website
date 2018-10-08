using OwinFramework.Pages.Core.Attributes;

namespace Website.Content.Layouts.Lists
{
    [IsLayout("project__list", "title,list")]
    [PartOf("application")]
    [UsesRegion("title","blank")]
    [UsesRegion("list", "project__list")]
    [RegionComponent("title", "project__list_heading")] // TODO: When RegionHtml is implemented use here
    internal class ProjectList : ContentElement
    {
    }
}