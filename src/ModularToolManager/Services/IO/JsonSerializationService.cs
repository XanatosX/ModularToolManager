using System;
using System.IO;
using System.Text.Json;

namespace ModularToolManager.Services.IO
{
    internal class JsonSerializationService : ISerializeService
    {
        public T? GetDeserialized<T>(string data) where T : class
        {
            throw new NotImplementedException();
        }

        public T? GetDeserialized<T>(Stream data) where T : class
        {
            throw new NotImplementedException();
        }

        public string GetSerialized<T>(T data) where T : class
        {
            string returnData = string.Empty;
            try
            {
                returnData = JsonSerializer.Serialize<T>(data);
            }
            catch (Exception)
            {
            }
            return returnData;
        }

        public Stream GetSerializedStream<T>(T data) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
