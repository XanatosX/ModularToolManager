using Avalonia.Controls;
using Avalonia.Platform;

namespace ModularToolManager.Strategies.WindowPosition;

/// <summary>
/// Strategy to position a window on the screen
/// </summary>
public interface IWindowPositionStrategy
{
    /// <summary>
    /// Position the given window on the screen
    /// </summary>
    /// <param name="window">The window which should be moved</param>
    /// <param name="screen">The screen to position the window on</param>
    void PositionWindow(Window window, Screen? screen);
}
