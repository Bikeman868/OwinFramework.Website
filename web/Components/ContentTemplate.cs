using Microsoft.Owin;
using OwinFramework.Pages.Core.Attributes;
using OwinFramework.Pages.Core.Enums;
using OwinFramework.Pages.Core.Interfaces.Builder;
using OwinFramework.Pages.Core.Interfaces.Runtime;
using OwinFramework.Pages.Html.Elements;

namespace Website.Components
{
    /// <summary>
    /// Displays a template that corresponds to the URL of the page
    /// </summary>
    [IsComponent("content__template")]
    [PartOf("application")]
    public class ContentTemplate: Component
    {
        private readonly PathString _rootPath;

        public ContentTemplate(IComponentDependenciesFactory dependencies)
            : base(dependencies)
        {
            PageAreas = new [] { PageArea.Body };
            _rootPath = new PathString("/content");
        }

        public override IWriteResult WritePageArea(IRenderContext context, PageArea pageArea)
        {
            if (pageArea == PageArea.Body)
            {
                var requestPath = context.OwinContext.Request.Path;
                PathString relativePath;
                if (requestPath.StartsWithSegments(_rootPath, out relativePath))
                {
                    var template = Dependencies.NameManager.ResolveTemplate(relativePath.Value);
                    if (template != null)
                        template.WritePageArea(context, pageArea);
                }
            }

            return base.WritePageArea(context, pageArea);
        }
    }
}