using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.ComponentModel;
using ModularToolManager.Models;
using ModularToolManager.Services.Styling;
using ModularToolManager.ViewModels.Extenions;
using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace ModularToolManager.ViewModels;

/// <summary>
/// Class to open modals based on the modal information model
/// </summary>
public class ModalWindowViewModel : ObservableObject
{
    /// <summary>
    /// The service to use for loading styles
    /// </summary>
    private readonly IStyleService? styleService;

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

    /// <summary>
    /// Interaction to close the modal window
    /// </summary>
    //public Interaction<Unit, Unit> CloseWindowInteraction { get; }


    public ModalWindowViewModel(string title, string pathName, ObservableObject modalContent)
    {
        Title = title;
        ModalContent = modalContent;
        //styleService = Locator.Current.GetService<IStyleService>();
        //CloseWindowInteraction = new Interaction<Unit, Unit>();

        if (modalContent is IModalWindowEvents windowEvents)
        {
            windowEvents.Closing += (_, _) =>
            {
                Task.Run(async () =>
                {
                    //_ = await CloseWindowInteraction.Handle(new Unit());
                }).GetAwaiter().GetResult();
            };
        }

        IStyle? styles = styleService?.GetCurrentAppIncludeStyles().FirstOrDefault();
        PathIcon = styleService?.GetStyleByName<StreamGeometry>(pathName) ?? null;
    }
}