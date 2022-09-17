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
    public ReadOnlyObservableCollection<FunctionButtonViewModel> AllFunctions => allFunctions;

    /// <summary>
    /// Private all the possible functions currently available
    /// </summary>
    private readonly ReadOnlyObservableCollection<FunctionButtonViewModel> allFunctions;

    /// <summary>
    /// Private all the possible functions currently available
    /// </summary>
    private readonly SourceList<FunctionButtonViewModel> allAvailableFunctions;

    /// <summary>
    /// The search text used for the filtering of the plugins
    /// </summary>
    [Reactive]
    public string? SearchText { get; set; }

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

    /// <summary>
    /// Reload the functions of the application
    /// </summary>
    public void ReloadFunctions()
    {
        foreach (FunctionButtonViewModel? functionViewModel in functionService?.GetAvailableFunctions().Select(function => new FunctionButtonViewModel(function)) ?? Enumerable.Empty<FunctionButtonViewModel?>())
        {
            if (functionViewModel is null || allAvailableFunctions is null)
            {
                continue;
            }
            if (allAvailableFunctions.Items.Select(item => item.Identifier).Contains(functionViewModel?.Identifier))
            {
                continue;
            }
            allAvailableFunctions!.Add(functionViewModel);
        }
    }
}
