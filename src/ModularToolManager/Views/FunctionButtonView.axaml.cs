using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ModularToolManager.Views;

/// <summary>
/// Class for the function button view
/// </summary>
public partial class FunctionButtonView : UserControl
{
    /// <inheritdoc/>
    public FunctionButtonView()
    {
        InitializeComponent();
    }

    /// <inheritdoc/>
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
