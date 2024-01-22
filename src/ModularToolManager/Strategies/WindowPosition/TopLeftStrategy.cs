using Avalonia;
using Avalonia.Controls;
using Avalonia.Platform;

namespace ModularToolManager.Strategies.WindowPosition;

/// <summary>
/// Position a Window in the bottom left of a given screen
/// </summary>
public class TopLeftStrategy : IWindowPositionStrategy
{
    /// <inheritdoc/>
    public void PositionWindow(Window window, Screen? screen)
    {
        if (screen is null )
        {
            return;
        }

        PixelRect workingArea = screen.WorkingArea;
        double newXPos = workingArea.X;
        double newYPos = workingArea.Y;
        window.Position = new PixelPoint((int)newXPos, (int)newYPos);
    }
}
