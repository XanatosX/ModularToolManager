using ModularToolManager.Models;
using ReactiveUI;
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

    /// <summary>
    /// The command to execute if function should run
    /// </summary>
    public ICommand ExecuteFunctionCommand { get; }

    /// <summary>
    /// THe display name of the function
    /// </summary>
    public string DisplayName
    {
        get => functionModel.DisplayName;
        set
        {
            this.RaisePropertyChanged("DisplayName");
            functionModel.DisplayName = value;
        }
    }

    /// <summary>
    /// The description of the function
    /// </summary>
    public string Description
    {
        get => functionModel.Description;
        set
        {
            this.RaisePropertyChanged("Description");
            functionModel.Description = value;
        }
    }

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    /// <param name="functionModel">The function model to use</param>
    public FunctionButtonViewModel(FunctionModel functionModel)
    {
        this.functionModel = functionModel;
        ExecuteFunctionCommand = ReactiveCommand.Create(async () => await Task.Run(() => functionModel?.Plugin?.Execute(functionModel.Parameters, functionModel.Path)));
    }
}
