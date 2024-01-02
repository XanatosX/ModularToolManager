using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using ModularToolManager.DependencyInjection;
using ModularToolManager.Models.Messages;
using ModularToolManager.Services.Settings;
using ModularToolManager.ViewModels;
using ModularToolManager.Views;
using ModularToolManagerModel.Services.IO;
using ModularToolManagerModel.Services.Language;
using Serilog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace ModularToolManager;

/// <inheritdoc/>
public class App : Application
{
    /// <inheritdoc/>
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    /// <summary>
    /// Build the service collection for dependency injection
    /// </summary>
    /// <returns>A usable service collection</returns>
    private IServiceCollection BuildServiceCollection()
    {
        IServiceCollection collection = new ServiceCollection();
        return collection.AddServices()
                                      .AddViewModels()
                                      .AddViews()
                                      .AddLogging(config =>
                                      {
                                          config.AddSerilog(CreateLoggerConfig(collection));
                                      });
    }

    /// <summary>
    /// Create logger configuration for the application
    /// </summary>
    /// <param name="collection"></param>
    /// <returns></returns>
    private Serilog.ILogger CreateLoggerConfig(IServiceCollection collection)
    {
        var provider = collection.BuildServiceProvider();
        IPathService pathService = provider.GetRequiredService<IPathService>();
        string basePath = pathService.GetSettingsFolderPathString() ?? Path.GetTempPath();
        string logFolder = Path.Combine(basePath, "logs");

        string fileTemplate = "{Timestamp} [{Level:w4}] [{SourceContext}] {Message:l}{NewLine}{Exception}";
        return new LoggerConfiguration().MinimumLevel.Debug().WriteTo
                                                                      .File(Path.Combine(logFolder, "application.log"),
                                                                            outputTemplate: fileTemplate,
                                                                            rollOnFileSizeLimit: true,
                                                                            fileSizeLimitBytes: 5242880,
                                                                            restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Debug)
                                                                      .WriteTo.File(Path.Combine(logFolder, "error.log"),
                                                                                    outputTemplate: fileTemplate,
                                                                                    rollOnFileSizeLimit: true,
                                                                                    fileSizeLimitBytes: 5242880,
                                                                                    restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error)
                                                                      .Enrich.FromLogContext()
                                                                      .CreateLogger();
    }

    /// <inheritdoc/>
    public override void OnFrameworkInitializationCompleted()
    {
        var provider = BuildServiceCollection().BuildServiceProvider();
        WeakReferenceMessenger.Default.Register<RefreshMainWindow>(this, (_, _) =>
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                if (desktop is null || desktop.MainWindow is null)
                {
                    return;
                }
                desktop.ShutdownMode = Avalonia.Controls.ShutdownMode.OnExplicitShutdown;
                if (desktop.MainWindow is IDisposable disposable)
                {
                    disposable.Dispose();
                }
                desktop!.MainWindow!.Close();
                SetupMainWindow(provider);
                desktop.MainWindow.Show();
                desktop.ShutdownMode = Avalonia.Controls.ShutdownMode.OnLastWindowClose;
            }
        });

        SetupApplicationContainer(provider);
        SetupMainWindow(provider);

        base.OnFrameworkInitializationCompleted();
    }

    /// <summary>
    /// Method to setup the application main container
    /// </summary>
    /// <param name="provider">The provider to use for getting class instances</param>
    private void SetupApplicationContainer(ServiceProvider provider)
    {
        ILanguageService langService = provider.GetRequiredService<ILanguageService>();
        ISettingsService settingsService = provider.GetRequiredService<ISettingsService>();
        CultureInfo language = settingsService.GetApplicationSettings().CurrentLanguage ?? CultureInfo.CurrentCulture;
        langService.ChangeLanguage(language);

        List<IDataValidationPlugin> validatorsToRemove = BindingPlugins.DataValidators.Where(item => item is DataAnnotationsValidationPlugin).ToList();
        foreach (var item in validatorsToRemove)
        {
            BindingPlugins.DataValidators.Remove(item);
        }
        DataContext = provider.GetService<AppViewModel>();

        var locator = provider.GetService<ViewLocator>();
        if (locator is not null)
        {
            DataTemplates.Add(locator);
        }
    }

    /// <summary>
    /// Method to use for setting the main Window
    /// </summary>
    /// <param name="provider">The provider to use for creating the main window</param>
    private void SetupMainWindow(ServiceProvider provider)
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = provider.GetService<MainWindow>();
        }
    }
}
