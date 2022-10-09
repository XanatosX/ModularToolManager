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
	private List<IPluginSettingModel> pluginSettings;

	private IFunctionPlugin? currentPlugin;

	public PluginSettingsViewModel(IFunctionSettingsService settingsService, ISettingsService settingsSerializeService)
	{
		this.settingsService = settingsService;
		this.settingsSerializeService = settingsSerializeService;
		pluginSettings = new List<IPluginSettingModel>();
	}

	public void SetPlugin(IFunctionPlugin plugin)
	{
		currentPlugin = plugin;
		List<IPluginSettingModel> settings = settingsService.GetPluginSettingsValues(plugin)
												   .Select(setting => GetViewModel(setting))
												   .Where(view => view is not null)
												   .OfType<IPluginSettingModel>()
												   .ToList();
		PluginSettings.Clear();
		PluginSettings.AddRange(settings);

	}

	private IPluginSettingModel? GetViewModel(SettingModel settingModel)
	{
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
