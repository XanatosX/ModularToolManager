using System.Text.Json.Serialization;

namespace ModularToolManager.Models;

/// <summary>
/// Class to save application settings
/// </summary>
public class ApplicationSettings
{
    /// <summary>
    /// Should the application be always on top of all others
    /// </summary>
    [JsonPropertyName("always_on_top")]
    public bool AlwaysOnTop { get; set; }

    /// <summary>
    /// Should the application be started minimized
    /// </summary>
    [JsonPropertyName("start_minimized")]
    public bool StartMinimized { get; set; }

    /// <summary>
    /// Show the application in task bar
    /// </summary>
    [JsonPropertyName("show_in_taskbar")]
    public bool ShowInTaskbar { get; set; }
}
