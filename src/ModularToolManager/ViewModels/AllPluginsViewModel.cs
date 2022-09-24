using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ModularToolManager.Models.Messages;
using ModularToolManagerModel.Services.Dependency;
using ModularToolManagerModel.Services.Plugin;
using ModularToolManagerPlugin.Plugin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularToolManager.ViewModels;
internal partial class AllPluginsViewModel : ObservableObject
{
    private readonly IDependencyResolverService dependencyResolverService;

    [ObservableProperty]
    private List<FunctionPluginViewModel> plugins;

    [ObservableProperty]
    private FunctionPluginViewModel? selectedEntry;

    [ObservableProperty]
    private ObservableObject? pluginView;

    public AllPluginsViewModel(IPluginService pluginService, IDependencyResolverService dependencyResolverService)
    {

        this.dependencyResolverService = dependencyResolverService;

        plugins = new();
        Plugins = pluginService.GetAvailablePlugins()
                               .Select(plugin => new FunctionPluginViewModel(plugin))
                               .Where(pluginView => pluginView is not null)
                               .OfType<FunctionPluginViewModel>()
                               .ToList();
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        if (e.PropertyName == nameof(SelectedEntry))
        {
            if (PluginView is null)
            {
                PluginView = dependencyResolverService.GetDependency<PluginViewModel>();
            }
            if (SelectedEntry is not null && PluginView is PluginViewModel viewModel)
            {
                viewModel.SetPlugin(SelectedEntry.Plugin);
            }


        }
    }



    [RelayCommand]
    private void Abort()
    {
        WeakReferenceMessenger.Default.Send(new CloseModalMessage(this));
    }
}
