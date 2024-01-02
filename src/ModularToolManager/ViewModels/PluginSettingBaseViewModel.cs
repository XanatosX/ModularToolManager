using CommunityToolkit.Mvvm.ComponentModel;
using ModularToolManager.ViewModels.Settings;
using ModularToolManagerPlugin.Models;

namespace ModularToolManager.ViewModels;

/// <summary>
/// Base class for plugin settings
/// </summary>
internal abstract partial class PluginSettingBaseViewModel : ObservableValidator, IPluginSettingModel
{
    /// <summary>
    /// The model represented by the view
    /// </summary>
    protected readonly SettingModel storedModel;

    /// <summary>
    /// The translated key required to build the display name
    /// </summary>
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(DisplayName))]
    private string? translationKey;

    /// <summary>
    /// The name to show for the setting
    /// </summary>
    public string DisplayName => $"{TranslationKey}:";

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    /// <param name="settingModel">The setting model to create the data set from</param>
    protected PluginSettingBaseViewModel(SettingModel settingModel)
    {
        storedModel = settingModel;
        TranslationKey = settingModel.DisplayName ?? string.Empty;
    }

    /// <inheritdoc/>
    public abstract SettingModel GetSettingsModel();

    /// <inheritdoc/>
    public abstract void UpdateValue(object? newData);
}