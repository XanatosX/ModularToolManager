using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Reflection;
using System.IO;

namespace JSONSettings
{
    public class Settings
    {
        private SettingJSON _settings;
        private string _defaultApp;
        public string DefaultApp => _defaultApp;
        private string _saveFile;


        public Settings(string SettingsFile)
        {
            _settings = new SettingJSON();
            _saveFile = SettingsFile;
            Load();
        }

        public void AddNewField(string Name)
        {
            _settings.AddNew(Name);
            if (_settings.nodes.Count == 1)
                _defaultApp = Name;
        }

        public void AddOrChangeKeyValue(string Name, string Key, object Value)
        {
            _settings.AddValue(Name, Key, Value);
        }
        public void AddOrChangeKeyValue(string Key, object Value)
        {
            AddOrChangeKeyValue(_defaultApp, Key, Value);
        }

        public bool GetBoolValue(string Name, string key)
        {
            SettingsType type;
            bool returnBool = false; ;
            string value = _settings.GetValue(Name, key, out type);
            if (type == SettingsType.Bool)
            {
                bool.TryParse(value, out returnBool);
            }
            return returnBool;
        }
        public bool GetBoolValue(string key)
        {
            return GetBoolValue(_defaultApp, key);
        }

        public int GetIntValue(string Name, string key)
        {
            SettingsType type;
            int returnInt = -1;
            string value = _settings.GetValue(Name, key, out type);
            if (type == SettingsType.Int)
                int.TryParse(value, out returnInt);
            return returnInt;
        }
        public int GetIntValue(string key)
        {
            return GetIntValue(_defaultApp, key);
        }

        public float GetFloatValue(string Name, string key)
        {
            SettingsType type;
            float returnFloat = -1;
            string value = _settings.GetValue(Name, key, out type);
            if (type == SettingsType.Float)
                float.TryParse(value, out returnFloat);
            return returnFloat;
        }
        public float GetFloatValue(string key)
        {
            return GetFloatValue(_defaultApp, key);
        }

        public string GetValue(string Name, string key, out SettingsType type)
        {
            string returnString = _settings.GetValue(Name, key, out type);
            if (type == SettingsType.Error)
                return "";
            return returnString;
        }
        public string GetValue(string key, out SettingsType type)
        {
            return GetValue(_defaultApp, key, out type);
        }

        public string GetValue(string Name, string key)
        {
            SettingsType type;
            string returnString = _settings.GetValue(Name, key, out type);
            if (type == SettingsType.Error)
                return "";
            return returnString;
        }
        public string GetValue(string key)
        {
            return GetValue(_defaultApp, key);
        }




        public void Save(bool compressed = false )
        {
            Formatting format = Formatting.Indented;
            if (compressed)
                format = Formatting.None;
            FileInfo FI = new FileInfo(_saveFile);
            if (!Directory.Exists(FI.DirectoryName))
                Directory.CreateDirectory(FI.DirectoryName);
            string settings = JsonConvert.SerializeObject(_settings, format);
            using (StreamWriter writer = new StreamWriter(_saveFile))
            {
                writer.Write(settings);
            }
        }

        private void Load()
        {
            if (!File.Exists(_saveFile))
                return;
            using (StreamReader reader = new StreamReader(_saveFile))
            {
                string data = reader.ReadToEnd();
                try
                {
                    _settings = JsonConvert.DeserializeObject<SettingJSON>(data);
                    if (_settings.nodes.Count > 0)
                        _defaultApp = _settings.nodes[0].Name;
                }
                catch (Exception)
                {

                    return;
                }
            }
        }

        public void Clear()
        {
            for (int i = _settings.nodes.Count; i > 0; i--)
            {
                int Index = i - 1;
                _settings.nodes.RemoveAt(Index);
            }
            Cleanup();
        }

        public void Cleanup()
        {
            File.Delete(_saveFile);
            Save();
            Load();
        }



        public void ForceLoad()
        {
            Load();
        }
    }
}
