using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ModularToolManager.Models;
using ModularToolManager.Models.Messages;
using ModularToolManager.Services.Settings;
using ModularToolManager.Services.Ui;
using ModularToolManager.Views;

namespace ModularToolManager.ViewModels;

/// <summary>
/// View model for the settings of the application
/// </summary>
internal partial class SettingsViewModel : ObservableObject
{
	private readonly ISettingsService settingsService;

	[ObservableProperty]
	private bool topMost;

	[ObservableProperty]
	private bool startMinimized;

	[ObservableProperty]
	private bool showInTaskbar;

	public SettingsViewModel(ISettingsService settingsService)
	{
		this.settingsService = settingsService;
		ApplicationSettings appSettings = settingsService.GetApplicationSettings();
		TopMost = appSettings.AlwaysOnTop;
		StartMinimized = appSettings.StartMinimized;
		ShowInTaskbar = appSettings.ShowInTaskbar;
	}

	[RelayCommand]
	private void Ok()
	{
		ApplicationSettings appSettings = settingsService.GetApplicationSettings();
		appSettings.StartMinimized = StartMinimized;
		appSettings.ShowInTaskbar = ShowInTaskbar;
		appSettings.AlwaysOnTop = topMost;
		if (settingsService.SaveApplicationSettings(appSettings))
		{
			Abort();
		}
	}

	[RelayCommand]
	private void Abort()
	{
		WeakReferenceMessenger.Default.Send(new CloseModalMessage(this));
	}
}
