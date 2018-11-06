# Configuration

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
