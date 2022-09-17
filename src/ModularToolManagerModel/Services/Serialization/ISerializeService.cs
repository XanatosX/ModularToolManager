using System.IO;
using System.Threading.Tasks;

namespace ModularToolManager.Services.Serialization;

/// <summary>
/// Interface to define a serialization service to parse or set a string serialization
/// </summary>
/// <typeparam name="T">The data type to de/serialize</typeparam>
public interface ISerializeService
{
    /// <summary>
    /// Get the given string as a deserialized object
    /// </summary>
    /// <param name="data">The data to deserialize</param>
    /// <returns>The read to use object or null if something went wrong</returns>
    T? GetDeserialized<T>(string data) where T : class;

    /// <summary>
    /// Get the given string as a deserialized object async
    /// </summary>
    /// <param name="data">The data to deserialize</param>
    /// <returns>The read to use object or null if something went wrong</returns>
    async Task<T?> GetDeserializedAsync<T>(string data) where T : class => await Task.Run(() => GetDeserialized<T>(data));

    /// <summary>
    /// Get the given string as a deserialized object
    /// </summary>
    /// <param name="data">The stream containing the data to deserialize</param>
    /// <returns>The read to use object or null if something went wrong</returns>
    T? GetDeserialized<T>(Stream data) where T : class;

    /// <summary>
    /// Get the given string as a deserialized object async
    /// </summary>
    /// <param name="data">The stream containing the data to deserialize</param>
    /// <returns>The read to use object or null if something went wrong</returns>
    async Task<T?> GetDeserializedAsync<T>(Stream data) where T : class => await Task.Run(() => GetDeserialized<T>(data));

    /// <summary>
    /// Serialize the give data as a string
    /// </summary>
    /// <param name="data">The data object to serialize</param>
    /// <returns>The serialized object as a string</returns>
    string GetSerialized<T>(T data) where T : class;

    /// <summary>
    /// Serialize the give data as a string async
    /// </summary>
    /// <param name="data">The data object to serialize</param>
    /// <returns>The serialized object as a string</returns>
    async Task<string> GetSerializedAsync<T>(T data) where T : class => await Task.Run(() => GetSerialized(data));

    /// <summary>
    /// Serialize the give data as a string
    /// </summary>
    /// <param name="data">The data object to serialize</param>
    /// <returns>The serialized object as a stream</returns>
    Stream GetSerializedStream<T>(T data) where T : class;

    /// <summary>
    /// Serialize the give data as a string async
    /// </summary>
    /// <param name="data">The data object to serialize</param>
    /// <returns>The serialized object as a stream</returns>
    async Task<Stream> GetSerializedStreamAsync<T>(T data) where T : class => await Task.Run(() => GetSerializedStream(data));


}
