using Avalonia.Controls;
using CommunityToolkit.Mvvm.Messaging;
using ModularToolManager.Models.Messages;
using ModularToolManager.ViewModels;
using System;

namespace ModularToolManager.Views;

public partial class ModalWindow : Window
{
    public ModalWindow()
    {
        InitializeComponent();
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

    protected override void OnOpened(EventArgs e)
    {
        WeakReferenceMessenger.Default.Send<ModalWindowOpened>(new ModalWindowOpened(true));
        base.OnOpened(e);
    }

    protected override void OnClosing(WindowClosingEventArgs e)
    {
        WeakReferenceMessenger.Default.Send<ModalWindowOpened>(new ModalWindowOpened(false));
        base.OnClosing(e);
    }
}
