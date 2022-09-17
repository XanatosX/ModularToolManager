using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Microsoft.Extensions.Logging;
using ModularToolManager.Models;
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
    /// Service to use to resolve dependencies
    /// </summary>
    private readonly IDependencyResolverService dependencyResolverService;

    /// <summary>
    /// Create a new isntance of this class
    /// </summary>
    /// <param name="loggingService">The logging service to use</param>
    public WindowManagementService(ILogger<WindowManagementService>? loggingService, IDependencyResolverService dependencyResolverService, IStyleService styleService)
    {
        this.loggingService = loggingService;
        this.dependencyResolverService = dependencyResolverService;
        this.styleService = styleService;
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
        var modalContent = new ModalWindowViewModel(modalData.Title, modalData.ImagePath, modalData.ModalContent, styleService);
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
        OpenFileDialog openFileDialog = new OpenFileDialog
        {
            AllowMultiple = fileDialogModel.AllowMultipleSelection,
            Filters = fileDialogModel.FileDialogFilters.ToList(),
        };
        if (!string.IsNullOrEmpty(fileDialogModel.InialDirectory))
        {
            openFileDialog.Directory = fileDialogModel.InialDirectory;
        }
        string[] files = await openFileDialog.ShowAsync(parent) ?? Array.Empty<string>();
        return files;
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
