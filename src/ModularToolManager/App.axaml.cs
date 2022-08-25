using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using ModularToolManager.Services.Functions;
using ModularToolManager.Services.IO;
using ModularToolManager.Services.Language;
using ModularToolManager.Services.Plugin;
using ModularToolManager.Services.Settings;
using ModularToolManager.Services.Styling;
using ModularToolManager.Services.Ui;
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

        RegisterServices(Locator.CurrentMutable, Locator.Current);
        RegisterViewModels(Locator.CurrentMutable, Locator.Current);
    }

    /// <summary>
    /// Register services for the application
    /// </summary>
    /// <param name="dependencyContainer">The container to register the models into</param>
    /// <param name="resolver">The resolver to resolve dependencies</param>
    private void RegisterServices(IMutableDependencyResolver dependencyContainer, IReadonlyDependencyResolver resolver)
    {
        dependencyContainer.Register<IStyleService>(() => new DefaultStyleService());
        dependencyContainer.RegisterConstant<IUrlOpenerService>(new UrlOpenerService());
        dependencyContainer.RegisterConstant<ILanguageService>(new ResourceCultureService());
        dependencyContainer.RegisterConstant<IFunctionService>(new MockupFunctionService());
        dependencyContainer.RegisterConstant<IPluginTranslationFactoryService>(new PluginTranslationFactoryService());
        dependencyContainer.RegisterConstant<IFunctionSettingsService>(new FunctionSettingsService());
        dependencyContainer.RegisterConstant<IModalService>(new WindowModalService());
        dependencyContainer.RegisterConstant<IPathService>(new PathService());
        dependencyContainer.RegisterConstant<IPluginService>(new PluginService(
            resolver.GetService<IPluginTranslationFactoryService>(),
            resolver.GetService<IFunctionSettingsService>(),
            resolver.GetService<IPathService>()
        ));

    }

    /// <summary>
    /// Method to register the view models for the application
    /// </summary>
    /// <param name="dependencyContainer">The container to register the models into</param>
    /// <param name="resolver">The resolver to resolve dependencies</param>
    private void RegisterViewModels(IMutableDependencyResolver dependencyContainer, IReadonlyDependencyResolver resolver)
    {
        dependencyContainer.Register(() => new AddFunctionViewModel(
                resolver.GetService<IPluginService>(),
                resolver.GetService<IFunctionService>()
            ));
        dependencyContainer.Register(() => new FunctionSelectionViewModel());
        dependencyContainer.Register(() => new MainWindowViewModel(resolver.GetService<FunctionSelectionViewModel>(), resolver.GetService<IUrlOpenerService>()));

        dependencyContainer.Register(() => new MainWindow(resolver.GetService<IModalService>())
        {
            DataContext = resolver.GetService<MainWindowViewModel>(),
        });
    }

    /// <inheritdoc/>
    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = Locator.Current.GetService<MainWindow>();
        }

        base.OnFrameworkInitializationCompleted();
    }
}
