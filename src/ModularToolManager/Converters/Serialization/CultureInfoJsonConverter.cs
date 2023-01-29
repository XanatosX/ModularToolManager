using ModularToolManagerPlugin.Plugin;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ModularToolManager.Converters.Serialization;

/// <summary>
/// Class used to convert culture info into valid setting
/// </summary>
internal class CultureInfoJsonConverter : JsonConverter<CultureInfo>
{
    /// <inheritdoc/>
    public override CultureInfo? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? isoThreeCode = reader.GetString();
        CultureInfo? culture = null;
        try
        {
            culture = CultureInfo.GetCultureInfo(isoThreeCode ?? String.Empty);
        }
        catch (Exception)
        {
        }
        return culture;
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, CultureInfo value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ThreeLetterISOLanguageName);
    }
}
