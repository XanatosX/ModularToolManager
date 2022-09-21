using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ModularToolManager.Models.Messages;
using ModularToolManagerModel.Services.Dependency;
using ModularToolManagerModel.Services.Plugin;
using ModularToolManagerPlugin.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularToolManager.ViewModels;
internal partial class AllPluginsViewModel : ObservableObject
{
    /// <summary>
    /// Plugin Service to use
    /// </summary>
    private readonly IPluginService pluginService;
    private readonly IDependencyResolverService dependencyResolverService;
    [ObservableProperty]
    private List<PluginViewModel> plugins;

    public AllPluginsViewModel(IPluginService pluginService, IDependencyResolverService dependencyResolverService)
    {

        this.dependencyResolverService = dependencyResolverService;

        Plugins = pluginService.GetAvailablePlugins()
                               .Select(plugin => GetViewModel(plugin))
                               .Where(pluginView => pluginView is not null)
                               .OfType<PluginViewModel>()
                               .ToList();
    }

    private PluginViewModel? GetViewModel(IFunctionPlugin functionPlugin)
    {
        PluginViewModel? pluginView = dependencyResolverService.GetDependency<PluginViewModel>();
        pluginView?.SetPlugin(functionPlugin);
        return pluginView;
    }

    [RelayCommand]
    private void Abort()
    {
        WeakReferenceMessenger.Default.Send(new CloseModalMessage(this));
    }
}
