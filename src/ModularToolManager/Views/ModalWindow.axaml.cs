using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.Messaging;
using ModularToolManager.Models;
using ModularToolManager.Models.Messages;
using ModularToolManager.Services.Ui;
using ModularToolManager.ViewModels;
using System;
using System.Linq;
using System.Reactive;

namespace ModularToolManager.Views;

public partial class ModalWindow : Window
{
    public ModalWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
        WeakReferenceMessenger.Default.Register<CloseModalMessage>(this, (sender, data) =>
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
        //this.WhenActivated(d => d(ViewModel!.CloseWindowInteraction.RegisterHandler(HandleWindowClose)));
    }

    /**
    private async void HandleWindowClose(InteractionContext<Unit, Unit> obj)
    {
        obj.SetOutput(new Unit());
        await Dispatcher.UIThread.InvokeAsync(() =>
        {
            Close();
        });

    }
    */

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
