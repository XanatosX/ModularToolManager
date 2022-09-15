using CommunityToolkit.Mvvm.Messaging.Messages;

namespace ModularToolManager.Models.Messages;
internal class DeleteFunctionMessage : RequestMessage<bool>
{
	public FunctionModel Function { get; init; }

	public DeleteFunctionMessage(FunctionModel functionModel)
	{
		Function = functionModel;
	}
}
