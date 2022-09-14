using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using DynamicData;
using DynamicData.Binding;
using ModularToolManagerModel.Services.Functions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace ModularToolManager.ViewModels;

/// <summary>
/// View model to select a function
/// </summary>
public partial class FunctionSelectionViewModel : ObservableObject
{
    /// <summary>
    /// The service to use for function selection
    /// </summary>
    private readonly IFunctionService? functionService;

    /// <summary>
    /// Private all the possible functions currently available
    /// </summary>
    [ObservableProperty]
    private ReadOnlyObservableCollection<FunctionButtonViewModel> allFunctions;

    /// <summary>
    /// Private all the possible functions currently available
    /// </summary>
    private readonly SourceList<FunctionButtonViewModel> allAvailableFunctions;

    /// <summary>
    /// The search text used for the filtering of the plugins
    /// </summary>
    [ObservableProperty]
    public string? searchText;

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    public FunctionSelectionViewModel(IFunctionService? functionService)
    {
        this.functionService = functionService;
        allAvailableFunctions = new SourceList<FunctionButtonViewModel>();

        var subject = new Subject<FunctionButtonViewModel>();
        var dymFilter = subject.Select(sub => (Func<FunctionButtonViewModel, bool>)(CreateFilter(SearchText)));


        //@Note: Not updating ...
        allAvailableFunctions.Connect()
                             //.Filter(dymFilter)
                             .Sort(SortExpressionComparer<FunctionButtonViewModel>.Ascending(g => g.SortId))
                             .ObserveOn(AvaloniaScheduler.Instance)
                             .Bind(out allFunctions)
                             .AutoRefresh()
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
                                 OnPropertyChanged(nameof(AllFunctions));
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
            allAvailableFunctions.Add(functionViewModel);
        }
    }
}
