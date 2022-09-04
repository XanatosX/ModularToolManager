using ModularToolManager.Services.Plugin;
using ModularToolManagerModel.Services.Plugin;
using ModularToolManagerPlugin.Plugin;
using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ModularToolManager.Converters.Serialization;

/// <summary>
/// Json Converter to save function plugins
/// </summary>
internal class PluginConverter : JsonConverter<IFunctionPlugin>
{
    /// <summary>
    /// The plugin service to use for loading plugins
    /// </summary>
    private readonly IPluginService? pluginService;

    /// <summary>
    /// Create a new instance of this converter
    /// </summary>
    /// <param name="pluginService">The plugin service to use for loading plugins</param>
    public PluginConverter(IPluginService? pluginService)
    {
        this.pluginService = pluginService;
    }

    /// <inheritdoc/>
    public override IFunctionPlugin? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? pluginNameToLoad = reader.GetString();
        IFunctionPlugin? plugin = pluginService?.GetAvailablePlugins().FirstOrDefault(plugin => plugin.GetType().FullName == pluginNameToLoad);
        return plugin;
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, IFunctionPlugin value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.GetType().FullName);
    }
}
