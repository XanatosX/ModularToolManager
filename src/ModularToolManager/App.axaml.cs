using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using ModularToolManager.Services.IO;
using ModularToolManager.Services.Language;
using ModularToolManager.Services.Plugin;
using ModularToolManager.Services.Serialization;
using ModularToolManager.Services.Styling;
using ModularToolManager.Services.Ui;
using ModularToolManager.ViewModels;
using ModularToolManager.Views;
using ModularToolManagerModel.Services.Functions;
using ModularToolManagerModel.Services.IO;
using ModularToolManagerModel.Services.Language;
using ModularToolManagerModel.Services.Plugin;
using ModularToolManagerModel.Services.Serialization;
using ModularToolManagerPlugin.Services;
using Splat;
using System.Text.Json;

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
        dependencyContainer.RegisterConstant<IPathService>(new PathService());
        dependencyContainer.RegisterConstant<IStyleService>(new DefaultStyleService());
        dependencyContainer.Register<IPluginTranslationService>(() => new PluginTranslationService());
        dependencyContainer.RegisterConstant<IFunctionSettingsService>(new FunctionSettingService());
        dependencyContainer.RegisterConstant<IUrlOpenerService>(new UrlOpenerService());
        dependencyContainer.RegisterConstant<ILanguageService>(new ResourceCultureService());
        dependencyContainer.RegisterConstant<IFunctionSettingsService>(new FunctionSettingService());
        dependencyContainer.RegisterConstant<IModalService>(new WindowModalService());

        dependencyContainer.RegisterConstant<IPluginService>(new PluginService(
            resolver.GetService<IFunctionSettingsService>(),
            resolver.GetService<IPathService>()

        ));

        dependencyContainer.Register<ISerializationOptionFactory<JsonSerializerOptions>>(() => new JsonSerializationOptionFactory(resolver));
        dependencyContainer.RegisterConstant<ISerializeService>(new JsonSerializationService(resolver.GetService<ISerializationOptionFactory<JsonSerializerOptions>>()));
        dependencyContainer.RegisterConstant<IFunctionService>(new SerializedFunctionService(resolver.GetService<ISerializeService>(), resolver.GetService<IPathService>()));
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
        dependencyContainer.Register(() => new FunctionSelectionViewModel(resolver.GetService<IFunctionService>()));
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
