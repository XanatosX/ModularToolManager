using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ModularToolManager.Views;
public partial class PluginSettingsView : UserControl
{
    public PluginSettingsView()
    {
        InitializeComponent();
    }



    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
