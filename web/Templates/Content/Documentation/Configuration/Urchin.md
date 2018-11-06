# Urchin

To use Urchin as your configuration mechanism you must include the 
[Owin.Framework.Urchin NuGet package](/content/project/owinframework.configuration.urchin/landing) into your solution.

Urchin is an enterprise configuration management system. Urchin comprises
a client assembly that you add to your applications and an optional server.
If you do not use the Urchin server you can update the configuration in the
application by supplying a file containing JSON, provide the URL of an Http
endpoint that will retrn the JSON, or you can pass the JSON as a string (for
example from your database).

If you choose to use the Urchin Server then it provides a centralized rule
based configuration management solution where each application can retrieve
its configuration from an Http endpoint exposed by the Urchin Server.

In both scenarios the configuration is a JSON document.

The layout of this JSON document is at the discretion of the application 
developer. Middleware developers do not need to know how the application
devloper chose to organise their configuration because when each middleware 
is configured the path to the middleware's configuration is passed in as
a parameter.

GitHub contains [more information about Urchin](https://github.com/Bikeman868/Urchin).

## Using a Configuration File
The source code below is an example of getting an Urchin configuration from a 
JSON file using Ninject as the IoC container. You will need to adapt this code
to your specific needs.

The code assumes that you have used a package manager like [Paket](https://fsprojects.github.io/Paket/) or
[NuGet](https://www.nuget.org/) to add the Owin Framework into your solution.

```
   using System;
   using System.IO;
   using System.Reflection;
   using Microsoft.Owin;
   using Ioc.Modules;
   using Ninject;
   using Urchin.Client.Sources;
   using Owin;
   using OwinFramework.Builder;
   using OwinFramework.Interfaces.Builder;
   using OwinFramework.Interfaces.Utility;
   
   [assembly: OwinStartup(typeof(Startup))]
   
   public class Startup
   {
       private static IDisposable _configurationSource;
   
       public void Configuration(IAppBuilder app)
       {
           var packageLocator = new PackageLocator()
               .ProbeBinFolderAssemblies()
               .Add(Assembly.GetExecutingAssembly());
   
           var ninject = new StandardKernel(new Ioc.Modules.Ninject.Module(packageLocator));
   
           var hostingEnvironment = ninject.Get<IHostingEnvironment>();
           var configFile = new FileInfo(hostingEnvironment.MapPath("config.json"));
           _configurationSource = ninject.Get<FileSource>().Initialize(configFile, TimeSpan.FromSeconds(5));
   
           var configuration = ninject.Get<IConfiguration>();
   
           //
           // More code here that uses the configuration variable
           //
       }
   }
```

## Using a Configuration URL

To alter the example above to load the configuration from a URL instead, you just 
need to replace the three lines that construct and initialize a `FileSource` with
code to construct and initialize a `UriSource` instead like this:

```
using System;
using System.IO;
using System.Reflection;
using Microsoft.Owin;
using Ioc.Modules;
using Ninject;
using Urchin.Client.Sources;
using Owin;
using OwinFramework.Builder;
using OwinFramework.Interfaces.Builder;
using OwinFramework.Interfaces.Utility;

[assembly: OwinStartup(typeof(Startup))]

public class Startup
{
    private static IDisposable _configurationSource;

    public void Configuration(IAppBuilder app)
    {
        var packageLocator = new PackageLocator()
            .ProbeBinFolderAssemblies()
            .Add(Assembly.GetExecutingAssembly());

        var ninject = new StandardKernel(new Ioc.Modules.Ninject.Module(packageLocator));

        var configUri = new Uri("https://mydomain/configuration/myapp.json");
        _configurationSource = ninject.Get<UriSource>().Initialize(configUri, TimeSpan.FromSeconds(5));

        var configuration = ninject.Get<IConfiguration>();

		//
		// More code here that uses the configuration variable
		//
	}
}
```


## Hard coded configuration
This is a useful option for unit testing where you want to test that
your code works with different configurations.

To change the code above to use a hard coded configuration JSON you do
not need a configuration source, just set the configuration directly
into the configuration store like this:

```
using System;
using System.IO;
using System.Reflection;
using Microsoft.Owin;
using Ioc.Modules;
using Ninject;
using Urchin.Client.Interfaces;
using Owin;
using OwinFramework.Builder;
using OwinFramework.Interfaces.Builder;
using OwinFramework.Interfaces.Utility;

[assembly: OwinStartup(typeof(Startup))]

public class Startup
{
    public void Configuration(IAppBuilder app)
    {
        var packageLocator = new PackageLocator()
            .ProbeBinFolderAssemblies()
            .Add(Assembly.GetExecutingAssembly());

        var ninject = new StandardKernel(new Ioc.Modules.Ninject.Module(packageLocator));

        var configurationStore = ninject.Get<IConfigurationStore>();
        configurationStore.UpdateConfiguration("{....json-config-goes-here....}");

        var configuration = ninject.Get<IConfiguration>();

		//
		// More code here that uses the configuration variable
		//
	}
}
```
