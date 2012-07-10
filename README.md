Youzee .NET Logger
==================
Simple logger for .NET framework.

Configuration
-------------

To configure the logger, you should edit the **App.config** file.
```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <configSections>
    <section name="logger" type="Youzee.Util.Logger.LoggerSection, Youzee.Util.Logger"/>
  </configSections>
  <logger>
    <log level="*"/>
  </logger>
</configuration>
```

If you want to log only a specific level, you can do it setting the "level" attribute to "debug", "error" or "info".
```xml
<log level="debug"/>
```

You can also disable the logging with
```xml
<log level="debug" disabled="true"/>
```

If you want to output the log to a file, you should define a "filePath" attribute
```xml
<log level="debug" filePath="C:/logs.txt"/>
```

Configuration for Microsoft Unity dependency injection container
```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <configSections>
    <section name="logger" type="Youzee.Util.Logger.LoggerSection, Youzee.Util.Logger"/>
    <section name="unity" type="Microsoft.Practices.Unity.Configuration.UnityConfigurationSection, Microsoft.Practices.Unity.Configuration"/>
  </configSections>

  <unity xmlns="http://schemas.microsoft.com/practices/2010/unity">
    <namespace name="Youzee.Util.Logger" />
    <assembly name="Youzee.Util.Logger" />

    <container name="container">
      <register type="LogWriter" mapTo="EventLogWriter"/>
      <register type="LoggerConfiguration" mapTo="FileLoggerConfiguration"/>
      <register type="Logger">
        <constructor>
          <param name="logWriter" type="LogWriter"/>
          <param name="loggerConfiguration" type="LoggerConfiguration" />
        </constructor>
      </register>
    </container>
  </unity>

  <logger>
    <log level="*" filePath="C:/test.txt"/>
  </logger>
</configuration>
```

In this case, you can choose between the **EventLogWriter** or **FileLogWriter**. The **EventLogWriter** will write into the Windows Event System and the **FileLogWriter** will do it over a configured file (last one require a filePath attribute into the log tag).

Usage
-----

```csharp
using Youzee.Util.Logger;

class Program
{
    static void Main(string[] args)
    {
        LogWriter logWriter = new EventLogWriter();//Log the events through the Windows Event System
        //LogWriter logWriter = new FileLogWriter();//Log to a file
        LoggerConfiguration loggerConfiguration = new FileLoggerConfiguration();
        Logger logger = new Logger(logWriter, loggerConfiguration);

        logger.Error("This is an error!");
        logger.Debug("This is a debug message!");
        logger.Info("This is an info message!");
    }
}
```

Using Microsoft Unity dependency injection container
```csharp
using Youzee.Util.Logger;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

class Program
{
    static void Main(string[] args)
    {
        IUnityContainer container = new UnityContainer();
        container.LoadConfiguration("container");
        Logger logger = container.Resolve<Logger>();

        logger.Error("This is an error!");
        logger.Debug("This is a debug message!");
        logger.Info("This is an info message!");
    }
}
```

Dependencies
------------
* .NET framework 4.5