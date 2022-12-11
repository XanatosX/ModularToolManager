using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ModularToolManager.Views;
public partial class FloatPluginSettingView : UserControl
{
    public FloatPluginSettingView()
    {
        InitializeComponent();
    }

    /// <inheritdoc/>
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
