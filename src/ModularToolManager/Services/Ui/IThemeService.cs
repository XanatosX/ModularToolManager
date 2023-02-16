using ModularToolManager.Models;
using System.Collections.Generic;

namespace ModularToolManager.Services.Ui;
public interface IThemeService
{
    /// <summary>
    /// Get all the available styles
    /// </summary>
    /// <returns>A list with all the styles</returns>
    IEnumerable<ApplicationStyle> GetAllStyles();

    /// <summary>
    /// Change the theme for the application
    /// </summary>
    /// <param name="theme">The theme to use</param>
    void ChangeApplicationTheme(ApplicationStyle theme);

    /// <summary>
    /// Get a style by it's id
    /// </summary>
    /// <param name="id">The id of the style to recieve</param>
    /// <returns>The matching style or null if nothing was found</returns>
    ApplicationStyle? GetStyleById(int id);
}
