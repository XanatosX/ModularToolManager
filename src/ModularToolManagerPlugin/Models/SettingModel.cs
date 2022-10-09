using ModularToolManagerPlugin.Enums;

namespace ModularToolManagerPlugin.Models;
public class SettingModel
{
    public string? DisplayName { get; set; }

    public string? Key { get; init; }

    public SettingType Type { get; init; }

    public object? Value { get; private set; }

    public SettingModel() : this(null) { }

    public SettingModel(object? data)
    {
        Value = data;
    }

    public T? GetData<T>()
    {
        T? returnData = default;
        try
        {
            returnData = Value is null ? default : (T)Value;
        }
        catch (Exception)
        {
        }
        return returnData;
    }

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
