using ModularToolManagerPlugin.Attributes;
using ModularToolManagerPlugin.Enums;

namespace ModularToolManagerPlugin.Services
{
    /// <summary>
    /// Interface to log data in the application
    /// </summary>
    [PluginInjectable]
    public interface IPluginLoggerService
    {
        /// <summary>
        /// Log data to the application logger
        /// </summary>
        /// <param name="logSeverity">The severity of the log entry</param>
        /// <param name="format">The format of the message</param>
        /// <param name="parameter">The parameters to use in the format</param>
        void Log(LogSeverity logSeverity, string format, params string[] parameter);

        /// <summary>
        /// Log data to the application logger on debug level
        /// </summary>
        /// <param name="format">The format of the message</param>
        /// <param name="parameter">The parameters to use in the format</param>
        void LogDebug(string format, params string[] parameter);

        /// <summary>
        /// Log data to the application logger on information level
        /// </summary>
        /// <param name="format">The format of the message</param>
        /// <param name="parameter">The parameters to use in the format</param>
        void LogInformation(string format, params string[] parameter);

        /// <summary>
        /// Log data to the application logger on warning level
        /// </summary>
        /// <param name="format">The format of the message</param>
        /// <param name="parameter">The parameters to use in the format</param>
        void LogWarning(string format, params string[] parameter);

        /// <summary>
        /// Log data to the application logger on error level
        /// </summary>
        /// <param name="format">The format of the message</param>
        /// <param name="parameter">The parameters to use in the format</param>
        void LogError(string format, params string[] parameter);

        /// <summary>
        /// Log data to the application logger on fatal error level
        /// </summary>
        /// <param name="format">The format of the message</param>
        /// <param name="parameter">The parameters to use in the format</param>
        void LogFatalError(string format, params string[] parameter);
    }
}
