using Microsoft.Extensions.Logging;
using ModularToolManagerPlugin.Enums;
using ModularToolManagerPlugin.Services;

namespace ModularToolManagerModel.Services.Logging;

/// <summary>
/// Adapter class to allow logging
/// </summary>
public class LoggingPluginAdapter<T> : IPluginLoggerService<T>
{
    /// <summary>
    /// The real logging service to use
    /// </summary>
    private readonly ILogger<T>? loggingService;

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    /// <param name="loggingService">The logging service to use</param>
    public LoggingPluginAdapter(ILogger<T> loggingService)
    {
        this.loggingService = loggingService;
    }

    /// <inheritdoc/>
    public void Log(LogSeverity logSeverity, string format, params string[] parameter)
    {
        loggingService?.Log(MatchLogLevel(logSeverity), format, parameter);
    }

    /// <summary>
    /// Match the log level for the logging service
    /// </summary>
    /// <param name="severity">The severity to use</param>
    /// <returns>The log level for the logging service</returns>
    private Microsoft.Extensions.Logging.LogLevel MatchLogLevel(LogSeverity severity)
    {
        return severity switch
        {
            LogSeverity.Trace => Microsoft.Extensions.Logging.LogLevel.Trace,
            LogSeverity.Debug => Microsoft.Extensions.Logging.LogLevel.Debug,
            LogSeverity.Information => Microsoft.Extensions.Logging.LogLevel.Information,
            LogSeverity.Warning => Microsoft.Extensions.Logging.LogLevel.Warning,
            LogSeverity.Error => Microsoft.Extensions.Logging.LogLevel.Error,
            LogSeverity.Fatal => Microsoft.Extensions.Logging.LogLevel.Critical,
            _ => Microsoft.Extensions.Logging.LogLevel.None
        };
    }

    /// <inheritdoc/>
    public void LogTrace(string format, params string[] parameter) => Log(LogSeverity.Trace, format, parameter);

    /// <inheritdoc/>
    public void LogDebug(string format, params string[] parameter) => Log(LogSeverity.Debug, format, parameter);

    /// <inheritdoc/>
    public void LogError(string format, params string[] parameter) => Log(LogSeverity.Error, format, parameter);

    /// <inheritdoc/>
    public void LogFatalError(string format, params string[] parameter) => Log(LogSeverity.Fatal, format, parameter);

    /// <inheritdoc/>
    public void LogInformation(string format, params string[] parameter) => Log(LogSeverity.Information, format, parameter);

    /// <inheritdoc/>
    public void LogWarning(string format, params string[] parameter) => Log(LogSeverity.Warning, format, parameter);
}
