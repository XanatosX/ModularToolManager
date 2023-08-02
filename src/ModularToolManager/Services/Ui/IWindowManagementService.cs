using Avalonia.Controls;
using ModularToolManager.Enums;
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
    /// <summary>
    /// Get all the currently active windows
    /// </summary>
    /// <returns>All currently active windows</returns>
    IEnumerable<Window> GetAllActiveWindows();

    /// <summary>
    /// The the topmost window
    /// </summary>
    /// <returns>The topmost window</returns>
    Window? GetTopmostWindow();

    /// <summary>
    /// Get the main window of the application
    /// </summary>
    /// <returns>The main window or nothing if no main window was found</returns>
    Window? GetMainWindow();

    /// <summary>
    /// Show a new modal async
    /// </summary>
    /// <param name="modalData">Thhe modal data to be placed inside of the modal</param>
    /// <param name="parent">The parent of the modal</param>
    /// <returns>A awaitable task for the modal</returns>
    Task ShowModalWindowAsync(ShowWindowModel modalData, Window? parent);

    /// <summary>
    /// Show modal window async unsig the main window as a parent
    /// </summary>
    /// <param name="modalData">The data to show</param>
    /// <returns>The task to await until the window got closed</returns>
    async Task ShowModalWindowAsync(ShowWindowModel modalData) => await ShowModalWindowAsync(modalData, GetMainWindow());

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


