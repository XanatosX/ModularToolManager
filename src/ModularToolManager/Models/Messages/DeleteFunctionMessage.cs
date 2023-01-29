using CommunityToolkit.Mvvm.Messaging.Messages;

namespace ModularToolManager.Models.Messages;

/// <summary>
/// Message to inform that a function was deleted
/// </summary>
internal class DeleteFunctionMessage : RequestMessage<bool>
{
	/// <summary>
	/// The function Identifier to delete
	/// </summary>
	public string Identifier { get; init; }

	/// <summary>
	/// Create a new instance of this message
	/// </summary>
	/// <param name="functionModel">The function to delete</param>
	public DeleteFunctionMessage(FunctionModel functionModel)
	{
		Identifier = functionModel.UniqueIdentifier;
	}
}
