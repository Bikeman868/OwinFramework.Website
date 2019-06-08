using OwinFramework.Pages.Core.Attributes;

namespace Website.Content.Layouts.Lists
{
    [IsLayout("repository__list", "title,list")]
    [PartOf("application")]
    [ZoneRegion("title", "layouts:null")]
    [ZoneRegion("list", "repository__list")]
    [ZoneHtml("title", "repository__list_heading", "<h3>Repositories</h3>")]
    internal class RepositoryList : ContentElement
    {
    }
}