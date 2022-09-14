using Avalonia.Controls;
using ModularToolManager.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ModularToolManager.Services.Ui;

/// <summary>
/// Interface to use for open up modals
/// </summary>
public interface IWindowManagementService
{
    IEnumerable<Window> GetAllActiveWindows();

    Window? GetTopmostWindow();

    Window? GetMainWindow();

    /// <summary>
    /// Show a new modal async
    /// </summary>
    /// <param name="modalData">Thhe modal data to be placed inside of the modal</param>
    /// <param name="parent">The parent of the modal</param>
    /// <returns>A awaitable task for the modal</returns>
    Task ShowModalWindowAsync(ShowWindowModel modalData, Window? parent);

    /// <summary>
    /// Show a new open file dialog
    /// </summary>
    /// <param name="fileDialogModel">The open file dialog model with all the data for the file dialog</param>
    /// <returns>A list with strings contaning the selected files</returns>
    Task<string[]> ShowOpenFileDialogAsync(ShowOpenFileDialogModel fileDialogModel);

    /// <summary>
    /// Show a new open file dialog
    /// </summary>
    /// <param name="fileDialogModel">The open file dialog model with all the data for the file dialog</param>
    /// <param name="parent">The parent to set for the new file open dialog</param>
    /// <returns>A list with strings contaning the selected files</returns>
    Task<string[]> ShowOpenFileDialogAsync(ShowOpenFileDialogModel fileDialogModel, Window parent);

    /// <summary>
    /// Show a new save file dialog
    /// </summary>
    /// <param name="fileDialogModel">The open file dialog model with all the data for the file dialog</param>
    /// <returns>A list with strings contaning the selected file to save to</returns>
    Task<string?> ShowSaveFileDialogAsync(ShowOpenFileDialogModel fileDialogModel);

    /// <summary>
    /// Show a new save file dialog
    /// </summary>
    /// <param name="fileDialogModel">The open file dialog model with all the data for the file dialog</param>
    /// <param name="parent">The parent to set for the save file  dialog</param>
    /// <returns>A list with strings contaning the selected file to save to</returns>
    Task<string?> ShowSaveFileDialogAsync(ShowOpenFileDialogModel fileDialogModel, Window parent);
}


