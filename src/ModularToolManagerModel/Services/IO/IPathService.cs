using System.IO;

namespace ModularToolManagerModel.Services.IO;

/// <summary>
/// Service to get paths for this application
/// </summary>
public interface IPathService
{
    /// <summary>
    /// Get the path to the application executable as string
    /// </summary>
    /// <returns>The path to the application executable or an empty string if nothing found</returns>
    string GetApplicationExecutableString() => GetApplicationExecutable()?.FullName ?? string.Empty;

    /// <summary>
    /// Get the path to the application executable
    /// </summary>
    /// <returns>The path to the application executable or null if nothing found</returns>
    FileInfo? GetApplicationExecutable();

    /// <summary>
    /// Get the path to the application folder as a string
    /// </summary>
    /// <returns>The path to the application or empty string if not set</returns>
    public string GetApplicationPathString() => GetApplicationPath()?.FullName ?? string.Empty;

    /// <summary>
    /// Get the path to the application folder
    /// </summary>
    /// <returns>The path to the application or null if not set</returns>
    DirectoryInfo? GetApplicationPath();

    /// <summary>
    /// Get the path to the settings folder
    /// </summary>
    /// <returns>Get the path to the settings folder or an empty string if not set</returns>
    public string GetSettingsFolderPathString() => GetSettingsFolderPath()?.FullName ?? string.Empty;

    /// <summary>
    /// Get the path to the settings folder
    /// </summary>
    /// <returns>Get the path to the settings folder or null if not set</returns>
    DirectoryInfo? GetSettingsFolderPath();

    /// <summary>
    /// Get the path to the settings file
    /// </summary>
    /// <returns>The path to the settings file or an empty string if nothing set</returns>
    public string GetSettingsFilePathString() => GetSettingsFilePath()?.FullName ?? string.Empty;


    /// <summary>
    /// Get the path to the settings file
    /// </summary>
    /// <returns>The path to the settings file or null if nothing set</returns>
    FileInfo? GetSettingsFilePath();

    /// <summary>
    /// Get the path to the plugin path
    /// </summary>
    /// <returns>The path to the plugin path an empty string if nothing set</returns>
    public string GetPluginPathString() => GetPluginPath()?.FullName ?? string.Empty;

    /// <summary>
    /// Get the path to the plugin path
    /// </summary>
    /// <returns>The path to the plugin path or null if nothing set</returns>
    FileInfo? GetPluginPath();
}
