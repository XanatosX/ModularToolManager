using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.ComponentModel;
using ModularToolManager.Services.Styling;
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
    /// The title of the modal
    /// </summary>
    public string Title { get; }

    /// <summary>
    /// The content of the modal to display
    /// </summary>
    //@TODO: Change to proper type as soon as obsolete part removed
    public object ModalContent { get; }

    public ModalWindowViewModel(string title, string pathName, ObservableObject modalContent, IStyleService styleService)
    {
        Title = title;
        ModalContent = modalContent;
        IStyle? styles = styleService?.GetCurrentAppIncludeStyles().FirstOrDefault();
        PathIcon = styleService?.GetStyleByName<StreamGeometry>(pathName) ?? null;
    }
}