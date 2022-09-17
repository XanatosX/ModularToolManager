using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.Messaging;
using ModularToolManager.Models.Messages;
using ModularToolManager.ViewModels;

namespace ModularToolManager.Views;

public partial class ModalWindow : Window
{
    public ModalWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
        WeakReferenceMessenger.Default.Register<CloseModalMessage>(this, (_, data) =>
        {
            ModalWindowViewModel? viewModel = DataContext as ModalWindowViewModel;
            if (viewModel is null || viewModel.ModalContent != data.ModalToClose)
            {
                return;
            }
            data.Reply(true);
            WeakReferenceMessenger.Default.Unregister<CloseModalMessage>(this);
            Close();

        });
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
