using OwinFramework.Pages.Core.Attributes;
using OwinFramework.Pages.Core.Enums;

namespace Website.Packaging
{
    [IsModule("content", AssetDeployment.PerWebsite)]
    internal class ContentModule { }
}