using System;
using System.IO;
using System.Text;
using System.Text.Json;

namespace ModularToolManager.Services.Serialization;

internal class JsonSerializationService : ISerializeService
{
    private readonly JsonSerializerOptions jsonSerializerOptions;

    public JsonSerializationService(ISerializationOptionFactory<JsonSerializerOptions>? serializationOptionFactory)
    {
        jsonSerializerOptions = serializationOptionFactory?.CreateOptions() ?? throw new NullReferenceException();
    }

    public T? GetDeserialized<T>(string data) where T : class
    {
        T? returnData = default;

        try
        {
            returnData = JsonSerializer.Deserialize<T>(data, jsonSerializerOptions);
        }
        catch (Exception)
        {

        }

        return returnData;
    }

    public T? GetDeserialized<T>(Stream data) where T : class
    {
        T? returnData = default;
        using (StreamReader reader = new StreamReader(data))
        {
            try
            {
                returnData = GetDeserialized<T>(reader.ReadToEnd());
            }
            catch (Exception)
            {
            }
        }
        return returnData;
    }

    public string GetSerialized<T>(T data) where T : class
    {
        string returnData = string.Empty;
        using (var reader = new StreamReader(GetSerializedStream<T>(data)))
        {
            returnData = reader.ReadToEnd();
        }
        return returnData;
    }

    public Stream GetSerializedStream<T>(T data) where T : class
    {
        MemoryStream memoryStream = new MemoryStream();
        StreamWriter writer = new StreamWriter(memoryStream, Encoding.UTF8);
        try
        {
            JsonSerializer.Serialize(writer.BaseStream, data, jsonSerializerOptions);
        }
        catch (Exception)
        {
        }
        memoryStream.Position = 0;
        return memoryStream;
    }
}