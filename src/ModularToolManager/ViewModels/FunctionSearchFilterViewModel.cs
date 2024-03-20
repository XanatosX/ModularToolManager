using CommunityToolkit.Mvvm.ComponentModel;
using ModularToolManager.Strategies.Filters;

namespace ModularToolManager.ViewModels;

/// <summary>
/// View model to display a function search filter
/// </summary>
public partial class FunctionSearchFilterViewModel : ObservableObject
{
    /// <summary>
    /// The name for this entry
    /// </summary>
    [ObservableProperty]
    private string name;

    /// <summary>
    /// The description for this entry
    /// </summary>
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TooltipShowTime))]
    private string description;

    /// <summary>
    /// Time until the tooltip is getting shwon
    /// </summary>
    public int TooltipShowTime => string.IsNullOrEmpty(Description) ? int.MaxValue : 500;

    /// <summary>
    /// The key of this entry
    /// </summary>
    public string Key { get; }

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    /// <param name="name">The name of the search filter</param>
    /// <param name="description">The description of the search filter</param>
    /// <param name="filter">The filter which is represented by this data set</param>
    public FunctionSearchFilterViewModel(string name, string? description, IFunctionFilter filter)
    {
        this.name = name;
        this.description = description ?? string.Empty;
        Key = filter.GetType().Name; ;
    }
}
