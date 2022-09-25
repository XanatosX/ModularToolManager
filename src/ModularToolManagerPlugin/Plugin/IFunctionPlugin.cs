using ModularToolManagerPlugin.Models;
using System.Globalization;

namespace ModularToolManagerPlugin.Plugin;

/// <summary>
/// Plugin interface class to define a single plugin usable by the main application
/// </summary>
public interface IFunctionPlugin : IDisposable
{

    /// <summary>
    /// Event if the language of the main application was changed
    /// </summary>
    /// <param name="culture">The new culture used by the main application</param>
    void ChangeLanguage(CultureInfo culture);

    /// <summary>
    /// Can this plugin be used on the current operation system
    /// </summary>
    /// <returns>True if the plugin can be used on this OS</returns>
    bool IsOperationSystemValid();

    /// <summary>
    /// Get the display name of the plugin
    /// </summary>
    /// <returns></returns>
    string GetDisplayName();

    /// <summary>
    /// Get the information abour the plugin
    /// </summary>
    /// <returns>A plugin information object</returns>
    PluginInformation GetPluginInformation();

    /// <summary>
    /// Run the plugin with a given path and parameters
    /// </summary>
    /// <param name="parameters">The parameters provided by the application</param>
    /// <param name="path">The path to execute the plugin on</param>
    /// <returns>True if execution was successful</returns>
    bool Execute(string parameters, string path);

    /// <summary>
    /// Get a list with all the file endings this plugin can work with
    /// </summary>
    /// <returns>A list with all file extensions</returns>
    IEnumerable<FileExtension> GetAllowedFileEndings();
}
