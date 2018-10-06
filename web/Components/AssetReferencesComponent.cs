using OwinFramework.Pages.Core.Attributes;
using OwinFramework.Pages.Core.Enums;
using OwinFramework.Pages.Core.Interfaces.Builder;
using OwinFramework.Pages.Core.Interfaces.Runtime;
using OwinFramework.Pages.Html.Elements;

namespace Website.Components
{
    [IsComponent("assetReferences")]
    [PartOf("application")]
    public class AssetReferencesComponent: Component
    {
        public AssetReferencesComponent(IComponentDependenciesFactory dependencies) : base(dependencies)
        {
            PageAreas = new [] { PageArea.Head };
        }

        public override IWriteResult WritePageArea(IRenderContext context, PageArea pageArea)
        {
            if (pageArea == PageArea.Head)
            {
                context.Html.WriteElementLine("link", null, "rel", "stylesheet", "type", "text/css", "href", "/assets/styles/main.css");
            }

            return base.WritePageArea(context, pageArea);
        }
    }
}