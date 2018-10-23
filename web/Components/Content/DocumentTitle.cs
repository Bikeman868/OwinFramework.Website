using OwinFramework.Pages.Core.Attributes;
using OwinFramework.Pages.Core.Enums;
using OwinFramework.Pages.Core.Interfaces.Builder;
using OwinFramework.Pages.Core.Interfaces.Runtime;
using OwinFramework.Pages.Html.Elements;
using OwinFramework.Pages.Html.Runtime;
using Website.Navigation;

namespace Website.Components.Content
{
    [IsComponent("document__title")]
    [NeedsData(typeof(SiteMap.Document))]
    [PartOf("application")]
    public class DocumentTitle : Component
    {
        public DocumentTitle(IComponentDependenciesFactory dependencies)
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
                    context.Html.WriteOpenTag("h3", "class", Package.NamespaceName + "_document-title");
                    context.Html.WriteElementLine("a", document.Title, "href", document.LandingPageTemplate);
                    context.Html.WriteCloseTag("h3");
                }
            }
            return WriteResult.Continue();
        }
    }
}