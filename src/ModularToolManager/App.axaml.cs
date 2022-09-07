using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Microsoft.Extensions.DependencyInjection;
using ModularToolManager.DependencyInjection;
using ModularToolManager.Services.IO;
using ModularToolManager.Services.Logging;
using ModularToolManager.Views;
using ModularToolManagerModel.Services.IO;
using ModularToolManagerModel.Services.Logging;
using NLog.Config;
using NLog.Extensions.Logging;
using NLog.Targets;
using ReactiveUI;
using Splat;
using Splat.Microsoft.Extensions.DependencyInjection;
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

    private IServiceCollection BuildServiceCollection()
    {
        var NLogConfiguration = new LoggingConfiguration();
        return new ServiceCollection().AddAvaloniaDefault()
                                      .AddServices()
                                      .AddViewModels()
                                      .AddViews()
                                      .AddLogging(config =>
                                      {
                                          var pathService = Locator.Current.GetService<IPathService>() ?? new PathService();
                                          string basePath = pathService?.GetSettingsFolderPathString() ?? Path.GetTempPath();
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
        BuildServiceCollection().UseMicrosoftDependencyResolver();


        Locator.CurrentMutable.InitializeSplat();
        Locator.CurrentMutable.InitializeReactiveUI();
        RxApp.MainThreadScheduler = AvaloniaScheduler.Instance;
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = Locator.Current.GetService<MainWindow>();
        }

        base.OnFrameworkInitializationCompleted();
    }
}
