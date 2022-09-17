using ModularToolManager.Converters.Serialization;
using ModularToolManagerModel.Services.Plugin;
using System.Text.Json;

namespace ModularToolManagerModel.Services.Serialization;

/// <summary>
/// Class to get json serailazation options
/// </summary>
internal class JsonSerializationOptionFactory : ISerializationOptionFactory<JsonSerializerOptions>
{
    /// <summary>
    /// Instance of the dependency resolver
    /// </summary>
    private readonly IPluginService? pluginService;

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    /// <param name="pluginService">The resolver to use to resolve dependencies</param>
    public JsonSerializationOptionFactory(IPluginService? pluginService)
    {
        this.pluginService = pluginService;
    }

    ///<inheritdoc/>
    public JsonSerializerOptions CreateOptions()
    {
        JsonSerializerOptions options = new JsonSerializerOptions();
        options.Converters.Add(new PluginConverter(pluginService));
        return options;
    }
}
