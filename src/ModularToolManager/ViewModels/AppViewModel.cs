using Avalonia.Styling;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Logging;
using ModularToolManager.Models.Messages;
using ModularToolManager.Services.Settings;
using ModularToolManager.Services.Ui;
using Serilog;
using System;
using System.Windows.Input;

namespace ModularToolManager.ViewModels;

/// <summary>
/// View model for the app itself
/// </summary>
public partial class AppViewModel : ObservableObject
{
    /// <summary>
    /// Command to exit the application
    /// </summary>
    public ICommand ExitApplicationCommand { get; }

    /// <summary>
    /// Command to show the application of currently hidden
    /// </summary>
    public ICommand ShowApplicationCommand { get; }

    /// <summary>
    /// The current number of modal windows which are open
    /// </summary>
    private int numberOfOpenModalWindows;

    /// <summary>
    /// Logger to use for the app view
    /// </summary>
    private readonly ILogger<AppViewModel> logger;

    /// <summary>
    /// The service to use for loading and swichting a theme
    /// </summary>
    private readonly IThemeService themeService;

    /// <summary>
    /// The theme variant to use
    /// </summary>
    [ObservableProperty]
    private ThemeVariant themeVariant;

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    public AppViewModel(ILogger<AppViewModel> logger, IThemeService themeService, ISettingsService settingsService)
    {
        this.logger = logger;
        this.themeService = themeService;

        ThemeVariant = ThemeVariant.Light;
        var themeId = settingsService.GetApplicationSettings().SelectedThemeId;
        SwitchTheme(themeId);
        WeakReferenceMessenger.Default.Register<ApplicationThemeUpdated>(this, (_, e) => SwitchTheme(e.Value));

        numberOfOpenModalWindows = 0;

        WeakReferenceMessenger.Default.Register<ModalWindowOpened>(this, (_, message) => numberOfOpenModalWindows += message.Value ? 1 : -1);

        ShowApplicationCommand = new RelayCommand(() =>
        {
            if (numberOfOpenModalWindows > 0)
            {
                logger.LogWarning($"Tried to minimize app while {numberOfOpenModalWindows} where opend");
                return;
            }
            var response = WeakReferenceMessenger.Default.Send(new RequestApplicationVisiblity());
            bool toggleMode = response.HasReceivedResponse ? response.Response : false;
            WeakReferenceMessenger.Default.Send(new ToggleApplicationVisibilityMessage(toggleMode));
        });
        ExitApplicationCommand = new RelayCommand(() => WeakReferenceMessenger.Default.Send(new CloseApplicationMessage()));
    }

    private void SwitchTheme(int themeId)
    {
        var theme = themeService.GetStyleById(themeId);
        if (theme is null)
        {
            return;
        }

        ThemeVariant = theme.Variant;
    }
}
