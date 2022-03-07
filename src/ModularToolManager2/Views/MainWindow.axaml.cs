using Avalonia;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ModularToolManager2.ViewModels;
using ReactiveUI;
using System.Reactive;
using System.Threading.Tasks;

namespace ModularToolManager2.Views
{
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
                    PositionWindow((double)e.NewValue);
                }
            };


            PositionWindow(Height);
        }

        private void DoToggleWindowVisibility(InteractionContext<Unit, Unit> context)
        {
            if (IsVisible)
            {
                Hide();
                return;
            }
            Show();
        }

        private void PositionWindow(double newHeight)
        {
            PixelRect workingArea = Screens.Primary.WorkingArea;
            double newXPos = workingArea.X + workingArea.Width - Width;
            double newYPos = workingArea.Y + workingArea.Height - Height;
            Position = new PixelPoint((int)newXPos, (int)newYPos);
        }

        private async Task DoHandleShowModalWindow(InteractionContext<ModalWindowViewModel, Unit> context)
        {
            ModalWindow window = new ModalWindow();
            window.DataContext = context.Input;
            await window.ShowDialog(this);

        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
