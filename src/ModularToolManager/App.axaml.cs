using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using ModularToolManager.Services.IO;
using ModularToolManager.Services.Language;
using ModularToolManager.Services.Styling;
using ModularToolManager.ViewModels;
using ModularToolManager.Views;
using Splat;

namespace ModularToolManager;

/// <inheritdoc/>
public class App : Application
{
    /// <inheritdoc/>
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        Locator.CurrentMutable.Register(() => new DefaultStyleService(), typeof(IStyleService));
        Locator.CurrentMutable.RegisterConstant<IUrlOpenerService>(new UrlOpenerService());
        Locator.CurrentMutable.RegisterConstant<ILanguageService>(new ResourceCultureService());
    }

    /// <inheritdoc/>
    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
