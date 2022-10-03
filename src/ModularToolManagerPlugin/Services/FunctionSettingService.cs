using ModularToolManagerPlugin.Attributes;
using ModularToolManagerPlugin.Enums;
using ModularToolManagerPlugin.Models;
using ModularToolManagerPlugin.Plugin;
using System.Linq;
using System.Reflection;

namespace ModularToolManagerPlugin.Services;

/// <summary>
/// Class to get and set settings for plugins via reflection
/// </summary>
public class FunctionSettingService : IFunctionSettingsService
{
    /// <inheritdoc/>
    public IEnumerable<SettingAttribute> GetPluginSettings(Type type)
    {
        if (!typeof(IFunctionPlugin).IsAssignableFrom(type))
        {
            return Enumerable.Empty<SettingAttribute>();
        }
        return type.GetProperties().Select(property => property.GetCustomAttribute<SettingAttribute>())
                                   .Where(attribute => attribute is not null)
                                   .DistinctBy(attribute => attribute!.Key)
                                   .OfType<SettingAttribute>();
    }

    public IEnumerable<SettingModel> GetPluginSettingsValues(IFunctionPlugin plugin)
    {
        IFunctionSettingsService local = this as IFunctionSettingsService;
        return local.GetPluginSettings(plugin).Select(settings => GetSettingModel(settings, plugin));
    }

    private SettingModel GetSettingModel(SettingAttribute attribute, IFunctionPlugin plugin)
    {
        return new SettingModel
        {
            DisplayName = attribute.Key,
            Key = attribute.Key,
            Type = attribute.SettingType,
            Value = GetSettingsData(attribute, plugin),
        };
    }

    private object? GetSettingsData(SettingAttribute attribute, IFunctionPlugin plugin)
    {
        return attribute.SettingType switch
        {
            SettingType.String => GetSettingValue<string>(attribute, plugin) as object,
            SettingType.Boolean => GetSettingValue<bool>(attribute, plugin) as object,
            SettingType.Int => GetSettingValue<int>(attribute, plugin) as object,
            SettingType.Float => GetSettingValue<float>(attribute, plugin) as object,
            _ => null
        };
    }

    /// <inheritdoc/>
    public T? GetSettingValue<T>(SettingAttribute settingAttribute, IFunctionPlugin plugin)
    {
        PropertyInfo? setting = plugin.GetType().GetProperties()
                                              .FirstOrDefault(property => IsSearchedSettingValue(property, settingAttribute));
        if (setting?.PropertyType == typeof(T) && setting.CanRead)
        {
            return (T?)setting?.GetValue(plugin);
        }
        return default(T);
    }

    /// <inheritdoc/>
    public bool SetSettingValue<T>(SettingAttribute attribute, IFunctionPlugin plugin, T value)
    {
        PropertyInfo? setting = plugin.GetType().GetProperties()
                                               .FirstOrDefault(property => IsSearchedSettingValue(property, attribute));
        if (setting?.PropertyType == typeof(T) && setting.CanWrite)
        {
            setting.SetValue(plugin, value);
            return true;
        }
        return false;

    }

    /// <summary>
    /// Method to check if the given property info is the searched setting value
    /// </summary>
    /// <param name="propertyInfo">The property info to check</param>
    /// <param name="searchedAttribute">The setting attribute to search</param>
    /// <returns>True if property info is the searched attribute</returns>
    private bool IsSearchedSettingValue(PropertyInfo propertyInfo, SettingAttribute searchedAttribute)
    {
        SettingAttribute? attribute = propertyInfo.GetCustomAttribute<SettingAttribute>();
        return attribute is not null && attribute.Key == searchedAttribute.Key;
    }


}
