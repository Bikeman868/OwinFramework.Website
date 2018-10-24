using OwinFramework.Pages.Core.Attributes;
using OwinFramework.Pages.Core.Enums;
using OwinFramework.Pages.Core.Interfaces.Builder;
using OwinFramework.Pages.Core.Interfaces.Runtime;
using OwinFramework.Pages.Html.Elements;
using OwinFramework.Pages.Html.Runtime;
using Website.Navigation;

namespace Website.Components.InterfaceDefinition
{
    [IsComponent("interface__list_item")]
    [NeedsData(typeof(SiteMap.InterfaceDefinition))]
    [PartOf("application")]
    public class IntarfaceListItem: Component
    {
        public IntarfaceListItem(IComponentDependenciesFactory dependencies) 
            : base(dependencies)
        {
        }

        public override IWriteResult WritePageArea(IRenderContext context, PageArea pageArea)
        {
           if (pageArea == PageArea.Body)
           {
               var interfaceDefinition  = context.Data.Get<SiteMap.InterfaceDefinition>();
               if (interfaceDefinition != null)
               {
                   context.Html.WriteOpenTag("li", "class", Package.NamespaceName + "_list-item " + Package.NamespaceName + "_project-caption");
                   context.Html.WriteElement("a", interfaceDefinition.Document.Title, "href", interfaceDefinition.Document.LandingPageTemplate);
                   context.Html.WriteCloseTag("li");
                   context.Html.WriteLine();
               }
           }
           return WriteResult.Continue();
        }
    }
}