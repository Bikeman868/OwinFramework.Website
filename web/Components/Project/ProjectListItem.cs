using OwinFramework.Pages.Core.Attributes;
using OwinFramework.Pages.Core.Enums;
using OwinFramework.Pages.Core.Interfaces.Builder;
using OwinFramework.Pages.Core.Interfaces.Runtime;
using OwinFramework.Pages.Html.Elements;
using OwinFramework.Pages.Html.Runtime;
using Website.Navigation;

namespace Website.Components.Project
{
    [IsComponent("project__list_item")]
    [NeedsData(typeof(SiteMap.Project))]
    [PartOf("application")]
    public class ProjectListItem: Component
    {
        public ProjectListItem(IComponentDependenciesFactory dependencies) 
            : base(dependencies)
        {
        }

        public override IWriteResult WritePageArea(IRenderContext context, PageArea pageArea)
        {
           if (pageArea == PageArea.Body)
           {
               var project  = context.Data.Get<SiteMap.Project>();
               if (project != null)
               {
                   context.Html.WriteOpenTag("li", "class", Package.NamespaceName + "_list-item " + Package.NamespaceName + "_project-caption");
                   context.Html.WriteElement("a", project.ProjectCaption, "href", project.Document.LandingPageTemplate);
                   context.Html.WriteCloseTag("li");
                   context.Html.WriteLine();
               }
           }
           return WriteResult.Continue();
        }
    }
}