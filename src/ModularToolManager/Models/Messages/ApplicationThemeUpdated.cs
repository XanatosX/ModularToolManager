using CommunityToolkit.Mvvm.Messaging.Messages;

namespace ModularToolManager.Models.Messages;

/// <summary>
/// Mesage that the application theme got changed
/// </summary>
internal class ApplicationThemeUpdated : ValueChangedMessage<int>
{
    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    /// <param name="value">The theme id the application got updated to</param>
    public ApplicationThemeUpdated(int value) : base(value)
    {
    }
}
