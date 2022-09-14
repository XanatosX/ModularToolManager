using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.Messaging;
using ModularToolManager.Models.Messages;
using ModularToolManager.Services.Ui;

namespace ModularToolManager.Views;

public partial class MainWindow : Window
{
    private readonly IWindowManagementService? modalService;

    public MainWindow() : this(null)
    {

    }

    public MainWindow(IWindowManagementService? modalService)
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif

        WeakReferenceMessenger.Default.Register<CloseApplicationMessage>(this, (s, e) =>
        {
            Close();
        });
        WeakReferenceMessenger.Default.Register<ToggleApplicationVisibilityMessage>(this, (s, e) =>
        {
            if (e.Hide)
            {
                Hide();
                return;
            }
            Show();
            Topmost = true;
            Topmost = false;
        });

        PropertyChanged += (s, e) =>
        {
            if (e.Property.Name == "Height")
            {
                PositionWindow();
            }
        };


        PositionWindow();
        this.modalService = modalService;
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

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
