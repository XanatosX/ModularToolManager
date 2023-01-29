using Avalonia.Media;
using Avalonia.Media.Imaging;

namespace ModularToolManager.Services.Ui;

/// <summary>
/// Service to get bitmap images from stream geometry
/// </summary>
public interface IImageService
{
    /// <summary>
    /// Create a bitmap with a black brush
    /// </summary>
    /// <param name="streamGeometry">The geometry to convert</param>
    /// <returns>If a bitmap could be generated the generated bitmap otherwise null</returns>
    Bitmap? CreateBitmap(StreamGeometry streamGeometry) => CreateBitmap(streamGeometry, new SolidColorBrush(Color.FromRgb(0, g: 0, 0)));

    /// <summary>
    /// Create a bitmap with a brush
    /// </summary>
    /// <param name="streamGeometry">The geometry to convert</param>
    /// <param name="brush">The brush to use</param>
    /// <returns>If a bitmap could be generated the generated bitmap otherwise null</returns>
    Bitmap? CreateBitmap(StreamGeometry streamGeometry, Brush brush);
}
