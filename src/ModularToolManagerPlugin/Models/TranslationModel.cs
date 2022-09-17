using System.Text.Json.Serialization;

namespace ModularToolManagerPlugin.Models;

/// <summary>
/// Translation entry key
/// </summary>
public class TranslationModel
{
    /// <summary>
    /// The key to find the translation
    /// </summary>
    [JsonPropertyName("key")]
    public string Key { get; init; }

    /// <summary>
    /// The value of the transtalation
    /// </summary>

    [JsonPropertyName("value")]
    public string Value { get; init; }

    /// <summary>
    /// Create a new empty instance of this class
    /// </summary>
    public TranslationModel()
    {
        Key = string.Empty;
        Value = string.Empty;
    }
}
