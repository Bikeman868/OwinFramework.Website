using OwinFramework.Pages.Core.Attributes;

namespace Website.Content.Layouts.Lists
{
    [IsLayout("interface__list", "title,list")]
    [PartOf("application")]
    [ZoneRegion("title", "layouts:null")]
    [ZoneRegion("list", "interface__list")]
    [ZoneHtml("title", "interface__list_heading", "<h3>Interfaces</h3>")]
    internal class InterfaceList : ContentElement
    {
    }
}