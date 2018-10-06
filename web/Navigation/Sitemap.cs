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
        public NugetPackage[] NugetPackages { get; private set; }
        public Project[] Projects { get; private set; }
        public RepositoryOwner[] RepositoryOwners { get; private set; }

        public class FunctionalArea
        {
            public Document Document { get; private set; }
            public NugetPackage[] NugetPackages { get; private set; }
            public Project[] Projects { get; private set; }

            public FunctionalArea(Document document)
            {
                Document = document;
            }

            public void Initialize()
            {
                Projects = Instance.Projects.Where(p => p.FunctionalArea == this).ToArray();
                NugetPackages = Instance.NugetPackages.Where(n => Projects.Contains(n.Project)).ToArray();
            }
        }

        public class RepositoryOwner
        {
            public string Name { get; private set; }
            public string HomePageUrl { get; private set; }
            public Repository[]  Repositories { get; private set; }

            public RepositoryOwner(string name, string homePageUrl = null)
            {
                Name = name;
                HomePageUrl = homePageUrl ?? "https://github.com/" + name;
            }

            public void Initialize()
            {
                Repositories = Instance.Repositories.Where(r => r.Owner == this).ToArray();
            }
        }

        public class Repository
        {
            public string Identifier { get; set; }
            public string Url { get; set; }
            public RepositoryOwner Owner { get; set; }
            public Project[] Projects { get; set; }

            public Repository(string identifier, string owner, string url = null)
            {
                Identifier = identifier;
                Owner = Instance.RepositoryOwners.First(o => o.Name == owner);
                Url = url ?? "https://github.com/" + owner + "/" + identifier;
            }

            public void Initialize()
            { }
        }

        public class Project
        {
            public Document Document { get; private set;}
            public Repository  Repository { get; private set; }
            public FunctionalArea FunctionalArea { get; private set; }

            public void Initialize()
            { }
        }

        public class NugetPackage
        {
            public Document Document { get; private set;}
            public Project Project { get; private set; }

            public void Initialize()
            { }
        }

        public class InterfaceDefinition
        {
            public Document Document { get; private set; }
            public Project Project { get; private set; }

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
                OverviewTemplate = templateRoot + Identifier + "/index";
                return this;
            }

            public Document SetImage(string imageRoot, string fileExt)
            {
                ImageUrl = imageRoot + Identifier + fileExt;
                return this;
            }
        }

        public Sitemap()
        {
            FunctionalAreas = new[]
            {
                new FunctionalArea(new Document("area.framework", "Owin Framework")),
                new FunctionalArea(new Document("area.content", "Content")),
                new FunctionalArea(new Document("area.authorization", "Authorization")),
                new FunctionalArea(new Document("area.pages", "Html Pages"))
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

            NugetPackages = new[]
            {
                new NugetPackage()
            };

            Projects = new[]
            {
                new Project()
            };
        }

        public Sitemap Initialize()
        {
            for (var i = 0; i < FunctionalAreas.Length; i++) FunctionalAreas[i].Initialize();
            for (var i = 0; i < Repositories.Length; i++) Repositories[i].Initialize();
            for (var i = 0; i < NugetPackages.Length; i++) NugetPackages[i].Initialize();
            for (var i = 0; i < Projects.Length; i++) Projects[i].Initialize();
            for (var i = 0; i < RepositoryOwners.Length; i++) RepositoryOwners[i].Initialize();

            return this;
        }
    }
}