using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ModularToolManager.Views;
public partial class SingleHotkeyView : UserControl
{
    public SingleHotkeyView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
