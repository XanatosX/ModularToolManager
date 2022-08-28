using System.IO;

namespace ModularToolManager.Services.IO
{
    internal interface IPathService
    {
        string GetApplicationPathString();

        DirectoryInfo? GetApplicationPath();

        string GetSettingsFolderPathString();

        DirectoryInfo? GetSettingsFolderPath();

        string GetSettingsFilePathString();

        FileInfo? GetSettingsFilePath();

        string GetPluginPathString();

        FileInfo? GetPluginPath();
    }
}
