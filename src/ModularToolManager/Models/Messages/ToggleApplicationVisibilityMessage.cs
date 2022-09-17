using CommunityToolkit.Mvvm.Messaging.Messages;

namespace ModularToolManager.Models.Messages;

/// <summary>
/// Message to toggle the visiblity of the application
/// </summary>
internal class ToggleApplicationVisibilityMessage : RequestMessage<bool>
{
	/// <summary>
	/// Should the application be hidden or shown
	/// </summary>
	public bool Hide { get; init; }

	/// <summary>
	/// Create a new instance of this message
	/// </summary>
	/// <param name="hide">Set to true if the application should be hidden</param>
	public ToggleApplicationVisibilityMessage(bool hide)
	{
		Hide = hide;
	}
}
