using CommunityToolkit.Mvvm.ComponentModel;
using ModularToolManager.Models;

namespace ModularToolManager.ViewModels;

/// <summary>
/// View model for the application style 
/// </summary>
internal partial class ApplicationStyleViewModel : ObservableObject
{
    /// <summary>
    /// The application style to display
    /// </summary>
    private readonly ApplicationStyle applicationStyle;

    /// <summary>
    /// The id of the style 
    /// </summary>
    public int Id => applicationStyle.Id;

    /// <summary>
    /// The displayname of the style
    /// </summary>
    [ObservableProperty]
    private string displayName;

    /// <summary>
    /// The description of the style
    /// </summary>
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TooltipShowTime))]
    private string description;

    /// <summary>
    /// Time until the tooltip is getting shwon
    /// </summary>
    public int TooltipShowTime => string.IsNullOrEmpty(Description) ? int.MaxValue : 500;

    /// <summary>
    /// Create a new instance of this model
    /// </summary>
    /// <param name="applicationStyle">The application style to use</param>
    public ApplicationStyleViewModel(ApplicationStyle applicationStyle)
    {
        this.applicationStyle = applicationStyle;
        DisplayName = applicationStyle.Name ?? string.Empty;
        description = applicationStyle.Description ?? string.Empty;
    }
}
