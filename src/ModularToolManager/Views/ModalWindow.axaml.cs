using Avalonia;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Avalonia.Threading;
using ModularToolManager.Services.Ui;
using ModularToolManager.ViewModels;
using ReactiveUI;
using System.Reactive;

namespace ModularToolManager.Views;

public partial class ModalWindow : ReactiveWindow<ModalWindowViewModel>
{
    private readonly IWindowManagmentService? windowManagmentService;

    public ModalWindow() : this(null)
    {

    }

    public ModalWindow(IWindowManagmentService? windowManagmentService)
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
        this.WhenActivated(d => d(ViewModel!.CloseWindowInteraction.RegisterHandler(HandleWindowClose)));
        //this.WhenActivated(d => d(ViewModel!.OpenFileDialog.RegisterHandler(HandleOpenFileDialog)));
        this.windowManagmentService = windowManagmentService;
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
