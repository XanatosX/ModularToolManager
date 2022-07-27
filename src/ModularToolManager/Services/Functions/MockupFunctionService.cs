using ModularToolManager.Models;
using ModularToolManager.Services.Plugin;
using ModularToolManagerPlugin.Plugin;
using ModularToolManagerPlugin.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularToolManager.Services.Functions
{
    internal class MockupFunctionService : IFunctionService, IDisposable
    {
        private List<IFunctionPlugin> plugins;

        public MockupFunctionService()
        {
            plugins = new List<IFunctionPlugin>();
        }

        public List<FunctionModel> GetAvailableFunctions()
        {
            List<FunctionModel> functions = new List<FunctionModel>();

            return functions;
        }

        public void Dispose()
        {
            foreach (var plugin in plugins)
            {
                plugin.Dispose();
            }
            plugins.Clear();
        }
    }

    internal class MockupPlugin : IFunctionPlugin
    {
        private IPluginTranslationService translationService;
        private IFunctionSettingsService functionSettingsService;
        private OperatingSystem operatingSystem;

        public bool Startup(IPluginTranslationService translationService, IFunctionSettingsService settingsService, OperatingSystem operatingSystem)
        {
            this.translationService = translationService;
            functionSettingsService = settingsService;
            this.operatingSystem = operatingSystem;
            return true;
        }

        public string GetFunctionDisplayName()
        {
            throw new NotImplementedException();
        }

        public Version GetFunctionVersion()
        {
            throw new NotImplementedException();
        }

        public bool IsOperationSystemValid()
        {
            OperatingSystem.IsWindows();
            return operatingSystem.Platform == PlatformID.Win32NT;
        }



        public bool Execute(string parameters, string path)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
