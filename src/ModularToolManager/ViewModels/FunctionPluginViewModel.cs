using CommunityToolkit.Mvvm.ComponentModel;
using ModularToolManagerPlugin.Plugin;

namespace ModularToolManager.ViewModels;

/// <summary>
/// View model for function plugin
/// </summary>
public class FunctionPluginViewModel : ObservableObject
{
    /// <summary>
    /// The function plugin to show
    /// </summary>
    public IFunctionPlugin Plugin { get; init; }

    /// <summary>
    /// The name of the function plugin
    /// </summary>
    public string PluginName => Plugin.GetDisplayName();

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    /// <param name="functionPlugin">The function plugin to display</param>
    public FunctionPluginViewModel(IFunctionPlugin functionPlugin)
    {
        Plugin = functionPlugin;
    }
}
