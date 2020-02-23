using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Website.Navigation
{
    public class SiteMap
    {
        public static SiteMap Instance { get; private set; }

        static SiteMap()
        {
            Instance = new SiteMap();
            Instance.Initialize();
        }

        public FunctionalArea[] FunctionalAreas { get; private set; }
        public Repository[] Repositories { get; private set; }
        public Project[] Projects { get; private set; }
        public RepositoryOwner[] RepositoryOwners { get; private set; }
        public InterfaceDefinition[] Interfaces { get; private set; }

        public class FunctionalArea
        {
            public string Identifier { get; private set; }
            public string Caption { get; private set; }
            public Document Document { get; private set; }
            public Project[] Projects { get; private set; }
            public Topic[] Topics { get; private set; }

            public FunctionalArea(string identifier, string name, string description)
            {
                Identifier = identifier;
                Caption = name;
                Document = new Document(identifier.ToLower(), name)
                {
                    Description = description
                }
                    .SetPages("/content/area/")
                    .SetImage("/assets/images/area/", ".png");
            }

            public FunctionalArea AddTopic(Topic topic)
            {
                Topics = Topics == null
                    ? new Topic[] { topic }
                    : Topics.Concat(Enumerable.Repeat(topic, 1)).ToArray();
                return this;
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
            public Repository[] Repositories { get; private set; }

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
            public string Description { get; set; }
            public RepositoryOwner Owner { get; set; }
            public Project[] Projects { get; set; }

            public Repository(string gitHubRepositoryName, string gitHubAccountName, string description, string url = null)
            {
                GitHubRepositoryName = gitHubRepositoryName;
                Caption = gitHubRepositoryName.Substring(gitHubRepositoryName.IndexOf('.') + 1).Replace(".", " ");
                Description = description;
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
            public Repository Repository { get; private set; }
            public FunctionalArea FunctionalArea { get; private set; }
            public Topic[] Topics { get; private set; }

            public Project(string projectName, string gitHubRepositoryName, string areaIdentifier, string description)
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
                Document.Description = description;
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

            public Project AddTopic(Topic topic)
            {
                Topics = Topics == null
                    ? new Topic[] { topic }
                    : Topics.Concat(Enumerable.Repeat(topic, 1)).ToArray();
                return this;
            }

            public Project Initialize()
            {
                return this;
            }
        }

        public class InterfaceDefinition
        {
            public string Name { get; private set; }
            public string NamespaceName { get; private set; }
            public string FullyQualifiedName { get; private set; }
            public string SourceUrl { get; private set; }
            public Document Document { get; private set; }
            public Project Project { get; private set; }
            public Topic[] Topics { get; set; }

            public InterfaceDefinition(string typeName, string projectName)
            {
                FullyQualifiedName = typeName;

                var lastPeriod = typeName.LastIndexOf('.');
                Name = typeName.Substring(lastPeriod + 1);
                NamespaceName = typeName.Substring(0, lastPeriod);

                Document = new Document(typeName.ToLower(), Name)
                    .SetPages("/content/interface/")
                    .SetImage("/assets/images/interface/", ".png");
                Project = Instance.Projects.First(p => p.ProjectName == projectName);
            }

            public InterfaceDefinition Initialize()
            {
                SourceUrl =
                    "https://raw.githubusercontent.com/" +
                    Project.Repository.Owner.GitHubAccountName + "/" +
                    Project.ProjectName + "/master/" +
                    FullyQualifiedName.Replace('.', '/') + ".cs";
                return this;
            }
        }

        public class Topic
        {
            public string Name { get; private set; }
            public Document Document { get; private set; }
            public Topic[] SubTopics { get; set; }

            public Topic(string name, string title, string templatePath)
            {
                Name = name;
                if (!string.IsNullOrEmpty(templatePath))
                {
                    Document = new Document(name.ToLower() + "_topic", title);
                    Document.LandingPageTemplate = templatePath;
                }
            }

            public Topic AddTopic(Topic topic)
            {
                SubTopics = SubTopics == null
                    ? new Topic[] { topic }
                    : SubTopics.Concat(Enumerable.Repeat(topic, 1)).ToArray();
                return this;
            }

            public Topic Initialize()
            {
                return this;
            }
        }

        public class Document
        {
            public string Identifier { get; private set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string ImageUrl { get; set; }
            public string LandingPageTemplate { get; set; }
            public string OverviewTemplate { get; set; }

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
                OverviewTemplate = templateRoot + Identifier + "/readme";
                return this;
            }

            public Document SetImage(string imageRoot, string fileExt)
            {
                ImageUrl = imageRoot + Identifier + fileExt;
                return this;
            }
        }

        public SiteMap Initialize()
        {
            FunctionalAreas = new[]
            {
                new FunctionalArea("framework", "Owin Framework", "Interface definitions and their mocks. Owin Pipeline builder"),
                new FunctionalArea("content", "Content", "Middleware components that render content to the Http response"),
                new FunctionalArea("diagnostics", "Diagnostics", "Tools that help developers to track down issues in development or production environments"),
                new FunctionalArea("authorization", "Authorization", "Identifying who or what is making the Http request and restricting them with permissions"),
                new FunctionalArea("pages", "Pages Framework", "Rapid development of web pages and web services using templates and code annotation")
                    .AddTopic(new Topic("Elements", "Elements", "/content/area/pages/elements")
                        .AddTopic(new Topic("Pages", "Page Elements", "/content/area/pages/pages"))
                        .AddTopic(new Topic("Regions", "Region Elements", "/content/area/pages/regions"))
                        .AddTopic(new Topic("Layouts", "Layout Elements", "/content/area/pages/layouts"))
                        .AddTopic(new Topic("Modules", "Module Elements", "/content/area/pages/modules"))
                        .AddTopic(new Topic("Components", "Component Elements", "/content/area/pages/components")) ),
                new FunctionalArea("testing", "Unit Testing", "Mocks and scafolding for testing code in an isolated sandbox")
            };

            RepositoryOwners = new[]
            {
                new RepositoryOwner("Bikeman868")
            };

            Repositories = new[]
            {
                new Repository("OwinFramework", "Bikeman868", "The interface definitions that define the Owin Framework"),
                new Repository("OwinFramework.Middleware", "Bikeman868", "A collection of middleware packages that many websites frequently need. Provides examples of how to write middleware"),
                new Repository("OwinFramework.Pages", "Bikeman868", "A large framework for defining web pages and services using templates and code annotations"),
                new Repository("OwinFramework.Authorization", "Bikeman868", "Mechanisms for identifying the caller and restricting what they have access to"),
                new Repository("OwinFramework.Facilities", "Bikeman868", "A collection of packages that provide implementations of some of the Owin Framework interfaces that define cross-cutting concerns"),
            };

            Projects = new[]
            {
                new Project("OwinFramework", "OwinFramework", "framework", 
                    "Defines the interfaces that allow all of the other NuGet packages to work seamlessly together")
                    .Menus(true, true),

                new Project("OwinFramework.Configuration.ConfigurationManager", "OwinFramework", "framework", 
                    "Implements IConfiguration using the .Net ConfigurationManager class. This allows you to configure the OWIN Framework in the web.config file")
                    .Menus(false, false),

                new Project("OwinFramework.Configuration.Urchin", "OwinFramework", "framework", 
                    "Implements IConfiguration using the Urchin centralized enterprise configuration management system")
                    .Menus(false, false),

                new Project("OwinFramework.Mocks", "OwinFramework", "testing", 
                    "Contains mocks of OWIN Framework interfaces that you can use to write unit tests for your middleware")
                    .Menus(false, false),

                new Project("OwinFramework.Facilities.Cache.Local", "OwinFramework.Facilities", "framework", 
                    "An implementation of the ICache facility that caches in process memory")
                    .Menus(false, false),

                new Project("OwinFramework.Facilities.IdentityStore.Prius", "OwinFramework.Facilities", "authorization", 
                    "An implementation of the IIdentityStore facility that persists user account and login information using the Prius ORM")
                    .Menus(false, false),

                new Project("OwinFramework.Facilities.TokenStore.Cache", "OwinFramework.Facilities", "framework", 
                    "An implementation of the ITokenStore facility that persists tokens using the ICache facility")
                    .Menus(false, false),

                new Project("OwinFramework.Facilities.TokenStore.Prius", "OwinFramework.Facilities", "framework", 
                    "An implementation of the ITokenStore facility that persists tokens using the Prius ORM")
                    .Menus(false, false),

                new Project("OwinFramework.AnalysisReporter", "OwinFramework.Middleware", "diagnostics", 
                    "Enumerates all middleware in the Owin pipeline that implements IAnalysable and formats their stats in html, md, json, text or xml format")
                    .Menus(false, false),

                new Project("OwinFramework.Dart", "OwinFramework.Middleware", "content", 
                    "Detects if the user agent has native support for the Dart programming language and serves either Dart code or compiled Javascript as needed")
                    .Menus(false, false),
        
                new Project("OwinFramework.DefaultDocument", "OwinFramework.Middleware", "content", 
                    "Rewrites requests for the root of your site to a default document")
                    .Menus(true, true),

                new Project("OwinFramework.Documenter", "OwinFramework.Middleware", "diagnostics", 
                    "Enumerates all middleware in the Owin pipeline that implements ISelfDocumenting and formats an html response documenting the endpoints")
                    .Menus(false, false),

                new Project("OwinFramework.ExceptionReporter", "OwinFramework.Middleware", "diagnostics", 
                    "Catches exceptions thrown by downstream middleware and returns an exception report. Returns templated public apology or detailed technical information. Can optionally send email")
                    .Menus(true, false),

                new Project("OwinFramework.FormIdentification", "OwinFramework.Middleware", "authorization", 
                    "Allows other middleware to define required roles and permissions. Uses a system of identities, groups, roles and permissions stored in a database. Supports wildcard matching of resource specific permissions. Implements IAuthorization and depends on IIdentification middleware")
                    .Menus(true, false),

                new Project("OwinFramework.Less", "OwinFramework.Middleware", "content", 
                    "Serves requests for CSS files by compiling and caching LESS files on the fly")
                    .Menus(true, false),

                new Project("OwinFramework.NotFound", "OwinFramework.Middleware", "content", 
                    "Returns a 404 (not found) response when no other middleware handled the request")
                    .Menus(true, false),

                new Project("OwinFramework.OutputCache", "OwinFramework.Middleware", "content", 
                    "Receives hints from downstream middleware about the volatility of generated output, and uses rules based configuration to decide what to cache and for how long")
                    .Menus(true, false),

                new Project("OwinFramework.RouteVisualizer", "OwinFramework.Middleware", "diagnostics", 
                    "Responds with an SVG drawing of all middleware components configured in the Owin pipeline. If these middleware implement ISelfDocumenting or IAnalysable then this information will be included on the drawing")
                    .Menus(false, false),

                new Project("OwinFramework.Session", "OwinFramework.Middleware", "authorization", 
                    "Contains a few alternate implementations of ISession that your application can choose from")
                    .Menus(true, false),

                new Project("OwinFramework.StaticFiles", "OwinFramework.Middleware", "content", 
                    "Responds to requests by returning the contents of files in the file system. Supports directory mapping, security and file extension restrictions")
                    .Menus(true, true),

                new Project("OwinFramework.Versioning", "OwinFramework.Middleware", "content", 
                    "Modifies asset URLs in generated pages to include a version number then strips these versions numbers off again on incomming requests. This allows the assets to be cached indefinately by the browser and updated on new versions of the website")
                    .Menus(true, false),

                new Project("OwinFramework.Authorization", "OwinFramework.Authorization", "authorization", 
                    "Allows other middleware to define required roles and permissions. Uses a system of identities, groups, roles and permissions stored in a database. Supports wildcard matching of resource specific permissions. Implements IAuthorization and depends on IIdentification middleware")
                    .Menus(true, true),

                new Project("OwinFramework.Authorization.Core", "OwinFramework.Authorization", "authorization", 
                    "This very small package is required by the other packages that relate to authorization")
                    .Menus(false, false),

                new Project("OwinFramework.Authorization.Prius", "OwinFramework.Authorization", "authorization", 
                    "Stores Authorization information relating to groups, roles and permissions in a relational database using the Prius ORM")
                    .Menus(true, false),

                new Project("OwinFramework.Authorization.UI", "OwinFramework.Authorization", "authorization", 
                    "A user interface for managing identities, groups, roles and permissions. Uses built-in CSS or you can provide a custom one. Renders the UI into a &lt;div> element on your page")
                    .Menus(true, false),

                new Project("OwinFramework.Pages.Core", "OwinFramework.Pages", "pages", 
                    "Contract definitions for the packages that render Html. Required dependency for other packages in the Pages framework")
                    .Menus(false, false)
                    .AddTopic(new Topic("Attributes", "Core pages framework attributes", null)
                        .AddTopic(new Topic("Description", "The [Description] attribute", "/content/project/owinframework.pages.core/attributes/description"))
                        .AddTopic(new Topic("Example", "The [Example] attribute", "/content/project/owinframework.pages.core/attributes/example")) ),

                new Project("OwinFramework.Pages.DebugMiddleware", "OwinFramework.Pages", "diagnostics", 
                    "Allows you to add ?debug=xxx to your website page URLs to see diagnostic information about page construction and data binding")
                    .Menus(false, false),

                new Project("OwinFramework.Pages.Framework", "OwinFramework.Pages", "pages", 
                    "Default implementation of the contracts in Owin.Framework.Pages.Core")
                    .Menus(false, false),

                new Project("OwinFramework.Pages.Html", "OwinFramework.Pages", "pages", 
                    "Allows you to build a website with pages of Html content using templates, regions, layouts and 3rd party packages")
                    .Menus(true, true)
                    .AddTopic(new Topic("Attributes", "Html framework attributes", null)
                        .AddTopic(new Topic("CacheOutput", "The [CacheOutput] attribute", "/content/project/owinframework.pages.html/attributes/cacheoutput"))
                        .AddTopic(new Topic("ChildContainer", "The [ChildContainer] attribute", "/content/project/owinframework.pages.html/attributes/childcontainer"))
                        .AddTopic(new Topic("ChildStyle", "The [ChildStyle] attribute", "/content/project/owinframework.pages.html/attributes/childstyle"))
                        .AddTopic(new Topic("Container", "The [Container] attribute", "/content/project/owinframework.pages.html/attributes/container"))
                        .AddTopic(new Topic("DataScope", "The [DataScope] attribute", "/content/project/owinframework.pages.html/attributes/datascope"))
                        .AddTopic(new Topic("DeployCss", "The [DeployCss] attribute", "/content/project/owinframework.pages.html/attributes/deploycss"))
                        .AddTopic(new Topic("DeployedAs", "The [DeployedAs] attribute", "/content/project/owinframework.pages.html/attributes/deployedas"))
                        .AddTopic(new Topic("DeployFunction", "The [DeployFunction] attribute", "/content/project/owinframework.pages.html/attributes/deployfunction"))
                        .AddTopic(new Topic("IsPage", "The [IsPage] attribute", "/content/project/owinframework.pages.html/attributes/ispage")) )
                    .AddTopic(new Topic("Templates", "Overview of the pages framework templating system", "/content/project/owinframework.pages.html/templateoverview")
                        .AddTopic(new Topic("Markdown parser", "The markdown template parser", "/content/project/owinframework.pages.html/templates/markdownparser"))
                        .AddTopic(new Topic("Multi-part parser", "The multi-part template parser", "/content/project/owinframework.pages.html/templates/multipartparser"))
                        .AddTopic(new Topic("Mustache parser", "The mustache template parser", "/content/project/owinframework.pages.html/templates/mustacheparser")) ),

                new Project("OwinFramework.Pages.Mocks", "OwinFramework.Pages", "testing", 
                    "Provides mock implementations for the interfaces defined in OwinFramework.Pages.Core so that you can write unit tests")
                    .Menus(false, false),

                new Project("OwinFramework.Pages.Restful", "OwinFramework.Pages", "pages", 
                    "Allows you to add REST endpoints to a website using classes and methods decorated with attributes")   
                    .Menus(true, true)
                    .AddTopic(new Topic("Attributes", "Restful framework attributes", null)
                        .AddTopic(new Topic("IsService", "The [IsService] attribute", "/content/project/owinframework.pages.restful/attributes/isservice"))
                        .AddTopic(new Topic("CacheOutput", "The [CacheOutput] attribute", "/content/project/owinframework.pages.restful/attributes/cacheoutput"))
                        .AddTopic(new Topic("Endpoint", "The [Endpoint] attribute", "/content/project/owinframework.pages.restful/attributes/endpoint"))
                        .AddTopic(new Topic("EndpointParameter", "The [EndpointParameter] attribute", "/content/project/owinframework.pages.restful/attributes/endpointparameter")) )
                    .AddTopic(new Topic("Serialization", "Custom response serialization from service endpoints", "/content/project/owinframework.pages.restful/customserialization"))
                    .AddTopic(new Topic("Deserialization", "Custom request deserialization into service endpoints", "/content/project/owinframework.pages.restful/customdeserialization"))
                    .AddTopic(new Topic("Endpoint parameters", "Passing parameters to service endpoints", "/content/project/owinframework.pages.restful/parameters"))
                    .AddTopic(new Topic("Endpoint permissions", "Restricting access to service endpoints", "/content/project/owinframework.pages.restful/permissions"))
                    .AddTopic(new Topic("Endpoint routing", "Routing requests to service endpoints", "/content/project/owinframework.pages.restful/routing")),
            };

            Interfaces = new[]
            {
                new InterfaceDefinition("OwinFramework.InterfacesV1.Capability.IAnalysable", "OwinFramework"),
                new InterfaceDefinition("OwinFramework.InterfacesV1.Capability.IConfigurable", "OwinFramework"),
                new InterfaceDefinition("OwinFramework.InterfacesV1.Capability.ISelfDocumenting", "OwinFramework"),
                new InterfaceDefinition("OwinFramework.InterfacesV1.Capability.ITraceable", "OwinFramework"),

                new InterfaceDefinition("OwinFramework.InterfacesV1.Facilities.ICache", "OwinFramework"),
                new InterfaceDefinition("OwinFramework.InterfacesV1.Facilities.ICertificateStore", "OwinFramework"),
                new InterfaceDefinition("OwinFramework.InterfacesV1.Facilities.ICredentialStore", "OwinFramework"),
                new InterfaceDefinition("OwinFramework.InterfacesV1.Facilities.IIdentityDirectory", "OwinFramework"),
                new InterfaceDefinition("OwinFramework.InterfacesV1.Facilities.IMimeTypeEvaluator", "OwinFramework"),
                new InterfaceDefinition("OwinFramework.InterfacesV1.Facilities.IPasswordHasher", "OwinFramework"),
                new InterfaceDefinition("OwinFramework.InterfacesV1.Facilities.ISharedSecretStore", "OwinFramework"),
                new InterfaceDefinition("OwinFramework.InterfacesV1.Facilities.ISocialIdentityStore", "OwinFramework"),
                new InterfaceDefinition("OwinFramework.InterfacesV1.Facilities.ITokenStore", "OwinFramework"),

                new InterfaceDefinition("OwinFramework.InterfacesV1.Middleware.IAuthorization", "OwinFramework"),
                new InterfaceDefinition("OwinFramework.InterfacesV1.Middleware.IIdentification", "OwinFramework"),
                new InterfaceDefinition("OwinFramework.InterfacesV1.Middleware.IOutputCache", "OwinFramework"),
                new InterfaceDefinition("OwinFramework.InterfacesV1.Middleware.IRequestRewriter", "OwinFramework"),
                new InterfaceDefinition("OwinFramework.InterfacesV1.Middleware.IResponseProducer", "OwinFramework"),
                new InterfaceDefinition("OwinFramework.InterfacesV1.Middleware.IResponseRewriter", "OwinFramework"),
                new InterfaceDefinition("OwinFramework.InterfacesV1.Middleware.ISession", "OwinFramework"),
            };

            for (var i = 0; i < FunctionalAreas.Length; i++) FunctionalAreas[i].Initialize();
            for (var i = 0; i < Repositories.Length; i++) Repositories[i].Initialize();
            for (var i = 0; i < Projects.Length; i++) Projects[i].Initialize();
            for (var i = 0; i < RepositoryOwners.Length; i++) RepositoryOwners[i].Initialize();
            for (var i = 0; i < Interfaces.Length; i++) Interfaces[i].Initialize();

            return this;
        }
    }
}