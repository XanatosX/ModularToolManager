using PluginInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolMangerInterface
{
    public interface IFunction : IPlugin
    {
        Dictionary<string, string> FileEndings { get; }
        HashSet<FunctionSetting> Settings { get;  }
    }

    public class FunctionSetting
    {
        private Type _type;
        public Type objectType => _type;

        private string _displayName;
        public string DisplayName => _displayName;

        private string _key;
        public string Key => _key;

        private object _value;
        public object Value => _value;

        public FunctionSetting(string key, string displayName, Type objectType, object value)
        {
            _key = key;
            _displayName = displayName;
            _value = value;
            _type = objectType;
            init();
        }

        public FunctionSetting(string key, string displayName, object value)
        {
            _key = key;
            _displayName = displayName;
            _value = value;
            _type = value.GetType();
            init();
        }

        private void init()
        {
            _key = _key.Replace(" ", "");
        }
    }

    public interface IFunctionContext : PluginContext
    {
        string FilePath { get; set; }
    }

    public class FunctionContext : IFunctionContext
    {
        private string _filePath;
        public string FilePath
        {
            get
            {
                return _filePath;
            }
            set
            {
                _filePath = value;
            }
        }

        public string SaveFile
        {
            get
            {
                return null;
            }
            set
            {
                SaveFile = null;
            }
        }

        public FunctionContext(string path)
        {
            _filePath = path;
        }
    }

}
