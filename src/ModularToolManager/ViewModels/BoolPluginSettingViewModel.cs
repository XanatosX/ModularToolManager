using CommunityToolkit.Mvvm.ComponentModel;
using ModularToolManager.ViewModels.Settings;
using ModularToolManagerPlugin.Models;

namespace ModularToolManager.ViewModels;

/// <summary>
/// View model to represent a bool plugin value
/// </summary>
internal partial class BoolPluginSettingViewModel : ObservableObject, IPluginSettingModel
{
    /// <summary>
    /// The model represented by the view
    /// </summary>
    private readonly SettingModel storedModel;

    /// <summary>
    /// The name to show for the setting
    /// </summary>
    public string DisplayName => $"{TranslationKey}:";

    /// <summary>
    /// The translated key required to build the display name
    /// </summary>
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(DisplayName))]
    private string? translationKey;

    /// <summary>
    /// Is the option checked right now
    /// </summary>
    [ObservableProperty]
    private bool isChecked;

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    /// <param name="settingModel">The setting model to create the view for</param>
    public BoolPluginSettingViewModel(SettingModel settingModel)
    {
        storedModel = settingModel;
        TranslationKey = settingModel.DisplayName ?? string.Empty;
        IsChecked = settingModel.GetData<bool>();
    }

    /// <inheritdoc/>
    public SettingModel GetSettingsModel()
    {
        storedModel.SetValue(IsChecked);
        return storedModel;
    }
}
