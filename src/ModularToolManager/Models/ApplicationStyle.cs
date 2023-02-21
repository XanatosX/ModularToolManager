using Avalonia.Media;
using Avalonia.Themes.Fluent;
using System.Text.Json.Serialization;

namespace ModularToolManager.Models;

/// <summary>
/// Style information for the application
/// </summary>
public class ApplicationStyle
{
    /// <summary>
    /// The id of the style
    /// </summary>
    [JsonPropertyName("id")]
    public int Id { get; init; }

    /// <summary>
    /// IS this a dark or light style variant
    /// </summary>
    [JsonPropertyName("mode")]
    public FluentThemeMode Mode { get; init; }

    /// <summary>
    /// The name of the translation key which is getting used for the name
    /// </summary>
    [JsonPropertyName("name_translation_key")]
    public string? NameTranslationKey { get; init; }

    /// <summary>
    /// The name of the translation key which is getting used for the description
    /// </summary>
    [JsonPropertyName("description_translation_key")]
    public string? DescriptionTranslationKey { get; init; }

    /// <summary>
    /// The translated name for the application style
    /// </summary>
    [JsonIgnore]
    public string? Name { get; set; }

    /// <summary>
    /// The translated description for the application style
    /// </summary>
    [JsonIgnore]
    public string? Description { get; set; }

    /// <summary>
    /// The tint color for the material
    /// </summary>
    [JsonPropertyName("tint_color")]
    public Color? TintColor { get; init; }

    /// <summary>
    /// The opacity of the material
    /// </summary>
    [JsonPropertyName("material_opacity")]
    public float MaterialOpacity { get; init; }

    /// <summary>
    /// The opacity of the tint
    /// </summary>
    [JsonPropertyName("tint_opacity")]
    public float TintOpacity { get; init; }
}
