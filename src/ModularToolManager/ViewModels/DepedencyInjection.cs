using Microsoft.Extensions.DependencyInjection;

namespace ModularToolManager.ViewModels;

/// <summary>
/// Dependency Injection for view models
/// </summary>
public static class DependencyInjection
{
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
                         .AddTransient<AboutViewModel>();
    }
}
