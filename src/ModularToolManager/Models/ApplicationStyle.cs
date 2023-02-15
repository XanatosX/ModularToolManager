using Avalonia.Media;
using Avalonia.Themes.Fluent;
using System.Text.Json.Serialization;

namespace ModularToolManager.Models;

public class ApplicationStyle
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("mode")]
    public FluentThemeMode Mode { get; init; }

    [JsonPropertyName("name_translation_key")]
    public string? NameTranslationKey { get; init; }

    [JsonPropertyName("description_translation_key")]
    public string? DescriptionTranslationKey { get; init; }

    [JsonIgnore]
    public string? Name { get; set; }

    [JsonIgnore]
    public string? Description { get; set; }

    [JsonPropertyName("tint_color")]
    public Color? TintColor { get; init; }

    [JsonPropertyName("material_opacity")]
    public float MaterialOpacity { get; init; }

    [JsonPropertyName("tint_opacity")]
    public float TintOpacity { get; init; }
}
