using CommunityToolkit.Mvvm.ComponentModel;
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

	[ObservableProperty]
	private List<ObservableObject> pluginSettings;

	public PluginSettingsViewModel(IFunctionSettingsService settingsService)
	{
		this.settingsService = settingsService;
		pluginSettings = new List<ObservableObject>();
	}

	public void SetPlugin(IFunctionPlugin plugin)
	{
		List<ObservableObject> settings = settingsService.GetPluginSettingsValues(plugin)
												   .Select(setting => GetViewModel(setting))
												   .Where(view => view is not null)
												   .OfType<ObservableObject>()
												   .ToList();
		PluginSettings.Clear();
		PluginSettings.AddRange(settings);

	}

	private ObservableObject? GetViewModel(SettingModel settingModel)
	{
		return settingModel.Type switch
		{
			SettingType.Boolean => new BoolPluginSettingViewModel(settingModel),
			_ => null
		};
	}
}
