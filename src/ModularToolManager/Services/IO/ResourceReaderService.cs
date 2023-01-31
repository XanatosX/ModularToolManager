using Microsoft.Extensions.Logging;
using System.IO;
using System.Reflection;

namespace ModularToolManager.Services.IO;

/// <summary>
/// Service to read resource files from the project
/// </summary>
internal class ResourceReaderService
{
    /// <summary>
    /// The base namespace for resource files
    /// </summary>
    private const string BASE_NAMESPACE = "ModularToolManager.Resources";

    /// <summary>
    /// The logger to use for writing errors
    /// </summary>
    private readonly ILogger<ResourceReaderService> logger;

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    /// <param name="logger">The logger to use</param>
    public ResourceReaderService(ILogger<ResourceReaderService> logger)
    {
        this.logger = logger;
    }

    /// <summary>
    /// Get the stream to the resource by its file name
    /// </summary>
    /// <param name="fileName">The file name to use</param>
    /// <returns>The stream to the file, Stream.Null if nothing was found or null</returns>
    public Stream? GetResourceStream(string fileName)
    {
        return Assembly.GetExecutingAssembly().GetManifestResourceStream(GetResourcePath(fileName)) ?? Stream.Null;
    }

    /// <summary>
    /// Get the content of the resource to load
    /// </summary>
    /// <param name="fileName">The file name to load</param>
    /// <returns>The string with the content or null if nothing was found</returns>
    public string? GetResourceData(string fileName)
    {
        string? returnString = null;
        var stream = GetResourceStream(fileName);
        if (stream is null || stream == Stream.Null)
        {
            logger.LogError($"Could not get stream for resource with name {fileName}");
            return returnString;
        }
        using (StreamReader reader = new StreamReader(stream))
        {
            returnString = reader.ReadToEnd();
        }
        return returnString;
    }

    /// <summary>
    /// Get the path of the resource by the file name
    /// </summary>
    /// <param name="fileName">The file name to get the resource from</param>
    /// <returns>The path to the resource file</returns>
    private string GetResourcePath(string fileName)
    {
        return $"{BASE_NAMESPACE}.{fileName}";
    }
}
