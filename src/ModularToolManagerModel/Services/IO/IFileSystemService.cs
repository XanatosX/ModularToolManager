namespace ModularToolManagerModel.Services.IO;

/// <summary>
/// Interface for file system interactions
/// </summary>
public interface IFileSystemService
{
    /// <summary>
    /// Write data to the disc
    /// </summary>
    /// <param name="targetFile">The target file to write the data to</param>
    /// <returns>A writer to write data to</returns>
    StreamWriter? GetWriteStream(FileInfo targetFile);

    /// <summary>
    /// Write data to the disc
    /// </summary>
    /// <param name="targetFile">The target file to write the data to</param>
    /// <returns>A writer to write data to</returns>
    StreamWriter? GetWriteStream(string targetFile);

    /// <summary>
    /// Read data from a file
    /// </summary>
    /// <param name="sourceFile">The source file to get the data from</param>
    /// <returns>A stream reader to read the data from</returns>
    StreamReader? GetReadStream(FileInfo sourceFile);

    /// <summary>
    /// Read data from a file
    /// </summary>
    /// <param name="sourceFile">The source file to get the data from</param>
    /// <returns>A stream reader to read the data from</returns>
    StreamReader? GetReadStream(string sourceFile);
}
