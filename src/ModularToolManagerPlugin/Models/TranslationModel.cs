using System.Text.Json.Serialization;

namespace ModularToolManagerPlugin.Models
{
    public class TranslationModel
    {
        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }

        public TranslationModel()
        {
            Key = string.Empty;
            Value = string.Empty;
        }
    }
}
