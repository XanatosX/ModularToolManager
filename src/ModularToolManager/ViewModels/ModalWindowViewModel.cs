using Avalonia.Media;
using Avalonia.Styling;
using ModularToolManager.Services.Styling;
using ModularToolManager.ViewModels.Extenions;
using ReactiveUI;
using Splat;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace ModularToolManager.ViewModels;

/// <summary>
/// Class to open modals based on the modal information model
/// </summary>
public class ModalWindowViewModel : ViewModelBase
{
    /// <summary>
    /// The service to use for loading styles
    /// </summary>
    private readonly IStyleService styleService;

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
    public ViewModelBase ModalContent { get; }

    /// <summary>
    /// Interaction to close the modal window
    /// </summary>
    public Interaction<Unit, Unit> CloseWindowInteraction { get; }

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    /// <param name="title">The title to display</param>
    /// <param name="pathName">The name of the path icon to use</param>
    /// <param name="modalContent">The content of the modal to show</param>
    public ModalWindowViewModel(string title, string pathName, ViewModelBase modalContent)
    {
        Title = title;
        ModalContent = modalContent;
        styleService = Locator.Current.GetService<IStyleService>();
        CloseWindowInteraction = new Interaction<Unit, Unit>();

        if (modalContent is IModalWindowEvents windowEvents)
        {
            windowEvents.Closing += (_, _) =>
            {
                Task.Run(async () =>
                {
                    _ = await CloseWindowInteraction.Handle(new Unit());
                }).GetAwaiter().GetResult();
            };
        }

        IStyle? styles = styleService.GetCurrentAppIncludeStyles().FirstOrDefault();
        PathIcon = styleService.GetStyleByName<StreamGeometry>(pathName) ?? null;
    }
}