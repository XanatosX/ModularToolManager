using CommunityToolkit.Mvvm.ComponentModel;
using ModularToolManagerPlugin.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace ModularToolManager.ViewModels;

/// <summary>
/// View model class for float based plugin settings
/// </summary>
internal partial class FloatPluginSettingViewModel : PluginSettingBaseViewModel
{
    /// <summary>
    /// The number to use for the settings
    /// </summary>
    [ObservableProperty]
    [Range(float.MinValue, float.MaxValue)]
    private float? floatNumber;

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    /// <param name="settingModel">The setting model to get the data from</param>
    public FloatPluginSettingViewModel(SettingModel settingModel) : base(settingModel)
    {
        FloatNumber = settingModel.GetData<float>();
    }

    /// <inheritdoc/>
    public override SettingModel GetSettingsModel()
    {
        storedModel.SetValue(FloatNumber);
        return storedModel;
    }

    public override void UpdateValue(object? newData)
    {
        if (newData is float)
        {
            FloatNumber = (float)newData;
        }
    }
}
