using CommunityToolkit.Mvvm.Messaging.Messages;

namespace ModularToolManager.Models.Messages;

/// <summary>
/// Class to inform that a function should be edited
/// </summary>
internal class EditFunctionMessage : AsyncRequestMessage<bool>
{
	/// <summary>
	/// The unique identifier for the function to edit
	/// </summary>
	public string Identifier;

	/// <summary>
	/// Create a new instance of this class
	/// </summary>
	/// <param name="functionModel">The function model to edit</param>
	public EditFunctionMessage(FunctionModel functionModel)
	{
		Identifier = functionModel.UniqueIdentifier;
	}
}
