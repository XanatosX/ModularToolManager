using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using ModularToolManager.Models;
using ModularToolManager.Models.Messages;
using ModularToolManager.Services.Settings;
using ModularToolManager.Services.Ui;

namespace ModularToolManager.Views;

/// <summary>
/// The main window of the application
/// </summary>
public partial class MainWindow : Window
{
    /// <summary>
    /// The modal service to use
    /// </summary>
    private readonly IWindowManagementService? modalService;

    /// <summary>
    /// Is this the first render of the application
    /// </summary>
    private bool firstRender;

    /// <summary>
    /// The settings service to use
    /// </summary>
    private readonly ISettingsService? settingsService;

    /// <summary>
    /// Create a new empty instance of this class
    /// </summary>
    public MainWindow() : this(null, null)
    {

    }

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    /// <param name="modalService">The modal service to use</param>
    /// <param name="settingsService">The settings service to use</param>
    public MainWindow(IWindowManagementService? modalService, ISettingsService? settingsService)
    {

        this.modalService = modalService;
        this.settingsService = settingsService;

        firstRender = true;
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif

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
        if (firstRender)
        {
            if (settingsService?.GetApplicationSettings().StartMinimized ?? false)
            {
                var mainWindow = modalService?.GetMainWindow();
                mainWindow?.Hide();
            }
        }

        PositionWindow();
        firstRender = false;
    }

    /// <summary>
    /// Method to position a given window in the bottom right corner based on the window height
    /// </summary>
    private void PositionWindow()
    {
        PixelRect workingArea = Screens.Primary.WorkingArea;
        double newXPos = workingArea.X + workingArea.Width - Width;
        double newYPos = workingArea.Y + workingArea.Height - Height;
        Position = new PixelPoint((int)newXPos, (int)newYPos);
    }

    /// <inheritdoc/>
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
