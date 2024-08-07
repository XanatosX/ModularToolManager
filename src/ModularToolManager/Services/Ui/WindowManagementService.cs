﻿using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;
using Microsoft.Extensions.Logging;
using ModularToolManager.Models;
using ModularToolManager.Services.Settings;
using ModularToolManager.Services.Styling;
using ModularToolManager.ViewModels;
using ModularToolManager.Views;
using ModularToolManagerModel.Services.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModularToolManager.Services.Ui;

/// <summary>
/// Service class to show modal as a new window
/// </summary>
internal class WindowManagementService : IWindowManagementService
{
    /// <summary>
    /// The logger service to use
    /// </summary>
    private readonly ILogger<WindowManagementService>? loggingService;

    /// <summary>
    /// Service to use to load styles
    /// </summary>
    private readonly IStyleService styleService;

    /// <summary>
    /// The settings service to use
    /// </summary>
    private readonly ISettingsService settingsService;

    /// <summary>
    /// The service used to load available themes
    /// </summary>
    private readonly IThemeService themeService;

    /// <summary>
    /// The service used to load the window icons
    /// </summary>
    private readonly IImageService imageService;

    /// <summary>
    /// Service to use to resolve dependencies
    /// </summary>
    private readonly IDependencyResolverService dependencyResolverService;

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    /// <param name="loggingService">The logging service to use</param>
    public WindowManagementService(
        ILogger<WindowManagementService>? loggingService,
        IDependencyResolverService dependencyResolverService,
        IStyleService styleService,
        ISettingsService settingsService,
        IThemeService themeService,
        IImageService imageService)
    {
        this.loggingService = loggingService;
        this.dependencyResolverService = dependencyResolverService;
        this.styleService = styleService;
        this.settingsService = settingsService;
        this.themeService = themeService;
        this.imageService = imageService;
    }

    /// <inheritdoc/>
    public IEnumerable<Window> GetAllActiveWindows()
    {
        var desktop = Avalonia.Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime;
        if (desktop is null)
        {
            loggingService?.LogError("Could not get main window from application");
            return Enumerable.Empty<Window>();
        }
        return desktop.Windows;
    }

    /// <inheritdoc/>
    public Window? GetMainWindow()
    {
        var desktop = Avalonia.Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime;
        if (desktop is null)
        {
            loggingService?.LogError("Can't get application lifetime");
            return null;
        }
        return desktop!.MainWindow;
    }

    /// <inheritdoc/>
    public Window? GetTopmostWindow()
    {
        IEnumerable<Window> windows = GetAllActiveWindows().Where(window => window.IsActive).Where(window => window.IsVisible);
        return windows.LastOrDefault();

    }

    /// <inheritdoc/>
    public async Task ShowModalWindowAsync(ShowWindowModel modalData, Window? parent)
    {
        ModalWindow? window = dependencyResolverService?.GetDependency<ModalWindow>();
        if (modalData.ModalContent is null)
        {
            loggingService?.LogError("No content for the modal was provided, cannot open the window");
            return;
        }
        var modalContent = new ModalWindowViewModel(
            new ModalWindowInformation(settingsService.GetApplicationSettings().SelectedThemeId,
                                       modalData.Title,
                                       modalData.ModalContent,
                                       modalData.ImagePath,
                                       modalData.CanResize),
            styleService,
            themeService,
            imageService);
        parent = parent ?? GetMainWindow();
        if (window is null)
        {
            loggingService?.LogError($"Could not get {typeof(ModalWindow).FullName}, abort opening modal!");
            return;
        }
        if (parent is null)
        {
            loggingService?.LogError("Could not get parent for modal window, abort opening modal!");
            return;
        }
        window.WindowStartupLocation = modalData.StartupLocation;
        window.DataContext = modalContent;
        await window.ShowDialog(parent);
        if (window.DataContext is ModalWindowViewModel modalWindowViewModel)
        {
            var data = modalWindowViewModel.ModalContent as IDisposable;
            data?.Dispose();
        }
    }

    /// <inheritdoc/>
    public async Task<string[]> ShowOpenFileDialogAsync(ShowOpenFileDialogModel fileDialogModel, Window parent)
    {
        IStorageFolder? initialDirectory = await parent.StorageProvider.TryGetFolderFromPathAsync(fileDialogModel.InitialDirectory ?? string.Empty);
        var files = await parent.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions {
            AllowMultiple = fileDialogModel.AllowMultipleSelection,
            FileTypeFilter = fileDialogModel.FileDialogFilters.ToList(),
            SuggestedStartLocation = initialDirectory
        });
        return files.Select(file => file.Path.LocalPath).ToArray();
    }

    /// <inheritdoc/>
    public async Task<string[]> ShowOpenFileDialogAsync(ShowOpenFileDialogModel fileDialogModel)
    {
        var returnValue = Array.Empty<string>();
        var desktop = Avalonia.Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime;
        if (desktop is null)
        {
            return returnValue;
        }
        Window? mainWindow = GetMainWindow();
        if (mainWindow is null)
        {
            loggingService?.LogError("Could not find main window to use a parent for open file dialog");
            return returnValue;
        }
        returnValue = await ShowOpenFileDialogAsync(fileDialogModel, mainWindow!);
        return returnValue;
    }

    /// <inheritdoc/>
    public Task<string?> ShowSaveFileDialogAsync(ShowOpenFileDialogModel fileDialogModel)
    {
        throw new System.NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<string?> ShowSaveFileDialogAsync(ShowOpenFileDialogModel fileDialogModel, Window parent)
    {
        throw new System.NotImplementedException();
    }
}
