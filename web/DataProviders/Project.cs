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
    [IsDataProvider(typeof(SiteMap.FunctionalArea))]
    public class Project : DataProvider
    {
        private readonly Regex _urlRegex = new Regex("/content/project/([^/]*)", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);

        public Project(IDataProviderDependenciesFactory dependencies)
            : base(dependencies)
        {
        }

        protected override void Supply(
            IRenderContext renderContext, 
            IDataContext dataContext, 
            IDataDependency dependency)
        {
            if (dependency == null)
                return;

            if (dependency.DataType == typeof(SiteMap.Project))
            {
                var match = _urlRegex.Match(renderContext.OwinContext.Request.Path.Value);
                if (match.Success)
                {
                    var projectName = match.Groups[1].Value;
                    dataContext.Set(SiteMap.Instance.Projects.FirstOrDefault(p => string.Equals(p.ProjectName, projectName)));
                }
                return;
            }

            throw new Exception(GetType().DisplayName() + " can not supply " +
                dependency.DataType.DisplayName());
        }
    }
}