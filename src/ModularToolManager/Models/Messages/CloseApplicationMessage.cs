using CommunityToolkit.Mvvm.Messaging.Messages;

namespace ModularToolManager.Models.Messages;

/// <summary>
/// Message to close the application
/// </summary>
internal class CloseApplicationMessage : RequestMessage<bool>
{
}
