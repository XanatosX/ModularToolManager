using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ModularToolManager.Models.Messages;
using ModularToolManagerModel.Services.Functions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;

namespace ModularToolManager.ViewModels;

/// <summary>
/// View model to select a function
/// </summary>
public partial class FunctionSelectionViewModel : ObservableObject, IDisposable
{
    /// <summary>
    /// Was the class already disposed
    /// </summary>
    private bool isDisposed;

    /// <summary>
    /// The service to use for function selection
    /// </summary>
    private readonly IFunctionService? functionService;

    /// <summary>
    /// Private all the possible functions currently available
    /// </summary>
    private readonly IList<FunctionButtonViewModel> functions;

    [ObservableProperty]
    private ObservableCollection<FunctionButtonViewModel> filteredFunctions;

    /// <summary>
    /// The search text used for the filtering of the plugins
    /// </summary>
    [ObservableProperty]
    private string? searchText;

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    public FunctionSelectionViewModel(IFunctionService? functionService)
    {
        this.functionService = functionService;
        functions = new List<FunctionButtonViewModel>();
        filteredFunctions = new ObservableCollection<FunctionButtonViewModel>();

        ReloadFunctions();

        WeakReferenceMessenger.Default.Register<DeleteFunctionMessage>(this, (_, e) =>
        {
            functionService?.DeleteFunction(e.Identifier);
            ReloadFunctions();
        });

        WeakReferenceMessenger.Default.Register<ReloadFunctionsMessage>(this, (_, _) => ReloadFunctions());
    }

    /// <inheritdoc/>
    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        Action action = e.PropertyName == nameof(SearchText) ? () => FilterFunctionList() : () => { return; };
        action();
        base.OnPropertyChanged(e);
    }

    /// <summary>
    /// Filter the function list for displaying
    /// </summary>
    private void FilterFunctionList()
    {
        IEnumerable<FunctionButtonViewModel> tempFiltered = functions.Where(function => string.IsNullOrEmpty(SearchText) || (function.DisplayName ?? string.Empty).ToLower().Contains(SearchText.ToLower()))
                                                                     .OrderBy(function => function.SortId);
        FilteredFunctions.Clear();
        foreach (var function in tempFiltered)
        {
            FilteredFunctions.Add(function);
        }
    }

    /// <summary>
    /// Reload the functions of the application
    /// </summary>
    public void ReloadFunctions()
    {
        functions.Clear();
        foreach (FunctionButtonViewModel? functionViewModel in functionService?.GetAvailableFunctions().Select(function => new FunctionButtonViewModel(function)) ?? Enumerable.Empty<FunctionButtonViewModel?>())
        {
            if (functionViewModel is null || functions is null)
            {
                continue;
            }
            if (functions.Any(item => item.Identifier == functionViewModel?.Identifier))
            {
                continue;
            }
            functions.Add(functionViewModel);
        }
        FilterFunctionList();
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (isDisposed)
        {
            return;
        }
        WeakReferenceMessenger.Default.UnregisterAll(this);
        isDisposed = true;
    }

    /// <summary>
    /// Finalizer to make sure to unregister all the message hooks
    /// </summary>
    ~FunctionSelectionViewModel()
    {
        Dispose();
    }
}
