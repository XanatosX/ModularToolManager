using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ModularToolManager2.Views;

public partial class CultureInfoView : UserControl
{
    public CultureInfoView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
