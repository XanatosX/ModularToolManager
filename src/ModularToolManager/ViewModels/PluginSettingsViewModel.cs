﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ModularToolManager.Models;
using ModularToolManager.Services.Settings;
using ModularToolManager.Services.Ui;
using ModularToolManager.ViewModels.Settings;
using ModularToolManagerPlugin.Enums;
using ModularToolManagerPlugin.Models;
using ModularToolManagerPlugin.Plugin;
using ModularToolManagerPlugin.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ModularToolManager.ViewModels;

/// <summary>
/// View model for the settings for a given plugin
/// </summary>
internal partial class PluginSettingsViewModel : ObservableObject
{
	/// <summary>
	/// The service used to load the settings for a plugin or to set them
	/// </summary>
	private readonly IFunctionSettingsService pluginSettingService;

	/// <summary>
	/// Service used to load setting for the application
	/// </summary>
	private readonly ISettingsService applicationSettingService;

	/// <summary>
	/// Service to use to get correct view for settings model
	/// </summary>
	private readonly PluginSettingViewModelService pluginSettingView;

	/// <summary>
	/// The settings of the plugin
	/// </summary>
	[ObservableProperty]
	private ObservableCollection<IPluginSettingModel> pluginSettings;

	/// <summary>
	/// The current plugin this settings view belongs to
	/// </summary>
	private IFunctionPlugin? currentPlugin;

	/// <summary>
	/// Create a new instance of this class
	/// </summary>
	/// <param name="pluginSettingService">The plugin service to use for loading plugin settings</param>
	/// <param name="applicationSettingService">The service used to load application settings and sync plugin settings with</param>
	public PluginSettingsViewModel(IFunctionSettingsService pluginSettingService,
		ISettingsService applicationSettingService,
		PluginSettingViewModelService pluginSettingView)
	{
		this.pluginSettingService = pluginSettingService;
		this.applicationSettingService = applicationSettingService;
		this.pluginSettingView = pluginSettingView;
		pluginSettings = new ObservableCollection<IPluginSettingModel>();
	}

	/// <summary>
	/// Set the plugin for the setting view
	/// </summary>
	/// <param name="plugin">The plugin to show the settings for</param>
	public void SetPlugin(IFunctionPlugin plugin)
	{
		currentPlugin = plugin;
		PluginSettings applicationPluginSettings = applicationSettingService.GetApplicationSettings()?.PluginSettings.FirstOrDefault(setting => setting?.Plugin?.GetType() == plugin.GetType()) ?? new();
		List<SettingModel> persistantSettings = applicationPluginSettings.Settings ?? new();
		List<IPluginSettingModel> settings = pluginSettingService.GetPluginSettingsValues(plugin)
												   .Select(setting => GetViewModel(setting, persistantSettings))
												   .Where(view => view is not null)
												   .OfType<IPluginSettingModel>()
												   .ToList();



		PluginSettings.Clear();
		foreach (var setting in settings)
		{
			PluginSettings.Add(setting);
		}

	}

	/// <summary>
	/// Get the view model for a given setting model configuration
	/// </summary>
	/// <param name="settingModel">The setting model to get the view from</param>
	/// <param name="pluginSettings">The loaded plugin settings stored for the application</param>
	/// <returns>A setting model for the setting model</returns>
	private IPluginSettingModel? GetViewModel(SettingModel settingModel, List<SettingModel> pluginSettings)
	{
		var matchingSetting = pluginSettings.FirstOrDefault(setting => setting.Key == settingModel.Key);
		settingModel.SetValue(matchingSetting is not null ? matchingSetting.Value : settingModel.Value);
		return pluginSettingView.GetViewModel(settingModel);
	}

	/// <summary>
	/// Save the settings into the application settings
	/// </summary>
	[RelayCommand]
	private void SaveSettings()
	{
		List<SettingModel> settingsToSave = PluginSettings.Select(setting => setting.GetSettingsModel())
													.Select(model => new SettingModel(model.Value) { Key = model.Key, Type = model.Type })
													.Where(model => model.Key is not null)
													.ToList();
		PluginSettings newPluginSettings = new()
		{
			Plugin = currentPlugin,
			Settings = settingsToSave
		};
		applicationSettingService.ChangeSettings(settings =>
		{
			settings.AddPluginSettings(newPluginSettings);
		});
	}
}
