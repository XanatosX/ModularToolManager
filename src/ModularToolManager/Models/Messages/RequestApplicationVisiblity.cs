using CommunityToolkit.Mvvm.Messaging.Messages;

namespace ModularToolManager.Models.Messages;

/// <summary>
/// Get the current visiblilty for the application
/// </summary>
internal class RequestApplicationVisiblity : RequestMessage<bool> { }
