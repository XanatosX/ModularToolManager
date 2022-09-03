using ModularToolManager.Services.Plugin;
using ModularToolManagerPlugin.Plugin;
using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ModularToolManager.Converters.Serialization;

internal class PluginConverter : JsonConverter<IFunctionPlugin>
{
    private readonly IPluginService? pluginService;

    public PluginConverter(IPluginService? pluginService)
    {
        this.pluginService = pluginService;
    }

    public override IFunctionPlugin? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? pluginNameToLoad = reader.GetString();
        IFunctionPlugin? plugin = pluginService?.GetAvailablePlugins().FirstOrDefault(plugin => plugin.GetType().FullName == pluginNameToLoad);
        return plugin;
    }

    public override void Write(Utf8JsonWriter writer, IFunctionPlugin value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.GetType().FullName);
    }
}
