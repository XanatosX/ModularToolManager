using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ModularToolManager.Views
{
    public partial class FunctionPluginView : UserControl
    {
        public FunctionPluginView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
