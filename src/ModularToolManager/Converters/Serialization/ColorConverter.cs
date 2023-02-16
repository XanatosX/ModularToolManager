using Avalonia.Media;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ModularToolManager.Converters.Serialization;

/// <summary>
/// Converter class to get avalonia colors correctly laoded/saved into a json
/// </summary>
internal class ColorConverter : JsonConverter<Color>
{
    /// <summary>
    /// The current color mode for reading
    /// </summary>
    internal enum CurrentColorMode
    {
        Unknown,
        Alpha,
        Red,
        Green,
        Blue
    }

    /// <inheritdoc/>
    public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {

        byte alpha = 0;
        byte red = 0;
        byte green = 0;
        byte blue = 0;
        CurrentColorMode currentColorMode = CurrentColorMode.Unknown;


        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
            {
                break;
            }
            if (reader.TokenType == JsonTokenType.PropertyName)
            {
                currentColorMode = reader.GetString() switch
                {
                    "A" => CurrentColorMode.Alpha,
                    "R" => CurrentColorMode.Red,
                    "G" => CurrentColorMode.Green,
                    "B" => CurrentColorMode.Blue,
                    _ => CurrentColorMode.Unknown,
                };
            }
            if (reader.TokenType == JsonTokenType.Number)
            {
                byte number = reader.GetByte();
                switch (currentColorMode)
                {
                    case CurrentColorMode.Unknown:
                        break;
                    case CurrentColorMode.Alpha:
                        alpha = number;
                        break;
                    case CurrentColorMode.Red:
                        red = number;
                        break;
                    case CurrentColorMode.Green:
                        green = number;
                        break;
                    case CurrentColorMode.Blue:
                        blue = number;
                        break;
                    default: break;
                }
            }
        }

        return new Color(alpha, red, green, blue);
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteNumber("A", value.A);
        writer.WriteNumber("R", value.R);
        writer.WriteNumber("G", value.G);
        writer.WriteNumber("B", value.B);
        writer.WriteEndObject();
    }
}
