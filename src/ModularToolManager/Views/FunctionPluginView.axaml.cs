using Avalonia.Controls;
using Avalonia.Markup.Xaml;


namespace ModularToolManager.Views;

/// <summary>
/// Class for the function plugin view
/// </summary>
public partial class FunctionPluginView : UserControl
{

    /// <inheritdoc/>
    public FunctionPluginView()
    {
        InitializeComponent();
    }

    /// <inheritdoc/>
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
