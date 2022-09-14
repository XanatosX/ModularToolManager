using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ModularToolManager.Models.Messages;
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
	/// Create a new instance of this class
	/// </summary>
	public AppViewModel()
	{
		ShowApplicationCommand = new RelayCommand(() => WeakReferenceMessenger.Default.Send(new ToggleApplicationVisibilityMessage(false)));
		ExitApplicationCommand = new RelayCommand(() => WeakReferenceMessenger.Default.Send(new CloseApplicationMessage()));
	}
}
