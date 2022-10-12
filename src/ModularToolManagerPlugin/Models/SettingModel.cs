using ModularToolManagerPlugin.Enums;

namespace ModularToolManagerPlugin.Models;

/// <summary>
/// The setting model to use for saving plugin settings
/// </summary>
public class SettingModel
{
    /// <summary>
    /// The display name to show in the ui
    /// </summary>
    public string? DisplayName { get; set; }

    /// <summary>
    /// The key of the setting
    /// </summary>
    public string? Key { get; init; }

    /// <summary>
    /// The type of the setting
    /// </summary>
    public SettingType Type { get; init; }

    /// <summary>
    /// The value for the setting
    /// </summary>
    public object? Value { get; private set; }

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    /// <param name="data">The data to use for the newly created object</param>
    public SettingModel(object? data)
    {
        Value = data;
    }

    /// <summary>
    /// Get the data from the setting model converted to type T
    /// </summary>
    /// <typeparam name="T">The type to convert the data to</typeparam>
    /// <returns>The stored data is conversion succeded otherwise the default value for T</returns>
    public T? GetData<T>()
    {
        T? returnData = default;
        try
        {
            returnData = Value is null ? default : (T)Value;
        }
        catch (Exception)
        {
            // Something got wrong while parsing, whatever
        }
        return returnData;
    }

    /// <summary>
    /// Set the value for the setting model
    /// </summary>
    /// <typeparam name="T">The type of the value to set</typeparam>
    /// <param name="value">The value to use for setting the data</param>
    /// <returns>True if setting was a success</returns>
    public bool SetValue<T>(T? value)
    {
        if (value is null || value.GetType() != Value?.GetType())
        {
            return false;
        }
        Value = value;
        return true;
    }
}
