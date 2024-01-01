using Avalonia.Controls;
using Avalonia.Platform.Storage;
using System.Collections.Generic;

namespace ModularToolManager.Models;

/// <summary>
/// Record for the opening a file open dialog
/// </summary>
/// <param name="FileDialogFilters">The filter for the dialog</param>
/// <param name="InitialDirectory">The directory to start in</param>
/// <param name="AllowMultipleSelection">Allow selecting multiple files</param>
public record ShowOpenFileDialogModel(List<FilePickerFileType> FileDialogFilters, string? InitialDirectory, bool AllowMultipleSelection);
