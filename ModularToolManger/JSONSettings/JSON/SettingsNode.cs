using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONSettings
{
    public class SettingsNode
    {
        readonly string _name;
        public string Name => _name;

        readonly List<KeyValue> _settings;
        public List<KeyValue> Settings => _settings;

        public SettingsNode(string name)
        {
            _settings = new List<KeyValue>();
            _name = name;
        }

        private void AddKeyValue(string key, object Value, SettingsType valueType = SettingsType.String)
        {
            KeyValue pair = GetKeyValue(key);
            if (pair == null)
            {
                Settings.Add(new KeyValue
                {
                    Key = key,
                    Value = Value.ToString(),
                    ValueType = valueType,
                });
                return;
            }

            pair.Value = Value.ToString();
            pair.ValueType = valueType;
        }

        public bool AddOrChangeKeyValue(string key, object Value)
        {
            SettingsType type = GetValueType(Value);
            if (type != SettingsType.Error)
            {
                AddKeyValue(key, Value, type);
                return true;
            }
                
            return false;
        }

        private SettingsType GetValueType(object valueToCheck)
        {
            TypeCode code = Type.GetTypeCode(valueToCheck.GetType());
            SettingsType returnType = SettingsType.Error;
            switch (code)
            {
                case TypeCode.Boolean:
                    returnType = SettingsType.Bool;
                    break;
                case TypeCode.Int32:
                    returnType = SettingsType.Int;
                    break;
                case TypeCode.Single:
                case TypeCode.Decimal:
                    returnType = SettingsType.Float;
                    break;
                default:
                    returnType = SettingsType.String;
                    break;
            }
            return returnType;
        }

        public string GetKeyValue(string key, out SettingsType type)
        {
            type = SettingsType.Error;
            KeyValue pair = GetKeyValue(key);
           
            if (pair == null)
            {
                return String.Empty;
            }

            type = pair.ValueType;
            return pair.Value;
        }

        private KeyValue GetKeyValue(string key)
        {
            foreach (KeyValue pair in Settings)
            {
                if (pair.Key == key)
                {
                    return pair;
                }
            }
            return null;
        }
    }
}
