using CommunityToolkit.Mvvm.ComponentModel;
using ModularToolManagerPlugin.Models;

namespace ModularToolManager.ViewModels;

/// <summary>
/// View model for a string plugin setting
/// </summary>
internal partial class StringPluginSettingViewModel : PluginSettingBaseViewModel
{
    /// <summary>
    /// The text for this setting
    /// </summary>
    [ObservableProperty]
    public string? settingText;

    /// <inheritdoc/>
    public StringPluginSettingViewModel(SettingModel settingModel) : base(settingModel)
    {
        SettingText = settingModel.GetData<string>() ?? string.Empty;
    }

    /// <inheritdoc/>
    public override SettingModel GetSettingsModel()
    {
        storedModel.SetValue(SettingText);
        return storedModel;
    }
}
