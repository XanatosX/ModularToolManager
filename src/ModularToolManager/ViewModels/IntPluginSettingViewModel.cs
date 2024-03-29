﻿using CommunityToolkit.Mvvm.ComponentModel;
using ModularToolManagerPlugin.Models;
using System.ComponentModel.DataAnnotations;

namespace ModularToolManager.ViewModels;

/// <summary>
/// View Model for int plugin setting
/// </summary>
internal partial class IntPluginSettingViewModel : PluginSettingBaseViewModel
{
    /// <summary>
    /// The number to use for the settings
    /// </summary>
    [ObservableProperty]
    [Range(int.MinValue, int.MaxValue)]
    private int? integerNumber;

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    /// <param name="settingModel">The setting model to get the data from</param>
    public IntPluginSettingViewModel(SettingModel settingModel) : base(settingModel)
    {
        IntegerNumber = settingModel.GetData<int>();
    }

    /// <inheritdoc/>
    public override SettingModel GetSettingsModel()
    {
        storedModel.SetValue(IntegerNumber);
        return storedModel;
    }

    /// <inheritdoc/>
    public override void UpdateValue(object? newData)
    {
        if (newData is int)
        {
            IntegerNumber = (int)newData;
        }
    }
}
