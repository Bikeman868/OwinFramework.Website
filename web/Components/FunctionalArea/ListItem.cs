using OwinFramework.Pages.Core.Attributes;
using OwinFramework.Pages.Core.Enums;
using OwinFramework.Pages.Core.Interfaces.Builder;
using OwinFramework.Pages.Core.Interfaces.Runtime;
using OwinFramework.Pages.Html.Elements;
using OwinFramework.Pages.Html.Runtime;
using Website.Navigation;

namespace Website.Components.FunctionalArea
{
    [IsComponent("functional_area__list_item")]
    [NeedsData(typeof(SiteMap.FunctionalArea))]
    [PartOf("application")]
    public class ListItem: Component
    {
        public ListItem(IComponentDependenciesFactory dependencies) 
            : base(dependencies)
        {
        }

        public override IWriteResult WritePageArea(IRenderContext context, PageArea pageArea)
        {
           if (pageArea == PageArea.Body)
           {
               var functionalArea  = context.Data.Get<SiteMap.FunctionalArea>();
               if (functionalArea != null)
               {
                   context.Html.WriteOpenTag("li", "class", Package.NamespaceName + "_list-item " + Package.NamespaceName + "_area-identifier");
                   context.Html.WriteElement("a", functionalArea.Identifier, "href", functionalArea.Document.LandingPageTemplate);
                   context.Html.WriteCloseTag("li");
                   context.Html.WriteLine();
               }
           }
           return WriteResult.Continue();
        }
    }
}