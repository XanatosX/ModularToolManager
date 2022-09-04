using ModularToolManagerModel.Data.Enum;
using ModularToolManagerPlugin.Enums;
using ModularToolManagerPlugin.Services;

namespace ModularToolManagerModel.Services.Logging;

/// <summary>
/// Adapter class to allow logging
/// </summary>
public class LoggingPluginAdapter : IPluginLoggerService
{
    /// <summary>
    /// The real logging service to use
    /// </summary>
    private readonly ILoggingService? loggingService;

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    /// <param name="loggingService">The logging service to use</param>
    public LoggingPluginAdapter(ILoggingService? loggingService)
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
    private LogLevel MatchLogLevel(LogSeverity severity)
    {
        return severity switch
        {
            LogSeverity.Trace => LogLevel.Trace,
            LogSeverity.Debug => LogLevel.Debug,
            LogSeverity.Information => LogLevel.Information,
            LogSeverity.Warning => LogLevel.Warning,
            LogSeverity.Error => LogLevel.Error,
            LogSeverity.Fatal => LogLevel.Fatal,
            _ => LogLevel.Unknown
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
