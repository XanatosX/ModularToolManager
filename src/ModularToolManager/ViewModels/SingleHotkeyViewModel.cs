using CommunityToolkit.Mvvm.ComponentModel;
using ModularToolManager.Models;
using System.Collections.Generic;
using System.Linq;

namespace ModularToolManager.ViewModels;
internal partial class SingleHotkeyViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Name))]
    [NotifyPropertyChangedFor(nameof(Description))]
    [NotifyPropertyChangedFor(nameof(WorkingOn))]
    [NotifyPropertyChangedFor(nameof(Keys))]
    [NotifyPropertyChangedFor(nameof(ToolTipShowDelay))]
    [NotifyPropertyChangedFor(nameof(WorkingOnComplete))]
    private HotkeyModel hotkey;

    public string? Name => hotkey?.Name;

    public string? Description => hotkey?.Description;

    public string? WorkingOn => hotkey?.WorkingOn;

    public string? WorkingOnComplete => Properties.Resources.Hotkey_Abort_WorkingOn_Prefix_Colon + hotkey?.WorkingOn;

    public int ToolTipShowDelay => string.IsNullOrWhiteSpace(WorkingOn) ? int.MaxValue : 500;

    public List<KeyboardKeyViewModel>? Keys => hotkey?.Keys?.Select((button, index) => new KeyboardKeyViewModel(button, index != 0 ? "+" : string.Empty, string.Empty))
                                                            .ToList();

    public SingleHotkeyViewModel(HotkeyModel hotkey)
    {
        Hotkey = hotkey;
    }
}
