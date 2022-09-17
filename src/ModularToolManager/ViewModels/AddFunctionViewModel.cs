using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ModularToolManager.Models;
using ModularToolManager.Models.Messages;
using ModularToolManager.Services.Ui;
using ModularToolManagerModel.Services.Functions;
using ModularToolManagerModel.Services.Plugin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ModularToolManager.ViewModels;

/// <summary>
/// View model to add a new function to the application
/// </summary>
internal partial class AddFunctionViewModel : ObservableValidator
{
    /// <summary>
    /// A list with all the function plugin possiblities
    /// </summary>
    public List<FunctionPluginViewModel> FunctionPlugins => functionPlugins;

    /// <summary>
    /// Private list for all the function plugins
    /// </summary>
    private readonly List<FunctionPluginViewModel> functionPlugins;
    private readonly IFunctionService? functionService;

    /// <summary>
    /// Service to use for opening windows or modals
    /// </summary>
    private readonly IWindowManagementService windowManagmentService;

    /// <summary>
    /// The display name of the function
    /// </summary>
    [Required]
    [MinLength(5), MaxLength(25)]
    [NotifyDataErrorInfo]
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(OkCommand))]
    private string? displayName;

    /// <summary>
    /// The description of the function model
    /// </summary>
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(OkCommand))]
    private string? description;

    /// <summary>
    /// The currenctly selected plugin for the function
    /// </summary>
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(OkCommand))]
    [NotifyCanExecuteChangedFor(nameof(OpenFunctionPathCommand))]
    private FunctionPluginViewModel? selectedFunctionPlugin;

    /// <summary>
    /// The parameters for the function
    /// </summary>
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(OkCommand))]
    private string? functionParameters;

    /// <summary>
    /// The currently selected path
    /// </summary>
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(OkCommand))]
    private string? selectedPath;

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    /// <param name="pluginService">The plugin service to use</param>
    /// <param name="functionService">The function service to use</param>
    public AddFunctionViewModel(IPluginService? pluginService, IFunctionService? functionService, IWindowManagementService windowManagmentService)
    {
        functionPlugins = new();
        this.functionService = functionService;
        this.windowManagmentService = windowManagmentService;
        if (pluginService is not null)
        {
            functionPlugins.AddRange(pluginService!.GetAvailablePlugins()
                                                   .Select(plugin => new FunctionPluginViewModel(plugin)));
        }

        InitialValueSet();
    }

    /// <summary>
    /// The method will set some values as initialization, this is required to kick off the field validation
    /// </summary>
    private void InitialValueSet()
    {
        DisplayName = string.Empty;
        Description = string.Empty;
        FunctionParameters = string.Empty;
        SelectedPath = string.Empty;
    }

    /// <summary>
    /// Command to close current modal window
    /// </summary>
    [RelayCommand]
    private void Abort()
    {
        WeakReferenceMessenger.Default.Send(new CloseModalMessage(this));
    }

    /// <summary>
    /// Command to save current function
    /// </summary>
    [RelayCommand(CanExecute = nameof(CanExecuteOk))]
    private void Ok()
    {
        var functionModel = new FunctionModel
        {
            DisplayName = DisplayName!,
            Description = Description,
            Plugin = SelectedFunctionPlugin!.Plugin,
            Parameters = FunctionParameters!,
            Path = SelectedPath!,
            SortOrder = 0

        };
        bool success = functionService?.AddFunction(functionModel) ?? false;
        if (success)
        {
            AbortCommand.Execute(null);
        }
    }

    /// <summary>
    /// Can the okay command be executed
    /// </summary>
    /// <returns>True if execution is possible</returns>
    private bool CanExecuteOk()
    {
        bool valid = DisplayName?.Length >= 5 && DisplayName?.Length <= 25;
        valid &= SelectedFunctionPlugin is not null && SelectedFunctionPlugin.Plugin is not null;
        valid &= File.Exists(SelectedPath);
        if (valid)
        {
            FileInfo info = new FileInfo(SelectedPath ?? String.Empty);
            valid &= SelectedFunctionPlugin!.Plugin!.GetAllowedFileEndings().Select(fileExtension => fileExtension.Extension.ToLower())
                                                                                                           .Any(ending => ending == info.Extension.TrimStart('.').ToLowerInvariant());
        }
        return valid;
    }

    /// <summary>
    /// Command to open the allow selecting a path to execute by the function
    /// </summary>
    [RelayCommand(CanExecute = nameof(CanOpenFunctionPath))]
    private async void OpenFunctionPath()
    {
        var fileDialogs = SelectedFunctionPlugin?.Plugin?.GetAllowedFileEndings().Select(fileEnding => new FileDialogFilter
        {
            Extensions = new List<string> { fileEnding.Extension },
            Name = fileEnding.Name
        }).ToList();
        if (fileDialogs is null)
        {
            return;
        }
        ShowOpenFileDialogModel openConfig = new ShowOpenFileDialogModel(fileDialogs, null, false);
        var files = await windowManagmentService.ShowOpenFileDialogAsync(openConfig);
        string file = files.FirstOrDefault(string.Empty);
        if (string.IsNullOrEmpty(file))
        {
            return;
        }
        SelectedPath = file;
    }

    /// <summary>
    /// Can the open function path command be called
    /// </summary>
    /// <returns>True if callable</returns>
    private bool CanOpenFunctionPath()
    {
        return SelectedFunctionPlugin is not null;
    }
}
