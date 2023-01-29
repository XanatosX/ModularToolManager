using Microsoft.Extensions.Logging;
using ModularToolManager.Services.Serialization;
using ModularToolManagerModel.Services.Logging;
using ModularToolManagerPlugin.Models;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

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
    /// The logging service to use
    /// </summary>
    private readonly ILogger<JsonSerializationService>? loggingService;

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    /// <param name="serializationOptionFactory">The factory to use for creating the serialization options</param>
    /// /// <param name="loggingService">The logging service to use</param>
    /// <exception cref="NullReferenceException">A empty factory was recievend, the class cannot be used</exception>
    public JsonSerializationService(
        ISerializationOptionFactory<JsonSerializerOptions>? serializationOptionFactory,
        ILogger<JsonSerializationService>? loggingService,
        JsonConverter<SettingModel> settingConverter)
    {
        jsonSerializerOptions = serializationOptionFactory?.CreateOptions() ?? throw new NullReferenceException();
        jsonSerializerOptions.Converters.Add(settingConverter);
        this.loggingService = loggingService;
    }

    /// <inheritdoc/>
    public T? GetDeserialized<T>(string data) where T : class
    {
        T? returnData = default;

        try
        {
            returnData = JsonSerializer.Deserialize<T>(data, jsonSerializerOptions);
        }
        catch (Exception e)
        {
            loggingService?.LogError($"{GetType().FullName}: Error while deserialaizing {data}: {e.Message}");
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
            catch (Exception e)
            {
                loggingService?.LogError($"{GetType().FullName}: Error while deserialaizing {data}: {e.Message}");
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
        catch (Exception e)
        {
            loggingService?.LogError($"{GetType().FullName}: Error while serializing data: {e.Message}");
        }
        memoryStream.Position = 0;
        return memoryStream;
    }
}