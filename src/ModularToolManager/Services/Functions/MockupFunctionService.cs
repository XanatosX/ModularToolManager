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
}
