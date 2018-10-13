using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Website.Navigation
{
    public class Sitemap
    {
        public static Sitemap Instance { get; private set; }

        static Sitemap()
        {
            Instance = new Sitemap();
            Instance.Initialize();
        }

        public FunctionalArea[] FunctionalAreas { get; private set; }
        public Repository[] Repositories { get; private set; }
        public Project[] Projects { get; private set; }
        public RepositoryOwner[] RepositoryOwners { get; private set; }

        public class FunctionalArea
        {
            public string Identifier { get; private set;}
            public Document Document { get; private set; }
            public Project[] Projects { get; private set; }

            public FunctionalArea(string identifier, string name)
            {
                Identifier = identifier;
                Document = new Document(identifier.ToLower(), name)
                    .SetPages("/content/area/")
                    .SetImage("/assets/images/area/", ".png");
            }

            public void Initialize()
            {
                Projects = Instance.Projects.Where(p => p.FunctionalArea == this).ToArray();
            }
        }

        public class RepositoryOwner
        {
            public string GitHubAccountName { get; private set; }
            public string HomePageUrl { get; private set; }
            public Repository[]  Repositories { get; private set; }

            public RepositoryOwner(string gitHubAccountName, string homePageUrl = null)
            {
                GitHubAccountName = gitHubAccountName;
                HomePageUrl = homePageUrl ?? "https://github.com/" + gitHubAccountName;
            }

            public void Initialize()
            {
                Repositories = Instance.Repositories.Where(r => r.Owner == this).ToArray();
            }
        }

        public class Repository
        {
            public string GitHubRepositoryName { get; private set; }
            public string Caption { get; private set; }
            public string Url { get; set; }
            public RepositoryOwner Owner { get; set; }
            public Project[] Projects { get; set; }

            public Repository(string gitHubRepositoryName, string gitHubAccountName, string url = null)
            {
                GitHubRepositoryName = gitHubRepositoryName;
                Caption = gitHubRepositoryName.Substring(gitHubRepositoryName.IndexOf('.') + 1).Replace(".", " ");
                Owner = Instance.RepositoryOwners.First(o => o.GitHubAccountName == gitHubAccountName);
                Url = url ?? "https://github.com/" + gitHubAccountName + "/" + gitHubRepositoryName;
            }

            public void Initialize()
            {
                Projects = Instance.Projects.Where(p => p.Repository == this).ToArray();
            }
        }

        public class Project
        {
            public string ProjectCaption { get; private set; }
            public string NugetCaption { get; private set; }
            public string ProjectName { get; private set; }
            public bool DesktopMenu { get; private set; }
            public bool MobileMenu { get; private set; }
            public string NugetPackage { get; private set; }
            public Document Document { get; private set; }
            public Repository  Repository { get; private set; }
            public FunctionalArea FunctionalArea { get; private set; }

            public Project(string projectName, string gitHubRepositoryName, string areaIdentifier)
            {
                ProjectCaption = projectName.Substring(projectName.IndexOf('.') + 1).Replace(".", " ");
                ProjectName = projectName;
                NugetPackage = projectName.Replace("OwinFramework", "Owin.Framework");
                NugetCaption = NugetPackage.StartsWith("Owin.Framework.") ? NugetPackage.Substring(15) : NugetPackage;
                DesktopMenu = true;
                MobileMenu = false;
                Document = new Document(projectName.ToLower(), ProjectCaption + " Project")
                    .SetPages("/content/project/")
                    .SetImage("/assets/images/project/", ".png");
                Repository = Instance.Repositories.First(r => r.GitHubRepositoryName == gitHubRepositoryName);
                FunctionalArea = Instance.FunctionalAreas.First(a => a.Identifier == areaIdentifier);
            }

            public Project Menus(bool desktop, bool mobile)
            {
                DesktopMenu = desktop;
                MobileMenu = mobile;
                return this;
            }

            public Project Nuget(string packageName)
            {
                NugetPackage = packageName;
                return this;
            }

            public Project Initialize()
            {
                return this;
            }
        }

        public class InterfaceDefinition
        {
            public Document Document { get; private set; }
            public Project Project { get; private set; }

            public InterfaceDefinition(string typeName, string projectName)
            {
                Document = new Document(typeName.ToLower(), typeName + " Interface")
                    .SetPages("/content/interface/")
                    .SetImage("/assets/images/interface/", ".png");
                Project = Instance.Projects.First(p => p.ProjectName == projectName);
            }
            public void Initialize()
            { }
        }

       public class Document
        {
            public string Identifier { get; private set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string ImageUrl { get; set; }
            public string LandingPageTemplate { get; set; }
            public string OverviewTemplate { get; set; }
            public string IndexTemplate { get; set; }

            public Document(string identifier, string title)
            {
                Identifier = identifier;

                Title = title;
                Description = string.Empty;
                SetPages("/content/general/");
                SetImage("/assets/images/document/", ".png");
            }

            public Document SetPages(string templateRoot)
            {
                LandingPageTemplate = templateRoot + Identifier + "/landing";
                OverviewTemplate = templateRoot + Identifier + "/overview";
                IndexTemplate = templateRoot + Identifier + "/index";
                return this;
            }

            public Document SetImage(string imageRoot, string fileExt)
            {
                ImageUrl = imageRoot + Identifier + fileExt;
                return this;
            }
        }

        public Sitemap Initialize()
        {
            FunctionalAreas = new[]
            {
                new FunctionalArea("framework", "Owin Framework"),
                new FunctionalArea("content", "Content"),
                new FunctionalArea("diagnostics", "Diagnostics"),
                new FunctionalArea("authorization", "Authorization"),
                new FunctionalArea("pages", "Html Pages"),
                new FunctionalArea("testing", "Unit Testing")
            };

            RepositoryOwners = new[]
            {
                new RepositoryOwner("Bikeman868")
            };

            Repositories = new[] 
            { 
                new Repository("OwinFramework", "Bikeman868"),
                new Repository("OwinFramework.Middleware", "Bikeman868"),
                new Repository("OwinFramework.Pages", "Bikeman868"),
                new Repository("OwinFramework.Authorization", "Bikeman868"),
                new Repository("OwinFramework.Facilities", "Bikeman868"),
            };

            Projects = new[]
            {
                new Project("OwinFramework", "OwinFramework", "framework").Menus(true, true),
                new Project("OwinFramework.Configuration.ConfigurationManager", "OwinFramework", "framework").Menus(false, false),
                new Project("OwinFramework.Configuration.Urchin", "OwinFramework", "framework").Menus(false, false),
                new Project("OwinFramework.Mocks", "OwinFramework", "testing").Menus(false, false),
                new Project("OwinFramework.UnitTests", "OwinFramework", "testing").Menus(false, false).Nuget(null),

                new Project("OwinFramework.Facilities.Cache.Local", "OwinFramework.Facilities", "framework").Menus(false, false),
                new Project("OwinFramework.Facilities.IdentityStore.Prius", "OwinFramework.Facilities", "authorization").Menus(false, false),
                new Project("OwinFramework.Facilities.TokenStore.Cache", "OwinFramework.Facilities", "framework").Menus(false, false),
                new Project("OwinFramework.Facilities.TokenStore.Prius", "OwinFramework.Facilities", "framework").Menus(false, false),

                new Project("OwinFramework.AnalysisReporter", "OwinFramework.Middleware", "diagnostics").Menus(false, false),
                new Project("OwinFramework.Dart", "OwinFramework.Middleware", "content").Menus(false, false),
                new Project("OwinFramework.DefaultDocument", "OwinFramework.Middleware", "content").Menus(true, true),
                new Project("OwinFramework.Documenter", "OwinFramework.Middleware", "diagnostics").Menus(false, false),
                new Project("OwinFramework.ExceptionReporter", "OwinFramework.Middleware", "diagnostics").Menus(true, false),
                new Project("OwinFramework.FormIdentification", "OwinFramework.Middleware", "authorization").Menus(true, false),
                new Project("OwinFramework.Less", "OwinFramework.Middleware", "content").Menus(true, false),
                new Project("OwinFramework.NotFound", "OwinFramework.Middleware", "content").Menus(true, false),
                new Project("OwinFramework.OutputCache", "OwinFramework.Middleware", "content").Menus(true, false),
                new Project("OwinFramework.RouteVisualizer", "OwinFramework.Middleware", "diagnostics").Menus(false, false),
                new Project("OwinFramework.Session", "OwinFramework.Middleware", "authorization").Menus(true, false),
                new Project("OwinFramework.StaticFiles", "OwinFramework.Middleware", "content").Menus(true, true),
                new Project("OwinFramework.Versioning", "OwinFramework.Middleware", "content").Menus(true, false),

                new Project("OwinFramework.Authorization", "OwinFramework.Authorization", "authorization").Menus(true, true),
                new Project("OwinFramework.Authorization.Core", "OwinFramework.Authorization", "authorization").Menus(false, false),
                new Project("OwinFramework.Authorization.Prius", "OwinFramework.Authorization", "authorization").Menus(true, false),
                new Project("OwinFramework.Authorization.UI", "OwinFramework.Authorization", "authorization").Menus(true, false),

                new Project("OwinFramework.Pages.Core", "OwinFramework.Pages", "pages").Menus(false, false),
                new Project("OwinFramework.Pages.DebugMiddleware", "OwinFramework.Pages", "diagnostics").Menus(false, false),
                new Project("OwinFramework.Pages.Framework", "OwinFramework.Pages", "pages").Menus(false, false),
                new Project("OwinFramework.Pages.Html", "OwinFramework.Pages", "pages").Menus(true, true),
                new Project("OwinFramework.Pages.Mocks", "OwinFramework.Pages", "testing").Menus(false, false),
                new Project("OwinFramework.Pages.Restful", "OwinFramework.Pages", "pages").Menus(true, true),
                new Project("OwinFramework.Pages.UnitTests", "OwinFramework.Pages", "testing").Menus(false, false).Nuget(null),
            };

            for (var i = 0; i < FunctionalAreas.Length; i++) FunctionalAreas[i].Initialize();
            for (var i = 0; i < Repositories.Length; i++) Repositories[i].Initialize();
            for (var i = 0; i < Projects.Length; i++) Projects[i].Initialize();
            for (var i = 0; i < RepositoryOwners.Length; i++) RepositoryOwners[i].Initialize();

            return this;
        }
    }
}