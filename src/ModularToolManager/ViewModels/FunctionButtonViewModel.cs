using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Logging;
using ModularToolManager.Models;
using ModularToolManager.Models.Messages;
using ModularToolManager.Services.Settings;
using ModularToolManagerPlugin.Attributes;
using ModularToolManagerPlugin.Models;
using ModularToolManagerPlugin.Plugin;
using ModularToolManagerPlugin.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModularToolManager.ViewModels;

/// <summary>
/// Class to use as a view model for a single function
/// </summary>
public partial class FunctionButtonViewModel : ObservableObject
{
    /// <summary>
    /// Message to use if function execution did fail
    /// </summary>
    private const string FUNCTION_EXECUTION_FAILED_MESSAGE = "Could not execute function {1} with identifier {0} ";

    /// <summary>
    /// The function model to display
    /// </summary>
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(EditFunctionCommand), nameof(DeleteFunctionCommand))]
    [NotifyPropertyChangedFor(nameof(Identifier), nameof(DisplayName), nameof(SortId), nameof(Description))]
    private FunctionModel? functionModel;

    /// <summary>
    /// The identifier for this function button
    /// </summary>
    public string Identifier => functionModel?.UniqueIdentifier ?? string.Empty;

    /// <summary>
    /// The display name of the function
    /// </summary>
    public string? DisplayName => functionModel?.DisplayName;

    /// <summary>
    /// The sort id to use for this function button
    /// </summary>
    public int SortId => functionModel?.SortOrder ?? 0;

    /// <summary>
    /// The description of the function
    /// </summary>
    public string? Description => functionModel?.Description;

    /// <summary>
    /// The logger to use
    /// </summary>
    private readonly ILogger<FunctionButtonViewModel> logger;
    private readonly ISettingsService settingsService;
    private readonly IFunctionSettingsService functionSettingsService;

    /// <summary>
    /// The tool tip delay to use, a really high value is returned if no description is present
    /// </summary>
    public int ToolTipDelay => string.IsNullOrEmpty(Description) ? int.MaxValue : 400;

    /// <summary>
    /// Is the tooltip active right now
    /// </summary>
    [ObservableProperty]
    private bool isActive;

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    /// <param name="functionModel">The function model to use</param>
    public FunctionButtonViewModel(ILogger<FunctionButtonViewModel> logger, ISettingsService settingsService, IFunctionSettingsService functionSettingsService)
    {
        IsActive = true;
        this.logger = logger;
        this.settingsService = settingsService;
        this.functionSettingsService = functionSettingsService;
    }

    /// <summary>
    /// Set the function model for this button
    /// </summary>
    /// <param name="functionModel">The function model to use</param>
    public void SetFunctionModel(FunctionModel functionModel)
    {
        this.functionModel = functionModel;
    }

    /// <summary>
    /// Command to exetute the current function
    /// </summary>
    /// <returns>A empty task to await until execution is complete</returns>
    [RelayCommand]
    private async Task ExecuteFunction()
    {
        try
        {
            var plugin = functionModel?.Plugin;
            if (plugin is null)
            {
                return;
            }
            ApplyPluginSettings(plugin);
            await Task.Run(() => functionModel?.Plugin?.Execute(functionModel.Parameters, functionModel.Path));
        }
        catch (System.Exception e)
        {
            logger.LogError(FUNCTION_EXECUTION_FAILED_MESSAGE, Identifier, DisplayName);
            logger.LogError(e, null);
        }
    }

    /// <summary>
    /// Method to apply the plugin settings
    /// </summary>
    /// <param name="plugin">The plugin to apply the settings for</param>
    private void ApplyPluginSettings(IFunctionPlugin? plugin)
    {
        if (plugin is null)
        {
            return;
        }
        plugin.ResetSettings();

        List<SettingAttribute> pluginSettings = functionSettingsService.GetPluginSettings(plugin).ToList() ?? new();
        var settings = settingsService.GetApplicationSettings().PluginSettings.FirstOrDefault(setting => setting?.Plugin?.GetType() == functionModel?.Plugin?.GetType());
        foreach (var loadedPluginSetting in settings?.Settings?.Select(setting => setting.GetSettingModel()) ?? Enumerable.Empty<SettingModel>())
        {
            var matchingAttribute = pluginSettings.FirstOrDefault(setting => setting.Key == loadedPluginSetting.Key);
            if (matchingAttribute is null)
            {
                continue;
            }
            switch (loadedPluginSetting.Type)
            {
                case ModularToolManagerPlugin.Enums.SettingType.Boolean:
                    functionSettingsService.SetSettingValue(matchingAttribute, plugin, loadedPluginSetting.GetData<bool>());
                    break;
                case ModularToolManagerPlugin.Enums.SettingType.String:
                    functionSettingsService.SetSettingValue(matchingAttribute, plugin, loadedPluginSetting.GetData<string>());
                    break;
                case ModularToolManagerPlugin.Enums.SettingType.Int:
                    functionSettingsService.SetSettingValue(matchingAttribute, plugin, loadedPluginSetting.GetData<int>());
                    break;
                case ModularToolManagerPlugin.Enums.SettingType.Float:
                    functionSettingsService.SetSettingValue(matchingAttribute, plugin, loadedPluginSetting.GetData<float>());
                    break;
                default:
                    break;
            }
        }
    }

    private void GetSettingModelFromDataSet()
    {

    }

    /// <summary>
    /// Command to edit the current function
    /// </summary>
    /// <returns>A waitable task</returns>
    [RelayCommand(CanExecute = nameof(CanEditOrDeleteFunction))]
    private async Task EditFunction()
    {
        bool result = false;
        try
        {
            result = await WeakReferenceMessenger.Default.Send(new EditFunctionMessage(functionModel!));
        }
        catch (System.Exception e)
        {
            logger.LogError(e, null);
        }
    }

    /// <summary>
    /// Command to delete the current function
    /// </summary>
    [RelayCommand(CanExecute = nameof(CanEditOrDeleteFunction))]
    private void DeleteFunction()
    {
        IsActive = false;
        WeakReferenceMessenger.Default.Send(new DeleteFunctionMessage(functionModel!));
    }

    /// <summary>
    /// Can the function be edited or deleted
    /// </summary>
    /// <returns>True if model is present</returns>
    private bool CanEditOrDeleteFunction()
    {
        return functionModel is not null;
    }
}
