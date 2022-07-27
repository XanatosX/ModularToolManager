using ModularToolManager.Services.Plugin;
using ModularToolManagerPlugin.Plugin;
using ReactiveUI;
using Splat;
using System.Collections.Generic;
using System.Linq;

namespace ModularToolManager.ViewModels;

/// <summary>
/// View model to add a new function to the application
/// </summary>
public class AddFunctionViewModel : ViewModelBase
{
    private readonly IPluginService pluginService;
    public List<FunctionPluginViewModel> FunctionServices => functionServices;
    private readonly List<FunctionPluginViewModel> functionServices;

    public FunctionPluginViewModel SelectedFunctionService
    {
        get => selectedFunctionService;
        set => this.RaiseAndSetIfChanged(ref selectedFunctionService, value);
    }
    private FunctionPluginViewModel selectedFunctionService;


    public AddFunctionViewModel()
    {
        pluginService = Locator.Current.GetService<IPluginService>();
        functionServices = pluginService.GetAvailablePlugins().Select(plugin => new FunctionPluginViewModel(plugin)).ToList();
    }
}
