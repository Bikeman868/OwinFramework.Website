using OwinFramework.Pages.Core.Attributes;
using OwinFramework.Pages.Core.Enums;
using OwinFramework.Pages.Core.Interfaces.Builder;
using OwinFramework.Pages.Core.Interfaces.Runtime;
using OwinFramework.Pages.Html.Elements;
using OwinFramework.Pages.Html.Runtime;
using Website.Navigation;

namespace Website.Components.Project
{
    [IsComponent("repository__list_item")]
    [NeedsData(typeof(SiteMap.Repository))]
    [PartOf("application")]
    public class RepositoryListItem: Component
    {
        public RepositoryListItem(IComponentDependenciesFactory dependencies) 
            : base(dependencies)
        {
        }

        public override IWriteResult WritePageArea(IRenderContext context, PageArea pageArea)
        {
           if (pageArea == PageArea.Body)
           {
               var repository  = context.Data.Get<SiteMap.Repository>();
               if (repository != null)
               {
                   context.Html.WriteOpenTag("li", "class", Package.NamespaceName + "_list-item " + Package.NamespaceName + "_repository-name");
                   context.Html.WriteElement("a", repository.GitHubRepositoryName, "href", "/content/repository/" + repository.GitHubRepositoryName + "/landing");
                   context.Html.WriteCloseTag("li");
                   context.Html.WriteLine();
               }
           }
           return WriteResult.Continue();
        }
    }
}