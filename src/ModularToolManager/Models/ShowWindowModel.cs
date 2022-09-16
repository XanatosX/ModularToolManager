using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ModularToolManager.Models;

/// <summary>
/// Model for window modal information
/// </summary>
/// <param name="Title">The title of the modal window</param>
/// <param name="ImagePath">An null string or the path to the image to show</param>
/// <param name="ModalContent">The content of the modal</param>
/// <param name="StartupLocation">The location where the modal should be positioned on startup</param>
/// 
public record ShowWindowModel(string Title, string? ImagePath, ObservableObject ModalContent, WindowStartupLocation StartupLocation);
