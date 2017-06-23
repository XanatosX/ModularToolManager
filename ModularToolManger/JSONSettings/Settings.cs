using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JSONSettings
{
    public class Settings
    {
        private SettingJSON _settings;

        public Settings()
        {
            _settings = new SettingJSON();
        }

        public void AddNewField(string Name)
        {
            _settings.AddNew(Name);
        }

        public void AddKeyValue(string Name, string Key, object Value)
        {
            _settings.AddValue(Name, Key, Value);
        }

        public void Save()
        {
            string settings = JsonConvert.SerializeObject(_settings);
        }
    }
}
