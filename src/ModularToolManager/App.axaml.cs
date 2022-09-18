using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using ModularToolManager.DependencyInjection;
using ModularToolManager.Models.Messages;
using ModularToolManager.Services.IO;
using ModularToolManager.Services.Logging;
using ModularToolManager.ViewModels;
using ModularToolManager.Views;
using ModularToolManagerModel.Services.IO;
using ModularToolManagerModel.Services.Logging;
using NLog.Config;
using NLog.Extensions.Logging;
using NLog.Targets;
using System;
using System.IO;

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
        var NLogConfiguration = new LoggingConfiguration();
        return new ServiceCollection().AddAvaloniaDefault()
                                      .AddServices()
                                      .AddViewModels()
                                      .AddViews()
                                      .AddLogging(config =>
                                      {
                                          IPathService pathService = new PathService();
                                          string basePath = pathService.GetSettingsFolderPathString() ?? Path.GetTempPath();
                                          string logFolder = Path.Combine(basePath, "logs");

                                          NLogConfiguration.AddTarget(new TraceTarget("trace-log"));
                                          NLogConfiguration.AddTarget(new FileTarget("default-file-log")
                                          {
                                              FileName = Path.Combine(logFolder, "application.log"),
                                              ArchiveAboveSize = 5242880,
                                              MaxArchiveFiles = 10
                                          });
                                          NLogConfiguration.AddTarget(new FileTarget("error-log")
                                          {
                                              FileName = Path.Combine(logFolder, "error.log"),
                                              ArchiveAboveSize = 5242880,
                                              MaxArchiveFiles = 5
                                          });
                                          NLogConfiguration.AddRule(NLog.LogLevel.Trace, NLog.LogLevel.Debug, "trace-log");
                                          NLogConfiguration.AddRule(NLog.LogLevel.Info, NLog.LogLevel.Warn, "default-file-log");
                                          NLogConfiguration.AddRule(NLog.LogLevel.Error, NLog.LogLevel.Fatal, "error-log");
                                          config.AddNLog(NLogConfiguration);
                                      })
                                      .AddSingleton<ILogFileService, NLogFileService>(provider => new NLogFileService(NLogConfiguration));
    }

    /// <inheritdoc/>
    public override void OnFrameworkInitializationCompleted()
    {
        var provider = BuildServiceCollection().BuildServiceProvider();

        WeakReferenceMessenger.Default.Register<RefreshMainWindow>(this, (_, e) =>
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.ShutdownMode = Avalonia.Controls.ShutdownMode.OnExplicitShutdown;
                if (desktop.MainWindow is IDisposable disposable)
                {
                    disposable.Dispose();
                }
                desktop.MainWindow.Close();
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
        ExpressionObserver.DataValidators.RemoveAll(x => x is DataAnnotationsValidationPlugin);
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
