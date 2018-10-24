using OwinFramework.Pages.Core.Attributes;
using OwinFramework.Pages.Core.Enums;
using OwinFramework.Pages.Core.Interfaces.Builder;
using OwinFramework.Pages.Core.Interfaces.Runtime;
using OwinFramework.Pages.Html.Elements;
using OwinFramework.Pages.Html.Runtime;
using Website.Navigation;

namespace Website.Components.Content
{
    [IsComponent("document__icon")]
    [NeedsData(typeof(SiteMap.Document))]
    [PartOf("application")]
    public class DocumentIcon: Component
    {
        public DocumentIcon(IComponentDependenciesFactory dependencies)
            : base(dependencies)
        {
        }

        public override IWriteResult WritePageArea(IRenderContext context, PageArea pageArea)
        {
            if (pageArea == PageArea.Body)
            {
                var document = context.Data.Get<SiteMap.Document>();
                if (document != null)
                {
                    context.Html.WriteOpenTag("a", "class", Package.NamespaceName + "_document-icon", "href", document.LandingPageTemplate);
                    context.Html.WriteElement("img", "src", document.ImageUrl, "alt", document.Title);
                    context.Html.WriteCloseTag("a");
                    context.Html.WriteLine();
                }
            }
            return WriteResult.Continue();
        }
    }
}