using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Microsoft.Extensions.Logging;
using ModularToolManager.Models;
using ModularToolManager.Views;
using Splat;
using System.Linq;
using System.Threading.Tasks;

namespace ModularToolManager.Services.Ui;

/// <summary>
/// Service class to show modal as a new window
/// </summary>
internal class WindowManagementService : IWindowManagmentService
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
            loggingService?.LogError("Could not find main window to use a parent for open file dialog");
            return new string[0];
        }
        return await ShowOpenFileDialogAsync(fileDialogModel, desktop!.MainWindow);
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
