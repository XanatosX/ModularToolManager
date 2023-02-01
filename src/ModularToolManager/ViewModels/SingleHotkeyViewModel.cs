using CommunityToolkit.Mvvm.ComponentModel;
using ModularToolManager.Models;
using System.Collections.Generic;
using System.Linq;

namespace ModularToolManager.ViewModels;

/// <summary>
/// Model class for a single hotkey line on the hotkey view
/// </summary>
internal partial class SingleHotkeyViewModel : ObservableObject
{
    /// <summary>
    /// The hotkey to show
    /// </summary>
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Name))]
    [NotifyPropertyChangedFor(nameof(Description))]
    [NotifyPropertyChangedFor(nameof(WorkingOn))]
    [NotifyPropertyChangedFor(nameof(Keys))]
    [NotifyPropertyChangedFor(nameof(ToolTipShowDelay))]
    [NotifyPropertyChangedFor(nameof(WorkingOnComplete))]
    private HotkeyModel hotkey;

    /// <summary>
    /// The name of the hotkey
    /// </summary>
    public string? Name => hotkey?.Name;

    /// <summary>
    /// The description for the hotkey
    /// </summary>
    public string? Description => hotkey?.Description;

    /// <summary>
    /// Where does this hotkey work
    /// </summary>
    public string? WorkingOn => hotkey?.WorkingOn;

    /// <summary>
    /// Complete text for the "Where does the hotkey work to display in the ui"
    /// </summary>
    public string? WorkingOnComplete => $"{Properties.Resources.Hotkey_Abort_WorkingOn_Prefix_Colon} {hotkey?.WorkingOn}";

    /// <summary>
    /// The deplay for the tooltip, set to max if no WorkinOn text is provided
    /// </summary>
    public int ToolTipShowDelay => string.IsNullOrWhiteSpace(WorkingOn) ? int.MaxValue : 500;

    /// <summary>
    /// All the keys to show
    /// </summary>
    public List<KeyboardKeyViewModel>? Keys => hotkey?.Keys?.Select((button, index) => new KeyboardKeyViewModel(button, index != 0 ? "+" : string.Empty, string.Empty))
                                                            .ToList();

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    /// <param name="hotkey">The hotkey to display in this single line</param>
    public SingleHotkeyViewModel(HotkeyModel hotkey)
    {
        Hotkey = hotkey;
    }
}
