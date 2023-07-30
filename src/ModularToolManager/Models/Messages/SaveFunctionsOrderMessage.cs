using CommunityToolkit.Mvvm.Messaging.Messages;

namespace ModularToolManager.Models.Messages;

/// <summary>
/// Message which does request a saving for the new function order
/// </summary>
internal class SaveFunctionsOrderMessage : RequestMessage<bool>
{
}
