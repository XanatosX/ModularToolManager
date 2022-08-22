using ModularToolManager.Models;
using ReactiveUI;
using System.Windows.Input;

namespace ModularToolManager.ViewModels
{
    internal class FunctionButtonViewModel : ViewModelBase
    {
        private readonly FunctionModel functionModel;

        public ICommand ExecuteFunctionCommand { get; }

        public string DisplayName
        {
            get => functionModel.DisplayName;
            set
            {
                this.RaisePropertyChanged("DisplayName");
                functionModel.DisplayName = value;
            }
        }

        public string Description
        {
            get => functionModel.Description;
            set
            {
                this.RaisePropertyChanged("Description");
                functionModel.Description = value;
            }
        }

        public FunctionButtonViewModel(FunctionModel functionModel)
        {
            this.functionModel = functionModel;

            ExecuteFunctionCommand = ReactiveCommand.Create(async () => functionModel?.Plugin?.Execute(functionModel.Parameters, functionModel.Path));
        }
    }
}
