using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ModularToolManager2.Views;

public partial class FunctionSelectionView : UserControl
{
    public FunctionSelectionView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}