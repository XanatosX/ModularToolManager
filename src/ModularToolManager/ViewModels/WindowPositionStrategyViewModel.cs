using CommunityToolkit.Mvvm.ComponentModel;
using ModularToolManager.Enums;

namespace ModularToolManager.ViewModels;

/// <summary>
/// View model for the window position strategy
/// </summary>
public partial class WindowPositionStrategyViewModel : ObservableObject
{
    /// <summary>
    /// The display name of the window position strategy
    /// </summary>
    [ObservableProperty]
    private string displayName;

    /// <summary>
    /// The stored window position enum
    /// </summary>
    public WindowPositionEnum WindowPosition {get; init;}

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    /// <param name="windowPositionEnum">The window position enum to create a view model for</param>
    public WindowPositionStrategyViewModel(WindowPositionEnum windowPositionEnum)
    {
        WindowPosition = windowPositionEnum;
        DisplayName = windowPositionEnum switch
        {
            WindowPositionEnum.TopLeft => Properties.Resources.Settings_WindowPosition_Top_Left,
            WindowPositionEnum.TopRight => Properties.Resources.Settings_WindowPosition_Top_Right,
            WindowPositionEnum.BottomLeft => Properties.Resources.Settings_WindowPosition_Bottom_Left,
            WindowPositionEnum.BottomRight => Properties.Resources.Settings_WindowPosition_Bottom_Right,
            _ => ""
        };
    }

}
