using OwinFramework.Pages.Core.Attributes;
using OwinFramework.Pages.Core.Enums;
using OwinFramework.Pages.Core.Interfaces.Builder;
using OwinFramework.Pages.Core.Interfaces.Runtime;
using OwinFramework.Pages.Html.Elements;
using OwinFramework.Pages.Html.Runtime;
using Website.Navigation;

namespace Website.Components.Content
{
    [IsComponent("document__list_item")]
    [NeedsData(typeof(SiteMap.Document))]
    [PartOf("application")]
    public class DocumentListItem: Component
    {
        public DocumentListItem(IComponentDependenciesFactory dependencies) 
            : base(dependencies)
        {
        }

        public override IWriteResult WritePageArea(IRenderContext context, PageArea pageArea)
        {
           if (pageArea == PageArea.Body)
           {
               var document  = context.Data.Get<SiteMap.Document>();
               if (document != null)
               {
                   context.Html.WriteOpenTag("li", "class", Package.NamespaceName + "_list-item " + Package.NamespaceName + "_document-title");
                   context.Html.WriteElement("a", document.Title, "href", document.LandingPageTemplate);
                   context.Html.WriteCloseTag("li");
                   context.Html.WriteLine();
               }
           }
           return WriteResult.Continue();
        }
    }
}