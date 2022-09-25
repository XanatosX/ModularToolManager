using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ModularToolManager.Models.Messages;
using ModularToolManagerModel.Services.Dependency;
using ModularToolManagerModel.Services.Plugin;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ModularToolManager.ViewModels;

/// <summary>
/// Class to use to show a list with all the plugins
/// </summary>
internal partial class AllPluginsViewModel : ObservableObject
{
    /// <summary>
    /// The resolver to use for getting new instances of classes
    /// </summary>
    private readonly IDependencyResolverService dependencyResolverService;

    /// <summary>
    /// All the possible plugins
    /// </summary>
    [ObservableProperty]
    private List<FunctionPluginViewModel> plugins;

    /// <summary>
    /// The currently selected plugin
    /// </summary>
    [ObservableProperty]
    private FunctionPluginViewModel? selectedEntry;

    /// <summary>
    /// The plugin view to use
    /// </summary>
    [ObservableProperty]
    private ObservableObject? pluginView;

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    /// <param name="pluginService">The plugin service to use</param>
    /// <param name="dependencyResolverService">The resolver to use for getting the plugins</param>
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

    /// <inheritdoc/>
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

    /// <summary>
    /// Command to use to close the modal
    /// </summary>
    [RelayCommand]
    private void Abort()
    {
        WeakReferenceMessenger.Default.Send(new CloseModalMessage(this));
    }
}
