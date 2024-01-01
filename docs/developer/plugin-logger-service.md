# IPluginLoggerService

This service does allow you to log information to the application log. The service does provide the following methods.

```csharp
public interface IPluginLoggerService<T>
{
    void Log(LogSeverity logSeverity, string format, params string[] parameter);
    void LogTrace(string format, params string[] parameter);
    void LogDebug(string format, params string[] parameter);
    void LogInformation(string format, params string[] parameter);
    void LogWarning(string format, params string[] parameter);
    void LogError(string format, params string[] parameter);
    void LogFatalError(string format, params string[] parameter);
}
```
>See the [interface][interface-class] for more information

## How to use it

This can be used like every other logging framework. To log a debug message [inject the service][how-to-inject-a-service] and use the following snippet to use it.

>:information_source: Service was injected as `loggingService`

```csharp
loggingService?.LogDebug($"Execute plugin with path attribute '{variable}' including the following parameters '{variable2}'");
```

## Other services

Also check out the other [injectable services][injectabe-services]

[how-to-inject-a-service]: ./injectable-services.md#how-to-inject-a-service
[injectabe-services]: ./injectable-services.md
[interface-class]: https://github.com/XanatosX/ModularToolManager/blob/main/src/ModularToolManagerPlugin/Services/IPluginLoggerService.cs