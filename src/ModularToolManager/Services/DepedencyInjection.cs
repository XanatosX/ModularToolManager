using Microsoft.Extensions.DependencyInjection;
using ModularToolManager.Services.Dependencies;
using ModularToolManager.Services.IO;
using ModularToolManager.Services.Language;
using ModularToolManager.Services.Settings;
using ModularToolManager.Services.Styling;
using ModularToolManager.Services.Ui;
using ModularToolManagerModel.DependencyInjection;
using ModularToolManagerModel.Services.Dependency;
using ModularToolManagerModel.Services.IO;
using ModularToolManagerModel.Services.Language;
using ModularToolManagerModel.Services.Serialization;
using ModularToolManagerPlugin.Services;
using System.Text.Json;

namespace ModularToolManager.Services;

/// <summary>
/// Dependency Injection for services
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Add all the services
    /// </summary>
    /// <param name="collection">The collection to extend</param>
    /// <returns>The extended collection</returns>
    public static IServiceCollection AddServices(this IServiceCollection collection)
    {
                return collection.AddAllModelDependencies()
                                 .AddSingleton<IPathService, PathService>()
                                 .AddSingleton<IStyleService, DefaultStyleService>()
                                 .AddSingleton<IPluginTranslationService, PluginTranslationService>()
                                 .AddSingleton<IFunctionSettingsService, FunctionSettingService>()
                                 .AddSingleton<ILanguageService, ResourceCultureService>()
                                 .AddSingleton<IWindowManagementService, WindowManagementService>()
                                 .AddSingleton<ISerializationOptionFactory<JsonSerializerOptions>, JsonSerializationOptionFactory>()
                                 .AddSingleton<IViewModelLocatorService, ViewModelLocator>()
                                 .AddSingleton<IDependencyResolverService, MicrosoftDepdencyResolverService>()
                                 .AddSingleton<ISettingsService, SerializedSettingsService>()
                                 .AddSingleton<PluginSettingViewModelService>()
                                 .AddSingleton<IThemeService, AvaloniaThemeService>()
                                 .AddSingleton<IWindowPositionFactory, DefaultWindowPositionStrategyFactory>()
                                 .AddTransient<IImageService, ImageService>()
                                 .AddTransient<ResourceReaderService>()
                                 .AddTransient<GetApplicationInformationService>()
                                 .AddTransient<ViewLocator>();
    }
}