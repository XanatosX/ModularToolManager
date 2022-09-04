using Avalonia;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Avalonia.Threading;
using ModularToolManager.ViewModels;
using ReactiveUI;
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
