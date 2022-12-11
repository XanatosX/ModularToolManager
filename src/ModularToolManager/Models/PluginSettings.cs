using ModularToolManagerPlugin.Models;
using ModularToolManagerPlugin.Plugin;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ModularToolManager.Models;

/// <summary>
/// The container to save plugin settings for a specific plugin
/// </summary>
public class PluginSettings
{
    /// <summary>
    /// The plugin to persist the settings for
    /// </summary>
    [JsonPropertyName("plugin")]
    public IFunctionPlugin? Plugin { get; set; }

    /// <summary>
    /// The settings to persist for the plugin
    /// </summary>
    [JsonPropertyName("settings")]
    public List<PersistantPluginSetting>? Settings { get; set; }
}
