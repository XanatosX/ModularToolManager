using CommunityToolkit.Mvvm.Messaging.Messages;

namespace ModularToolManager.Models.Messages;

/// <summary>
/// Message to toggle the order mode for the function buttons
/// </summary>
internal class ToggleOrderModeMessage : ValueChangedMessage<bool>
{
    /// <inheritdoc/>
    public ToggleOrderModeMessage(bool value) : base(value)
    {
    }
}
