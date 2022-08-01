﻿using ModularToolManagerPlugin.Plugin;

namespace ModularToolManager.ViewModels;

/// <summary>
/// View model for function plugin
/// </summary>
public class FunctionPluginViewModel : ViewModelBase
{


    /// <summary>
    /// The function plugin to show
    /// </summary>
    public readonly IFunctionPlugin Plugin;

    /// <summary>
    /// The name of the function plugin
    /// </summary>
    public string PluginName => Plugin.GetFunctionDisplayName();

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    /// <param name="functionPlugin">The function plugin to display</param>
    public FunctionPluginViewModel(IFunctionPlugin functionPlugin)
    {
        this.Plugin = functionPlugin;
    }
}
