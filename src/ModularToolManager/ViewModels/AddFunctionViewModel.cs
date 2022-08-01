using Avalonia.Controls;
using ModularToolManager.Models;
using ModularToolManager.Services.Functions;
using ModularToolManager.Services.Plugin;
using ModularToolManager.ViewModels.Extenions;
using ModularToolManagerPlugin.Models;
using ModularToolManagerPlugin.Plugin;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ModularToolManager.ViewModels;

/// <summary>
/// View model to add a new function to the application
/// </summary>
public class AddFunctionViewModel : ViewModelBase, IModalWindowEvents
{
    private readonly IPluginService? pluginService;
    private readonly IFunctionService? functionService;

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
    public ICommand OkCommand { get; }

    public ICommand AbortCommand { get; }

    public event EventHandler Closing;


    public AddFunctionViewModel()
    {
        functionModel = new FunctionModel();
        pluginService = Locator.Current.GetService<IPluginService>();
        functionService = Locator.Current.GetService<IFunctionService>();
        if (pluginService is not null)
        {
            functionServices = pluginService.GetAvailablePlugins()
                                            .Select(plugin => new FunctionPluginViewModel(plugin))
                                            .ToList();
        }

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

        IObservable<bool> canSave = this.WhenAnyValue(x => x.DisplayName,
                                                                x => x.SelectedFunctionService,
                                                                x => x.FunctionParameters,
                                                                x => x.SelectedPath,
                                                                (name, selectedFunction, parameters, path) =>
                                                                {
                                                                    IFunctionPlugin plugin = selectedFunction?.Plugin;
                                                                    bool valid = name.Length >= 5 && name.Length <= 25;
                                                                    valid &= plugin is not null;
                                                                    valid &= File.Exists(SelectedPath);
                                                                    if (valid)
                                                                    {
                                                                        FileInfo info = new FileInfo(SelectedPath);
                                                                        valid &= plugin.GetAllowedFileEndings().Select(fileExtension => fileExtension.Extension.ToLower())
                                                                                                               .Any(ending => ending == info.Extension.ToLower());
                                                                    }

                                                                    return valid;
                                                                });

        AbortCommand = ReactiveCommand.Create(async () => Closing?.Invoke(this, EventArgs.Empty));
        OkCommand = ReactiveCommand.Create(async () =>
        {
            functionService?.AddFunction(functionModel);
            Closing?.Invoke(this, EventArgs.Empty);
        }, canSave);
    }
}
