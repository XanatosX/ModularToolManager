using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONSettings
{
    public class SettingsNode
    {
        private String _name;
        public string Name => _name;

        readonly List<KeyValue> _settings;
        public List<KeyValue> Settings => _settings;

        public SettingsNode()
        {
            _settings = new List<KeyValue>();
        }

        public SettingsNode(string name)
        {
            _settings = new List<KeyValue>();
            _name = name;
        }

        private void AddKeyValue(string key, object Value, SettingsType valueType = SettingsType.String)
        {
            if (KeyContained(key))
            {
                foreach (KeyValue pair in Settings)
                {
                    if (pair.Key == key)
                    {
                        pair.Value = Value.ToString();
                        pair.ValueType = valueType;
                    }
                }
            }
            else
            {
                Settings.Add(new KeyValue
                {
                    Key = key,
                    Value = Value.ToString(),
                    ValueType = valueType,
                });
            }
        }

        public bool AddOrChangeKeyValue(string key, object Value)
        {
            SettingsType type = SettingsType.String;
            TypeCode code = Type.GetTypeCode(Value.GetType());
            switch (code)
            {
                case TypeCode.Boolean:
                    type = SettingsType.Bool;
                    break;
                case TypeCode.Int32:
                    type = SettingsType.Int;
                    break;
                case TypeCode.Single:
                case TypeCode.Decimal:
                    type = SettingsType.Float;
                    break;
                default:
                    type = SettingsType.String;
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
                foreach (KeyValue pair in Settings)
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

            foreach (KeyValue pair in Settings)
            {
                if (pair.Key == key)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
