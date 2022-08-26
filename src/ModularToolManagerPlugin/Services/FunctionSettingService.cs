﻿using ModularToolManagerPlugin.Attributes;
using ModularToolManagerPlugin.Plugin;
using System.Reflection;

namespace ModularToolManagerPlugin.Services
{
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
                                       .DistinctBy(attribute => attribute.Key);
        }

        /// <inheritdoc/>
        public T GetSettingValue<T>(SettingAttribute settingAttribute, IFunctionPlugin plugin)
        {
            PropertyInfo setting = plugin.GetType().GetProperties()
                                                  .FirstOrDefault(property => IsSearchedSettingValue(property, settingAttribute));
            if (setting?.PropertyType == typeof(T) && setting.CanRead)
            {
                return (T)setting?.GetValue(plugin);
            }
            return default(T);
        }

        /// <inheritdoc/>
        public bool SetSettingValue<T>(SettingAttribute attribute, IFunctionPlugin plugin, T value)
        {
            PropertyInfo setting = plugin.GetType().GetProperties()
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
}
