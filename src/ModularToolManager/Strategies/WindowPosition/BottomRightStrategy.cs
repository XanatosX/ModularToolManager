using Avalonia;
using Avalonia.Controls;
using Avalonia.Platform;

namespace ModularToolManager.Strategies.WindowPosition;

/// <summary>
/// Position a Window in the bottom right of a given screen
/// </summary>
public class BottomRightStrategy : IWindowPositionStrategy
{
    /// <inheritdoc/>
    public void PositionWindow(Window window, Screen? screen)
    {
        if (screen is null)
        {
            return;
        }
        PixelRect workingArea = screen.WorkingArea;
        double newXPos = workingArea.X + workingArea.Width - window.Width;
        double newYPos = workingArea.Y + workingArea.Height - window.Height;
        window.Position = new PixelPoint((int)newXPos, (int)newYPos);
    }
}
