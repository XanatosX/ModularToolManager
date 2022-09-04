using ModularToolManager.Converters.Serialization;
using ModularToolManagerModel.Services.Plugin;
using Splat;
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
    private readonly IReadonlyDependencyResolver? dependencyResolver;

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    /// <param name="dependencyResolver">The resolver to use to resolve dependencies</param>
    public JsonSerializationOptionFactory(IReadonlyDependencyResolver? dependencyResolver)
    {
        this.dependencyResolver = dependencyResolver;
    }

    ///<inheritdoc/>
    public JsonSerializerOptions CreateOptions()
    {
        JsonSerializerOptions options = new JsonSerializerOptions();
        options.Converters.Add(new PluginConverter(dependencyResolver?.GetService<IPluginService>()));
        return options;
    }
}
