using Avalonia;
using Avalonia.Controls;
using Avalonia.Platform;

namespace ModularToolManager.Strategies.WindowPosition;


/// </summary>
/// Strategy to position the given window in the bottom left corner of the screen.
/// </summary>
public class BottomLeftStrategy : IWindowPositionStrategy
{
    //<inheritdoc/>
    public void PositionWindow(Window window, Screen? screen)
    {
        if (screen is null)
        {
            return;
        }
        PixelRect workingArea = screen.WorkingArea;
        double newXPos = workingArea.X;
        double newYPos = workingArea.Bottom - window.Height;
        window.Position = new PixelPoint((int)newXPos, (int)newYPos);
    }
}
