using ModularToolManagerModel.Data;
using ModularToolManagerModel.Services.IO;
using NLog;
using NLog.Config;
using NLog.Targets;
using System.Data;

namespace ModularToolManagerModel.Services.Logging;

/// <summary>
/// Nlogger warpper service
/// </summary>
public class NLogLoggingWrapperService : ILoggingService
{
    /// <summary>
    /// The real logger to use
    /// </summary>
    private readonly Logger logger;

    private readonly IEnumerable<LoggingFileModel> loggingFileModels;

    /// <summary>
    /// Create a new instance of this class the given configuration or if null set the default one
    /// </summary>
    /// <param name="configuration">The configuration to use</param>
    /// <param name="pathService">The path service to use for the default configuration</param>
    public NLogLoggingWrapperService(LoggingConfiguration? configuration, IPathService? pathService)
    {
        LogManager.Configuration = configuration ?? CreateLoggingConfiguration(pathService);
        logger = LogManager.GetLogger("Modular Tool Manager");
        loggingFileModels = CreateLoggingFileList(LogManager.Configuration);
    }

    /// <summary>
    /// Create a new instance of this class with a default log configuration
    /// </summary>
    /// <param name="pathService">The path service to use</param>
    public NLogLoggingWrapperService(IPathService? pathService)
    {
        LogManager.Configuration = CreateLoggingConfiguration(pathService);
        logger = LogManager.GetLogger("Modular Tool Manager");
        loggingFileModels = CreateLoggingFileList(LogManager.Configuration);
    }

    /// <summary>
    /// Create the logging file list for returning
    /// </summary>
    /// <param name="configuration">The configuration to use for generating the list</param>
    /// <returns>A list with the logging file models</returns>
    private IEnumerable<LoggingFileModel> CreateLoggingFileList(LoggingConfiguration configuration)
    {
        return configuration.LoggingRules.Where(rule => rule.Targets.Any(target => target is FileTarget))
                                         .SelectMany(rule => CreateFileModelFromRule(rule));
    }

    /// <summary>
    /// Convert a rule to a logging file model
    /// </summary>
    /// <param name="rule">The rule to convert</param>
    /// <returns>A list with all the valid logging file models</returns>
    private IEnumerable<LoggingFileModel> CreateFileModelFromRule(LoggingRule rule)
    {
        var levels = rule.Levels.Select(level => GetModelLogLevel(level)).ToArray();
        return rule.Targets.OfType<FileTarget>()
                           .Select(target => new LoggingFileModel(levels, target.FileName.ToString() ?? String.Empty))
                           .Where(loggingModel => loggingModel.Path != null && loggingModel.LogLevels.Length > 0);
    }

    /// <summary>
    /// Create a new default logging configuration
    /// </summary>6
    /// <returns></returns>
    private LoggingConfiguration CreateLoggingConfiguration(IPathService? pathService)
    {
        var returnConfiguration = new LoggingConfiguration();
        string basePath = pathService?.GetSettingsFolderPathString() ?? Path.GetTempPath();
        string logFolder = Path.Combine(basePath, "logs");
        returnConfiguration.AddTarget(new TraceTarget("trace-log"));
        returnConfiguration.AddTarget(new FileTarget("default-file-log")
        {
            FileName = Path.Combine(logFolder, "application.log"),
            ArchiveAboveSize = 5242880,
            MaxArchiveFiles = 10
        });
        returnConfiguration.AddTarget(new FileTarget("error-log")
        {
            FileName = Path.Combine(logFolder, "error.log"),
            ArchiveAboveSize = 5242880,
            MaxArchiveFiles = 5
        });

        returnConfiguration.AddRule(NLog.LogLevel.Trace, NLog.LogLevel.Debug, "trace-log");
        returnConfiguration.AddRule(NLog.LogLevel.Info, NLog.LogLevel.Warn, "default-file-log");
        returnConfiguration.AddRule(NLog.LogLevel.Error, NLog.LogLevel.Fatal, "error-log");
        return returnConfiguration;
    }


    /// <inheritdoc/>
    public void Log(Data.Enum.LogLevel logLevel, string format, params string[] parameter)
    {
        logger.Log(GetNLogLogLevel(logLevel), string.Format(format, parameter));
    }

    /// <summary>
    /// Map log level of model to nlog log level
    /// </summary>
    /// <param name="logLevel">The log level to map</param>
    /// <returns>The mapped log level</returns>
    private NLog.LogLevel GetNLogLogLevel(Data.Enum.LogLevel logLevel)
    {
        return logLevel switch
        {
            Data.Enum.LogLevel.Trace => NLog.LogLevel.Trace,
            Data.Enum.LogLevel.Debug => NLog.LogLevel.Debug,
            Data.Enum.LogLevel.Information => NLog.LogLevel.Info,
            Data.Enum.LogLevel.Warning => NLog.LogLevel.Warn,
            Data.Enum.LogLevel.Error => NLog.LogLevel.Error,
            Data.Enum.LogLevel.Fatal => NLog.LogLevel.Fatal,
            _ => NLog.LogLevel.Off
        };
    }

    /// <summary>
    /// Map log level of model to nlog log level
    /// </summary>
    /// <param name="logLevel">The log level to map</param>
    /// <returns>The mapped log level</returns>
    private Data.Enum.LogLevel GetModelLogLevel(NLog.LogLevel logLevel)
    {
        Data.Enum.LogLevel returnLevel = Data.Enum.LogLevel.Unknown;
        if (logLevel == NLog.LogLevel.Trace)
        {
            returnLevel = Data.Enum.LogLevel.Trace;
        }
        if (logLevel == NLog.LogLevel.Debug)
        {
            returnLevel = Data.Enum.LogLevel.Debug;
        }
        if (logLevel == NLog.LogLevel.Info)
        {
            returnLevel = Data.Enum.LogLevel.Information;
        }
        if (logLevel == NLog.LogLevel.Warn)
        {
            returnLevel = Data.Enum.LogLevel.Warning;
        }
        if (logLevel == NLog.LogLevel.Error)
        {
            returnLevel = Data.Enum.LogLevel.Error;
        }
        if (logLevel == NLog.LogLevel.Fatal)
        {
            returnLevel = Data.Enum.LogLevel.Fatal;
        }
        return returnLevel;
    }

    /// <inheritdoc/>
    public IEnumerable<LoggingFileModel> GetLoggingFiles()
    {
        return loggingFileModels.ToList();
    }
}
