using Microsoft.Extensions.DependencyInjection;
using ModularToolManager.Services.Settings;
using ModularToolManager.Services.Ui;
using ModularToolManager.ViewModels;

namespace ModularToolManager.Views;

/// <summary>
/// Dependency Injection for views
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Add all the views
    /// </summary>
    /// <param name="collection">The collection to extend</param>
    /// <returns>The extended collection</returns>
    public static IServiceCollection AddViews(this IServiceCollection collection)
    {
        return collection.AddTransient(resolver => new MainWindow(
            resolver.GetRequiredService<IWindowManagementService>(),
            resolver.GetRequiredService<ISettingsService>(),
            resolver.GetRequiredService<IWindowPositionFactory>())
        {
            DataContext = resolver?.GetService<MainWindowViewModel>(),
        })
        .AddTransient<FloatPluginSettingView>()
        .AddTransient<ModalWindow>();
    }
}
