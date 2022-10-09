using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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

    /// <summary>
    /// The current language to use
    /// </summary>
    [JsonPropertyName("language")]
    public CultureInfo? CurrentLanguage { get; set; }

    [JsonPropertyName("plugin_settings")]
    public List<PluginSettings> PluginSettings { get; set; }

    public ApplicationSettings()
    {
        PluginSettings = new();
    }

    public void AddPluginSettings(PluginSettings pluginSettings)
    {

        if (PluginSettings.Any(setting => setting.Plugin?.GetType() == pluginSettings.Plugin?.GetType()))
        {
            PluginSettings? settings = PluginSettings.FirstOrDefault(data => data.Plugin?.GetType() == pluginSettings.Plugin?.GetType());
            if (settings is not null)
            {
                settings = pluginSettings;
                return;
            }
        }
        PluginSettings.Add(pluginSettings);
    }
}
