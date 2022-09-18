using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using ModularToolManager.Models;
using ModularToolManager.Models.Messages;
using ModularToolManager.Services.Settings;
namespace ModularToolManager.ViewModels;

/// <summary>
/// View model for the settings of the application
/// </summary>
internal partial class SettingsViewModel : ObservableObject
{
	/// <summary>
	/// The settings service to use
	/// </summary>
	private readonly ISettingsService settingsService;

	/// <summary>
	/// Baking field for the top most checkbox
	/// </summary>
	[ObservableProperty]
	private bool topMost;

	/// <summary>
	/// Baking field if should be started minimitzed
	/// </summary>
	[ObservableProperty]
	private bool startMinimized;

	/// <summary>
	/// Baking field if should be shown in taskbar
	/// </summary>
	[ObservableProperty]
	private bool showInTaskbar;

	/// <summary>
	/// Create a new instance of this class
	/// </summary>
	/// <param name="settingsService">The settings service to use</param>
	public SettingsViewModel(ISettingsService settingsService)
	{
		this.settingsService = settingsService;
		ApplicationSettings appSettings = settingsService.GetApplicationSettings();
		TopMost = appSettings.AlwaysOnTop;
		StartMinimized = appSettings.StartMinimized;
		ShowInTaskbar = appSettings.ShowInTaskbar;
	}

	/// <summary>
	/// The ok button to save and confirm the changes
	/// </summary>
	[RelayCommand]
	private void Ok()
	{
		ApplicationSettings appSettings = settingsService.GetApplicationSettings();
		appSettings.StartMinimized = StartMinimized;
		appSettings.ShowInTaskbar = ShowInTaskbar;
		appSettings.AlwaysOnTop = topMost;
		if (settingsService.SaveApplicationSettings(appSettings))
		{
			WeakReferenceMessenger.Default.Send(new ValueChangedMessage<ApplicationSettings>(appSettings));
			Abort();
		}
	}

	/// <summary>
	/// The abort button to discard the changes and close the modal
	/// </summary>
	[RelayCommand]
	private void Abort()
	{
		WeakReferenceMessenger.Default.Send(new CloseModalMessage(this));
	}
}
