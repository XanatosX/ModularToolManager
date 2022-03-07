using Avalonia;
using ModularToolManager2.Models;
using ModularToolManager2.Services.IO;
using ModularToolManager2.Services.Language;
using ReactiveUI;
using Splat;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Input;

namespace ModularToolManager2.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private IUrlOpenerService urlOpenerService;

        public ViewModelBase MainContentModel { get; }

        public ICommand SelectLanguageCommand { get; }

        public ICommand HideApplicationCommand { get; }


        private ICommand ShowApplicationCommand { get; }

        public ICommand OpenSettingsCommand { get; }

        public ICommand NewFunctionCommand { get; }

        public ICommand ExitApplicationCommand { get; }

        public ICommand ReportBugCommand { get; }

        public Interaction<Unit, Unit> HideWindowInteraction { get; }

        public Interaction<Unit, Unit> ToggleApplicationVisibilityInteraction { get; }

        public Interaction<Unit, Unit> CloseWindowInteraction { get; }

        public Interaction<ShowWindowModel, Unit> ShowModalWindowInteraction { get; }


        public MainWindowViewModel()
        {
            urlOpenerService = Locator.Current.GetService<IUrlOpenerService>();
            MainContentModel = new FunctionSelectionViewModel();
            CloseWindowInteraction = new Interaction<Unit, Unit>();
            ShowModalWindowInteraction = new Interaction<ShowWindowModel, Unit>();
            ExitApplicationCommand = ReactiveCommand.Create(async () =>
            {
                _ = await CloseWindowInteraction?.Handle(new Unit());
            });

            OpenSettingsCommand = ReactiveCommand.Create(async () =>
            {
                _ = await ShowModalWindowInteraction?.Handle(
                    new ShowWindowModel(
                        new ModalWindowViewModel(Properties.Resources.SubMenu_Settings, "settings_regular", new SettingsViewModel()),
                        Avalonia.Controls.WindowStartupLocation.CenterScreen
                    ));
            });
            NewFunctionCommand = ReactiveCommand.Create(async () =>
            {
                _ = await ShowModalWindowInteraction?.Handle(
                                        new ShowWindowModel(
                        new ModalWindowViewModel(Properties.Resources.SubMenu_NewFunction, "settings_regular", new AddFunctionViewModel()),
                        Avalonia.Controls.WindowStartupLocation.CenterScreen
                    ));
            });
            ToggleApplicationVisibilityInteraction = new Interaction<Unit, Unit>();
            HideApplicationCommand = ReactiveCommand.Create(async () =>
            {
                _ = await ToggleApplicationVisibilityInteraction?.Handle(new());
            });

            ShowApplicationCommand = ReactiveCommand.Create(async () =>
            {
                _ = await ToggleApplicationVisibilityInteraction?.Handle(new());
            });

            ReportBugCommand = ReactiveCommand.Create(async () =>
            {
                if (urlOpenerService == null)
                {
                    return;
                }

                urlOpenerService.OpenUrl(Properties.Properties.GithubUrl);
            });

            SelectLanguageCommand = ReactiveCommand.Create(async () =>
            {
                _ = await ShowModalWindowInteraction?.Handle(
                            new ShowWindowModel(
                            new ModalWindowViewModel(Properties.Resources.SubMenu_Language, "flag_regular", new ChangeLanguageViewModel()),
                            Avalonia.Controls.WindowStartupLocation.CenterScreen
                        ));
            });
        }
    }
}
