using System.Collections.Generic;
using System.Linq;
using OwinFramework.Pages.Core.Attributes;
using OwinFramework.Pages.Core.Interfaces.Builder;
using OwinFramework.Pages.Core.Interfaces.DataModel;
using OwinFramework.Pages.Core.Interfaces.Runtime;
using OwinFramework.Pages.Framework.DataModel;

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
                SubMenu = new List<MenuPackage.MenuItem>
                    {
                        new MenuPackage.MenuItem { Name = "Hello world" },
                        new MenuPackage.MenuItem { Name = "Sample websites" },
                        new MenuPackage.MenuItem { Name = "NuGet packages" },
                        new MenuPackage.MenuItem { Name = "Configuration" },
                    }
            };

            var tutorialsMenu = new MenuPackage.MenuItem
            {
                Name = "Tutorials",
                SubMenu = new List<MenuPackage.MenuItem>
                    {
                        new MenuPackage.MenuItem { Name = "Website walkthrough" },
                        new MenuPackage.MenuItem { Name = "Microservice walkthrough" },
                        new MenuPackage.MenuItem { Name = "Localization" },
                        new MenuPackage.MenuItem { Name = "Best practices" },
                    }
            };

            var documentationMenu = new MenuPackage.MenuItem
            {
                Name = "Documentation",
                SubMenu = new List<MenuPackage.MenuItem>
                    {
                        new MenuPackage.MenuItem { Name = "The Owin Frameowrk" },
                        new MenuPackage.MenuItem { Name = "GitHub Wikki", Url = "https://github.com/Bikeman868/OwinFramework/wiki", Target = "_blank" },
                        new MenuPackage.MenuItem { Name = "Identification and authorization" },
                        new MenuPackage.MenuItem { Name = "Session handling" },
                        new MenuPackage.MenuItem { Name = "Configuration" },
                        new MenuPackage.MenuItem { Name = "Configuration" },
                    }
            };

            var nuGetDesktopMenu = new MenuPackage.MenuItem
            {
                Name = "NuGet",
                SubMenu = Sitemap.Instance.Projects
                    .Where(p => !string.IsNullOrEmpty(p.NugetPackage) && p.DesktopMenu)
                    .OrderBy(p => p.NugetPackage)
                    .Select(r => new MenuPackage.MenuItem 
                        { 
                            Name = r.NugetPackage, 
                            Url = "https://www.nuget.org/packages/" + r.NugetPackage + "/", 
                            Target = "_blank" 
                        })
                    .ToList()
            };
            nuGetDesktopMenu.SubMenu.Add(new MenuPackage.MenuItem
            {
                Name = "More ...",
                Url = "/content/nuget/index"
            });

            var nuGetMobileMenu = new MenuPackage.MenuItem
            {
                Name = "NuGet",
                SubMenu = Sitemap.Instance.Projects
                    .Where(p => !string.IsNullOrEmpty(p.NugetPackage) && p.MobileMenu)
                    .OrderBy(p => p.NugetPackage)
                    .Select(r => new MenuPackage.MenuItem
                    {
                        Name = r.NugetPackage,
                        Url = "https://www.nuget.org/packages/" + r.NugetPackage + "/",
                        Target = "_blank"
                    })
                    .ToList()
            };
            nuGetMobileMenu.SubMenu.Add(new MenuPackage.MenuItem
            {
                Name = "More ...",
                Url = "/content/nuget/index"
            });

            var sourceDesktopMenu = new MenuPackage.MenuItem
            {
                Name = "Source code",
                SubMenu = Sitemap.Instance.Repositories
                    .OrderBy(r => r.GitHubRepositoryName)
                    .Select(r => new MenuPackage.MenuItem
                    {
                        Name = r.GitHubRepositoryName + " Repo",
                        Url = r.Url,
                        Target = "_blank"
                    })
                    .Concat(Sitemap.Instance.Projects
                        .Where(p => p.DesktopMenu)
                        .OrderBy(p => p.ProjectName)
                        .Select(p => new MenuPackage.MenuItem
                        {
                            Name = p.Caption + " Project",
                            Url = p.Repository.Url + "/tree/master/" + p.ProjectName,
                            Target = "_blank"
                        }))
                    .ToList()
            };
            sourceDesktopMenu.SubMenu.Add(new MenuPackage.MenuItem
            {
                Name = "More ...",
                Url = "/content/source/index"
            });

            var sourceMobileMenu = new MenuPackage.MenuItem
            {
                Name = "Source code",
                SubMenu = Sitemap.Instance.Repositories
                    .OrderBy(r => r.GitHubRepositoryName)
                    .Select(r => new MenuPackage.MenuItem
                    {
                        Name = r.GitHubRepositoryName,
                        Url = r.Url,
                        Target = "_blank"
                    })
                    .ToList()
            };
            sourceMobileMenu.SubMenu.Add(new MenuPackage.MenuItem
                    {
                        Name = "More ...",
                        Url = "/content/source/index"
                    });

            _desktopMenu = new List<MenuPackage.MenuItem>
            {
                gettingStartedMenu,
                tutorialsMenu,
                documentationMenu,
                nuGetDesktopMenu,
                sourceDesktopMenu
            };

            _mobileMenu = new List<MenuPackage.MenuItem>
            {
                gettingStartedMenu,
                tutorialsMenu,
                documentationMenu,
                nuGetMobileMenu,
                sourceMobileMenu
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

            switch (dependency.ScopeName)
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