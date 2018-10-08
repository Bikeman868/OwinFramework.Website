using OwinFramework.Pages.Core.Attributes;

namespace Website.Components.FunctionalArea
{
    [IsComponent("functional_area__list_heading")]
    [PartOf("application")]
    [RenderHtml("functional_area__list_heading", "<h3>Functional areas</h3>")]
    public class ListHeading
    {
    }
}