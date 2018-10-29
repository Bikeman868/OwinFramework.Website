using OwinFramework.Pages.Core.Attributes;
using Website.Content;

namespace Website.Components.Repository
{
    [IsComponent("repository__list_heading")]
    [RenderHtml("repository__list_heading", "<h3>Repositories</h3>")]
    internal class ListHeading : ContentElement {}
}