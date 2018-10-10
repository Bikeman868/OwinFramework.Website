using OwinFramework.Pages.Core.Attributes;
using Website.Content;

namespace Website.Components.FunctionalArea
{
    [IsComponent("functional_area__list_heading")]
    [PartOf("application")]
    [RenderHtml("functional_area__list_heading", "<h3>Functional areas</h3>")]
    internal class ListHeading : ContentElement {}
}