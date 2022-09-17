using Avalonia.Controls;
using System.Collections.Generic;

namespace ModularToolManager.Models;

/// <summary>
/// Record for the openinig a file open dialog
/// </summary>
/// <param name="FileDialogFilters">The filter for the dialog</param>
/// <param name="InialDirectory">The directory to start in</param>
/// <param name="AllowMultipleSelection">Allow selecting multiple files</param>
public record ShowOpenFileDialogModel(List<FileDialogFilter> FileDialogFilters, string? InialDirectory, bool AllowMultipleSelection);
