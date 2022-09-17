
using ModularToolManagerModel.Data.Enum;

namespace ModularToolManagerModel.Data;

/// <summary>
/// Record containing information about a single log file
/// </summary>
/// <param name="LogLevels">The log levels in the file</param>
/// <param name="Path">The path of the file</param>
public record LoggingFileModel(LogLevel[] LogLevels, string Path);
