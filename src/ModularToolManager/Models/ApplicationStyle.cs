using Avalonia.Media;
using System.Text.Json.Serialization;

namespace ModularToolManager.Models;

public class ApplicationStyle
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("resource_key")]
    public string ResourceKey { get; init; }

    [JsonIgnore]
    public string? Name { get; set; }

    [JsonPropertyName("translation_key")]
    public string? Translation_Key { get; init; }

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("tint_color")]
    public Color? TintColor { get; init; }
}
