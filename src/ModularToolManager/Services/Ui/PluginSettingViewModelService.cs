using ModularToolManager.ViewModels;
using ModularToolManager.ViewModels.Settings;
using ModularToolManagerPlugin.Enums;
using ModularToolManagerPlugin.Models;

namespace ModularToolManager.Services.Ui;

/// <summary>
/// Service to get the matching view model for a given setting model
/// </summary>
internal class PluginSettingViewModelService
{
    /// <summary>
    /// Get the view model based on the provided setting model
    /// </summary>
    /// <param name="settingModel">The setting model to get the view model for</param>
    /// <returns>A matching view model or null if nothing was found</returns>
    public IPluginSettingModel? GetViewModel(SettingModel settingModel)
    {
        return settingModel.Type switch
        {
            SettingType.Boolean => new BoolPluginSettingViewModel(settingModel),
            SettingType.String => new StringPluginSettingViewModel(settingModel),
            SettingType.Float => new FloatPluginSettingViewModel(settingModel),
            SettingType.Int => new IntPluginSettingViewModel(settingModel),
            _ => null
        };
    }
}
