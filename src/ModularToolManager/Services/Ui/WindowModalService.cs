using Avalonia.Controls;
using ModularToolManager.Models;
using ModularToolManager.Views;
using System.Threading.Tasks;

namespace ModularToolManager.Services.Ui;

/// <summary>
/// Service class to show modal as a new window
/// </summary>
internal class WindowModalService : IModalService
{
    /// <inheritdoc/>
    public async Task ShowModalWindowAsync(ShowWindowModel modalData, Window parent)
    {
        ModalWindow window = new ModalWindow();
        window.WindowStartupLocation = modalData.StartupLocation;
        window.DataContext = modalData.ViewModel;
        await window.ShowDialog(parent);
    }
}
