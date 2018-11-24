using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using OwinFramework.Pages.Core.Attributes;
using OwinFramework.Pages.Core.Extensions;
using OwinFramework.Pages.Core.Interfaces.Builder;
using OwinFramework.Pages.Core.Interfaces.DataModel;
using OwinFramework.Pages.Core.Interfaces.Runtime;
using OwinFramework.Pages.Framework.DataModel;
using Website.Navigation;

namespace Website.DataProviders
{
    [IsDataProvider(typeof(IList<SiteMap.InterfaceDefinition>))]
    public class InterfaceList : DataProvider
    {
        private readonly IList<SiteMap.InterfaceDefinition> _interfaces;

        private readonly Regex _projectRegex = new Regex(
            "/content/project/([^/]*)",
            RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);

        public InterfaceList(IDataProviderDependenciesFactory dependencies)
            : base(dependencies)
        {
            _interfaces = SiteMap.Instance.Interfaces.ToList();
        }

        protected override void Supply(
            IRenderContext renderContext, 
            IDataContext dataContext, 
            IDataDependency dependency)
        {
            if (dependency == null)
                return;

            if (dependency.DataType == typeof(IList<SiteMap.InterfaceDefinition>))
            {
                var path = renderContext.OwinContext.Request.Path.Value;

                var projectMatch = _projectRegex.Match(path);

                if (projectMatch.Success)
                {
                    var projectName = projectMatch.Groups[1].Value;
                    var interfaces = _interfaces
                        .Where(i => string.Equals(i.Project.ProjectName, projectName, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                    dataContext.Set<IList<SiteMap.InterfaceDefinition>>(interfaces);
                }
                else
                {
                    dataContext.Set(_interfaces);
                }
                return;
            }

            throw new Exception(GetType().DisplayName() + " can not supply " +
                dependency.DataType.DisplayName());
        }
    }
}