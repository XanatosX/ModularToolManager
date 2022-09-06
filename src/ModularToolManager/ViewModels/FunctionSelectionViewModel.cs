using Avalonia.Threading;
using DynamicData;
using DynamicData.Binding;
using ModularToolManagerModel.Services.Functions;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;

namespace ModularToolManager.ViewModels;

/// <summary>
/// View model to select a function
/// </summary>
public class FunctionSelectionViewModel : ViewModelBase
{
    /// <summary>
    /// The service to use for function selection
    /// </summary>
    private readonly IFunctionService? functionService;

    /// <summary>
    /// All the possible functions currently available
    /// </summary>
    //public ObservableCollection<FunctionButtonViewModel> AllFunctions { get; set; }


    /// <summary>
    /// All the possible functions currently available
    /// </summary>
    public ReadOnlyObservableCollection<FunctionButtonViewModel> AllFunctions => allFunctions;

    /// <summary>
    /// Private all the possible functions currently available
    /// </summary>
    private readonly ReadOnlyObservableCollection<FunctionButtonViewModel> allFunctions;

    /// <summary>
    /// Private all the possible functions currently available
    /// </summary>
    private readonly SourceList<FunctionButtonViewModel> allAvailableFunctions;

    [Reactive]
    public string SearchText { get; set; }

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    public FunctionSelectionViewModel(IFunctionService? functionService)
    {
        this.functionService = functionService;
        allAvailableFunctions = new SourceList<FunctionButtonViewModel>();

        IObservable<Func<FunctionButtonViewModel, bool>> filter = this.WhenAnyValue(x => x.SearchText)
                                                    .Throttle(TimeSpan.FromMilliseconds(200))
                                                    .ObserveOn(RxApp.MainThreadScheduler)
                                                    .Select(CreateFilter);

        allAvailableFunctions.Connect()
                             .Filter(filter)
                             .Sort(SortExpressionComparer<FunctionButtonViewModel>.Ascending(g => g.SortId))
                             .ObserveOn(AvaloniaScheduler.Instance)
                             .Bind(out allFunctions)
                             .AutoRefreshOnObservable(x => x.DeleteFunctionCommand)
                             .Select(_ => allAvailableFunctions.Items.FirstOrDefault(item => !item.IsActive))
                             .Subscribe(inactive =>
                             {
                                 if (inactive is null)
                                 {
                                     return;
                                 }
                                 functionService?.DeleteFunction(inactive.Identifier);
                                 var deleted = allAvailableFunctions.Items.Where(x => functionService?.GetFunction(x.Identifier) is null).ToList();
                                 foreach (var model in deleted)
                                 {
                                     allAvailableFunctions.Remove(model);
                                 }
                             });

        ReloadFunctions();

        //.AutoRefreshOnObservable(x => x.IsActive)
        //.Select(x => )
    }

    /// <summary>
    /// Method to build the search text filter
    /// </summary>
    /// <param name="text">The text to search for</param>
    /// <returns>A func which can be used for filtering</returns>
    private Func<FunctionButtonViewModel, bool> CreateFilter(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return t => true;
        }
        return t => t.DisplayName.Contains(text, StringComparison.OrdinalIgnoreCase);
    }

    public void ReloadFunctions()
    {
        //allAvailableFunctions.Clear();
        foreach (var item in functionService?.GetAvailableFunctions().Select(function => new FunctionButtonViewModel(function)))
        {
            allAvailableFunctions.Add(item);
        }
    }
}
