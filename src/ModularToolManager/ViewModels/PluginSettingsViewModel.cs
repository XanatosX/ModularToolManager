using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ModularToolManager.Models;
using ModularToolManager.Services.Serialization;
using ModularToolManager.Services.Settings;
using ModularToolManager.ViewModels.Settings;
using ModularToolManagerPlugin.Attributes;
using ModularToolManagerPlugin.Enums;
using ModularToolManagerPlugin.Models;
using ModularToolManagerPlugin.Plugin;
using ModularToolManagerPlugin.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularToolManager.ViewModels;
internal partial class PluginSettingsViewModel : ObservableObject
{
	private readonly IFunctionSettingsService settingsService;
	private readonly ISettingsService settingsSerializeService;

	[ObservableProperty]
	private ObservableCollection<IPluginSettingModel> pluginSettings;

	private IFunctionPlugin? currentPlugin;

	public PluginSettingsViewModel(IFunctionSettingsService settingsService, ISettingsService settingsSerializeService)
	{
		this.settingsService = settingsService;
		this.settingsSerializeService = settingsSerializeService;
		pluginSettings = new ObservableCollection<IPluginSettingModel>();
	}

	public void SetPlugin(IFunctionPlugin plugin)
	{
		currentPlugin = plugin;
		PluginSettings pluginSettings = settingsSerializeService.GetApplicationSettings()?.PluginSettings.FirstOrDefault(setting => setting?.Plugin?.GetType() == plugin.GetType()) ?? new();
		List<PersistantPluginSetting> persistantSettings = pluginSettings.Settings ?? new();
		List<IPluginSettingModel> settings = settingsService.GetPluginSettingsValues(plugin)
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

	private IPluginSettingModel? GetViewModel(SettingModel settingModel, List<PersistantPluginSetting> pluginSettings)
	{
		var matchingSetting = pluginSettings.FirstOrDefault(setting => setting.Key == settingModel.Key)?.GetSettingModel();
		settingModel.SetValue(matchingSetting is not null ? matchingSetting.Value : settingModel.Value);
		return settingModel.Type switch
		{
			SettingType.Boolean => new BoolPluginSettingViewModel(settingModel),
			_ => null
		};
	}

	[RelayCommand]
	private void SaveSettings()
	{
		List<PersistantPluginSetting> settingsToSave = PluginSettings.Select(setting => setting.GetSettingsModel())
													.Select(model => new PersistantPluginSetting(model))
													.Where(model => model.Key is not null)
													.ToList();
		PluginSettings pluginSettings = new PluginSettings()
		{
			Plugin = currentPlugin,
			Settings = settingsToSave
		};
		settingsSerializeService.ChangeSettings(settings =>
		{
			settings.AddPluginSettings(pluginSettings);
		});

	}
}
