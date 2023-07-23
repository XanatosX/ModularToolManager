using System.Diagnostics;

namespace DefaultPlugins.ProcessStartStrategies;

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