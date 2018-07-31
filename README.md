# ConfigTransform
生成事件中调用此独立程序转换 appsettings 和 connectionstrings 中 的一部分 </add> 节点，参照分别是 key 和 name。
此方案是根据官方建议的[如何使用生成事件？](https://msdn.microsoft.com/zh-cn/library/ke5z92ks.aspx)，开发出简单易用的配置文件管理程序。
你可以尝试其他方案，[.net framework 灵活配置](https://www.cnblogs.com/zhuwansu/p/9262561.html)。

#### Web.config 配置方案

- 添加转换程序到解决方案目录下。例：

> ```
> 【解决方案目录】\_BuildTools\ConfigTransform
> ```

*_BuildTools 、ConfigTransform  为新建的目录。 ConfigTransform 为转换工具根目录。*

- 生成前事件命令行调用工具

> ```bash
> call $(SolutionDir)_BuildTools\ConfigTransform\DBEN.ConfigHelper.exe $(ConfigurationName) $(ProjectDir)Config\ $(ProjectDir)Web.config
> ```

##### 项目配置目录结构

> ```
> -proj 项目目录
>     -Config 目录 *新建*
>         -Debug.config 文件   *替换 Base.config 中 APPSettings、 connectionStrings 中部分子节点*
>         -Release.config 文件 *替换 Base.config 中 APPSettings、 connectionStrings 中部分子节点*
> ```

##### 示例

- Web.config 

```xml
<?xml version="1.0" encoding="utf-8"?>
<!--
  有关如何配置 ASP.NET 应用程序的详细信息，请访问
  http://go.microsoft.com/fwlink/?LinkId=169433
  -->
<configuration>
  <appSettings>
    <add key="base" value="base-appSetting" />
    <!--以下由转换文件替换-->
    <add key="fromDebug" value ="fromDebug-default-appSetting" />
    <add key="fromRelease" value ="fromRelease-default-appSetting" />
  </appSettings>
  <connectionStrings>
      
    <add name="base" connectionString="base-connectionString" />
    <!--以下由转换文件替换-->
    <add name="fromDebug" connectionString ="fromDebug-default-connectionString" />
    <add name="fromRelease" connectionString ="fromRelease-default-connectionString" />
 
  </connectionStrings>
  <!--
   ..... 
   这里是除了 appSettings 、 connectionStrings 节点之外的 其他配置，比如 runtime 节点.
 -->
</configuration>
```

- Debug.config

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <appSettings>
    <add key="fromDebug" value ="fromDebug-appSetting" />
  </appSettings>
  <connectionStrings>
    <add name="fromDebug" connectionString ="fromDebug-connectionString" />
  </connectionStrings>
</configuration>
 
```

- Release.config

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <appSettings>
    <add key="fromRelease" value ="fromRelease-appSetting" />
  </appSettings>
  <connectionStrings>
    <add name="fromRelease" connectionString ="fromRelease-connectionString" />
  </connectionStrings>
</configuration>
```
