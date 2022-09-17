using ModularToolManagerModel.Services.IO;
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
    public FileInfo? GetApplicationExecutable()
    {
        return new FileInfo(Assembly.GetExecutingAssembly().Location);
    }

    /// <inheritdoc/>
    public DirectoryInfo? GetApplicationPath()
    {
        FileInfo? appLocation = GetApplicationExecutable();
        return appLocation?.Directory;
    }

    /// <inheritdoc/>
    public FileInfo? GetPluginPath()
    {
        FileInfo? executable = GetApplicationExecutable();
        string pluginPath = Path.Combine(executable?.DirectoryName ?? Path.GetTempPath(), "plugins");
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
