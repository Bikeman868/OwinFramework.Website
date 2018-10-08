using OwinFramework.Pages.Core.Attributes;
using Website.Content;

namespace Website.Regions.Content
{
    /// <summary>
    /// For layouts where the region has no behavior
    /// </summary>
    [IsRegion("blank")]
    internal class BlankRegion: ContentElement{}
}