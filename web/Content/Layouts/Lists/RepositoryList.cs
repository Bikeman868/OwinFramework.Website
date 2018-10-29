using OwinFramework.Pages.Core.Attributes;

namespace Website.Content.Layouts.Lists
{
    [IsLayout("repository__list", "title,list")]
    [PartOf("application")]
    [LayoutRegion("title", "layouts:null")]
    [LayoutRegion("list", "repository__list")]
    [RegionComponent("title", "repository__list_heading")] // TODO: When RegionHtml is implemented use here
    internal class RepositoryList : ContentElement
    {
    }
}