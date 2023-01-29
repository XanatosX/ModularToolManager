using ModularToolManagerPlugin.Models;

namespace ModularToolManager.ViewModels.Settings;

/// <summary>
/// Interface to classify a plugin setting view model
/// </summary>
internal interface IPluginSettingModel
{
    /// <summary>
    /// Update the internal value from the outside
    /// </summary>
    /// <param name="newData">The new data to set</param>
    void UpdateValue(object? newData);

    /// <summary>
    /// Get the setting model from the view
    /// </summary>
    /// <returns>A useable setting model</returns>
    SettingModel GetSettingsModel();
}