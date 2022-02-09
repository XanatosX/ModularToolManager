using Avalonia;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ModularToolManager2.ViewModels;
using ReactiveUI;

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
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
