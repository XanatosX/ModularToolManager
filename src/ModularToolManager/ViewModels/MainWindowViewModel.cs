using Avalonia;
using ModularToolManager.Models;
using ModularToolManager.Services.IO;
using ReactiveUI;
using Splat;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Input;

namespace ModularToolManager.ViewModels;

/// <summary>
/// Main view model
/// </summary>
public class MainWindowViewModel : ViewModelBase
{
    /// <summary>
    /// Service to use for opening urls
    /// </summary>
    private IUrlOpenerService urlOpenerService;

    /// <summary>
    /// The current content model to show in the main view
    /// </summary>
    public ViewModelBase MainContentModel { get; }

    /// <summary>
    /// Command to select a new language
    /// </summary>
    public ICommand SelectLanguageCommand { get; }

    /// <summary>
    /// Command to open the settings
    /// </summary>
    public ICommand OpenSettingsCommand { get; }

    /// <summary>
    /// Command to open the modal to add a new function
    /// </summary>
    public ICommand NewFunctionCommand { get; }

    /// <summary>
    /// Command to execture for bug reporting
    /// </summary>
    public ICommand ReportBugCommand { get; }

    /// <summary>
    /// Command to use to close the application
    /// </summary>
    public ICommand ExitApplicationCommand { get; }

    /// <summary>
    /// Command to hide the application
    /// </summary>
    public ICommand HideApplicationCommand { get; }

    /// <summary>
    /// Command to show the application
    /// </summary>
    private ICommand ShowApplicationCommand { get; }

    /// <summary>
    /// Interaction to toggle the visibility of the window
    /// </summary>
    public Interaction<Unit, Unit> ToggleApplicationVisibilityInteraction { get; }

    /// <summary>
    /// Interaction to close thw window
    /// </summary>
    public Interaction<Unit, Unit> CloseWindowInteraction { get; }

    /// <summary>
    /// Interaction to show a model on top of the window
    /// </summary>
    public Interaction<ShowWindowModel, Unit> ShowModalWindowInteraction { get; }

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
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
