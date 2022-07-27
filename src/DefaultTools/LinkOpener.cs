using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginInterface;
using ToolMangerInterface;
using System.Diagnostics;
using System.IO;
using PluginCommunication;

namespace DefaultTools
{
    public class Shortcut : IFunction
    {
        public bool Active { get; set; }

        public string Author => "Simon Aberle";

        private PluginContext _context;
        public PluginContext context => _context;

        public string Description => "Open a windows shortcut";

        public string DisplayName => "Windows shortcut";

        private Dictionary<string, string> _fileEndings;
        public Dictionary<string, string> FileEndings => _fileEndings;

        private bool _initialized;
        public bool initialized => _initialized;

        public string UniqueName => "Shortcut";

        public Version Version => new Version(1, 0, 0, 0);

        private Module _comModule;
        public Module ComModule => _comModule;


        PluginSettings _settings;
        PluginSettings ISettingPlugin.Settings => _settings;

        public event EventHandler<ErrorData> Error;

        public bool destroy()
        {
            return true;
        }

        public bool initialize()
        {
            _fileEndings = new Dictionary<string, string>();
            _settings = new PluginSettings();

            _fileEndings.Add("Linkfile", ".lnk");
            _fileEndings.Add("Executable", ".exe");

            _comModule = new Module();
            _initialized = true;
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
            Process process = new Process();
            process.StartInfo.FileName = CurrentContext.FilePath;
            process.StartInfo.WorkingDirectory = (new FileInfo(CurrentContext.FilePath)).DirectoryName;
            try
            {
                process.Start();
            }
            catch (Exception ex)
            {
                _comModule.SendMessage("log", ex.Message);
            }

            return true;
        }

        public bool Save()
        {
            return true;
        }
    }
}
