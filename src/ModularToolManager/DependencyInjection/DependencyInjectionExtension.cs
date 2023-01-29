using Microsoft.Extensions.DependencyInjection;
using ModularToolManager.Services.Dependencies;
using ModularToolManager.Services.IO;
using ModularToolManager.Services.Language;
using ModularToolManager.Services.Serialization;
using ModularToolManager.Services.Settings;
using ModularToolManager.Services.Styling;
using ModularToolManager.Services.Ui;
using ModularToolManager.ViewModels;
using ModularToolManager.Views;
using ModularToolManagerModel.DependencyInjection;
using ModularToolManagerModel.Services.Dependency;
using ModularToolManagerModel.Services.Functions;
using ModularToolManagerModel.Services.IO;
using ModularToolManagerModel.Services.Language;
using ModularToolManagerModel.Services.Logging;
using ModularToolManagerModel.Services.Plugin;
using ModularToolManagerModel.Services.Serialization;
using ModularToolManagerPlugin.Services;
using System;
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
    [Obsolete]
    public static IServiceCollection AddAvaloniaDefault(this IServiceCollection collection)
    {
        return collection;
    }

    /// <summary>
    /// Add all the view model
    /// </summary>
    /// <param name="collection">The collection to extend</param>
    /// <returns>The extended collection</returns>
    public static IServiceCollection AddViewModels(this IServiceCollection collection)
    {
        return collection.AddTransient<AddFunctionViewModel>()
                         .AddTransient<FunctionSelectionViewModel>()
                         .AddTransient<MainWindowViewModel>()
                         .AddTransient<ChangeLanguageViewModel>()
                         .AddTransient<SettingsViewModel>()
                         .AddTransient<AppViewModel>()
                         .AddTransient<AllPluginsViewModel>()
                         .AddTransient<PluginViewModel>()
                         .AddTransient<BoolPluginSettingViewModel>()
                         .AddTransient<StringPluginSettingViewModel>()
                         .AddTransient<IntPluginSettingViewModel>()
                         .AddTransient<FloatPluginSettingView>();
    }

    /// <summary>
    /// Add all the views
    /// </summary>
    /// <param name="collection">The collection to extend</param>
    /// <returns>The extended collection</returns>
    public static IServiceCollection AddViews(this IServiceCollection collection)
    {
        return collection.AddTransient(resolver => new MainWindow(resolver.GetRequiredService<IWindowManagementService>(), resolver.GetRequiredService<ISettingsService>())
        {
            DataContext = resolver?.GetService<MainWindowViewModel>(),
        })
        .AddTransient<ModalWindow>();
    }

    /// <summary>
    /// Add all the services
    /// </summary>
    /// <param name="collection">The collection to extend</param>
    /// <returns>The extended collection</returns>
    public static IServiceCollection AddServices(this IServiceCollection collection)
    {
        return collection.AddAllModelDepdencies()
                         .AddSingleton<IPathService, PathService>()
                         .AddSingleton<IStyleService, DefaultStyleService>()
                         .AddSingleton<IPluginTranslationService, PluginTranslationService>()
                         .AddSingleton<IFunctionSettingsService, FunctionSettingService>()
                         .AddSingleton<ILanguageService, ResourceCultureService>()
                         .AddSingleton<IWindowManagementService, WindowManagementService>()
                         .AddSingleton<ISerializationOptionFactory<JsonSerializerOptions>, JsonSerializationOptionFactory>()
                         .AddSingleton<IFunctionService, SerializedFunctionService>()
                         .AddSingleton<IViewModelLocatorService, ViewModelLocator>()
                         .AddSingleton<IDependencyResolverService, MicrosoftDepdencyResolverService>()
                         .AddSingleton<ISettingsService, SerializedSettingsService>()
                         .AddTransient<ViewLocator>()

                         .AddTransient<IImageService, ImageService>();
    }
}
