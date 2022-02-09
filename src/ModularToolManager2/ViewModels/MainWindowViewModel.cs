using ReactiveUI;
using System.Reactive;
using System.Windows.Input;

namespace ModularToolManager2.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ViewModelBase MainContentModel { get; }

        public ICommand SelectLanguageCommand { get; }

        public ICommand OpenSettingsCommand { get; }

        public ICommand NewFunctionCommand { get; }

        public ICommand ExitApplicationCommand { get; }

        public ICommand ReportBugCommand { get; }

        public Interaction<Unit, Unit> CloseWindowInteraction { get; }


        public MainWindowViewModel()
        {
            MainContentModel = new FunctionSelectionViewModel();
            CloseWindowInteraction = new Interaction<Unit, Unit>();
            ExitApplicationCommand = ReactiveCommand.Create(() => CloseWindowInteraction?.Handle(new Unit()));
        }
    }
}
