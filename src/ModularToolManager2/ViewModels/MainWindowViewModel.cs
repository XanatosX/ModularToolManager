using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
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

        public Interaction<ModalWindowViewModel, Unit> ShowModalWindowInteraction { get; }


        public MainWindowViewModel()
        {
            MainContentModel = new FunctionSelectionViewModel();
            CloseWindowInteraction = new Interaction<Unit, Unit>();
            ShowModalWindowInteraction = new Interaction<ModalWindowViewModel, Unit>();
            ExitApplicationCommand = ReactiveCommand.Create(async () =>
            {
                _ = await CloseWindowInteraction?.Handle(new Unit());
            });

            OpenSettingsCommand = ReactiveCommand.Create(async () =>
            {
                _ = await ShowModalWindowInteraction?.Handle(
                    new ModalWindowViewModel(Properties.Resources.SubMenu_Settings, "settings_regular", new SettingsViewModel())
                    );
            });

            NewFunctionCommand = ReactiveCommand.Create(async () =>
            {
                _ = await ShowModalWindowInteraction?.Handle(
                    new ModalWindowViewModel(Properties.Resources.SubMenu_NewFunction, "settings_regular", new AddFunctionViewModel())
                    );
            });
        }
    }
}
