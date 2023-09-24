using CommunityToolkit.Mvvm.Messaging.Messages;

namespace ModularToolManager.Models.Messages;

/// <summary>
/// Message if a function was executed,
/// does contain the status if it was successful
/// </summary>
internal class FunctionExecutedMessage : ValueChangedMessage<bool>
{
    /// <inheritdoc/>
    public FunctionExecutedMessage(bool value) : base(value)
    {
    }
}
