using System.IO;

namespace ModularToolManager.Services.IO
{
    /// <summary>
    /// Service to get paths for this application
    /// </summary>
    internal interface IPathService
    {
        /// <summary>
        /// Get the path to the application itself as a string
        /// </summary>
        /// <returns>The path to the application or empty string if not set</returns>
        public string GetApplicationPathString() => GetApplicationPath()?.FullName ?? string.Empty;

        /// <summary>
        /// Get the path to the application itself
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
}
