﻿使用方法

在config文件中添加如下段落


  <configSections>
    <section name="log4net" type="log4net.Config.Log4NetConfigurationSectionHandler,log4net"/>
  </configSections>

    <appSettings>
      <add key="LogName" value="DefaultLogger,TestLogger"/>
    <add key="IsTrace" value="1"/>
    <add key="IsLog" value="1"/>
  </appSettings>

    <log4net>
    <appender name="DefaultRollingLogFileAppender" type="log4net.Appender.RollingFileAppender">
      <lockingModel type="log4net.Appender.FileAppender+MinimalLock" />
      <param name="File" value="log\defaultlog_file.txt"/>
      <param name="AppendToFile" value="true"/>
      <param name="MaxSizeRollBackups" value="3"/>
      <param name="MaximumFileSize" value="1MB"/>
      <param name="RollingStyle" value="Size"/>
      <param name="StaticLogFileName" value="true"/>
      <layout type="log4net.Layout.PatternLayout">
        <param name="ConversionPattern" value="%d - %m%n"/>
      </layout>
    </appender>

    <appender name="TestRollingLogFileAppender" type="log4net.Appender.RollingFileAppender">
      <lockingModel type="log4net.Appender.FileAppender+MinimalLock" />
      <param name="File" value="log\testlog_file.txt"/>
      <param name="AppendToFile" value="true"/>
      <param name="MaxSizeRollBackups" value="3"/>
      <param name="MaximumFileSize" value="1MB"/>
      <param name="RollingStyle" value="Size"/>
      <param name="StaticLogFileName" value="true"/>
      <layout type="log4net.Layout.PatternLayout">
        <param name="ConversionPattern" value="%d - %m%n"/>
      </layout>

    </appender>
    <logger name="DefaultLogger">
      <level value="ALL"/>
      <appender-ref ref="DefaultRollingLogFileAppender"/>
    </logger>
    <logger name="TestLogger">
      <level value="ALL"/>
      <appender-ref ref="TestRollingLogFileAppender"/>
    </logger>
  </log4net>


