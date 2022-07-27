using Avalonia.Controls;
using ModularToolManager.ViewModels;

namespace ModularToolManager.Models;

/// <summary>
/// Model for window modal information
/// </summary>
/// <param name="ViewModel">The view model to use</param>
/// <param name="StartupLocation">The location where the modal should be positioned on startup</param>
public record ShowWindowModel(ModalWindowViewModel ViewModel, WindowStartupLocation StartupLocation);
