using System;
using System.IO;
using System.Reflection;

namespace ModularToolManager.Services.IO;

/// <summary>
/// Path service to provice basic path information
/// </summary>
internal class PathService : IPathService
{
    /// <inheritdoc/>
    public DirectoryInfo? GetApplicationPath()
    {
        FileInfo appLocation = new FileInfo(Assembly.GetExecutingAssembly().Location);
        return appLocation.Directory;
    }

    /// <inheritdoc/>
    public FileInfo? GetPluginPath()
    {
        FileInfo executable = new FileInfo(Assembly.GetExecutingAssembly().Location);
        string pluginPath = Path.Combine(executable.DirectoryName ?? Path.GetTempPath(), "plugins");
        return new FileInfo(pluginPath);
    }

    /// <inheritdoc/>
    public FileInfo? GetSettingsFilePath()
    {
        return new FileInfo(Path.Combine(GetSettingsFolderPath()?.FullName ?? string.Empty, Properties.Properties.SettingsFile));
    }

    /// <inheritdoc/>
    public DirectoryInfo? GetSettingsFolderPath()
    {
        DirectoryInfo folderPath = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Properties.Properties.ApplicationName.Replace(" ", string.Empty)));
        return folderPath;
    }
}
