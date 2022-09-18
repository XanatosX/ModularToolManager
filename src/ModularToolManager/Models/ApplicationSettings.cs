using System.Text.Json.Serialization;

namespace ModularToolManager.Models;
public class ApplicationSettings
{
    [JsonPropertyName("always_on_top")]
    public bool AlwaysOnTop { get; set; }

    [JsonPropertyName("start_minimized")]
    public bool StartMinimized { get; set; }

    [JsonPropertyName("show_in_taskbar")]
    public bool ShowInTaskbar { get; set; }
}
