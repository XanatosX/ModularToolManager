using System.Diagnostics;

namespace DefaultPlugins.ProcessStartStrategies;

/// <summary>
/// Starter strategy for windows
/// </summary>
internal sealed class WindowsStarterStrategy : IProcessStartStrategy
{
    /// <inheritdoc/>
    public ProcessStartInfo? GetStartInfo(string parameters, string path)
    {
        return new ProcessStartInfo
        {
            FileName = path,
            Arguments = parameters
        };
    }
}