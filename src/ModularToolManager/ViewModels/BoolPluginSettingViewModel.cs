using CommunityToolkit.Mvvm.ComponentModel;
using ModularToolManagerPlugin.Models;

namespace ModularToolManager.ViewModels;

/// <summary>
/// View model to represent a bool plugin value
/// </summary>
internal partial class BoolPluginSettingViewModel : PluginSettingBaseViewModel
{
    /// <summary>
    /// Is the option checked right now
    /// </summary>
    [ObservableProperty]
    private bool isChecked;

    /// <inheritdoc/>
    public BoolPluginSettingViewModel(SettingModel settingModel) : base(settingModel)
    {
        IsChecked = settingModel.GetData<bool>();
    }

    /// <inheritdoc/>
    public override SettingModel GetSettingsModel()
    {
        storedModel.SetValue(IsChecked);
        return storedModel;
    }
}
