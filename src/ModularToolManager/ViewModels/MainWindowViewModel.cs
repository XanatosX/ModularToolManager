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
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ModularToolManager.ViewModels;

/// <summary>
/// Main view model
/// </summary>
public partial class MainWindowViewModel : ObservableObject, IDisposable
{
    /// <summary>
    /// Is the view already disposed
    /// </summary>
    private bool isDisposed;

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

    /// <summary>
    /// The service used to switch the application theme
    /// </summary>
    private readonly IThemeService themeService;

    /// <summary>
    /// The current content model to show in the main view
    /// </summary>
    public ObservableObject MainContentModel { get; }

    /// <summary>
    /// The property for the application tint color
    /// </summary>
    [ObservableProperty]
    private Color applicationTintColor;

    /// <summary>
    /// The property for the application tint opacity
    /// </summary>
    [ObservableProperty]
    private float tintOpacity;

    /// <summary>
    /// The property for the application material opacity
    /// </summary>
    [ObservableProperty]
    private float materialOpacity;

    /// <summary>
    /// Should the window be shown in the taskbar
    /// </summary>
    [ObservableProperty]
    private bool showInTaskbar;

    /// <summary>
    /// Is the application in order mode
    /// </summary>
    [ObservableProperty]
    private bool inOrderMode;

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

        ReportBugCommand = new RelayCommand(() => urlOpenerService?.OpenUrl(Properties.Properties.GitHubIssueUrl));
        ExitApplicationCommand = new RelayCommand(() => WeakReferenceMessenger.Default.Send(new CloseApplicationMessage()));
        SelectLanguageCommand = new AsyncRelayCommand(async () => await OpenModalWindow(Properties.Resources.SubMenu_Language, Properties.Properties.Icon_language, nameof(ChangeLanguageViewModel)));
        HideApplicationCommand = new RelayCommand(() => WeakReferenceMessenger.Default.Send(new ToggleApplicationVisibilityMessage(true)));

        WeakReferenceMessenger.Default.Register<ValueChangedMessage<ApplicationSettings>>(this, (_, e) =>
        {
            UpdateShowInTaskbar();
        });
        WeakReferenceMessenger.Default.Register<ApplicationThemeUpdated>(this, (_, e) => SwitchTheme(e.Value));

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

    /// <summary>
    /// Switch the theme for the window
    /// </summary>
    /// <param name="themeId">The theme id to switch to</param>
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
    }

    /// <summary>
    /// Switch the theme for the window based on the settings
    /// </summary>
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
        await OpenModalWindow(Properties.Resources.SubMenu_Plugins, Properties.Properties.Icon_new_function, nameof(AllPluginsViewModel), false);
    }

    /// <summary>
    /// Open modal to allow adding a new function
    /// </summary>
    /// <returns>A awaitable task</returns>
    [RelayCommand]
    private async Task NewFunction()
    {
        await OpenModalWindow(Properties.Resources.SubMenu_NewFunction, Properties.Properties.Icon_new_function, nameof(AddFunctionViewModel), false);
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
        WeakReferenceMessenger.Default.Send(new ReloadFunctionsMessage());
        SwitchTheme();
    }


    /// <summary>
    /// Open the about window
    /// </summary>
    /// <returns>A awaitable task</returns>
    [RelayCommand]
    private async Task OpenAbout()
    {
        await OpenModalWindow(Properties.Resources.SubMenu_About, Properties.Properties.Icon_About, nameof(AboutViewModel), false);
    }

    /// <summary>
    /// Open the hotkey window
    /// </summary>
    /// <returns>A awaitable task</returns>
    [RelayCommand]
    private async Task OpenHotkey()
    {
        await OpenModalWindow(Properties.Resources.SubMenu_Hotkeys, Properties.Properties.Icon_Keyboard, nameof(HotkeysViewModel), false);
    }

    /// <summary>
    /// Toggle the order mode
    /// </summary>
    /// <param name="newState">The state to set the order mode to</param>
    [RelayCommand]
    private void ToggleOrderMode(bool newState)
    {
        InOrderMode = newState;
        WeakReferenceMessenger.Default.Send(new ToggleOrderModeMessage(InOrderMode));
    }

    /// <summary>
    /// Save the new order for the functions
    /// </summary>
    [RelayCommand]
    private void SaveOrderMode()
    {
        try
        {
            bool result = WeakReferenceMessenger.Default.Send(new SaveFunctionsOrderMessage());
            if (result)
            {
                WeakReferenceMessenger.Default.Send(new ReloadFunctionsMessage());
            }
        }
        catch (System.Exception)
        {
            //No response from save order message
        }

        ToggleOrderMode(false);
    }

    
    /// <summary>
    /// Method to open a modal window
    /// </summary>
    /// <param name="title">The title to use</param>
    /// <param name="imagePath">The image path to show</param>
    /// <param name="modalName">The name of the modal to show</param>
    /// <returns></returns>
    private async Task OpenModalWindow(string title, string imagePath, string modalName, bool canResize )
    {
        var modalContent = viewModelLocator.GetViewModel(modalName);
        if (modalContent is null)
        {
            return;
        }
        ShowWindowModel modalWindowData = new(title, imagePath, modalContent, WindowStartupLocation.CenterScreen, canResize);
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
    /// <param name="modalName">The name of the modal to show</param>
    /// <returns></returns>
    private async Task OpenModalWindow(string title, string imagePath, string modalName) => OpenModalWindow(title, imagePath, modalName, true);

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
        ShowWindowModel modalWindowData = new(title, imagePath, modal, WindowStartupLocation.CenterScreen);
        if (windowManagementService is not null)
        {
            await windowManagementService.ShowModalWindowAsync(modalWindowData, windowManagementService?.GetMainWindow());
        }
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (isDisposed)
        {
            return;
        }
        WeakReferenceMessenger.Default.UnregisterAll(this);
        if (MainContentModel is IDisposable disposable)
        {
            disposable.Dispose();
        }
        isDisposed = true;
    }

    /// <summary>
    /// Deconstructor to ensure dispose
    /// </summary>
    ~MainWindowViewModel()
    {
        Dispose();
    }
}
