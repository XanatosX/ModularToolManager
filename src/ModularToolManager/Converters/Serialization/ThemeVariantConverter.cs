
using Avalonia.Styling;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ModularToolManager.Converters.Serialization;
internal class ThemeVariantConverter : JsonConverter<ThemeVariant>
{
    public override ThemeVariant? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        reader.TryGetInt32(out int value);
        return value switch
        {
            0 => ThemeVariant.Light,
            1 => ThemeVariant.Dark,
            _ => ThemeVariant.Default
        };
    }

    public override void Write(Utf8JsonWriter writer, ThemeVariant value, JsonSerializerOptions options)
    {
        int themeId = -1;
        if (value == ThemeVariant.Light)
        {
            themeId = 0;
        }
        else if (value == ThemeVariant.Dark)
        {
            themeId = 1;
        }
        writer.WriteNumberValue(themeId);
    }
}
