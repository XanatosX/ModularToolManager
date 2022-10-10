using Microsoft.Extensions.Options;
using ModularToolManagerPlugin.Enums;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ModularToolManagerPlugin.Models;

/// <summary>
/// Plugin settings ready to save on the disc
/// </summary>
public class PersistantPluginSetting
{
	/// <summary>
	/// The key of the plugin settings
	/// </summary>
	[JsonPropertyName("key")]
	public string? Key { get; set; }

	/// <summary>
	/// The type of the plugin settings
	/// </summary>
	[JsonPropertyName("type")]
	public SettingType Type { get; set; }

	/// <summary>
	/// The value of the plugin setting
	/// </summary>
	[JsonPropertyName("value")]
	public object? Value { get; set; }

	/// <summary>
	/// Create a new instance of this class
	/// </summary>
	public PersistantPluginSetting()
	{
		//Required empty constructor for json serializer
		Type = SettingType.String;
		Key = null;
		Value = null;
	}

	/// <summary>
	/// Create a new instance of this class
	/// </summary>
	/// <param name="settingModel">The settings model to use as a base</param>
	public PersistantPluginSetting(SettingModel settingModel)
	{
		Key = settingModel.Key;
		Type = settingModel.Type;
		Value = settingModel.Value;
	}

	/// <summary>
	/// Get the value from the plugin setting
	/// </summary>
	/// <typeparam name="T">The type to convert the value to</typeparam>
	/// <returns>The converted value or the default of T</returns>
	private T? GetValue<T>()
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

	/// <summary>
	/// Convert to a valid setting model
	/// </summary>
	/// <returns>A useable setting model</returns>
	public SettingModel GetSettingModel()
	{
		object? data = Type switch
		{
			SettingType.Boolean => GetValue<bool>(),
			SettingType.Int => GetValue<int>(),
			SettingType.Float => GetValue<float>(),
			_ => GetValue<string>()
		};
		return new SettingModel(data)
		{
			DisplayName = Key,
			Key = Key,
			Type = Type
		};
	}
}
