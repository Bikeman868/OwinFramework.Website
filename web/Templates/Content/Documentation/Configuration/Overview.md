# Configuration
This overview is an introduction to the concepts, and covers what you need to get started. There
is more detailed documentation in the links from this page about all of the options available
for configuring the Owin pipeline and the middleware components.

## Microsoft OWIN
The Owin Framework is built on Microsoft OWIN. The way that Microsoft OWIN works is by chaining middleware
into a pipeline, then passing each http request through the pipeline until one of the middleware
chooses to handle the request and return a response.

The Microsoft OWIN assembly contains an `IAppBuilder` interface definition like this:

```
public interface IAppBuilder
{
    IDictionary<string, object> Properties { get; }

    object Build(Type returnType);
    IAppBuilder New();
    IAppBuilder Use(object middleware, params object[] args);
}
```

The way this is used, is that each middleware author is supposed to write an extension method
that adds their middleware into the Owin pipeline. A typical extension method looks like this:

```
public static IAppBuilder Use(
    this IAppBuilder appBuilder, 
    MyMiddleware myMiddleware)
{
    myMiddleware.Build(appBuilder);
    return appBuilder;
}
```

This allows application developers to add the middleware to their pipeline with code similar to this:

```
public void Configuration(IAppBuilder app)
{
   app.Use(new Middleware1())
      .Use(new Middleware2())
      .Use(new Middleware3())
      .Use(new Middleware4());
}
```
## Owin Framework
This Owin Framework provides an extension method that extends `IAppBuilder` as required by the
Microsoft implementation, but provides a richer fluent syntax for:

* Adding middleware the the pipeline
* Providing information about how the middleware is configured
* Defining dependencies between middleware
* Spliting the OWIN pipeline into a tree of routes rather than a linear list

The application code for setting up the Owin Framework looks like this example:

```
public void Configuration(IAppBuilder app)
{
  var builder = new Builder();

  builder.Register(new NotFoundError())
    .As("staticFilesNotFoundError")
    .RunAfter("loginId")
    .ConfigureWith(configuration, "/owin/notFound/staticFiles");

  builder.Register(new FormsIdentification())
    .As("loginId")
    .ConfigureWith(configuration, "/owin/auth/forms");

  app.Use(builder);
}
```

Note that the `app.Use(builder);` line at the end is calling the extension method that
is defined by the Microsoft OWIN assembly. The code above that is specific to the Owin
Framework.

Note that each middleware that is added to the `Builder` has fluent syntax for defining the
configuration provider and the path within the configuration file to the configuration data
for that middleware.

Note that each middleware can be named, and these names can be used to define dependencies.
The `Builder` will add them to the Owin pipeline in an order that satisfies these dependencies.

## Configuring Middleware
The Owin Framework provides a simple standardization of middleware configuration so that:

* Application developers can choose any method they like to provide configuration to the
  middleware in their application, and are not restricted to everything being in a `web.config` file.
  For example they can use the Urchin centralized rules-based configuration management system 
  instead of `web.config` if they like.
* Applications define the layout of their configuration data and can therefore include multiple
  instances of the same middleware with different configurations. This is in contrast to the Microsoft
  approach where each component defines where it's configuration data is located in the `web.config` 
  file which makes it impossible to have two instances of the same component with different configurations.
* Middleware developers do not need to know or care which configuration mechanism the application developer chose.
* All configuration data is strongly typed using simple class definitions. Middleware developers define
  the shape of the configuration data and the default values. Application developers define the layout 
  of their configuration data as a whole and selectively override the default values.
* It is possible to write unit tests that test different configurations by mocking the configuration data provider.
* Middleware that can be configured can implement the optional `IConfigurable` interface.
* Application developers configure the middleware in their application by using the fluid `.ConfigureWith()`
  method when adding middleware to the pipeline builder. The `ConfigureWith()` method is passed an object that
  implements the `IConfiguration` interface. The Owin Framework provides implementations of this interface for
  getting configuration data from `web.config` and for Urchin. This is a very simple interface that you can
  easily implement yourself to get configuration data from your database or any other source.

  ## Configuration Options

  [How to use Urchin as a configuration provider](/content/documentation/configuration/urchin)

  [How to use the web.config file for configuration data](/content/documentation/configuration/configurationmanager)
  
  ## NuGet package configurations
  
  For details of how to configure specific NuGet packages see the author's documentation for their package.
  In general Owin Framework middleware packages will have a `Configuration` class within the source code
  that is deserialized from the JSON configuration file. You can look at this source code to determine the
  definitive list of properties that can be configured.

  The [Index of NuGet packages](/content/index/nuget) has links the documentation and source code for all 
  Owin Framework NuGet packages.
