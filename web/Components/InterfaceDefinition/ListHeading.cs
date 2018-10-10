using OwinFramework.Pages.Core.Attributes;
using Website.Content;

namespace Website.Components.InterfaceDefinition
{
    [IsComponent("interface__list_heading")]
    [RenderHtml("interface__list_heading", "<h3>Interfaces</h3>")]
    internal class ListHeading: ContentElement {}
}