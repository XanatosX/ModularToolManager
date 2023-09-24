using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ModularToolManager.Models;
using ModularToolManager.Services.Data;
using ModularToolManager.Services.Serialization;
using ModularToolManagerModel.Data.Serialization;
using ModularToolManagerModel.Services.Functions;
using ModularToolManagerModel.Services.IO;
using ModularToolManagerModel.Services.Logging;
using ModularToolManagerModel.Services.Plugin;
using ModularToolManagerModel.Services.Serialization;
using ModularToolManagerPlugin.Models;
using ModularToolManagerPlugin.Services;
using System.Text.Json.Serialization;

namespace ModularToolManagerModel.DependencyInjection;

/// <summary>
/// Static extension method class to add dependencies from the model
/// </summary>
public static class ModelDependencyInjection
{
    /// <summary>
    /// Add all the dependencies for the model itself
    /// </summary>
    /// <param name="collection">The collection to add the dependencies to</param>
    /// <returns>The collection with the additional dependencies</returns>
    public static IServiceCollection AddModelDependencies(this IServiceCollection collection)
    {
        return collection.AddSingleton<IFunctionSettingsService, FunctionSettingService>()
                         .AddSingleton(typeof(IRepository<,>), typeof(GenericJsonRepository<,>))
                         .AddSingleton(typeof(IRepository<FunctionModel, string>), provider =>
                         {
                             var pathService = provider.GetRequiredService<IPathService>();
                             var fileService = provider.GetRequiredService<IFileSystemService>();
                             var serializeService = provider.GetRequiredService<ISerializeService>();
                             string path = Path.Combine(pathService.GetSettingsFolderPathString(), "functions.con");
                             return new GenericJsonRepository<FunctionModel, string>(
                                 path,
                                 fileService,
                                 serializeService);
                         })
                         .AddSingleton<IFileSystemService, FileSystemService>()
                         .AddTransient(typeof(IPluginLoggerService<>), typeof(LoggingPluginAdapter<>))
                         .AddSingleton<IPluginService, PluginService>()
                         .AddTransient<ISerializeService, JsonSerializationService>()
                         .AddSingleton<IFunctionService, DefaultFunctionService>()
                         .AddJsonConverters();
    }

    /// <summary>
    /// Add all additional dependencies for the model
    /// </summary>
    /// <param name="collection">The collection to add the dependencies to</param>
    /// <returns>The collection with the additional dependencies</returns>
    public static IServiceCollection AddAdditionalDependencies(this IServiceCollection collection)
    {
        return collection.AddSingleton<IUrlOpenerService, UrlOpenerService>();
    }

    /// <summary>
    /// Add all the json serializer converters to the dependencies
    /// </summary>
    /// <param name="collection">The collection to add the dependencies to</param>
    /// <returns>The collection with the additional dependencies</returns>
    private static IServiceCollection AddJsonConverters(this IServiceCollection collection)
    {
        return collection.AddSingleton<JsonConverter<SettingModel>, SettingModelJsonConverter>();
    }

    /// <summary>
    /// Add all possible dependencies
    /// </summary>
    /// <param name="collection">The collection to add the dependencies to</param>
    /// <returns>The collection with the additional dependencies</returns>
    public static IServiceCollection AddAllModelDependencies(this IServiceCollection collection)
    {
        return collection.AddModelDependencies()
                         .AddAdditionalDependencies();
    }
}
