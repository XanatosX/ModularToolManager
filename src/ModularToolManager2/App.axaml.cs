using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using ModularToolManager2.Services.IO;
using ModularToolManager2.Services.Language;
using ModularToolManager2.Services.Styling;
using ModularToolManager2.ViewModels;
using ModularToolManager2.Views;
using Splat;

namespace ModularToolManager2
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            Locator.CurrentMutable.Register(() => new DefaultStyleService(), typeof(IStyleService));
            Locator.CurrentMutable.RegisterConstant<IUrlOpenerService>(new UrlOpenerService());
            Locator.CurrentMutable.RegisterConstant<ILanguageService>(new ResourceCultureService());
        }

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
}
