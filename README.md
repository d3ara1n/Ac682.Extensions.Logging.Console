# Ac682.Extensions.Logging.Console

向控制台输出**支持自定义**对象格式的日志条目.

使用[Microsoft.Extensions.Logging门面库](https://www.nuget.org/packages/Microsoft.Extensions.Logging).

## 安装

```powershell
Install-Package Ac682.Extensions.Logging.Console
```

## 注入

```c#
// Startup.cs

public void ConfigureService(IServiceCollection services)
{
    services.AddLogging(options => options
    {
        .AddConsole(c => c
            .SetMinimalLevel(LogLevel.Information)
            .AddBuiltinFormatters()
            .AddFormater<CustomFormatter>())
    });
}
```

## 配置

配置使用 `ConsoleLoggerOptions` 传入.

## 自定义 Formatter

本扩展使用 `IObjectLoggingFormatter` 序列化对象为可在控制台显示的上色文本.
自己实现该接口然后注册即可自动捕捉特定对象并发挥作用.
实现过程中缺啥构造啥，没有任何坑.