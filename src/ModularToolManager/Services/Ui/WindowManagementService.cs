using Avalonia.Controls;
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
    private readonly ILogger<WindowManagementService>? logger;

    public WindowManagementService(ILogger<WindowManagementService>? logger)
    {
        this.logger = logger;
    }

    /// <inheritdoc/>
    public async Task ShowModalWindowAsync(ShowWindowModel modalData, Window parent)
    {
        ModalWindow? window = Locator.Current.GetService<ModalWindow>();
        if (window is null)
        {
            logger?.LogError($"Could not get {typeof(ModalWindow).FullName}, abort opening modal");
            return;
        }
        window.WindowStartupLocation = modalData.StartupLocation;
        window.DataContext = modalData.ViewModel;
        await window.ShowDialog(parent);
    }

    public Task<string[]> ShowOpenFileDialogAsync(ShowOpenFileDialogModel fileDialogModel, Window parent)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog
        {
            AllowMultiple = fileDialogModel.AllowMultipleSelection,
            Filters = fileDialogModel.FileDialogFilters.ToList(),
        };
        string? directory = openFileDialog.Directory;
        throw new System.NotImplementedException();
    }

    public Task<string?> ShowSaveFileDialogAsync(FileDialogFilter filter, Window parent)
    {
        throw new System.NotImplementedException();
    }
}
