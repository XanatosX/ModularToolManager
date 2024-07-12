using ModularToolManager.Models;
using System;

namespace ModularToolManager.Services.Settings;

/// <summary>
/// interface for loading and saving application settings
/// </summary>
public interface ISettingsService
{
    /// <summary>
    /// Load the application settings
    /// </summary>
    /// <returns>An instance of the application settings class</returns>
    ApplicationSettings GetApplicationSettings();

    /// <summary>
    /// Change the application settings
    /// </summary>
    /// <param name="changeSettingsAction">The current settings to change</param>
    /// <returns>True if changing was successful</returns>
    bool ChangeSettings(Action<ApplicationSettings> changeSettingsAction);

    /// <summary>
    /// Save the application settings
    /// </summary>
    /// <param name="newSettings">The new settings to save</param>
    /// <returns>True if saving was successful</returns>
    bool SaveApplicationSettings(ApplicationSettings newSettings);
}
