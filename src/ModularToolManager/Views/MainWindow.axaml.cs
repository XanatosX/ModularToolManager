using Avalonia;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ModularToolManager.Models;
using ModularToolManager.ViewModels;
using ReactiveUI;
using System.Reactive;
using System.Threading.Tasks;

namespace ModularToolManager.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
        this.WhenActivated(d => d(ViewModel!.CloseWindowInteraction.RegisterHandler(Close)));
        this.WhenActivated(d => d(ViewModel!.ShowModalWindowInteraction.RegisterHandler(DoHandleShowModalWindow)));
        this.WhenActivated(d => d(ViewModel!.ToggleApplicationVisibilityInteraction.RegisterHandler(DoToggleWindowVisibility)));

        PropertyChanged += (s, e) =>
        {
            if (e.Property.Name == "Height")
            {
                PositionWindow();
            }
        };


        PositionWindow();
    }

    /// <summary>
    /// Method to toggle the window visibilty based on an interaction
    /// </summary>
    /// <param name="context">Empty context</param>
    private void DoToggleWindowVisibility(InteractionContext<Unit, Unit> _)
    {
        if (IsVisible)
        {
            Hide();
            return;
        }
        Show();
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

    /// <summary>
    /// Show the given modal
    /// </summary>
    /// <param name="context">The interaction context which tells us which modal to instantiate and show</param>
    /// <returns>A reference to the new modal as task</returns>
    private async Task DoHandleShowModalWindow(InteractionContext<ShowWindowModel, Unit> context)
    {
        ModalWindow window = new ModalWindow();
        window.WindowStartupLocation = context.Input.StartupLocation;
        window.DataContext = context.Input.ViewModel;
        await window.ShowDialog(this);

    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
