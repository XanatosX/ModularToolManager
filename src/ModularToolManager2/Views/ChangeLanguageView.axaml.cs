using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ModularToolManager2.Views;

public partial class ChangeLanguageView : UserControl
{
    public ChangeLanguageView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
