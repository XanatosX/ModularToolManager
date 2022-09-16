using Microsoft.Extensions.Logging;

namespace ModularToolManagerModel.Services.IO;

/// <summary>
/// Service to get file stream for reading and writing
/// </summary>
public class FileSystemService : IFileSystemService
{
    /// <summary>
    /// The logger service to use
    /// </summary>
    private readonly ILogger<FileSystemService> logger;

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    /// <param name="logger">The logger to use</param>
    public FileSystemService(ILogger<FileSystemService> logger)
    {
        this.logger = logger;
    }

    /// <inheritdoc/>
    public StreamReader? GetReadStream(FileInfo sourceFile)
    {
        if (!sourceFile.Exists)
        {
            logger?.LogWarning($"The file to read {sourceFile.FullName} foes not exist");
            return null;
        }
        var fileStream = new FileStream(sourceFile.FullName, FileMode.Open, FileAccess.Read);
        return new StreamReader(fileStream);
    }

    /// <inheritdoc/>
    public StreamReader? GetReadStream(string sourceFile)
    {
        var info = new FileInfo(sourceFile);
        return info is null ? null : GetReadStream(info);
    }

    /// <inheritdoc/>
    public StreamWriter? GetWriteStream(FileInfo targetFile)
    {
        DirectoryInfo? directory = targetFile.Directory;
        if (directory is null)
        {
            logger?.LogWarning($"The folder {targetFile.DirectoryName} of the file to save seems to be invalid!");
            return null;
        }
        if (!directory.Exists)
        {
            directory.Create();
        }
        var fileStream = new FileStream(targetFile.FullName, FileMode.Create, FileAccess.Write);
        return new StreamWriter(fileStream);
    }

    /// <inheritdoc/>
    public StreamWriter? GetWriteStream(string targetFile)
    {
        var info = new FileInfo(targetFile);
        return info is null ? null : GetWriteStream(info);
    }
}
