using Avalonia;
using System;
using System.IO;
using System.Reflection;

namespace ModularToolManager.Services.IO
{
    internal class PathService : IPathService
    {
        /// <inheritdoc/>
        public DirectoryInfo? GetApplicationPath()
        {
            FileInfo appLocation = new FileInfo(Assembly.GetExecutingAssembly().Location);
            return appLocation.Directory;
        }

        /// <inheritdoc/>
        public string GetApplicationPathString()
        {
            return GetApplicationPath()?.FullName ?? string.Empty;
        }

        /// <inheritdoc/>
        public FileInfo? GetPluginPath()
        {
            FileInfo executable = new FileInfo(Assembly.GetExecutingAssembly().Location);
            string pluginPath = Path.Combine(executable.DirectoryName ?? Path.GetTempPath(), "plugins");
            return new FileInfo(pluginPath);
        }

        /// <inheritdoc/>
        public string GetPluginPathString()
        {
            return GetPluginPath()?.FullName ?? string.Empty;
        }

        /// <inheritdoc/>
        public FileInfo? GetSettingsFilePath()
        {
            return new FileInfo(Path.Combine(GetSettingsFolderPathString(), Properties.Properties.SettingsFile));
        }

        /// <inheritdoc/>
        public string GetSettingsFilePathString()
        {
            return GetSettingsFilePath()?.FullName ?? String.Empty;
        }

        /// <inheritdoc/>
        public DirectoryInfo? GetSettingsFolderPath()
        {
            DirectoryInfo folderPath = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Properties.Properties.ApplicationName.Replace(" ", string.Empty)));
            return folderPath;
        }

        /// <inheritdoc/>
        public string GetSettingsFolderPathString()
        {
            return GetSettingsFolderPath()?.FullName ?? string.Empty;
        }
    }
}
