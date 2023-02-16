using CommunityToolkit.Mvvm.ComponentModel;

namespace ModularToolManager.Models;

/// <summary>
/// Record to save internal data for showing a modal
/// </summary>
/// <param name="ThemeId">The id of the theme to use</param>
/// <param name="Title"> The title to show on the modal</param>
/// <param name="modalContent">The content of the modal</param>
/// <param name="IconName">The name of the icon to load</param>
/// <param name="CanResize">Can the modal be resized</param>
public record ModalWindowInformation(int ThemeId, string Title, ObservableObject modalContent, string? IconName, bool CanResize = true);
