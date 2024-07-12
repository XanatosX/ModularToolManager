using ModularToolManager.Enums;
using ModularToolManager.Strategies.WindowPosition;

namespace ModularToolManager.Services.Ui;

/// <summary>
/// Default factory for getting the window position strategy
/// </summary>
public class DefaultWindowPositionStrategyFactory : IWindowPositionFactory
{
    /// <inheritdoc/>
    public IWindowPositionStrategy? GetWindowPositionStrategy(WindowPositionEnum positionEnum)
    {
        return positionEnum switch
        {
            WindowPositionEnum.BottomRight => new BottomRightStrategy(),
            WindowPositionEnum.BottomLeft => new BottomLeftStrategy(),
            WindowPositionEnum.TopLeft => new TopLeftStrategy(),
            WindowPositionEnum.TopRight => new TopRightStrategy(),
            _ => new BottomRightStrategy()
        };
    }
}
