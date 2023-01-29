using ModularToolManagerPlugin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ModularToolManagerModel.Data.Serialization;
public class SettingModelJsonConverter : JsonConverter<SettingModel>
{
    private const string VALUE_KEY = "value";

    private const string KEY_NAME_KEY = "key";

    private const string TYPE_KEY = "type";

    public override SettingModel? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, SettingModel value, JsonSerializerOptions options)
    {
        writer.WriteStartObject(nameof(SettingModel));
        writer.WriteString(KEY_NAME_KEY, value.Key);
        writer.WriteNumber(TYPE_KEY, (int)value.Type);
        writer.WriteString(VALUE_KEY, value.Value?.ToString() ?? string.Empty);
        writer.WriteEndObject();
        throw new NotImplementedException();
    }
}


