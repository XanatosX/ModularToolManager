using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.ComponentModel;
using ModularToolManager.Services.Styling;
using ModularToolManager.Services.Ui;
using System.Linq;
using System.Reactive.Linq;

namespace ModularToolManager.ViewModels;

/// <summary>
/// Class to open modals based on the modal information model
/// </summary>
public class ModalWindowViewModel : ObservableObject
{
    /// <summary>
    /// The path to the icon to show in the upper left corner
    /// </summary>
    public StreamGeometry? PathIcon { get; }

    /// <summary>
    /// The window icon to use
    /// </summary>
    public WindowIcon? WindowIcon { get; }

    /// <summary>
    /// The title of the modal
    /// </summary>
    public string Title { get; }

    /// <summary>
    /// The content of the modal to display
    /// </summary>
    public ObservableObject ModalContent { get; }

    public ModalWindowViewModel(string title, string? pathName, ObservableObject modalContent, IStyleService styleService, IImageService imageService)
    {
        Title = title;
        ModalContent = modalContent;
        PathIcon = styleService.GetStyleByName<StreamGeometry>(pathName ?? string.Empty) ?? null;
        if (PathIcon is not null)
        {
            var image = imageService.CreateBitmap(PathIcon);
            WindowIcon = image is null ? null : new WindowIcon(image);
        }
    }
}