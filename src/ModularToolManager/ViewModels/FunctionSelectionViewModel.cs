using ModularToolManager.Models;
using ModularToolManager.Services.Functions;
using Splat;
using System.Collections.ObjectModel;

namespace ModularToolManager.ViewModels;

/// <summary>
/// View model to select a function
/// </summary>
internal class FunctionSelectionViewModel : ViewModelBase
{
    private readonly IFunctionService? functionService;

    public ObservableCollection<FunctionModel> AllFunctions { get; set; }

    public FunctionSelectionViewModel()
    {
        functionService = Locator.Current.GetService<IFunctionService>();
        AllFunctions = new ObservableCollection<FunctionModel>(functionService?.GetAvailableFunctions());
    }
}
