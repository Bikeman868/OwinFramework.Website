using System;
using OwinFramework.Pages.Core.Attributes;
using OwinFramework.Pages.Core.Extensions;
using OwinFramework.Pages.Core.Interfaces.Builder;
using OwinFramework.Pages.Core.Interfaces.DataModel;
using OwinFramework.Pages.Core.Interfaces.Runtime;
using OwinFramework.Pages.Framework.DataModel;
using Website.Navigation;

namespace Website.DataProviders
{
    [IsDataProvider("project__document", typeof(SiteMap.Document))]
    [NeedsData(typeof(SiteMap.Project))]
    public class ProjectDocument : DataProvider
    {
        public ProjectDocument(IDataProviderDependenciesFactory dependencies)
            : base(dependencies)
        {
        }

        protected override void Supply(
            IRenderContext renderContext, 
            IDataContext dataContext, 
            IDataDependency dependency)
        {
            SiteMap.Document document = null;

            if (dependency != null && dependency.DataType == typeof(SiteMap.Document))
            {
                var project = dataContext.Get<SiteMap.Project>();
                if (project != null)
                    document = project.Document;
            }

            dataContext.Set(document);
        }
    }
}