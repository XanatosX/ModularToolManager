using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using ModularToolManager.Enums;
using ModularToolManager.Models;
using ModularToolManager.Models.Messages;
using ModularToolManager.Services.Settings;
using ModularToolManager.Services.Ui;
using System;
using System.Linq;

namespace ModularToolManager.Views;

/// <summary>
/// The main window of the application
/// </summary>
public partial class MainWindow : Window, IDisposable
{
    /// <summary>
    /// Was the class already disposed
    /// </summary>
    private bool isDisposed;

    /// <summary>
    /// The modal service to use
    /// </summary>
    private readonly IWindowManagementService? modalService;

    /// <summary>
    /// Is this the first time the window was shown
    /// </summary>
    private bool firstTimeShown;

    /// <summary>
    /// The settings service to use
    /// </summary>
    private readonly ISettingsService? settingsService;

    /// <summary>
    /// The factory to get the window position strategy to use
    /// </summary>
    private readonly IWindowPositionFactory? windowPositionFactory;

    /// <summary>
    /// Create a new empty instance of this class
    /// </summary>
    public MainWindow() : this(null, null, null)
    {

    }

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    /// <param name="modalService">The modal service to use</param>
    /// <param name="settingsService">The settings service to use</param>
    public MainWindow(IWindowManagementService? modalService, ISettingsService? settingsService, IWindowPositionFactory? windowPositionFactory)
    {

        this.modalService = modalService;
        this.settingsService = settingsService;
        this.windowPositionFactory = windowPositionFactory;
        firstTimeShown = true;
        InitializeComponent();

        WeakReferenceMessenger.Default.Register<CloseApplicationMessage>(this, (_, _) =>
        {
            Close();
        });
        WeakReferenceMessenger.Default.Register<ToggleApplicationVisibilityMessage>(this, (_, e) =>
        {
            bool alwaysTopMost = settingsService?.GetApplicationSettings().AlwaysOnTop ?? false;
            if (e.Hide)
            {
                Hide();
                return;
            }
            Show();
            Topmost = true;
            if (!alwaysTopMost)
            {
                Topmost = false;
            }
        });
        WeakReferenceMessenger.Default.Register<RequestApplicationVisibility>(this, (_, e) =>
        {
            if (!e.HasReceivedResponse)
            {
                e.Reply(IsVisible);
            }
        });
        WeakReferenceMessenger.Default.Register<ValueChangedMessage<ApplicationSettings>>(this, (_, settings) =>
        {
            Topmost = settings.Value.AlwaysOnTop;
        });
        if (settingsService?.GetApplicationSettings().AlwaysOnTop ?? false)
        {
            Topmost = true;
        }
        PositionWindow();
    }

    /// <inheritdoc/>
    public override void Render(DrawingContext context)
    {
        base.Render(context);
        PositionWindow();
    }

    /// <inheritdoc/>
    public override void Show()
    {
        base.Show();
        if (firstTimeShown)
        {
            if (settingsService?.GetApplicationSettings().StartMinimized ?? false)
            {
                var mainWindow = modalService?.GetMainWindow();
                mainWindow?.Hide();
            }
        }
        firstTimeShown = false;
    }

    /// <summary>
    /// Method to position a given window in the bottom right corner based on the window height
    /// </summary>
    private void PositionWindow()
    {
        windowPositionFactory?.GetWindowPositionStrategy(WindowPositionEnum.BottomRight)?.PositionWindow(this, Screens.Primary);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (isDisposed)
        {
            return;
        }
        WeakReferenceMessenger.Default.UnregisterAll(this);
        if (DataContext is IDisposable disposable)
        {
            disposable.Dispose();
        }
        isDisposed = true;
    }

    /// <summary>
    /// Deconstructor to ensure disposal on object desctruction
    /// </summary>
    ~MainWindow()
    {
        Dispose();
    }
}
