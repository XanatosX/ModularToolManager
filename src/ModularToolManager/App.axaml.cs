using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using ModularToolManager.Services.Functions;
using ModularToolManager.Services.IO;
using ModularToolManager.Services.Language;
using ModularToolManager.Services.Plugin;
using ModularToolManager.Services.Settings;
using ModularToolManager.Services.Styling;
using ModularToolManager.ViewModels;
using ModularToolManager.Views;
using ModularToolManagerPlugin.Services;
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
        Locator.CurrentMutable.RegisterConstant<IFunctionService>(new MockupFunctionService());

        Locator.CurrentMutable.RegisterConstant<IPluginTranslationFactoryService>(new PluginTranslationFactoryService());
        Locator.CurrentMutable.RegisterConstant<IFunctionSettingsService>(new FunctionSettingsService());

        IPluginTranslationFactoryService translationFactoryService = Locator.Current.GetService<IPluginTranslationFactoryService>();
        IFunctionSettingsService settingsService = Locator.Current.GetService<IFunctionSettingsService>();
        Locator.CurrentMutable.RegisterConstant<IPluginService>(new PluginService(translationFactoryService, settingsService));
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
