using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ModularToolManager.Views;
public partial class AllPluginsView : UserControl
{
    public AllPluginsView()
    {
        InitializeComponent();
    }

    /// <inheritdoc/>
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
