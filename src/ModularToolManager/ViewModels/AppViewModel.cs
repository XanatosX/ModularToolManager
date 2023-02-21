using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Logging;
using ModularToolManager.Models.Messages;
using Serilog;
using System.Windows.Input;

namespace ModularToolManager.ViewModels;

/// <summary>
/// View model for the app itself
/// </summary>
public class AppViewModel : ObservableObject
{
	/// <summary>
	/// Command to exit the application
	/// </summary>
	public ICommand ExitApplicationCommand { get; }

	/// <summary>
	/// Command to show the application of currently hidden
	/// </summary>
	public ICommand ShowApplicationCommand { get; }

	/// <summary>
	/// The current number of modal windows which are open
	/// </summary>
	private int numberOfOpenModalWindows;

	/// <summary>
	/// Logger to use for the app view
	/// </summary>
	private readonly ILogger<AppViewModel> logger;

	/// <summary>
	/// Create a new instance of this class
	/// </summary>
	public AppViewModel(ILogger<AppViewModel> logger)
	{
		numberOfOpenModalWindows = 0;

		WeakReferenceMessenger.Default.Register<ModalWindowOpened>(this, (_, message) => numberOfOpenModalWindows += message.Value ? 1 : -1);

		ShowApplicationCommand = new RelayCommand(() =>
		{
			if (numberOfOpenModalWindows > 0)
			{
				logger.LogWarning($"Tried to minimize app while {numberOfOpenModalWindows} where opend");
				return;
			}
			var response = WeakReferenceMessenger.Default.Send(new RequestApplicationVisiblity());
			bool toggleMode = response.HasReceivedResponse ? response.Response : false;
			WeakReferenceMessenger.Default.Send(new ToggleApplicationVisibilityMessage(toggleMode));
		});
		ExitApplicationCommand = new RelayCommand(() => WeakReferenceMessenger.Default.Send(new CloseApplicationMessage()));
		this.logger = logger;
	}
}
