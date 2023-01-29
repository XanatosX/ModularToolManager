using CommunityToolkit.Mvvm.Messaging.Messages;
using System.Globalization;

namespace ModularToolManager.Models.Messages;

/// <summary>
/// Message class to refresh the main window
/// </summary>
internal class RefreshMainWindow : ValueChangedMessage<CultureInfo>
{
    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    /// <param name="value">The new culture to use</param>
    public RefreshMainWindow(CultureInfo value) : base(value)
    {
    }
}
