using Microsoft.Extensions.Options;
using ModularToolManagerPlugin.Enums;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ModularToolManagerPlugin.Models;
public class PersistantPluginSetting
{
	[JsonPropertyName("key")]
	public string? Key { get; set; }

	[JsonPropertyName("type")]
	public SettingType Type { get; set; }

	[JsonPropertyName("value")]
	public object? Value { get; set; }

	public PersistantPluginSetting()
	{
		Key = null;
		Value = null;
		Type = SettingType.String;
	}

	public PersistantPluginSetting(SettingModel settingModel)
	{
		Key = settingModel.Key;
		Type = settingModel.Type;
		Value = settingModel.Value;
	}

	public T? GetValue<T>()
	{
		object? dataObject = Value;
		if (dataObject is JsonElement element)
		{
			dataObject = Type switch
			{
				SettingType.Boolean => element.GetBoolean(),
				SettingType.Int => element.GetInt32(),
				SettingType.Float => (float)element.GetDecimal(),
				_ => element.GetRawText()
			};
		}
		return dataObject is null || dataObject.GetType() != typeof(T) ? default : (T?)dataObject;
	}
}
