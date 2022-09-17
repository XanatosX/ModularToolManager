using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace ModularToolManager.Models.Messages;

/// <summary>
/// Command to close a given modal
/// </summary>
internal class CloseModalMessage : RequestMessage<bool>
{
	/// <summary>
	/// The modal to close
	/// </summary>
	public ObservableObject ModalToClose { get; init; }

	/// <summary>
	/// Create a new instance of this message
	/// </summary>
	/// <param name="objectToClose">The modal to close</param>
	public CloseModalMessage(ObservableObject objectToClose)
	{
		ModalToClose = objectToClose;
	}

}
