using ModularToolManagerPlugin.Models;

namespace DefaultPlugins.Information;
internal sealed class PluginInformationFactory
{
    public static PluginInformationFactory Instance
    {
        get
        {
            if (instance is null)
            {
                instance = new PluginInformationFactory();
            }
            return instance;
        }
    }

    private static PluginInformationFactory? instance;

    private PluginInformationFactory()
    {
        instance = null;
    }

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
