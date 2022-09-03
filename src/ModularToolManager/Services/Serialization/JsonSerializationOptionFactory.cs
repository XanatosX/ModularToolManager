using ModularToolManager.Converters.Serialization;
using ModularToolManager.Services.Plugin;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ModularToolManager.Services.Serialization;
internal class JsonSerializationOptionFactory : ISerializationOptionFactory<JsonSerializerOptions>
{
    private readonly IReadonlyDependencyResolver? dependencyResolver;

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
