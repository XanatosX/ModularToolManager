using ModularToolManager.Services.Serialization;
using System;
using System.IO;
using System.Text;
using System.Text.Json;

namespace ModularToolManagerModel.Services.Serialization;

/// <summary>
/// Service to serialize data as json
/// </summary>
public class JsonSerializationService : ISerializeService
{
    /// <summary>
    /// The serialization options to use
    /// </summary>
    private readonly JsonSerializerOptions jsonSerializerOptions;

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    /// <param name="serializationOptionFactory">The factory to use for creating the serialization options</param>
    /// <exception cref="NullReferenceException">A empty factory was recievend, the class cannot be used</exception>
    public JsonSerializationService(ISerializationOptionFactory<JsonSerializerOptions>? serializationOptionFactory)
    {
        jsonSerializerOptions = serializationOptionFactory?.CreateOptions() ?? throw new NullReferenceException();
    }

    /// <inheritdoc/>
    public T? GetDeserialized<T>(string data) where T : class
    {
        T? returnData = default;

        try
        {
            returnData = JsonSerializer.Deserialize<T>(data, jsonSerializerOptions);
        }
        catch (Exception)
        {
            //Serialize did fail return empty object
        }

        return returnData;
    }

    /// <inheritdoc/>
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
                //Serialize did fail return empty object
            }
        }
        return returnData;
    }

    /// <inheritdoc/>
    public string GetSerialized<T>(T data) where T : class
    {
        string returnData = string.Empty;
        using (var reader = new StreamReader(GetSerializedStream(data)))
        {
            returnData = reader.ReadToEnd();
        }
        return returnData;
    }

    /// <inheritdoc/>
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
            //Serialize did fail return empty stream
        }
        memoryStream.Position = 0;
        return memoryStream;
    }
}