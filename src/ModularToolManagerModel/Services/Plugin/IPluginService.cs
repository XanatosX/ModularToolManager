using ModularToolManagerPlugin.Plugin;

namespace ModularToolManagerModel.Services.Plugin;

/// <summary>
/// Plugin service to load plugins from differenc assemblies
/// </summary>
public interface IPluginService
{
    /// <summary>
    /// Get all the available plguins for this application
    /// </summary>
    /// <returns>A list with plugins for this application</returns>
    List<IFunctionPlugin> GetAvailablePlugins();
}
