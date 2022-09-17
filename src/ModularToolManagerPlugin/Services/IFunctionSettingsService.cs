using ModularToolManagerPlugin.Attributes;
using ModularToolManagerPlugin.Plugin;

namespace ModularToolManagerPlugin.Services;

/// <summary>
/// Interface to define a service to get, set and list settings for a plugin
/// </summary>
public interface IFunctionSettingsService
{
    /// <summary>
    /// Get all the settings from the plugin
    /// </summary>
    /// <param name="type">The type of plugin to get</param>
    /// <returns>A list with all the setting attributes</returns>
    IEnumerable<SettingAttribute> GetPluginSettings(Type type);

    /// <summary>
    /// Get all the settings from a given plugin of type T
    /// </summary>
    /// <typeparam name="T">The plugin type to get the settings from</typeparam>
    /// <returns>A list with all the setting attributes</returns>
    IEnumerable<SettingAttribute> GetPluginSettings<T>() where T : IFunctionPlugin => GetPluginSettings(typeof(T));

    /// <summary>
    /// Get a single settings value from a given plugin
    /// </summary>
    /// <typeparam name="T">The type of setting to get</typeparam>
    /// <param name="settingAttribute">The setting attribute to use as a filter</param>
    /// <param name="plugin">The plugin to get the setting from</param>
    /// <returns>The setting or default if nothing could be recievend</returns>
    T? GetSettingValue<T>(SettingAttribute settingAttribute, IFunctionPlugin plugin);

    /// <summary>
    /// Set the value for a specific setting on the plugin
    /// </summary>
    /// <typeparam name="T">The type of value to set for the setting</typeparam>
    /// <param name="attribute">The filter to find the correct setting</param>
    /// <param name="plugin">The plugin to set the setting on</param>
    /// <param name="value">The value to set</param>
    /// <returns>True if setting was a success</returns>
    bool SetSettingValue<T>(SettingAttribute attribute, IFunctionPlugin plugin, T value);
}
