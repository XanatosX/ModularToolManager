using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ModularToolManager.Converters.Serialization;
internal class ColorConverter : JsonConverter<Color>
{
    internal enum CurrentColorMode
    {
        Unknown,
        Alpha,
        Red,
        Green,
        Blue
    }

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
                }
            }
        }

        return new Color(alpha, red, green, blue);
    }

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
