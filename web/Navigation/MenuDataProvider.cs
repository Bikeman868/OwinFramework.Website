﻿using System.Collections.Generic;
using OwinFramework.Pages.Core.Attributes;
using OwinFramework.Pages.Core.Interfaces.Builder;
using OwinFramework.Pages.Core.Interfaces.DataModel;
using OwinFramework.Pages.Core.Interfaces.Runtime;
using OwinFramework.Pages.Framework.DataModel;

namespace Website.Navigation
{
    [IsDataProvider("menu", typeof(IList<MenuPackage.MenuItem>))]
    public class MenuDataProvider: DataProvider
    {
        private readonly IList<MenuPackage.MenuItem> _mainMenu;

        public MenuDataProvider(IDataProviderDependenciesFactory dependencies) 
            : base(dependencies) 
        {

            var nuGetMenu = new MenuPackage.MenuItem
            {
                Name = "NuGet",
                SubMenu = new List<MenuPackage.MenuItem>
                    {
                        new MenuPackage.MenuItem { Name = "Analysis Reporter", Url = "https://www.nuget.org/packages/Owin.Framework.AnalysisReporter/", Target = "_blank" },
                        new MenuPackage.MenuItem { Name = "Authorization",     Url = "https://www.nuget.org/packages/Owin.Framework.Authorization/", Target = "_blank" },
                        new MenuPackage.MenuItem { Name = "Configuration Manager", Url = "https://www.nuget.org/packages/Owin.Framework.ConfigurationManager/", Target = "_blank" },
                        new MenuPackage.MenuItem { Name = "Dart", Url = "https://www.nuget.org/packages/Owin.Framework.Dart/", Target = "_blank" },
                        new MenuPackage.MenuItem { Name = "Default Document", Url = "https://www.nuget.org/packages/Owin.Framework.DefaultDocument/", Target = "_blank" },
                        new MenuPackage.MenuItem { Name = "Documenter", Url = "https://www.nuget.org/packages/Owin.Framework.Documenter/", Target = "_blank" },
                        new MenuPackage.MenuItem { Name = "Exception Reporter", Url = "https://www.nuget.org/packages/Owin.Framework.ExceptionReporter/", Target = "_blank" },
                        new MenuPackage.MenuItem { Name = "Form Identification", Url = "https://www.nuget.org/packages/Owin.Framework.FormIdentification/", Target = "_blank" },
                        new MenuPackage.MenuItem { Name = "Less", Url = "https://www.nuget.org/packages/Owin.Framework.Less/", Target = "_blank" },
                        new MenuPackage.MenuItem { Name = "Mocks", Url = "https://www.nuget.org/packages/Owin.Framework.Mocks/", Target = "_blank" },
                        new MenuPackage.MenuItem { Name = "Not Found", Url = "https://www.nuget.org/packages/Owin.Framework.NotFound/", Target = "_blank" },
                        new MenuPackage.MenuItem { Name = "Output Cache", Url = "https://www.nuget.org/packages/Owin.Framework.OutputCache/", Target = "_blank" },
                        new MenuPackage.MenuItem { Name = "Pages Html", Url = "https://www.nuget.org/packages/Owin.Framework.Pages.Html/", Target = "_blank" },
                        new MenuPackage.MenuItem { Name = "Route Visualizer", Url = "https://www.nuget.org/packages/Owin.Framework.RouteVisualizer/", Target = "_blank" },
                        new MenuPackage.MenuItem { Name = "Session", Url = "https://www.nuget.org/packages/Owin.Framework.Session/", Target = "_blank" },
                        new MenuPackage.MenuItem { Name = "Static Files", Url = "https://www.nuget.org/packages/Owin.Framework.StaticFiles/", Target = "_blank" },
                        new MenuPackage.MenuItem { Name = "Urchin", Url = "https://www.nuget.org/packages/Owin.Framework.Urchin/", Target = "_blank" },
                        new MenuPackage.MenuItem { Name = "Versioning", Url = "https://www.nuget.org/packages/Owin.Framework.Versioning/", Target = "_blank" },
                    }
            };

            var gitHubMenu = new MenuPackage.MenuItem
            {
                Name = "GitHub",
                SubMenu = new List<MenuPackage.MenuItem>
                    {
                        new MenuPackage.MenuItem { Name = "OWIN Framework", Url = "https://github.com/Bikeman868/OwinFramework", Target = "_blank" },
                        new MenuPackage.MenuItem { Name = "Facilities", Url = "https://github.com/Bikeman868/OwinFramework.Facilities", Target = "_blank" },
                        new MenuPackage.MenuItem { Name = "Middleware", Url = "https://github.com/Bikeman868/OwinFramework.Middleware", Target = "_blank" },
                        new MenuPackage.MenuItem { Name = "Authorization", Url = "https://github.com/Bikeman868/OwinFramework.Authorization", Target = "_blank" },
                        new MenuPackage.MenuItem { Name = "Pages", Url = "https://github.com/Bikeman868/OwinFramework.Pages", Target = "_blank" },
                    }
            };

            _mainMenu = new List<MenuPackage.MenuItem>();
            _mainMenu.Add(nuGetMenu);
            _mainMenu.Add(gitHubMenu);
        }

        protected override void Supply(
            IRenderContext renderContext,
            IDataContext dataContext,
            IDataDependency dependency)
        {
            dataContext.Set(_mainMenu);
        }
    }

}