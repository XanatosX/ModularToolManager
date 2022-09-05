using ModularToolManagerModel.Data;

namespace ModularToolManagerModel.Services.Logging;

/// <summary>
/// Service interface to reviece log files
/// </summary>
public interface ILogFileService
{
    /// <summary>
    /// Get all the logging files for the logging service
    /// </summary>
    /// <returns>A list with all the logging files</returns>
    IEnumerable<LoggingFileModel> GetLoggingFiles();
}
