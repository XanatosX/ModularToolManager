using ModularToolManagerPlugin.Plugin;
using System.Collections.Generic;

namespace ModularToolManager.Services.Plugin;

/// <summary>
/// Plugin service to load plugins from differenc assemblies
/// </summary>
internal interface IPluginService
{
    /// <summary>
    /// Get all the available plguins for this application
    /// </summary>
    /// <returns>A list with plugins for this application</returns>
    List<IFunctionPlugin> GetAvailablePlugins();
}
