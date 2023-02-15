using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Styling;
using Avalonia.Utilities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using ModularToolManager.Models;
using ModularToolManager.Models.Messages;
using ModularToolManager.Services.Settings;
using ModularToolManager.Services.Styling;
using ModularToolManager.Services.Ui;
using System.Linq;
using System.Reactive.Linq;

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

    [ObservableProperty]
    public Color applicationTintColor;

    [ObservableProperty]
    public float tintOpacity;

    [ObservableProperty]
    public float materialOpacity;
    private readonly IThemeService themeService;

    /// <summary>
    /// The title of the modal
    /// </summary>
    public string Title { get; }

    /// <summary>
    /// The content of the modal to display
    /// </summary>
    public ObservableObject ModalContent { get; }

    public ModalWindowViewModel(
        int themeId,
        string title,
        string? pathName,
        ObservableObject modalContent,
        IStyleService styleService,
        IThemeService themeService,
        IImageService imageService)
    {
        Title = title;
        ModalContent = modalContent;
        this.themeService = themeService;
        PathIcon = styleService.GetStyleByName<StreamGeometry>(pathName ?? string.Empty) ?? null;
        if (PathIcon is not null)
        {
            var image = imageService.CreateBitmap(PathIcon);
            WindowIcon = image is null ? null : new WindowIcon(image);
        }
        SwitchTheme(themeId);

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