using CommunityToolkit.Mvvm.Messaging.Messages;

namespace ModularToolManager.Models.Messages;
internal class FunctionExecutedMessage : ValueChangedMessage<bool>
{
    public FunctionExecutedMessage(bool value) : base(value)
    {
    }
}
