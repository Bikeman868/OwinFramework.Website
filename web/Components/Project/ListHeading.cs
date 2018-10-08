using OwinFramework.Pages.Core.Attributes;

namespace Website.Components.Project
{
    [IsComponent("project__list_heading")]
    [PartOf("application")]
    [RenderHtml("project__list_heading", "<h3>Projects</h3>")]
    public class ListHeading
    {
    }
}