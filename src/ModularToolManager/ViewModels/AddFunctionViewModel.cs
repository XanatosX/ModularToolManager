using Avalonia.Controls;
using ModularToolManager.Models;
using ModularToolManager.Services.Ui;
using ModularToolManager.ViewModels.Extenions;
using ModularToolManagerModel.Services.Functions;
using ModularToolManagerModel.Services.Plugin;
using ModularToolManagerPlugin.Plugin;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Input;

namespace ModularToolManager.ViewModels;

/// <summary>
/// View model to add a new function to the application
/// </summary>
internal class AddFunctionViewModel : ViewModelBase, IModalWindowEvents
{
    /// <summary>
    /// Service to use to manage plugins
    /// </summary>
    private readonly IPluginService? pluginService;

    /// <summary>
    /// Service to use for loading functions
    /// </summary>
    private readonly IFunctionService? functionService;
    private readonly IWindowManagmentService windowManagmentService;

    /// <summary>
    /// A list with all the function plugin possiblities
    /// </summary>
    public List<FunctionPluginViewModel> FunctionPlugins => functionPlugins;

    /// <summary>
    /// Private list for all the function plugins
    /// </summary>
    private readonly List<FunctionPluginViewModel> functionPlugins;

    /// <summary>
    /// The current function model
    /// </summary>
    private FunctionModel functionModel;

    /// <summary>
    /// The display name of the new function
    /// </summary>
    [MinLength(5), MaxLength(25)]
    public string DisplayName
    {
        get => functionModel.DisplayName;
        set
        {
            functionModel.DisplayName = value;
            this.RaisePropertyChanged(nameof(DisplayName));
        }
    }

    public string Description
    {
        get => functionModel.Description;
        set
        {
            functionModel.Description = value;
            this.RaisePropertyChanged(nameof(Description));
        }
    }

    /// <summary>
    /// The currenctly selected plugin for the function
    /// </summary>
    public FunctionPluginViewModel? SelectedFunctionPlugin
    {
        get => functionModel.Plugin is null ? null : new FunctionPluginViewModel(functionModel.Plugin!);
        set
        {
            functionModel.Plugin = value?.Plugin;
            this.RaisePropertyChanged(nameof(SelectedFunctionPlugin));
        }
    }

    /// <summary>
    /// The parameters for the function
    /// </summary>
    public string FunctionParameters
    {
        get => functionModel.Parameters;
        set
        {
            functionModel.Parameters = value;
            this.RaisePropertyChanged(nameof(FunctionParameters));
        }
    }

    /// <summary>
    /// The currently selected path
    /// </summary>
    public string SelectedPath
    {
        get => functionModel.Path;
        set
        {
            functionModel.Path = value;
            this.RaisePropertyChanged(nameof(SelectedPath));
        }
    }

    /// <summary>
    /// Command used to add the new function
    /// </summary>
    public ICommand OkCommand { get; }

    /// <summary>
    /// Command used to abord the current changes or addition
    /// </summary>
    public ICommand AbortCommand { get; }

    /// <summary>
    /// Command to use for opening a gile
    /// </summary>
    public ICommand OpenFunctionPathCommand { get; }

    /// <summary>
    /// Event if the window is getting a close requested
    /// </summary>
    public event EventHandler? Closing;

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    /// <param name="pluginService">The plugin service to use</param>
    /// <param name="functionService">The function service to use</param>
    public AddFunctionViewModel(IPluginService? pluginService, IFunctionService? functionService, IWindowManagmentService windowManagmentService)
    {
        functionModel = new FunctionModel();
        this.pluginService = pluginService;
        this.functionService = functionService;
        this.windowManagmentService = windowManagmentService;
        functionPlugins = new();
        if (pluginService is not null)
        {
            functionPlugins = pluginService.GetAvailablePlugins()
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
                                                                x => x.SelectedFunctionPlugin,
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
                                                                                                               .Any(ending => ending == info.Extension.TrimStart('.').ToLowerInvariant());
                                                                    }

                                                                    return valid;
                                                                });

        AbortCommand = ReactiveCommand.Create(() => Closing?.Invoke(this, EventArgs.Empty));
        OkCommand = ReactiveCommand.Create(() =>
        {
            bool success = functionService?.AddFunction(functionModel) ?? false;
            if (success)
            {
                Closing?.Invoke(this, EventArgs.Empty);
            }
        }, canSave);

        IObservable<bool> canOpenFile = this.WhenAnyValue(x => x.SelectedFunctionPlugin,
                                                          (FunctionPluginViewModel? plugin) => plugin is not null);

        OpenFunctionPathCommand = ReactiveCommand.Create(async () =>
        {
            var openConfig = new ShowOpenFileDialogModel(SelectedFunctionPlugin.Plugin.GetAllowedFileEndings().Select(fileEnding => new FileDialogFilter()
            {
                Extensions = new List<string>() { fileEnding.Extension },
                Name = fileEnding.Name
            }).ToList(), null, false);
            var files = await windowManagmentService?.ShowOpenFileDialogAsync(openConfig) ?? new string[0];
            string file = files.FirstOrDefault(string.Empty);
            if (string.IsNullOrEmpty(file))
            {
                return;
            }
            SelectedPath = file;
        }, canOpenFile);
    }
}
