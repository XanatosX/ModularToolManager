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

        //private bool _ready;
        //public bool Ready
        //{
        //    get
        //
        //    {
        //        return _ready;
        //    }
        //}

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
