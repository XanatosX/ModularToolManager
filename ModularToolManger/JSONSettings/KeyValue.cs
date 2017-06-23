using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONSettings
{
    public enum SettingsType
    {
        String,
        Bool,
        Int,
        Float,
        Error
    }

    public class KeyValue
    {
        public string Key;
        public string Value;
        public SettingsType ValueType;
    }
}
