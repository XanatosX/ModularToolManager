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
    [JsonPropertyName("selected_theme")]
    public int SelectedThemeId { get; set; }

    /// <summary>
    /// Should the application be always on top of all others
    /// </summary>
    [JsonPropertyName("always_on_top")]
    public bool AlwaysOnTop { get; set; }

    /// <summary>
    /// Should the application be minimized if a function is getting pressed
    /// </summary>
    [JsonPropertyName("close_on_function_execute")]
    public bool MinimizeOnFunctionExecute { get; set; }

    /// <summary>
    /// Should the search for a function be cleared if a function was executed successfully
    /// </summary>
    [JsonPropertyName("clear_search_after_function_execute")]
    public bool ClearSearchAfterFunctionExecute { get; set; }

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

    /// <summary>
    /// The settings for the plugins
    /// </summary>
    [JsonPropertyName("plugin_settings")]
    public List<PluginSettings> PluginSettings { get; set; }

    /// <summary>
    /// Setting for allowing to use autocomplete on the search
    /// </summary>
    [JsonPropertyName("search_autocomplete")]
    public bool EnableAutocompleteForFunctionSearch { get; set; }

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    public ApplicationSettings()
    {
        PluginSettings = new();
    }

    /// <summary>
    /// Add new plugin settings
    /// </summary>
    /// <param name="pluginSettings">The new plugin settings to add</param>
    public void AddPluginSettings(PluginSettings pluginSettings)
    {
        if (PluginSettings.Any(setting => setting.Plugin?.GetType() == pluginSettings.Plugin?.GetType()))
        {
            PluginSettings.RemoveAll(entry => entry.Plugin?.GetType() == pluginSettings.Plugin?.GetType());
        }
        PluginSettings.Add(pluginSettings);
    }
}
