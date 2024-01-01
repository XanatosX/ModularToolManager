using CommunityToolkit.Mvvm.Messaging.Messages;

namespace ModularToolManager.Models.Messages;

/// <summary>
/// Get the current visibility for the application
/// </summary>
internal class RequestApplicationVisibility : RequestMessage<bool> { }
