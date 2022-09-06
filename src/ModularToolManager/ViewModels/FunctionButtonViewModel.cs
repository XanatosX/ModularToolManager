﻿using ModularToolManager.Models;
using ReactiveUI;
using System.Reactive;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ModularToolManager.ViewModels;

/// <summary>
/// Class to use as a view model for a single function
/// </summary>
public class FunctionButtonViewModel : ViewModelBase
{
    /// <summary>
    /// The function model to show
    /// </summary>
    private readonly FunctionModel functionModel;

    public string Identifier => functionModel.UniqueIdentifier;

    /// <summary>
    /// THe display name of the function
    /// </summary>
    public string DisplayName
    {
        get => functionModel.DisplayName;
        set
        {
            functionModel.DisplayName = value;
            this.RaisePropertyChanged(nameof(DisplayName));
        }
    }

    public int SortId
    {
        get => functionModel.SortOrder;
        set
        {
            functionModel.SortOrder = value;
            this.RaisePropertyChanged(nameof(SortId));
        }
    }

    /// <summary>
    /// The description of the function
    /// </summary>
    public string? Description
    {
        get => functionModel.Description;
        set
        {
            this.RaisePropertyChanged(nameof(Description));
            this.RaisePropertyChanged(nameof(ToolTipDelay));
            functionModel.Description = value;
        }
    }

    public int ToolTipDelay => string.IsNullOrEmpty(Description) ? int.MaxValue : 400;

    public bool IsActive { get; private set; }

    /// <summary>
    /// The command to execute if function should run
    /// </summary>
    public ICommand ExecuteFunctionCommand { get; }

    /// <summary>
    /// Command to remove the entry
    /// </summary>
    public ReactiveCommand<Unit, Unit> DeleteFunctionCommand { get; }

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    /// <param name="functionModel">The function model to use</param>
    public FunctionButtonViewModel(FunctionModel functionModel)
    {
        IsActive = true;
        this.functionModel = functionModel;
        ExecuteFunctionCommand = ReactiveCommand.Create(async () => await Task.Run(() => functionModel?.Plugin?.Execute(functionModel.Parameters, functionModel.Path)));

        DeleteFunctionCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            this.IsActive = false;
        });
        //DeleteFunctionCommand = ReactiveCommand.Create(() => IsActive = false);
    }
}
