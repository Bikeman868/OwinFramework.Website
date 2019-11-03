using System.Collections.Generic;
using System.Linq;
using OwinFramework.Pages.Core.Attributes;
using OwinFramework.Pages.Core.Interfaces.Builder;
using OwinFramework.Pages.Core.Interfaces.DataModel;
using OwinFramework.Pages.Core.Interfaces.Runtime;
using OwinFramework.Pages.Framework.DataModel;
using OwinFramework.Pages.Standard;

namespace Website.Navigation
{
    [IsDataProvider("menu", typeof(IList<MenuPackage.MenuItem>))]
    [SuppliesData(typeof(IList<MenuPackage.MenuItem>), "mobile")]
    [SuppliesData(typeof(IList<MenuPackage.MenuItem>), "desktop")]
    public class MenuDataProvider : DataProvider
    {
        private readonly IList<MenuPackage.MenuItem> _desktopMenu;
        private readonly IList<MenuPackage.MenuItem> _mobileMenu;

        public MenuDataProvider(IDataProviderDependenciesFactory dependencies) 
            : base(dependencies) 
        {

            var gettingStartedMenu = new MenuPackage.MenuItem
            {
                Name = "Getting started",
                SubMenu = new []
                    {
                        new MenuPackage.MenuItem { Name = "Hello world", Url = "/content/general/walkthrough/helloworld" },
                        new MenuPackage.MenuItem { Name = "Sample websites", Url = "/content/general/samplewebsites" },
                        new MenuPackage.MenuItem { Name = "Configuration", Url = "/content/documentation/configuration/overview" },
                    }
            };

            var tutorialsMenu = new MenuPackage.MenuItem
            {
                Name = "Tutorials",
                SubMenu = new[]
                    {
                        new MenuPackage.MenuItem { Name = "Website walkthrough", Url = "/content/general/walkthrough/website" },
                        new MenuPackage.MenuItem { Name = "Microservice walkthrough", Url = "/content/general/walkthrough/userservice" },
                        new MenuPackage.MenuItem { Name = "Localization", Url = "/content/general/walkthrough/localization" },
                        new MenuPackage.MenuItem { Name = "Best practices", Url = "/content/general/bestpractice/landing" },
                    }
            };

            var documentationDesktopMenu = new MenuPackage.MenuItem
            {
                Name = "Documentation",
                SubMenu = new[]
                    {
                        new MenuPackage.MenuItem { Name = "The Owin Frameowrk", Url = "/content/documentation/concepts/overview" },
                        new MenuPackage.MenuItem { Name = "Functional areas", Url="/content/index/area" },
                        new MenuPackage.MenuItem { Name = "Projects", Url="/content/index/project" },
                        new MenuPackage.MenuItem { Name = "Repositories", Url="/content/index/repository" },
                        new MenuPackage.MenuItem { Name = "NuGet packages", Url="/content/index/nuget" },
                        new MenuPackage.MenuItem { Name = "Configuration", Url = "/content/documentation/configuration/overview" },
                    }
            };

            var documentationMobileMenu = new MenuPackage.MenuItem
            {
                Name = "Documentation",
                SubMenu = new[]
                    {
                        new MenuPackage.MenuItem { Name = "The Owin Frameowrk", Url = "/content/documentation/concepts/overview" },
                        new MenuPackage.MenuItem { Name = "Functional areas", Url="/content/index/area" },
                        new MenuPackage.MenuItem { Name = "NuGet packages", Url="/content/index/nuget" },
                        new MenuPackage.MenuItem { Name = "Configuration", Url = "/content/documentation/configuration/overview" },
                    }
            };

            var externalMenu = new MenuPackage.MenuItem
            {
                Name = "External",
                SubMenu = new[]
                    {
                        new MenuPackage.MenuItem { Name = "GitHub Wikki ...", Url = "https://github.com/Bikeman868/OwinFramework/wiki", Target = "_blank" },
                        new MenuPackage.MenuItem { Name = "Identification and authorization ...", Url="https://github.com/Bikeman868/OwinFramework.Authorization", Target = "_blank" },
                        new MenuPackage.MenuItem { Name = "Session handling ...", Url = "https://github.com/Bikeman868/OwinFramework.Middleware/tree/master/OwinFramework.Session", Target = "_blank" },
                        new MenuPackage.MenuItem { Name = "Page rendering ...", Url = "https://github.com/Bikeman868/OwinFramework.Pages/tree/master/OwinFramework.Pages.Html", Target = "_blank" },
                        new MenuPackage.MenuItem { Name = "REST services ...", Url = "https://github.com/Bikeman868/OwinFramework.Pages/tree/master/OwinFramework.Pages.Restful", Target = "_blank" },
                    }
            };

            var nuGetDesktopMenu = new MenuPackage.MenuItem
            {
                Name = "NuGet",
                SubMenu = SiteMap.Instance.Projects
                    .Where(p => !string.IsNullOrEmpty(p.NugetPackage) && p.DesktopMenu)
                    .OrderBy(p => p.NugetPackage)
                    .Select(r => new MenuPackage.MenuItem 
                        {
                            Name = r.NugetCaption,
                            Url = "/content/project/" + r.ProjectName + "/landing"
                        })
                    .ToArray()
            };

            var nuGetMobileMenu = new MenuPackage.MenuItem
            {
                Name = "NuGet",
                SubMenu = SiteMap.Instance.Projects
                    .Where(p => !string.IsNullOrEmpty(p.NugetPackage) && p.MobileMenu)
                    .OrderBy(p => p.NugetPackage)
                    .Select(r => new MenuPackage.MenuItem
                    {
                        Name = r.NugetCaption,
                        Url = "/content/project/" + r.ProjectName + "/landing"
                    })
                    .ToArray()
            };

            var gitHubMenu = new MenuPackage.MenuItem
            {
                Name = "Repositories",
                SubMenu = SiteMap.Instance.Repositories
                    .OrderBy(r => r.GitHubRepositoryName)
                    .Select(r => new MenuPackage.MenuItem
                    {
                        Name = r.Caption,
                        Url = "/content/repository/" + r.GitHubRepositoryName + "/landing"
                    })
                    .ToArray()
            };

            var projectDesktopMenu = new MenuPackage.MenuItem
            {
                Name = "Projects",
                SubMenu = SiteMap.Instance.Projects
                    .Where(p => p.DesktopMenu)
                    .OrderBy(p => p.ProjectName)
                    .Select(p => new MenuPackage.MenuItem
                        {
                            Name = p.ProjectCaption,
                            Url = "/content/project/" + p.ProjectName + "/landing"
                        })
                    .ToArray()
            };

            _desktopMenu = new List<MenuPackage.MenuItem>
            {
                gettingStartedMenu,
                tutorialsMenu,
                documentationDesktopMenu,
                nuGetDesktopMenu,
                gitHubMenu,
                //projectDesktopMenu,
                externalMenu
            };

            _mobileMenu = new List<MenuPackage.MenuItem>
            {
                gettingStartedMenu,
                tutorialsMenu,
                documentationMobileMenu,
                nuGetMobileMenu,
                gitHubMenu
            };
        }

        protected override void Supply(
            IRenderContext renderContext,
            IDataContext dataContext,
            IDataDependency dependency)
        {
            if (dependency == null || string.IsNullOrEmpty(dependency.ScopeName))
            {
                dataContext.Set(_desktopMenu);
                return;
            }

            switch (dependency.ScopeName.ToLower())
            {
                case "mobile":
                    dataContext.Set(_mobileMenu, dependency.ScopeName);
                    break;
                case "desktop":
                    dataContext.Set(_desktopMenu, dependency.ScopeName);
                    break;
                default:
                    dataContext.Set(_desktopMenu);
                    break;
            }
        }
    }

}