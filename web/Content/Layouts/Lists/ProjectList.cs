using OwinFramework.Pages.Core.Attributes;

namespace Website.Content.Layouts.Lists
{
    [IsLayout("project__list", "title,list")]
    [PartOf("application")]
    [ZoneRegion("title", "layouts:null")]
    [ZoneRegion("list", "project__list")]
    [ZoneHtml("title", "project__list_heading", "<h3>Projects</h3>")]
    internal class ProjectList : ContentElement
    {
    }
}