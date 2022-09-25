using System.Collections.ObjectModel;

namespace ModularToolManagerPlugin.Models;

/// <summary>
/// Information about the plugin
/// </summary>
public class PluginInformation
{
    /// <summary>
    /// The authors of the plugin
    /// </summary>
    public ReadOnlyCollection<string> Authors { get; init; }

    /// <summary>
    /// Description of the plugin
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// The current version of the plugin
    /// </summary>
    public Version? Version { get; init; }

    /// <summary>
    /// The License of the plugin
    /// </summary>
    public string? License { get; init; }

    /// <summary>
    /// The project url of the plugin
    /// </summary>
    public string? ProjectUrl { get; init; }

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    public PluginInformation()
    {
        Authors = new List<string>().AsReadOnly();
    }
}
