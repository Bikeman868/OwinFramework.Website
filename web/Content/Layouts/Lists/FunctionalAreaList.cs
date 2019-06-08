using OwinFramework.Pages.Core.Attributes;

namespace Website.Content.Layouts.Lists
{
    [IsLayout("functional_area__list", "title,list")]
    [ZoneRegion("title", "layouts:null")]
    [ZoneRegion("list", "functional_area__list")]
    [ZoneHtml("title", "functional_area__list_heading", "<h3>Functional areas</h3>")]
    internal class FunctionalAreaList: ContentElement
    {
    }
}