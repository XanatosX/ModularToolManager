using ModularToolManagerPlugin.Attributes;
using ModularToolManagerPlugin.Enums;
using ModularToolManagerPlugin.Models;
using ModularToolManagerPlugin.Plugin;
using System.Reflection;

namespace ModularToolManagerPlugin.Services;

/// <summary>
/// Class to get and set settings for plugins via reflection
/// </summary>
public class FunctionSettingService : IFunctionSettingsService
{
    /// <summary>
    /// Service used to load translations from the plugin
    /// </summary>
    private readonly IPluginTranslationService pluginTranslationService;

    /// <summary>
    /// Create a new instance of this service
    /// </summary>
    /// <param name="pluginTranslationService">The plugin translation service to use</param>
    public FunctionSettingService(IPluginTranslationService pluginTranslationService)
    {
        this.pluginTranslationService = pluginTranslationService;
    }

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

    /// <summary>
    /// Get the setting model for a given attribute for a specific plugin
    /// </summary>
    /// <param name="attribute">The attribute to get the setting model for</param>
    /// <param name="plugin">The plugin to create the setting model from</param>
    /// <returns>A useable setting model</returns>
    private SettingModel GetSettingModel(SettingAttribute attribute, IFunctionPlugin plugin)
    {
        Assembly? assembly = Assembly.GetAssembly(plugin.GetType());
        string? translation = null;
        if (assembly is not null)
        {
            translation = pluginTranslationService.GetTranslationByKey(assembly, attribute.Key);
        }
        SettingModel returnModel = new SettingModel(GetSettingsData(attribute, plugin))
        {
            DisplayName = translation ?? attribute.Key,
            Key = attribute.Key,
            Type = attribute.SettingType
        };
        return returnModel;
    }

    /// <summary>
    /// Get the settings data as object from a given plugin
    /// </summary>
    /// <param name="attribute">The plugin attribute to get the data from</param>
    /// <param name="plugin">The plugin to read the data from</param>
    /// <returns>A object if something was found otherwise null</returns>
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

    /// <inheritdoc/>
    public IEnumerable<SettingModel> GetPluginSettingsValues(IFunctionPlugin plugin, bool globalOnly)
    {
        IFunctionSettingsService local = this as IFunctionSettingsService;
        return local.GetPluginSettings(plugin).Where(plugin => globalOnly ? true : plugin.GlobalOnly == false).Select(settings => GetSettingModel(settings, plugin));
    }
}
