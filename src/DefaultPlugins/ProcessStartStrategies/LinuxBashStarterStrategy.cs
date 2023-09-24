using System.Diagnostics;

namespace DefaultPlugins.ProcessStartStrategies;

/// <summary>
/// Strategy to start a linux bash script
/// </summary>
class LinuxBashStarterStrategy : IProcessStartStrategy
{
    /// <inheritdoc/>
    public ProcessStartInfo? GetStartInfo(string parameters, string path)
    {
        FileInfo info = new FileInfo(path);

        path = path.Replace("\"", "\\\"");
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = "/bin/bash",
            Arguments = $"-c \"{path} {parameters}\"",
            WorkingDirectory = info.DirectoryName
        };

        return startInfo;
    }
}