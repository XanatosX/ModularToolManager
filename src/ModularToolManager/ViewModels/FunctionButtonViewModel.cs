using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ModularToolManager.Models;
using System.Reactive;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ModularToolManager.ViewModels;

/// <summary>
/// Class to use as a view model for a single function
/// </summary>
public partial class FunctionButtonViewModel : ObservableObject
{
    /// <summary>
    /// The identifier for this function button
    /// </summary>
    public string Identifier { get; }

    /// <summary>
    /// THe display name of the function
    /// </summary>
    [ObservableProperty]
    private string? displayName;

    /// <summary>
    /// The sort id to use for this function button
    /// </summary>
    [ObservableProperty]
    private int sortId;

    /// <summary>
    /// The description of the function
    /// </summary>
    [ObservableProperty]
    private string? description;

    /// <summary>
    /// The tool tip delay to use, a really high value is returned if no description is present
    /// </summary>
    public int ToolTipDelay => string.IsNullOrEmpty(Description) ? int.MaxValue : 400;

    /// <summary>
    /// Is the tooltip active right now
    /// </summary>
    [ObservableProperty]
    private bool isActive;

    /// <summary>
    /// The command to execute if function should run
    /// </summary>
    public ICommand ExecuteFunctionCommand { get; }

    /// <summary>
    /// Command to remove the entry
    /// </summary>
    public ICommand DeleteFunctionCommand { get; }

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    /// <param name="functionModel">The function model to use</param>
    public FunctionButtonViewModel(FunctionModel functionModel)
    {
        IsActive = true;
        Identifier = functionModel.UniqueIdentifier;
        DisplayName = functionModel.DisplayName;
        Description = functionModel.Description;
        SortId = functionModel.SortOrder;
        ExecuteFunctionCommand = new AsyncRelayCommand(async () => await Task.Run(() => functionModel?.Plugin?.Execute(functionModel.Parameters, functionModel.Path)));

        //@Todo replace to something else!
        DeleteFunctionCommand = new RelayCommand(() => IsActive = false);
        //DeleteFunctionCommand = ReactiveCommand.Create(() => { IsActive = false; });
    }
}
