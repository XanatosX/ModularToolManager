using ModularToolManagerModel.Data;
using ModularToolManagerModel.Data.Enum;
using ModularToolManagerModel.Services.Logging;
using NLog.Config;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ModularToolManager.Services.Logging;
public class NLogFileService : ILogFileService
{
    /// <summary>
    /// All the logging files with the log level
    /// </summary>
    private readonly IEnumerable<LoggingFileModel> loggingFileModels;

    public NLogFileService(LoggingConfiguration configuration)
    {
        CreateLoggingFileList(configuration);
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
    /// Map log level of model to nlog log level
    /// </summary>
    /// <param name="logLevel">The log level to map</param>
    /// <returns>The mapped log level</returns>
    private LogLevel GetModelLogLevel(NLog.LogLevel logLevel)
    {
        LogLevel returnLevel = LogLevel.Unknown;
        if (logLevel == NLog.LogLevel.Trace)
        {
            returnLevel = LogLevel.Trace;
        }
        if (logLevel == NLog.LogLevel.Debug)
        {
            returnLevel = LogLevel.Debug;
        }
        if (logLevel == NLog.LogLevel.Info)
        {
            returnLevel = LogLevel.Information;
        }
        if (logLevel == NLog.LogLevel.Warn)
        {
            returnLevel = LogLevel.Warning;
        }
        if (logLevel == NLog.LogLevel.Error)
        {
            returnLevel = LogLevel.Error;
        }
        if (logLevel == NLog.LogLevel.Fatal)
        {
            returnLevel = LogLevel.Fatal;
        }
        return returnLevel;
    }

    /// <inheritdoc/>
    public IEnumerable<LoggingFileModel> GetLoggingFiles()
    {
        return loggingFileModels.ToList();
    }
}
