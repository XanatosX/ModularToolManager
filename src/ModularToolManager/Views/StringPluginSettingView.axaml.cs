using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ModularToolManager.Views;

/// <summary>
/// View class for the string plugin setting view
/// </summary>
public partial class StringPluginSettingView : UserControl
{
    /// <summary>
    /// Create a new instance of the view
    /// </summary>
    public StringPluginSettingView()
    {
        InitializeComponent();
    }

    /// <inheritdoc/>
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}