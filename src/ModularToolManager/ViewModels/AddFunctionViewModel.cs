using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ModularToolManager.Models;
using ModularToolManager.Models.Messages;
using ModularToolManager.Services.Settings;
using ModularToolManager.Services.Ui;
using ModularToolManager.ViewModels.Settings;
using ModularToolManagerModel.Services.Functions;
using ModularToolManagerModel.Services.Plugin;
using ModularToolManagerPlugin.Models;
using ModularToolManagerPlugin.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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

    /// <summary>
    /// The service used to get plugins
    /// </summary>
    private readonly IPluginService? pluginService;

    /// <summary>
    /// The service used to get the function settings
    /// </summary>
    private readonly IFunctionSettingsService functionSettingsService;

    /// <summary>
    /// The function service to use
    /// </summary>
    private readonly IFunctionService? functionService;

    /// <summary>
    /// Service to use for opening windows or modals
    /// </summary>
    private readonly IWindowManagementService windowManagmentService;
    private readonly ISettingsService settingsService;
    private readonly PluginSettingViewModelService pluginSettingView;

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
    /// Are there any settings available for the plugin
    /// </summary>
    [ObservableProperty]
    private bool settingsAvailable;

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

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(PluginSettingsFound))]
    private List<IPluginSettingModel>? pluginSettings;

    /// <summary>
    /// Should the settings part be visible
    /// </summary>
    public bool PluginSettingsFound => PluginSettings?.Any() ?? false;

    /// <summary>
    /// The identifier to use for the function
    /// </summary>
    private string? identifier;

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    /// <param name="pluginService">The plugin service to use</param>
    /// <param name="functionService">The function service to use</param>
    public AddFunctionViewModel(IPluginService? pluginService,
                                IFunctionService? functionService,
                                IFunctionSettingsService functionSettingsService,
                                IWindowManagementService windowManagmentService,
                                ISettingsService settingsService,
                                PluginSettingViewModelService pluginSettingView)
    {
        functionPlugins = new();
        this.pluginService = pluginService;
        this.functionService = functionService;
        this.functionSettingsService = functionSettingsService;
        this.windowManagmentService = windowManagmentService;
        this.settingsService = settingsService;
        this.pluginSettingView = pluginSettingView;
        if (pluginService is not null)
        {
            functionPlugins.AddRange(pluginService!.GetAvailablePlugins()
                                                   .Select(plugin => new FunctionPluginViewModel(plugin)));
        }

        PropertyChanged += (_, e) =>
        {
            if (e.PropertyName == nameof(SelectedFunctionPlugin))
            {
                LoadPluginSettings();
            }
        };

        InitialValueSet();
    }

    /// <summary>
    /// Load all the possible settings for the plugin
    /// </summary>
    private void LoadPluginSettings()
    {
        if (SelectedFunctionPlugin is null || SelectedFunctionPlugin.Plugin is null)
        {
            return;
        }
        SelectedFunctionPlugin.Plugin.ResetSettings();
        List<IPluginSettingModel> settings = functionSettingsService.GetPluginSettingsValues(SelectedFunctionPlugin.Plugin, false)
                                                .OfType<SettingModel>()
                                                .Select(pluginSetting => pluginSettingView.GetViewModel(pluginSetting))
                                                .OfType<IPluginSettingModel>()
                                                .ToList();
        var appPluginSettings = settingsService.GetApplicationSettings().PluginSettings
                                  .FirstOrDefault(pSettings => pSettings.Plugin == SelectedFunctionPlugin.Plugin);
        if (appPluginSettings is not null)
        {
            foreach (var setting in appPluginSettings.Settings ?? Enumerable.Empty<SettingModel>())
            {
                var matchingSettingView = settings.FirstOrDefault(settingView => settingView.GetSettingsModel().Key == setting.Key);
                matchingSettingView?.UpdateValue(setting.Value);
            }
        }
        PluginSettings = settings;
    }

    /// <summary>
    /// Load on a function by the identifer to allow editing of the function
    /// </summary>
    /// <param name="identifier">The identifier of the function to edit</param>
    /// <returns>True if editing was successful</returns>
    public bool LoadInFunction(string identifier)
    {
        var function = functionService?.GetFunction(identifier);
        if (function is null)
        {
            return false;
        }
        this.identifier = identifier;
        DisplayName = function.DisplayName;
        Description = function.Description;
        SelectedFunctionPlugin = FunctionPlugins.FirstOrDefault(plugin => plugin.Plugin == function.Plugin);
        foreach (var setting in PluginSettings ?? Enumerable.Empty<IPluginSettingModel>())
        {
            var settingToUpdate = function.Settings?.FirstOrDefault(pSetting => pSetting.Key == setting.GetSettingsModel().Key);
            if (settingToUpdate is not null)
            {
                setting.UpdateValue(settingToUpdate.Value);
            }

        }
        FunctionParameters = function.Parameters;
        SelectedPath = function.Path;

        return true;
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
        var functionModel = identifier is null ? CreateNewFunctionModel() : CreateEditedFunctionModel();
        bool success = identifier is null ? functionService?.AddFunction(functionModel) ?? false : functionService?.ReplaceFunction(functionModel) ?? false;
        if (success)
        {
            AbortCommand.Execute(null);
        }
    }

    /// <summary>
    /// Create a new function model with a new identifier
    /// </summary>
    /// <returns></returns>
    private FunctionModel CreateNewFunctionModel()
    {
        return new FunctionModel
        {
            DisplayName = DisplayName!,
            Description = Description,
            Plugin = SelectedFunctionPlugin!.Plugin,
            Parameters = FunctionParameters!,
            Settings = GetPluginSettings(),
            Path = SelectedPath!,
            SortOrder = 0
        };
    }

    private IEnumerable<SettingModel> GetPluginSettings()
    {
        return PluginSettings?.Select(model => model.GetSettingsModel()) ?? Enumerable.Empty<SettingModel>();
    }

    /// <summary>
    /// Create a edited function model with given identifier
    /// </summary>
    /// <returns>The edited function model</returns>
    private FunctionModel CreateEditedFunctionModel()
    {
        return new FunctionModel
        {
            Id = identifier!,
            DisplayName = DisplayName!,
            Description = Description,
            Plugin = SelectedFunctionPlugin!.Plugin,
            Parameters = FunctionParameters!,
            Settings = GetPluginSettings(),
            Path = SelectedPath!,
            SortOrder = 0
        };
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
    private async Task OpenFunctionPath()
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
