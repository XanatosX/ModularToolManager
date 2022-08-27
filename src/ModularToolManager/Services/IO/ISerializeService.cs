using System.IO;
using System.Threading.Tasks;

namespace ModularToolManager.Services.IO
{
    /// <summary>
    /// Interface to define a serialization service to parse or set a string serialization
    /// </summary>
    /// <typeparam name="T">The data type to de/serialize</typeparam>
    internal interface ISerializeService<T> where T : class
    {
        /// <summary>
        /// Get the given string as a deserialized object
        /// </summary>
        /// <param name="data">The data to deserialize</param>
        /// <returns>The read to use object or null if something went wrong</returns>
        T? GetDeserialized(string data);

        /// <summary>
        /// Get the given string as a deserialized object async
        /// </summary>
        /// <param name="data">The data to deserialize</param>
        /// <returns>The read to use object or null if something went wrong</returns>
        async Task<T?> GetDeserializedAsync(string data) => await Task.Run(() => GetDeserialized(data));

        /// <summary>
        /// Get the given string as a deserialized object
        /// </summary>
        /// <param name="data">The stream containing the data to deserialize</param>
        /// <returns>The read to use object or null if something went wrong</returns>
        T? GetDeserialized(Stream data);

        /// <summary>
        /// Get the given string as a deserialized object async
        /// </summary>
        /// <param name="data">The stream containing the data to deserialize</param>
        /// <returns>The read to use object or null if something went wrong</returns>
        async Task<T?> GetDeserializedAsync(Stream data) => await Task.Run(() => GetDeserialized(data));

        /// <summary>
        /// Serialize the give data as a string
        /// </summary>
        /// <param name="data">The data object to serialize</param>
        /// <returns>The serialized object as a string</returns>
        string GetSerialized(T data);

        /// <summary>
        /// Serialize the give data as a string async
        /// </summary>
        /// <param name="data">The data object to serialize</param>
        /// <returns>The serialized object as a string</returns>
        async Task<string> GetSerializedAsync(T data) => await Task.Run(() => GetSerialized(data));

        /// <summary>
        /// Serialize the give data as a string
        /// </summary>
        /// <param name="data">The data object to serialize</param>
        /// <returns>The serialized object as a stream</returns>
        Stream GetSerializedStream(T data);

        /// <summary>
        /// Serialize the give data as a string async
        /// </summary>
        /// <param name="data">The data object to serialize</param>
        /// <returns>The serialized object as a stream</returns>
        async Task<Stream> GetSerializedStreamAsync(T data) => await Task.Run(() => GetSerializedStream(data));


    }
}
