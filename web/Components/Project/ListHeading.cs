using OwinFramework.Pages.Core.Attributes;
using Website.Content;

namespace Website.Components.Project
{
    [IsComponent("project__list_heading")]
    [RenderHtml("project__list_heading", "<h3>Projects</h3>")]
    internal class ListHeading : ContentElement {}
}