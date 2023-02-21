using CommunityToolkit.Mvvm.Messaging.Messages;

namespace ModularToolManager.Models.Messages;

/// <summary>
/// Message if a modal window was opened or closed
/// </summary>
internal class ModalWindowOpened : ValueChangedMessage<bool>
{
    /// <summary>
    /// Create a new instance of this message
    /// </summary>
    /// <param name="isOpen">Bool if the modal window was opened (true) or closed (false)</param>
    public ModalWindowOpened(bool isOpen) : base(isOpen)
    {
    }
}
