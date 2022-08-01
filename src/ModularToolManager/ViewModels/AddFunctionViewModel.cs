using ModularToolManager.Models;
using ModularToolManager.Services.Plugin;
using ModularToolManagerPlugin.Plugin;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;

namespace ModularToolManager.ViewModels;

/// <summary>
/// View model to add a new function to the application
/// </summary>
public class AddFunctionViewModel : ViewModelBase
{
    private readonly IPluginService pluginService;
    public List<FunctionPluginViewModel> FunctionServices => functionServices;
    private readonly List<FunctionPluginViewModel> functionServices;

    private FunctionModel functionModel;

    [MinLength(5), MaxLength(25)]
    public string DisplayName
    {
        get => functionModel.DisplayName;
        set
        {
            functionModel.DisplayName = value;
            this.RaisePropertyChanged("DisplayName");
        }
    }

    public FunctionPluginViewModel? SelectedFunctionService
    {
        get => functionModel.Plugin is null ? null : new FunctionPluginViewModel(functionModel.Plugin!);
        set
        {
            functionModel.Plugin = value?.Plugin;
            this.RaisePropertyChanged("SelectedFunctionService");
        }
    }

    public string FunctionParameters
    {
        get => functionModel.Parameters;
        set
        {
            functionModel.Parameters = value;
            this.RaisePropertyChanged("FunctionParameters");
        }
    }

    public string SelectedPath
    {
        get => functionModel.Path;
        set
        {
            functionModel.Path = value;
            this.RaisePropertyChanged("SelectedPath");
        }
    }

    public AddFunctionViewModel()
    {
        functionModel = new FunctionModel();
        pluginService = Locator.Current.GetService<IPluginService>();
        functionServices = pluginService.GetAvailablePlugins().Select(plugin => new FunctionPluginViewModel(plugin)).ToList();
        this.WhenAnyValue(x => x.SelectedPath)
            .Throttle(TimeSpan.FromSeconds(2))
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(path => SelectedPath = path.Trim());

        this.WhenAnyValue(x => x.FunctionParameters)
            .Throttle(TimeSpan.FromSeconds(2))
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(parameters => FunctionParameters = parameters.Trim());

        this.WhenAnyValue(x => x.DisplayName)
            .Throttle(TimeSpan.FromSeconds(2))
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(displayName => DisplayName = displayName.Trim());
    }
}
