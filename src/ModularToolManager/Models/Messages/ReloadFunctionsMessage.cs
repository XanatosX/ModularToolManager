using CommunityToolkit.Mvvm.Messaging.Messages;

namespace ModularToolManager.Models.Messages;

/// <summary>
/// Message to reload the functions
/// </summary>
internal class ReloadFunctionsMessage : RequestMessage<bool>
{
}
