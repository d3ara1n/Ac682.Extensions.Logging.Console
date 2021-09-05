# Ac682.Extensions.Logging.Console

向控制台输出**支持自定义**对象格式的日志条目.

使用[Microsoft.Extensions.Logging门面库](https://www.nuget.org/packages/Microsoft.Extensions.Logging).

## 安装

```powershell
Install-Package Ac682.Extensions.Logging.Console
```

## 注入

```csharp
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

## 使用 Template

消息模板位于 `ConsoleLoggerOptions`, 可以通过 `ConsoleLoggerOptionsBuilder` 配置.
形如:

```csharp
var template = "{DateTime:HH:mm:ss} {Level:u4} {Source:s} {Exception|Message}\n";
```

`|` 分隔多个变量，在 `|` 分隔的变量中取存在者优先，顺序靠前者优先，忽略其他。

`:` 分隔变量名和格式化字符串。

| 变量名 | 格式化字符串 | 说明 |
| --- | --- | --- |
| DateTime | 标准的 `System.DateTime` 格式 | 事件日期 |
| Level | "u"(默认, 大写), "l"(小写). 数字表示截取长度 | 日志等级 |
| Source | "s"(仅类型名), "l"(默认, 类型全名) | Logger 的名字(在MSEL中通常为注入者类型全名) |
| Exception | "full"(包含 StackTrace), "message"(仅 Message 属性) | 日志包含的异常对象 |
| Message | 无 | 消息本身 |
