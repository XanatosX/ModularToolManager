using ModularToolManagerModel.Data;
using ModularToolManagerModel.Data.Enum;

namespace ModularToolManagerModel.Services.Logging;

/// <summary>
/// interface for the logging service of the application
/// </summary>
public interface ILoggingService
{
    /// <summary>
    /// Log data 
    /// </summary>
    /// <param name="logSeverity">The severity of the log entry</param>
    /// <param name="format">The format of the message</param>
    /// <param name="parameter">The parameters to use in the format</param>
    void Log(LogLevel logLevel, string format, params string[] parameter);

    /// <summary>
    /// Log data on trace level
    /// </summary>
    /// <param name="format">The format of the message</param>
    /// <param name="parameter">The parameters to use in the format</param>
    void LogTrace(string format, params string[] parameter) => Log(LogLevel.Trace, format, parameter);

    /// <summary>
    /// Log data on trace level
    /// </summary>
    /// <param name="format">The format of the message</param>
    /// <param name="parameter">The parameters to use in the format</param>
    void LogDebug(string format, params string[] parameter) => Log(LogLevel.Debug, format, parameter);

    /// <summary>
    /// Log data on trace level
    /// </summary>
    /// <param name="format">The format of the message</param>
    /// <param name="parameter">The parameters to use in the format</param>
    void LogInfo(string format, params string[] parameter) => Log(LogLevel.Information, format, parameter);

    /// <summary>
    /// Log data on trace level
    /// </summary>
    /// <param name="format">The format of the message</param>
    /// <param name="parameter">The parameters to use in the format</param>
    void LogWarning(string format, params string[] parameter) => Log(LogLevel.Warning, format, parameter);

    /// <summary>
    /// Log data on trace level
    /// </summary>
    /// <param name="format">The format of the message</param>
    /// <param name="parameter">The parameters to use in the format</param>
    void LogError(string format, params string[] parameter) => Log(LogLevel.Error, format, parameter);

    /// <summary>
    /// Log data on trace level
    /// </summary>
    /// <param name="format">The format of the message</param>
    /// <param name="parameter">The parameters to use in the format</param>
    void LogFatal(string format, params string[] parameter) => Log(LogLevel.Fatal, format, parameter);

    /// <summary>
    /// Get all the logging files for the logging service
    /// </summary>
    /// <returns>A list with all the logging files</returns>
    IEnumerable<LoggingFileModel> GetLoggingFiles();
}
