using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Microsoft.Extensions.Logging;
using ModularToolManager.Models;
using ModularToolManager.Views;
using Splat;
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
    /// Create a new isntance of this class
    /// </summary>
    /// <param name="loggingService">The logging service to use</param>
    public WindowManagementService(ILogger<WindowManagementService>? loggingService)
    {
        this.loggingService = loggingService;
    }

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

    public Window? GetTopmostWindow()
    {
        IEnumerable<Window> windows = GetAllActiveWindows().Where(window => window.IsActive).Where(window => window.IsVisible);
        return windows.LastOrDefault();

    }

    /// <inheritdoc/>
    public async Task ShowModalWindowAsync(ShowWindowModel modalData, Window parent)
    {
        ModalWindow? window = Locator.Current.GetService<ModalWindow>();
        if (window is null)
        {
            loggingService?.LogError($"Could not get {typeof(ModalWindow).FullName}, abort opening modal");
            return;
        }
        window.WindowStartupLocation = modalData.StartupLocation;
        window.DataContext = modalData.ViewModel;
        await window.ShowDialog(parent);
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
        string[] files = await openFileDialog.ShowAsync(parent) ?? new string[0];
        return files;
    }

    /// <inheritdoc/>
    public async Task<string[]> ShowOpenFileDialogAsync(ShowOpenFileDialogModel fileDialogModel)
    {
        var desktop = Avalonia.Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime;
        if (desktop is null)
        {

            return new string[0];
        }
        Window? mainWindow = GetMainWindow();
        if (mainWindow is null)
        {
            loggingService?.LogError("Could not find main window to use a parent for open file dialog");
            return new string[0];
        }
        return await ShowOpenFileDialogAsync(fileDialogModel, mainWindow!);
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
