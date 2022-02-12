using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ModularToolManager2.Views
{
    public partial class ModalWindow : Window
    {
        public ModalWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
