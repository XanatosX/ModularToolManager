using CommunityToolkit.Mvvm.Messaging.Messages;

namespace ModularToolManager.Models.Messages;

/// <summary>
/// Message to inform that a function was deleted
/// </summary>
internal class DeleteFunctionMessage : RequestMessage<bool>
{
	/// <summary>
	/// The function which got deleted
	/// </summary>
	public FunctionModel Function { get; init; }

	/// <summary>
	/// Create a new instance of this message
	/// </summary>
	/// <param name="functionModel">The function to delete</param>
	public DeleteFunctionMessage(FunctionModel functionModel)
	{
		Function = functionModel;
	}
}
