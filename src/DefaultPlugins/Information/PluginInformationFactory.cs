using ModularToolManagerPlugin.Models;

namespace DefaultPlugins.Information;

/// <summary>
/// Class to use for getting the plugin information
/// </summary>
internal sealed class PluginInformationFactory
{
    /// <summary>
    /// The instance to use
    /// </summary>
    public static PluginInformationFactory Instance
    {
        get
        {
            instance ??= new PluginInformationFactory();
            return instance;
        }
    }

    /// <summary>
    /// Private readonly baking field for the instance
    /// </summary>
    private static PluginInformationFactory? instance;

    /// <summary>
    /// Create new plugin information
    /// </summary>
    /// <param name="description">The description of the plugin</param>
    /// <returns>Ne wplugin informations</returns>
    public PluginInformation GetPluginInformation(string description)
    {
        return new PluginInformation
        {
            Authors = new List<string> { "XanatosX " }.AsReadOnly(),
            Description = description,
            License = "MIT",
            ProjectUrl = "https://github.com/XanatosX/ModularToolManager",
            Version = new Version("1.0.0.0")

        };
    }
}
