namespace ModularToolManagerPlugin.Models;

/// <summary>
/// File extension to use
/// </summary>
public struct FileExtension
{
    /// <summary>
    /// Display name for the file open dialog
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// File extension which is allowed
    /// </summary>
    public string Extension { get; init; }

    /// <summary>
    /// Create a new instance of the structure
    /// </summary>
    /// <param name="name">The name to display in the file open dialog</param>
    /// <param name="extension">The file extension to use</param>
    public FileExtension(string name, string extension)
    {
        Name = name;
        Extension = extension;
    }
}
