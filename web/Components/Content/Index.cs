using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using OwinFramework.Pages.Core.Attributes;
using OwinFramework.Pages.Core.Enums;
using OwinFramework.Pages.Core.Interfaces.Builder;
using OwinFramework.Pages.Core.Interfaces.Managers;
using OwinFramework.Pages.Core.Interfaces.Runtime;
using OwinFramework.Pages.Html.Elements;
using OwinFramework.Pages.Html.Runtime;
using OwinFramework.Pages.Standard;
using Website.Navigation;

namespace Website.Components.Content
{
    [IsComponent("content_index")]
    [PartOf("application")]
    public class Index: Component
    {
        private readonly INameManager _nameManager;

        private readonly Regex _urlRegex = new Regex(
            "/content/index/([^/]*)",
            RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);

        public Index(
            IComponentDependenciesFactory dependencies,
            INameManager nameManager) 
            : base(dependencies)
        {
            _nameManager = nameManager;
            PageAreas = new[] { PageArea.Head, PageArea.Body };
        }

        public override IWriteResult WritePageArea(IRenderContext context, PageArea pageArea)
        {
            if (pageArea == PageArea.Head)
            {
                context.Html.WriteUnclosedElement(
                    "link", "rel", "canonical", "href", 
                    "http://owinframework.net" + context.OwinContext.Request.Path.ToString().ToLower());
                context.Html.WriteLine();
            }
            if (pageArea == PageArea.Body)
            {
                var area = GetContentArea(context);
                if (area == null) return WriteResult.Continue();

                if (area == "area") ListFunctionalAreas(context);
                else if (area == "project") ListProjects(context);
                else if (area == "nuget") ListNuGet(context);
                else if (area == "repository") ListRepositories(context);
            }
            return WriteResult.Continue();
        }

        private string GetContentArea(IRenderContext context)
        {
            var match = _urlRegex.Match(context.OwinContext.Request.Path.Value);
            if (match.Success)
                return match.Groups[1].Value;

            return null;
        }

        private void ListProjects(IRenderContext context)
        {
            WriteHead(context, "Visual Studio Projects");

            var template = _nameManager.ResolveTemplate("/data/project");
            if (template != null)
            {
                context.Html.WriteOpenTag("ul", "class", Package.NamespaceName + "_index " + Package.NamespaceName + "_project-index");
                context.Html.WriteLine();

                foreach (var project in SiteMap.Instance.Projects.OrderBy(p => p.ProjectName))
                {
                    context.Data.Set(project);
                    context.Data.Set(project.Document);
                    context.Data.Set(project.Repository);

                    context.Html.WriteOpenTag("li", "class", Package.NamespaceName + "_index " + Package.NamespaceName + "_project-index");
                    context.Html.WriteLine();

                    template.WritePageArea(context, PageArea.Body);

                    context.Html.WriteCloseTag("li");
                    context.Html.WriteLine();
                }

                context.Html.WriteCloseTag("ul");
                context.Html.WriteLine();
            }
        }

        private void ListNuGet(IRenderContext context)
        {
            WriteHead(context, "NuGet Packages");

            var template = _nameManager.ResolveTemplate("/data/nuget");
            if (template != null)
            {
                context.Html.WriteOpenTag("ul", "class", Package.NamespaceName + "_index " + Package.NamespaceName + "_nuget-index");
                context.Html.WriteLine();

                foreach (var project in 
                    SiteMap.Instance.Projects
                    .Where(p => !string.IsNullOrEmpty(p.NugetPackage))
                    .OrderBy(p => p.NugetPackage))
                {
                    context.Data.Set(project);
                    context.Data.Set(project.Document);
                    context.Data.Set(project.Repository);

                    context.Html.WriteOpenTag("li", "class", Package.NamespaceName + "_index " + Package.NamespaceName + "_nuget-index");
                    context.Html.WriteLine();

                    template.WritePageArea(context, PageArea.Body);

                    context.Html.WriteCloseTag("li");
                    context.Html.WriteLine();
                }

                context.Html.WriteCloseTag("ul");
                context.Html.WriteLine();
            }
        }

        private void ListFunctionalAreas(IRenderContext context)
        {
            WriteHead(context, "Functional Areas");

            var template = _nameManager.ResolveTemplate("/data/area");
            if (template != null)
            {
                context.Html.WriteOpenTag("ul", "class", Package.NamespaceName + "_index " + Package.NamespaceName + "_area-index");
                context.Html.WriteLine();

                foreach (var area in SiteMap.Instance.FunctionalAreas.OrderBy(a => a.Identifier))
                {
                    context.Data.Set(area);
                    context.Data.Set(area.Document);
                    context.Data.Set<IList<SiteMap.Project>>(area.Projects.ToList());

                    context.Html.WriteOpenTag("li", "class", Package.NamespaceName + "_index " + Package.NamespaceName + "_area-index");
                    context.Html.WriteLine();

                    template.WritePageArea(context, PageArea.Body);

                    context.Html.WriteCloseTag("li");
                    context.Html.WriteLine();
                }

                context.Html.WriteCloseTag("ul");
                context.Html.WriteLine();
            }
        }

        private void ListRepositories(IRenderContext context)
        {
            WriteHead(context, "Source Code Repositories");

            var template = _nameManager.ResolveTemplate("/data/repository");
            if (template != null)
            {
                context.Html.WriteOpenTag("ul", "class", Package.NamespaceName + "_index " + Package.NamespaceName + "_repository-index");
                context.Html.WriteLine();

                foreach (var repository in SiteMap.Instance.Repositories.OrderBy(p => p.Caption))
                {
                    context.Data.Set(repository);
                    context.Data.Set(repository.Owner);

                    context.Html.WriteOpenTag("li", "class", Package.NamespaceName + "_index " + Package.NamespaceName + "_repository-index");
                    context.Html.WriteLine();

                    template.WritePageArea(context, PageArea.Body);

                    context.Html.WriteCloseTag("li");
                    context.Html.WriteLine();
                }

                context.Html.WriteCloseTag("ul");
                context.Html.WriteLine();
            }
        }

        private void WriteHead(IRenderContext context, string title)
        {
            context.Html.WriteOpenTag("div", "class", Package.NamespaceName + "_document_head_layout");
            context.Html.WriteLine();

            context.Html.WriteOpenTag("div", "class", Package.NamespaceName + "_title");
            context.Html.WriteLine();
            var verticalTextComponent = _nameManager.ResolveComponent("text:verticalText", Package);
            context.Data.Set(new TextEffectsPackage.VerticalText("Index", 30, 90)
            {
                Background = null,
                TextStyle = "font: 20px serif; fill: white;"
            });
            verticalTextComponent.WritePageArea(context, PageArea.Body);
            context.Html.WriteCloseTag("div");
            context.Html.WriteLine();

            context.Html.WriteOpenTag("div", "class", Package.NamespaceName + "_detail");
            context.Html.WriteLine();
            context.Html.WriteElementLine("h2", title);
            context.Html.WriteCloseTag("div");
            context.Html.WriteLine();

            context.Html.WriteCloseTag("div");
            context.Html.WriteLine();
        }
    }
}