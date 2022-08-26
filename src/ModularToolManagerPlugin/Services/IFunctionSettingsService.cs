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

    IEnumerable<SettingAttribute> GetPluginSettings<T>() where T : IFunctionPlugin => GetPluginSettings(typeof(T));

    T GetSettingValue<T>(SettingAttribute settingAttribute, IFunctionPlugin plugin);

    bool SetSettingValue<T>(SettingAttribute attribute, IFunctionPlugin plugin, T value);
}
