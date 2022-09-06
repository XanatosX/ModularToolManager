using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Avalonia.Threading;
using ModularToolManager.Models;
using ModularToolManager.Services.Ui;
using ModularToolManager.ViewModels;
using ReactiveUI;
using System;
using System.Linq;
using System.Reactive;

namespace ModularToolManager.Views;

public partial class ModalWindow : ReactiveWindow<ModalWindowViewModel>
{
    public ModalWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
        this.WhenActivated(d => d(ViewModel!.CloseWindowInteraction.RegisterHandler(HandleWindowClose)));
    }

    private async void HandleWindowClose(InteractionContext<Unit, Unit> obj)
    {
        obj.SetOutput(new Unit());
        await Dispatcher.UIThread.InvokeAsync(() =>
        {
            Close();
        });

    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
