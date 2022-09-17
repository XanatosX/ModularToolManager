using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ModularToolManager.Models;
using ModularToolManager.Models.Messages;
using System.Threading.Tasks;

namespace ModularToolManager.ViewModels;

/// <summary>
/// Class to use as a view model for a single function
/// </summary>
public partial class FunctionButtonViewModel : ObservableObject
{
    /// <summary>
    /// The function model to display
    /// </summary>
    private readonly FunctionModel functionModel;

    /// <summary>
    /// The identifier for this function button
    /// </summary>
    public string Identifier => functionModel.UniqueIdentifier;

    /// <summary>
    /// The display name of the function
    /// </summary>
    public string? DisplayName => functionModel.DisplayName;

    /// <summary>
    /// The sort id to use for this function button
    /// </summary>
    public int SortId => functionModel.SortOrder;

    /// <summary>
    /// The description of the function
    /// </summary>
    public string? Description => functionModel.Description;

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
    /// Create a new instance of this class
    /// </summary>
    /// <param name="functionModel">The function model to use</param>
    public FunctionButtonViewModel(FunctionModel functionModel)
    {
        IsActive = true;
        this.functionModel = functionModel;
    }

    /// <summary>
    /// Command to exetute the current function
    /// </summary>
    /// <returns>A empty task to await until execution is complete</returns>
    [RelayCommand]
    private async Task ExecuteFunction()
    {
        await Task.Run(() => functionModel?.Plugin?.Execute(functionModel.Parameters, functionModel.Path));
    }

    /// <summary>
    /// Command to edit the current function
    /// </summary>
    /// <returns>A waitable task</returns>
    [RelayCommand]
    private async Task EditFunction()
    {
        bool result = false;
        try
        {
            result = await WeakReferenceMessenger.Default.Send(new EditFunctionMessage(functionModel));
        }
        catch (System.Exception)
        {
            //No result from the request returned!
        }
    }

    /// <summary>
    /// Command to delete the current function
    /// </summary>
    [RelayCommand]
    private void DeleteFunction()
    {
        IsActive = false;
        WeakReferenceMessenger.Default.Send(new DeleteFunctionMessage(functionModel));
    }
}
