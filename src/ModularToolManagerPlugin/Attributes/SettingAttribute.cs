using ModularToolManagerPlugin.Enums;

namespace ModularToolManagerPlugin.Attributes;

/// <summary>
/// Attribute to define a plugin setting
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class SettingAttribute : Attribute
{
    /// <summary>
    /// They key of the setting used for saving
    /// </summary>
    public string Key { get; init; }

    /// <summary>
    /// The type of the setting to save
    /// </summary>
    public SettingType SettingType { get; init; }

    /// <summary>
    /// Can the setting be set per function or only on a global level
    /// </summary>
    public bool GlobalOnly { get; init; }

    /// <summary>
    /// Create a new instance of this attribute
    /// </summary>
    /// <param name="key">The key of the attribute</param>
    /// <param name="settingType">The setting type</param>
    public SettingAttribute(string key, SettingType settingType)
    {
        Key = key;
        SettingType = settingType;
    }

    /// <summary>
    /// Create a new instance of this attribute
    /// </summary>
    /// <param name="key">The key of the attribute</param>
    /// <param name="settingType">The setting type</param>
    /// <param name="globalOnly">Is the setting only globally available</param>
    public SettingAttribute(string key, SettingType settingType, bool globalOnly) : this(key, settingType)
    {
        GlobalOnly = globalOnly;
    }
}
