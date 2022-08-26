using ModularToolManagerPlugin.Enums;

namespace ModularToolManagerPlugin.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SettingAttribute : Attribute
    {
        public string Key { get; init; }
        public SettingType SettingType { get; init; }
        public bool GlobalOnly { get; init; }

        public SettingAttribute(string key, SettingType settingType)
        {
            Key = key;
            SettingType = settingType;
        }

        public SettingAttribute(string key, SettingType settingType, bool globalOnly) : this(key, settingType)
        {
            GlobalOnly = globalOnly;
        }
    }
}
