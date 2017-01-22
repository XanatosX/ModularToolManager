using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginInterface;
using ToolMangerInterface;
using System.Diagnostics;
using System.IO;

namespace DefaultTools
{
    public class Shortcut : IFunction
    {
        public bool Active { get; set; }

        public string Author
        {
            get
            {
                return "Simon Aberle";
            }
        }

        private PluginContext _context;
        public PluginContext context
        {
            get
            {
                return _context;
            }
        }

        public string Description
        {
            get
            {
                return "Open a windows shortcut";
            }
        }

        public string DisplayName
        {
            get
            {
                return "Windows shortcut";
            }
        }

        private Dictionary<string, string> _fileEndings;
        public Dictionary<string,string> FileEndings
        {
            get
            {
                return _fileEndings;
            }
        }

        private bool _initialized;
        public bool initialized
        {
            get
            {
                return _initialized;
            }
        }

        public string UniqueName
        {
            get
            {
                return "Shortcut";
            }
        }

        public Version Version
        {
            get
            {
                return new Version(1, 0, 0, 0);
            }
        }

        public event EventHandler<ErrorData> Error;

        public bool destroy()
        {
            return true;
        }

        public bool initialize()
        {
            _fileEndings = new Dictionary<string, string>();
            _fileEndings.Add("Linkfile", ".lnk");
            return true;
        }

        public bool Load()
        {
            return true;
        }

        public bool PerformeAction(PluginContext context)
        {
            if (context.GetType() != typeof(FunctionContext))
                return false;

            FunctionContext CurrentContext = (FunctionContext)context;
            Process process = new Process(); ;
            process.StartInfo.FileName = CurrentContext.FilePath;
            process.StartInfo.WorkingDirectory = (new FileInfo(CurrentContext.FilePath)).DirectoryName;
            process.Start();


            return true;
        }

        public bool Save()
        {
            return true;
        }
    }
}
