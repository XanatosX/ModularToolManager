using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using ModularToolManager.Models;
using ModularToolManager.Models.Messages;
using ModularToolManager.Services.Settings;
using ModularToolManager.Strategies.Filters;
using ModularToolManagerModel.Services.Dependency;
using ModularToolManagerModel.Services.Functions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

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
    /// The settings service to use
    /// </summary>
    private readonly ISettingsService settingsService;

    /// <summary>
    /// All the filters available for this application
    /// </summary>
    private readonly IEnumerable<IFunctionFilter> filters;

    /// <summary>
    /// Private all the possible functions currently available
    /// </summary>
    private readonly IList<FunctionButtonViewModel> functions;

    /// <summary>
    /// A list with all the filtered functions
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<FunctionButtonViewModel> filteredFunctions;

    /// <summary>
    /// A list with all the function names
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<string> functionNames;

    /// <summary>
    /// The search text used for the filtering of the plugins
    /// </summary>
    [ObservableProperty]
    private string? searchText;

    /// <summary>
    /// Is the application in the order mode
    /// </summary>
    [ObservableProperty]
    private bool inOrderMode;

    /// <summary>
    /// Service used to resolve dependencies
    /// </summary>
    private readonly IDependencyResolverService dependencyResolverService;

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    public FunctionSelectionViewModel(IFunctionService? functionService,
                                      IDependencyResolverService dependencyResolverService,
                                      ISettingsService settingsService,
                                      IEnumerable<IFunctionFilter> filters)
    {
        this.functionService = functionService;
        this.dependencyResolverService = dependencyResolverService;
        this.settingsService = settingsService;
        this.filters = filters;
        functions = new List<FunctionButtonViewModel>();
        filteredFunctions = new ObservableCollection<FunctionButtonViewModel>();
        functionNames = new ObservableCollection<string>();

        ReloadFunctions();

        WeakReferenceMessenger.Default.Register<DeleteFunctionMessage>(this, (_, e) =>
        {
            functionService?.DeleteFunction(e.Identifier);
            ReloadFunctions();
        });
        WeakReferenceMessenger.Default.Register<FunctionExecutedMessage>(this, (_, e) =>
        {
            ApplicationSettings settings = settingsService.GetApplicationSettings();
            if (e.Value && settings.ClearSearchAfterFunctionExecute)
            {
                SearchText = string.Empty;
            }
        });
        WeakReferenceMessenger.Default.Register<ToggleOrderModeMessage>(this, (_, e) =>
        {
            if (e.Value)
            {
                SearchText = string.Empty;
            }
            InOrderMode = e.Value;
        });
        WeakReferenceMessenger.Default.Register<SaveFunctionsOrderMessage>(this, (_, saveFunctionMessage) => SaveFunctionsOrder(saveFunctionMessage));
        WeakReferenceMessenger.Default.Register<ReloadFunctionsMessage>(this, (_, _) => ReloadFunctions());
    }

    /// <summary>
    /// Save the order for the functions to the function service
    /// </summary>
    /// <param name="saveFunctionMessage">The message to save the functions</param>
    private void SaveFunctionsOrder(SaveFunctionsOrderMessage saveFunctionMessage)
    {
        if (saveFunctionMessage.HasReceivedResponse)
        {
            return;
        }
        foreach (var functionView in functions)
        {
            functionService?.UpdateFunction(functionView.Identifier, function => function.SortOrder = functionView.SortId);
        }

        saveFunctionMessage.Reply(true);
    }

    /// <inheritdoc/>
    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        Action action = e.PropertyName == nameof(SearchText) ? () => FilterFunctionList() : () => { };
        action();
        base.OnPropertyChanged(e);
    }

    /// <summary>
    /// Filter the function list for displaying
    /// </summary>
    private void FilterFunctionList()
    {
        IFunctionFilter filter = GetFunctionFilter();
        IEnumerable<FunctionButtonViewModel> tempFiltered = filter.GetFiltered(functions, SearchText)
                                                                     .OrderBy(function => function.SortId)
                                                                     .ThenBy(function => function.DisplayName);
        FilteredFunctions.Clear();
        foreach (var function in tempFiltered)
        {
            FilteredFunctions.Add(function);
        }
    }

    private IFunctionFilter GetFunctionFilter()
    {
        var applicationSettings = settingsService.GetApplicationSettings();
        return filters.Where(filter => filter.GetType().Name == applicationSettings.SearchFilterTypeName).FirstOrDefault() ?? filters.First();
    }

    /// <summary>
    /// Reload the functions of the application
    /// </summary>
    public void ReloadFunctions()
    {
        functions.Clear();
        FunctionNames.Clear();
        foreach (FunctionButtonViewModel? functionViewModel in functionService?.GetAvailableFunctions()
                                                                               .Select(function => dependencyResolverService.GetDependency<FunctionButtonViewModel>(createdObject => createdObject?.SetFunctionModel(function))) ?? Enumerable.Empty<FunctionButtonViewModel?>())
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
            if (settingsService.GetApplicationSettings().EnableAutocompleteForFunctionSearch && functionViewModel.DisplayName is not null)
            {
                FunctionNames.Add(functionViewModel.DisplayName);
            }
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
