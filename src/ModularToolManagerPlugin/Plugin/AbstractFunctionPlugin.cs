using ModularToolManagerPlugin.Models;
using System.Globalization;

namespace ModularToolManagerPlugin.Plugin;

/// <summary>
/// Abstract class for the function plugin interface to implement some basic methods
/// </summary>
public abstract class AbstractFunctionPlugin : IFunctionPlugin
{
    /// <summary>
    /// THe current culture used by the main application
    /// </summary>
    protected CultureInfo? currentCulture;

    /// <inheritdoc/>
    public void ChangeLanguage(CultureInfo culture)
    {
        currentCulture = culture;
    }

    /// <inheritdoc/>
    public abstract void Dispose();

    /// <inheritdoc/>
    public abstract bool Execute(string parameters, string path);

    /// <inheritdoc/>
    public abstract IEnumerable<FileExtension> GetAllowedFileEndings();

    /// <inheritdoc/>
    public abstract string GetDisplayName();

    public abstract PluginInformation GetPluginInformation();

    /// <inheritdoc/>
    public abstract bool IsOperationSystemValid();
}
