using Avalonia.Controls;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using ModularToolManager.Models;
using ModularToolManager.Models.Messages;
using ModularToolManager.Services.Styling;
using ModularToolManager.Services.Ui;
using System.Linq;

namespace ModularToolManager.ViewModels;

/// <summary>
/// Class to open modals based on the modal information model
/// </summary>
public partial class ModalWindowViewModel : ObservableObject
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
    /// The property for the application tint color
    /// </summary>
    [ObservableProperty]
    private Color applicationTintColor;

    /// <summary>
    /// The property for the application tint opacity
    /// </summary>
    [ObservableProperty]
    private float tintOpacity;

    /// <summary>
    /// The property for the application material opacity
    /// </summary>
    [ObservableProperty]
    private float materialOpacity;

    /// <summary>
    /// The service used to switch the application theme
    /// </summary>
    private readonly IThemeService themeService;

    /// <summary>
    /// The title of the modal
    /// </summary>
    public string Title { get; }

    /// <summary>
    /// The content of the modal to display
    /// </summary>
    public ObservableObject ModalContent { get; }

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    /// <param name="windowInformation">The information to create the window</param>
    /// <param name="styleService">The style service to use</param>
    /// <param name="themeService">The theme service to use</param>
    /// <param name="imageService">The service used to get the window icon</param>
    public ModalWindowViewModel(
        ModalWindowInformation windowInformation,
        IStyleService styleService,
        IThemeService themeService,
        IImageService imageService)
    {
        Title = windowInformation.Title;
        ModalContent = windowInformation.modalContent;
        this.themeService = themeService;
        PathIcon = styleService.GetStyleByName<StreamGeometry>(windowInformation.IconName ?? string.Empty) ?? null;
        if (PathIcon is not null)
        {
            var image = imageService.CreateBitmap(PathIcon);
            WindowIcon = image is null ? null : new WindowIcon(image);
        }
        SwitchTheme(windowInformation.ThemeId);

        WeakReferenceMessenger.Default.Register<ApplicationThemeUpdated>(this, (_, e) => SwitchTheme(e.Value));
    }

    private void SwitchTheme(int themeId)
    {
        var theme = themeService.GetStyleById(themeId);
        theme ??= theme ??= themeService.GetAllStyles().FirstOrDefault() ?? new ApplicationStyle { MaterialOpacity = 1, TintOpacity = 0.65f, TintColor = Colors.Pink };
        if (theme is null)
        {
            return;
        }
        ApplicationTintColor = theme.TintColor ?? Colors.Pink;
        MaterialOpacity = theme.MaterialOpacity;
        TintOpacity = theme.TintOpacity;
        themeService.ChangeApplicationTheme(theme);
    }
}