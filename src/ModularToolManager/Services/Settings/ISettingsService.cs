using ModularToolManager.Models;

namespace ModularToolManager.Services.Settings;

/// <summary>
/// interface for loading and saving application settings
/// </summary>
internal interface ISettingsService
{
    /// <summary>
    /// Load the application settings
    /// </summary>
    /// <returns>An instance of the application settings class</returns>
    ApplicationSettings GetApplicationSettings();

    /// <summary>
    /// Save the application settings
    /// </summary>
    /// <param name="newSettings">The new settings to save</param>
    /// <returns>True if saving was successful</returns>
    bool SaveApplicationSettings(ApplicationSettings newSettings);
}
