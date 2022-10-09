using ModularToolManagerPlugin.Models;
using ModularToolManagerPlugin.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ModularToolManager.Models;
public class PluginSettings
{
    [JsonPropertyName("plugin")]
    public IFunctionPlugin? Plugin { get; set; }

    [JsonPropertyName("settings")]
    public List<PersistantPluginSetting>? Settings { get; set; }
}
