# Configuration Manager

To use the Microsoft Configuration Manager (`web.config`) as your configuration 
mechanism you must include the [Owin.Framework.ConfigurationManager NuGet package](/content/project/owinframework.configuration.configurationmanager/landing) 
into your solution.

Note that this configuration option is very limiting because the `web.config` file
does not allow you to store structured data without defining configuration section
classes. This design makes it impossible to fully implementat the Owin Framework 
configuration design, and you are limited to just storing simple name/value pairs.

If you need backward compatiblilty with an existing `web.config` based configuration
mechanism (for example because your continuous delivery system knows how to make changes
during deployment) then this is a useful short term stop gap, but long term the
even Microsoft have admitted that `web.config` design is weak and moved away from this
in their latest software.
