using ModularToolManagerPlugin.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularToolManagerPlugin.Models;
public class SettingModel
{
    public string? DisplayName { get; set; }

    public string? Key { get; init; }

    public SettingType Type { get; init; }

    public object? Value { get; init; }

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
}
