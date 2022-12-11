using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ModularToolManager.Views;
public partial class IntPluginSettingView : UserControl
{
    public IntPluginSettingView()
    {
        InitializeComponent();
    }

    /// <inheritdoc/>
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
