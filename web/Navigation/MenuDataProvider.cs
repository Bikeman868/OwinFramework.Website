﻿using System.Collections.Generic;
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
                        new MenuPackage.MenuItem { Name = "Hello world", Url = "https://github.com/Bikeman868/OwinFramework.Pages/tree/master/Sample2", Target = "_blank" },
                        new MenuPackage.MenuItem { Name = "Sample websites", Url = "https://github.com/Bikeman868/OwinFramework.Website", Target = "_blank" },
                        new MenuPackage.MenuItem { Name = "NuGet packages", Url = "https://github.com/Bikeman868/OwinFramework/wiki/Package-Directory", Target = "_blank" },
                        new MenuPackage.MenuItem { Name = "Configuration", Url = "https://github.com/Bikeman868/OwinFramework/wiki/Configuring-middleware-components", Target = "_blank" },
                    }
            };

            var tutorialsMenu = new MenuPackage.MenuItem
            {
                Name = "Tutorials",
                SubMenu = new[]
                    {
                        new MenuPackage.MenuItem { Name = "Website walkthrough", Url = "/content/general/walkthrough/website" },
                        new MenuPackage.MenuItem { Name = "Microservice walkthrough", Url = "/content/general/walkthrough/microservice" },
                        new MenuPackage.MenuItem { Name = "Localization", Url = "/content/general/walkthrough/localization" },
                        new MenuPackage.MenuItem { Name = "Best practices", Url = "/content/general/bestpractice/landing" },
                    }
            };

            var documentationMenu = new MenuPackage.MenuItem
            {
                Name = "Documentation",
                SubMenu = new[]
                    {
                        new MenuPackage.MenuItem { Name = "The Owin Frameowrk", Url = "https://github.com/Bikeman868/OwinFramework/wiki/OWIN", Target = "_blank" },
                        new MenuPackage.MenuItem { Name = "GitHub Wikki", Url = "https://github.com/Bikeman868/OwinFramework/wiki", Target = "_blank" },
                        new MenuPackage.MenuItem { Name = "Identification and authorization", Url="https://github.com/Bikeman868/OwinFramework.Authorization", Target = "_blank" },
                        new MenuPackage.MenuItem { Name = "Session handling", Url = "https://github.com/Bikeman868/OwinFramework.Middleware/tree/master/OwinFramework.Session", Target = "_blank" },
                        new MenuPackage.MenuItem { Name = "Page rendering", Url = "https://github.com/Bikeman868/OwinFramework.Pages/tree/master/OwinFramework.Pages.Html", Target = "_blank" },
                        new MenuPackage.MenuItem { Name = "REST services", Url = "https://github.com/Bikeman868/OwinFramework.Pages/tree/master/OwinFramework.Pages.Restful", Target = "_blank" },
                        new MenuPackage.MenuItem { Name = "Configuration", Url = "https://github.com/Bikeman868/OwinFramework/wiki/Configuring-middleware-components", Target = "_blank" },
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
                            Url = "https://www.nuget.org/packages/" + r.NugetPackage + "/", 
                            Target = "_blank" 
                        })
                    .Concat(Enumerable.Repeat(new MenuPackage.MenuItem
                        {
                            Name = "More ...",
                            Url = "https://github.com/Bikeman868/OwinFramework/wiki/Package-Directory", 
                            Target = "_blank"
                        }, 1))
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
                        Url = "https://www.nuget.org/packages/" + r.NugetPackage + "/",
                        Target = "_blank"
                    })
                    .Concat(Enumerable.Repeat(new MenuPackage.MenuItem
                    {
                        Name = "More ...",
                        Url = "https://github.com/Bikeman868/OwinFramework/wiki/Package-Directory",
                        Target = "_blank"
                    }, 1))
                    .ToArray()
            };

            var gitHubMenu = new MenuPackage.MenuItem
            {
                Name = "GitHub repos",
                SubMenu = SiteMap.Instance.Repositories
                    .OrderBy(r => r.GitHubRepositoryName)
                    .Select(r => new MenuPackage.MenuItem
                    {
                        Name = r.Caption,
                        Url = r.Url,
                        Target = "_blank"
                    })
                    .ToArray()
            };

            var projectDesktopMenu = new MenuPackage.MenuItem
            {
                Name = "Project source",
                SubMenu = SiteMap.Instance.Projects
                    .Where(p => p.DesktopMenu)
                    .OrderBy(p => p.ProjectName)
                    .Select(p => new MenuPackage.MenuItem
                    {
                        Name = p.ProjectCaption,
                        Url = p.Repository.Url + "/tree/master/" + p.ProjectName,
                        Target = "_blank"
                    })
                    .Concat(Enumerable.Repeat(new MenuPackage.MenuItem
                    {
                        Name = "More ...",
                        Url = "/content/source/index"
                    }, 1))
                    .ToArray()
            };

            _desktopMenu = new List<MenuPackage.MenuItem>
            {
                gettingStartedMenu,
                tutorialsMenu,
                documentationMenu,
                nuGetDesktopMenu,
                gitHubMenu,
                projectDesktopMenu
            };

            _mobileMenu = new List<MenuPackage.MenuItem>
            {
                gettingStartedMenu,
                tutorialsMenu,
                documentationMenu,
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