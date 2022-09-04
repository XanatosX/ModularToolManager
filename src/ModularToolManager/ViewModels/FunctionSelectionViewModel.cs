using ModularToolManagerModel.Services.Functions;
using System.Collections.ObjectModel;
using System.Linq;

namespace ModularToolManager.ViewModels;

/// <summary>
/// View model to select a function
/// </summary>
internal class FunctionSelectionViewModel : ViewModelBase
{
    /// <summary>
    /// The service to use for function selection
    /// </summary>
    private readonly IFunctionService? functionService;

    /// <summary>
    /// All the possible functions currently available
    /// </summary>
    public ObservableCollection<FunctionButtonViewModel> AllFunctions { get; set; }

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    public FunctionSelectionViewModel(IFunctionService? functionService)
    {
        this.functionService = functionService;
        AllFunctions = new ObservableCollection<FunctionButtonViewModel>(
                functionService?.GetAvailableFunctions().Select(function => new FunctionButtonViewModel(function))
            );
    }
}
