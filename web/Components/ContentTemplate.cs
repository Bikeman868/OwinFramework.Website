using Microsoft.Owin;
using OwinFramework.Pages.Core.Attributes;
using OwinFramework.Pages.Core.Enums;
using OwinFramework.Pages.Core.Interfaces.Builder;
using OwinFramework.Pages.Core.Interfaces.Runtime;
using OwinFramework.Pages.Html.Elements;
using System.Text.RegularExpressions;

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
        private readonly Regex _titleRegex;
        private const string _className = "app_content-document";

        public ContentTemplate(IComponentDependenciesFactory dependencies)
            : base(dependencies)
        {
            PageAreas = new [] { PageArea.Head, PageArea.Body, PageArea.Initialization };
            _rootPath = new PathString();
            _titleRegex = new Regex(@"<title>(.*)</title>", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
        }

        public override IWriteResult WritePageArea(IRenderContext context, PageArea pageArea)
        {
            var requestPath = context.OwinContext.Request.Path;
            var relativePath = requestPath;
            var html = context.Html;

            if (_rootPath.HasValue && !requestPath.StartsWithSegments(_rootPath, out relativePath))
                return base.WritePageArea(context, pageArea);

            if (pageArea == PageArea.Head)
            {
                html.WriteUnclosedElement(
                    "link", "rel", "canonical", "href",
                    "http://owinframework.net" + requestPath.ToString().ToLower());
                context.Html.WriteLine();
            }
            else if (pageArea == PageArea.Body)
            {
                if (!_rootPath.HasValue || requestPath.StartsWithSegments(_rootPath, out relativePath))
                {
                    var template = Dependencies.NameManager.ResolveTemplate(relativePath.Value);
                    if (template != null)
                    {
                        //_titleRegex.Matches(template.);

                        html.WriteOpenTag("div", "class", _className);
                        html.WriteLine();
                        template.WritePageArea(context, pageArea);
                        html.WriteCloseTag("div");
                        html.WriteLine();

                        //if (!string.IsNullOrEmpty(title))
                        //{
                        //    html.WriteScriptOpen();
                        //    html.WriteLine("document.title='" + title + "';");
                        //    html.WriteScriptClose();
                        //    html.WriteLine();
                        //}
                    }
                }
            }
            else if (pageArea == PageArea.Initialization)
            {
                html.WriteScriptOpen();
                html.WriteLine("var contentDiv=document.getElementsByClassName('" + _className + "')[0];");
                html.WriteLine("var pageTitles=contentDiv.getElementsByTagName('title');");
                html.WriteLine("if (pageTitles.length>0)document.title=pageTitles[0].text;");
                html.WriteLine();
                html.WriteScriptClose();
            }

            

            return base.WritePageArea(context, pageArea);
        }
    }
}