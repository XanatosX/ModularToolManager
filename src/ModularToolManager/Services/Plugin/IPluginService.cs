using ModularToolManagerPlugin.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularToolManager.Services.Plugin
{
    internal interface IPluginService
    {
        List<IFunctionPlugin> GetAvailablePlugins();
    }
}
