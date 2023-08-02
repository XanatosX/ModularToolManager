using ModularToolManager.Enums;
using ModularToolManager.Strategies.WindowPosition;

namespace ModularToolManager.Services.Ui;

/// <summary>
/// Factory to get a matching strategy for a requested window position
/// </summary>
public interface IWindowPositionFactory
{
    /// <summary>
    /// Get the strategy for the given window
    /// </summary>
    /// <param name="positionEnum">The position enum to use</param>
    /// <returns>The strategy to position the window on the given position</returns>
    IWindowPositionStrategy? GetWindowPositionStrategy(WindowPositionEnum positionEnum);
}
