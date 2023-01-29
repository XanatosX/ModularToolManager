using Microsoft.Extensions.DependencyInjection;
using ModularToolManager.Services.Serialization;
using ModularToolManagerModel.Services.IO;
using ModularToolManagerModel.Services.Logging;
using ModularToolManagerModel.Services.Plugin;
using ModularToolManagerModel.Services.Serialization;
using ModularToolManagerPlugin.Services;

namespace ModularToolManagerModel.DependencyInjection;
public static class ModelDependencyInjection
{
    public static IServiceCollection AddModelDependencies(this IServiceCollection collection)
    {
        return collection.AddSingleton<IFunctionSettingsService, FunctionSettingService>()
                         .AddSingleton<IFileSystemService, FileSystemService>()
                         .AddTransient(typeof(IPluginLoggerService<>), typeof(LoggingPluginAdapter<>))
                         .AddSingleton<IPluginService, PluginService>()
                         .AddTransient<ISerializeService, JsonSerializationService>();
    }

    public static IServiceCollection AddAdditionalDependencies(this IServiceCollection collection)
    {
        return collection.AddSingleton<IUrlOpenerService, UrlOpenerService>();
    }

    public static IServiceCollection AddAllModelDepdencies(this IServiceCollection collection)
    {
        return collection.AddModelDependencies()
                         .AddAdditionalDependencies();
    }
}
