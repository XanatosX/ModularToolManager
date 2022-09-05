using Avalonia.ReactiveUI;
using Microsoft.Extensions.DependencyInjection;
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
using ModularToolManagerModel.Services.Logging;
using ModularToolManagerModel.Services.Plugin;
using ModularToolManagerModel.Services.Serialization;
using ModularToolManagerPlugin.Services;
using ReactiveUI;
using System.Text.Json;

namespace ModularToolManager.DependencyInjection;

/// <summary>
/// Static class to add services to depdency injeciton
/// Class will provide extensions methods for this use case
/// </summary>
internal static class DependencyInjectionExtension
{
    /// <summary>
    /// Add all the avalonia default requirements
    /// </summary>
    /// <param name="collection">The collection to extend</param>
    /// <returns>The extended collection</returns>
    public static IServiceCollection AddAvaloniaDefault(this IServiceCollection collection)
    {
        return collection.AddSingleton<IActivationForViewFetcher, AvaloniaActivationForViewFetcher>()
                         .AddSingleton<IPropertyBindingHook, AutoDataTemplateBindingHook>();
    }

    /// <summary>
    /// Add all the view model
    /// </summary>
    /// <param name="collection">The collection to extend</param>
    /// <returns>The extended collection</returns>
    public static IServiceCollection AddViewModels(this IServiceCollection collection)
    {
        return collection.AddTransient(resolver => new MainWindow(resolver?.GetService<IModalService>())
        {
            DataContext = resolver?.GetService<MainWindowViewModel>(),
        });
    }

    /// <summary>
    /// Add all the views
    /// </summary>
    /// <param name="collection">The collection to extend</param>
    /// <returns>The extended collection</returns>
    public static IServiceCollection AddViews(this IServiceCollection collection)
    {
        return collection.AddTransient<AddFunctionViewModel>()
                         .AddTransient<FunctionSelectionViewModel>()
                         .AddTransient<MainWindowViewModel>();
    }

    /// <summary>
    /// Add all the services
    /// </summary>
    /// <param name="collection">The collection to extend</param>
    /// <returns>The extended collection</returns>
    public static IServiceCollection AddServices(this IServiceCollection collection)
    {
        return collection.AddSingleton<IPathService, PathService>()
                         .AddSingleton<IStyleService, DefaultStyleService>()
                         .AddSingleton<IPluginTranslationService, PluginTranslationService>()
                         .AddSingleton<IFunctionSettingsService, FunctionSettingService>()
                         .AddSingleton<IUrlOpenerService, UrlOpenerService>()
                         .AddSingleton<ILanguageService, ResourceCultureService>()
                         .AddSingleton<IModalService, WindowModalService>()
                         .AddSingleton<IPluginService, PluginService>()
                         .AddSingleton<ISerializationOptionFactory<JsonSerializerOptions>, JsonSerializationOptionFactory>()
                         .AddSingleton<ISerializeService, JsonSerializationService>()
                         .AddSingleton<IFunctionService, SerializedFunctionService>()
                         .AddTransient(typeof(IPluginLoggerService<>), typeof(LoggingPluginAdapter<>));
    }
}
