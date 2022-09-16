using Avalonia;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using ModularToolManager.Models;
using ModularToolManager.Models.Messages;
using ModularToolManager.Services.Styling;
using ModularToolManager.Services.Ui;
using ModularToolManagerModel.Services.IO;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ModularToolManager.ViewModels;

/// <summary>
/// Main view model
/// </summary>
public class MainWindowViewModel : ObservableObject
{
    /// <summary>
    /// Service to use for locating view models
    /// </summary>
    private readonly IViewModelLocatorService viewModelLocator;

    /// <summary>
    /// Service to use for managing windows
    /// </summary>

    private readonly IWindowManagementService windowManagementService;

    /// <summary>
    /// Service to use for opening urls
    /// </summary>
    private IUrlOpenerService urlOpenerService;

    /// <summary>
    /// The current content model to show in the main view
    /// </summary>
    public ObservableObject MainContentModel { get; }
    private readonly IStyleService styleService;

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
    /// Create a new instance of this class
    /// </summary>
    public MainWindowViewModel(
        FunctionSelectionViewModel mainContentModel,
        IViewModelLocatorService viewModelLocator,
        IWindowManagementService windowManagementService,
        IStyleService styleService,
        IUrlOpenerService urlOpenerService)
    {
        this.urlOpenerService = urlOpenerService;
        MainContentModel = mainContentModel;
        this.viewModelLocator = viewModelLocator;
        this.windowManagementService = windowManagementService;
        this.styleService = styleService;
        ReportBugCommand = new RelayCommand(() => urlOpenerService?.OpenUrl(Properties.Properties.GithubUrl));

        ExitApplicationCommand = new RelayCommand(() => WeakReferenceMessenger.Default.Send(new CloseApplicationMessage()));
        SelectLanguageCommand = new AsyncRelayCommand(async () => await OpenModalWindow(Properties.Resources.SubMenu_Language, "flag_regular", nameof(ChangeLanguageViewModel)));
        OpenSettingsCommand = new AsyncRelayCommand(async () => await OpenModalWindow(Properties.Resources.SubMenu_Settings, "settings_regular", nameof(SettingsViewModel)));
        NewFunctionCommand = new AsyncRelayCommand(async () => await OpenModalWindow(Properties.Resources.SubMenu_NewFunction, "settings_regular", nameof(AddFunctionViewModel)));
        HideApplicationCommand = new RelayCommand(() => WeakReferenceMessenger.Default.Send(new ToggleApplicationVisibilityMessage(true)));
        ShowApplicationCommand = new RelayCommand(() => WeakReferenceMessenger.Default.Send(new ToggleApplicationVisibilityMessage(false)));
    }

    /// <summary>
    /// Method to open a modal window
    /// </summary>
    /// <param name="title">The title to use</param>
    /// <param name="imagePath">The image path to show</param>
    /// <param name="modalName">The name of the modal to show</param>
    /// <returns></returns>
    private Task OpenModalWindow(string title, string imagePath, string modalName)
    {
        var modalContent = viewModelLocator.GetViewModel(modalName);
        if (modalContent is null)
        {
            return null;
        }
        ShowWindowModel modalWindowData = new ShowWindowModel(title, imagePath, viewModelLocator.GetViewModel(modalName), WindowStartupLocation.CenterScreen);
        if (windowManagementService is not null)
        {
            return windowManagementService?.ShowModalWindowAsync(modalWindowData, windowManagementService?.GetMainWindow());
        }
        return null;
    }


}
