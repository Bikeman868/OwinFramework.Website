using OwinFramework.Pages.Core.Attributes;

namespace Website.Content.Regions
{
    /// <summary>
    /// For layouts where the region has no behavior
    /// </summary>
    [IsRegion("blank")]
    internal class BlankRegion: ContentElement{}
}