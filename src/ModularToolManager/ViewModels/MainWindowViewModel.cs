using Avalonia.Controls;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using ModularToolManager.Models;
using ModularToolManager.Models.Messages;
using ModularToolManager.Services.Settings;
using ModularToolManager.Services.Ui;
using ModularToolManagerModel.Services.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ModularToolManager.ViewModels;

/// <summary>
/// Main view model
/// </summary>
public partial class MainWindowViewModel : ObservableObject
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
    private readonly IUrlOpenerService urlOpenerService;

    /// <summary>
    /// The settings service to use
    /// </summary>
    private readonly ISettingsService settingsService;


    private readonly IThemeService themeService;

    /// <summary>
    /// The current content model to show in the main view
    /// </summary>
    public ObservableObject MainContentModel { get; }

    [ObservableProperty]
    public Color applicationTintColor;

    [ObservableProperty]
    public float tintOpacity;

    [ObservableProperty]
    public float materialOpacity;

    /// <summary>
    /// Should the window be shown in the taskbar
    /// </summary>
    [ObservableProperty]
    private bool showInTaskbar;

    /// <summary>
    /// Command to select a new language
    /// </summary>
    public ICommand SelectLanguageCommand { get; }

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
        IUrlOpenerService urlOpenerService,
        ISettingsService settingsService,
        IThemeService themeService)
    {
        this.urlOpenerService = urlOpenerService;
        this.settingsService = settingsService;
        this.themeService = themeService;
        MainContentModel = mainContentModel;
        this.viewModelLocator = viewModelLocator;
        this.windowManagementService = windowManagementService;

        SwitchTheme();
        UpdateShowInTaskbar();

        ReportBugCommand = new RelayCommand(() => urlOpenerService?.OpenUrl(Properties.Properties.GithubIssueUrl));
        ExitApplicationCommand = new RelayCommand(() => WeakReferenceMessenger.Default.Send(new CloseApplicationMessage()));
        SelectLanguageCommand = new AsyncRelayCommand(async () => await OpenModalWindow(Properties.Resources.SubMenu_Language, Properties.Properties.Icon_language, nameof(ChangeLanguageViewModel)));
        HideApplicationCommand = new RelayCommand(() => WeakReferenceMessenger.Default.Send(new ToggleApplicationVisibilityMessage(true)));
        ShowApplicationCommand = new RelayCommand(() => WeakReferenceMessenger.Default.Send(new ToggleApplicationVisibilityMessage(false)));

        WeakReferenceMessenger.Default.Register<ValueChangedMessage<ApplicationSettings>>(this, (_, e) =>
        {
            UpdateShowInTaskbar();
        });
        //WeakReferenceMessenger.Default.Register<ApplicationThemeUpdated>(this, (_, e) => SwitchTheme(e.Value));

        WeakReferenceMessenger.Default.Register<EditFunctionMessage>(this, async (_, e) =>
        {
            var editModal = viewModelLocator.GetViewModel(nameof(AddFunctionViewModel)) as AddFunctionViewModel;
            if (editModal is not null && editModal.LoadInFunction(e.Identifier))
            {
                await OpenModalWindow(Properties.Resources.Window_EditFunction, Properties.Properties.Icon_edit_function, editModal);
                e.Reply(true);
                WeakReferenceMessenger.Default.Send(new ReloadFunctionsMessage());
                return;
            }
            e.Reply(false);
        });
    }

    private void SwitchTheme(int themeId)
    {
        var theme = themeService.GetStyleById(themeId);
        theme ??= themeService.GetAllStyles().FirstOrDefault() ?? new ApplicationStyle { MaterialOpacity = 1, TintOpacity = 0.65f, TintColor = Colors.Pink };
        if (theme is null)
        {
            return;
        }
        ApplicationTintColor = theme.TintColor ?? Colors.Pink;
        MaterialOpacity = theme.MaterialOpacity;
        TintOpacity = theme.TintOpacity;
        themeService.ChangeApplicationTheme(theme);
    }

    private void SwitchTheme()
    {
        int themeId = settingsService.GetApplicationSettings().SelectedThemeId;
        SwitchTheme(themeId);
    }

    /// <summary>
    /// Set the setting if the application should be shown in the taskbar
    /// </summary>
    private void UpdateShowInTaskbar()
    {
        ShowInTaskbar = settingsService.GetApplicationSettings().ShowInTaskbar;
    }

    /// <summary>
    /// Command to open plugins view
    /// </summary>
    /// <returns>A awaitable task</returns>
    [RelayCommand]
    private async Task OpenPlugins()
    {
        await OpenModalWindow(Properties.Resources.SubMenu_Plugins, Properties.Properties.Icon_new_function, nameof(AllPluginsViewModel));
    }

    /// <summary>
    /// Open modal to allow adding a new function
    /// </summary>
    /// <returns>A awaitable task</returns>
    [RelayCommand]
    private async Task NewFunction()
    {
        await OpenModalWindow(Properties.Resources.SubMenu_NewFunction, Properties.Properties.Icon_new_function, nameof(AddFunctionViewModel));
        WeakReferenceMessenger.Default.Send(new ReloadFunctionsMessage());
    }

    /// <summary>
    /// Command to open the settings view
    /// </summary>
    /// <returns>A awaitable task</returns>
    [RelayCommand]
    private async Task OpenSettings()
    {
        await OpenModalWindow(Properties.Resources.SubMenu_Settings, Properties.Properties.Icon_settings, nameof(SettingsViewModel));
    }


    /// <summary>
    /// Open the about window
    /// </summary>
    /// <returns>A awaitable task</returns>
    [RelayCommand]
    private async Task OpenAbout()
    {
        await OpenModalWindow(Properties.Resources.SubMenu_About, Properties.Properties.Icon_About, nameof(AboutViewModel));
    }

    /// <summary>
    /// Open the hotkey window
    /// </summary>
    /// <returns>A awaitable task</returns>
    [RelayCommand]
    private async Task OpenHotkey()
    {
        await OpenModalWindow(Properties.Resources.SubMenu_Hotkeys, Properties.Properties.Icon_Keyboard, nameof(HotkeysViewModel));
    }

    /// <summary>
    /// Method to open a modal window
    /// </summary>
    /// <param name="title">The title to use</param>
    /// <param name="imagePath">The image path to show</param>
    /// <param name="modalName">The name of the modal to show</param>
    /// <returns></returns>
    private async Task OpenModalWindow(string title, string imagePath, string modalName)
    {
        var modalContent = viewModelLocator.GetViewModel(modalName);
        if (modalContent is null)
        {
            return;
        }
        ShowWindowModel modalWindowData = new ShowWindowModel(title, imagePath, modalContent, WindowStartupLocation.CenterScreen);
        if (windowManagementService is not null)
        {
            await windowManagementService.ShowModalWindowAsync(modalWindowData, windowManagementService?.GetMainWindow());
        }
    }

    /// <summary>
    /// Method to open a modal window
    /// </summary>
    /// <param name="title">The title to use</param>
    /// <param name="imagePath">The image path to show</param>
    /// <param name="modal">The modal to show</param>
    /// <returns></returns>
    private async Task OpenModalWindow(string title, string imagePath, ObservableObject? modal)
    {
        if (modal is null)
        {
            return;
        }
        ShowWindowModel modalWindowData = new ShowWindowModel(title, imagePath, modal, WindowStartupLocation.CenterScreen);
        if (windowManagementService is not null)
        {
            await windowManagementService.ShowModalWindowAsync(modalWindowData, windowManagementService?.GetMainWindow());
        }
    }
}
