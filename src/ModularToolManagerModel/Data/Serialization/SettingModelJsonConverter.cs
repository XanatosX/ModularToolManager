using ModularToolManagerPlugin.Enums;
using ModularToolManagerPlugin.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ModularToolManagerModel.Data.Serialization;

/// <summary>
/// Converter for the setting model
/// </summary>
public class SettingModelJsonConverter : JsonConverter<SettingModel>
{
    /// <summary>
    /// The key to use for the value
    /// </summary>
    private const string VALUE_KEY = "value";

    /// <summary>
    /// The key to use for the settings key
    /// </summary>
    private const string KEY_NAME_KEY = "key";

    /// <summary>
    /// The key to use for the type
    /// </summary>
    private const string TYPE_KEY = "type";

    /// <inheritdoc/>
    public override SettingModel? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? readKey = null;
        string keyValue = string.Empty;
        SettingType type = SettingType.String;
        object? value = null;
        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
            {
                break;
            }
            if (readKey is not null)
            {
                if (readKey == KEY_NAME_KEY)
                {
                    keyValue = reader.GetString() ?? string.Empty;
                }
                if (readKey == TYPE_KEY)
                {
                    var intType = reader.GetInt32();
                    try
                    {
                        type = (SettingType)intType;
                    }
                    catch (Exception)
                    {
                    }
                }
                if (readKey == VALUE_KEY)
                {
                    try
                    {
                        switch (type)
                        {
                            case SettingType.Boolean:
                                value = reader.GetBoolean();
                                break;
                            case SettingType.String:
                                value = reader.GetString();
                                break;
                            case SettingType.Float:
                                value = (float)reader.GetDouble();
                                break;
                            case SettingType.Int:
                                value = (float)reader.GetInt32();
                                break;
                            default:
                                break;
                        }
                    }
                    catch (Exception)
                    {

                    }

                }

                readKey = null;
                continue;
            }
            readKey = reader.GetString();

        }
        return new SettingModel(value)
        {
            Key = keyValue,
            Type = type
        };
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, SettingModel value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString(KEY_NAME_KEY, value.Key);
        writer.WriteNumber(TYPE_KEY, (int)value.Type);
        switch (value.Type)
        {
            case SettingType.Boolean:
                writer.WriteBoolean(VALUE_KEY, value.Value as bool? ?? false);
                break;
            case SettingType.String:
                writer.WriteString(VALUE_KEY, value.Value?.ToString() ?? string.Empty);
                break;
            case SettingType.Float:
                writer.WriteNumber(VALUE_KEY, value.Value as float? ?? 0f);
                break;
            case SettingType.Int:
                writer.WriteNumber(VALUE_KEY, value.Value as int? ?? 0);
                break;
            default:
                break;
        }

        writer.WriteEndObject();
    }
}


