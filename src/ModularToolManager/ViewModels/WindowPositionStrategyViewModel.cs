using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using ModularToolManager.Enums;

namespace ModularToolManager.ViewModels;

public partial class WindowPositionStrategyViewModel : ObservableObject
{
    [ObservableProperty]
    private string displayName;

    public WindowPositionEnum WindowPosition {get; init;}

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
