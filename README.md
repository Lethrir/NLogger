NLogger
=======

Simple logging framework

NLogger is designed to be as simple as possible while providing a few options to make for useful generic logging.

Multiple logging levels: Critical, Exception, Error, Warning, Info and Diagnostic.

Configuration through .NET config files or code.

Getting Started
===============

Firstly we can configure the logging in our Web.Config or App.Config file.

The following should be added to the configSections node:

    <section name="nLogger" type="NLogger.NLoggerSection, NLogger" allowDefinition="Everywhere" allowLocation="true" />

An nLogger config section can then be added.

    <nLogger logLevel="Info">
      <file path="C:\Logs\HelloWorld.log" maxSize="100" maxFiles="3" incrementCurrent="true" />
    </nLogger>

In our code we can then call 

  var logger = LoggerFactory.CreateLogger();

We can now log to the HelloWorld.log file using

  logger.LogInfo("Hello World");

Further information on the configuration and usage can be found in the [Wiki](https://github.com/Lethrir/NLogger/wiki)
