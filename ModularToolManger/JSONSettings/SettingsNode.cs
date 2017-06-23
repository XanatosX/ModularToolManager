using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONSettings
{
    public class SettingsNode
    {
        public string Name;
        public List<KeyValue> _settings;

        public SettingsNode()
        {
            init();
        }

        public SettingsNode(string name)
        {
            init();
            Name = name;
        }

        private void init()
        {
            _settings = new List<KeyValue>();
        }

        private bool AddKeyValue(string key, object Value, SettingsType valueType = SettingsType.String)
        {
            if (KeyContained(key))
                return false;
            _settings.Add(new KeyValue()
            {
                Key = key,
                Value = Value.ToString(),
                ValueType = valueType,
            });

            return true;
        }
        public bool AddKeyValue(string key, object Value)
        {
            SettingsType type = SettingsType.String;
            switch (Type.GetTypeCode(Value.GetType()))
            {
                case TypeCode.Boolean:
                    type = SettingsType.Bool;
                    break;
                case TypeCode.Int32:
                    type = SettingsType.Int;
                    break;
                case TypeCode.Decimal:
                    type = SettingsType.Float;
                    break;
                default:
                    break;
            }
            if (type != SettingsType.Error)
            {
                AddKeyValue(key, Value, type);
                return true;
            }
                
            return false;
              
        }

        public string GetKeyValue(string key, out SettingsType type)
        {
            if (KeyContained(key))
            {
                foreach (KeyValue pair in _settings)
                {
                    if (pair.Key == key)
                    {
                        type = pair.ValueType;
                        return pair.Value;
                    }
                }
                
            }
            type = SettingsType.Error;
            return String.Empty;
        }

        private bool KeyContained(string key)
        {

            foreach (KeyValue pair in _settings)
            {
                if (pair.Key == key)
                    return true;
            }
            return false;
        }

        internal void GetValue(string Name)
        {

        }
    }
}
