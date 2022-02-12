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
