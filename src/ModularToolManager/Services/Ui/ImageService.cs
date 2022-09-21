using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using System.IO;

namespace ModularToolManager.Services.Ui;

/// <summary>
/// Implementation of the image service interface
/// </summary>
public class ImageService : IImageService
{
    /// <inheritdoc/>
    public Bitmap? CreateBitmap(StreamGeometry streamGeometry, Brush brush)
    {
        var testImage = new DrawingImage
        {
            Drawing = new GeometryDrawing
            {
                Geometry = streamGeometry,
                Brush = brush
            }
        };
        var pixelSize = new PixelSize((int)testImage.Size.Width, (int)testImage.Size.Height);
        Bitmap? returnImage = null;
        using (MemoryStream memoryStream = new MemoryStream())
        {

            using (RenderTargetBitmap bitmap = new RenderTargetBitmap(pixelSize, new Vector(72, 72)))
            {
                using (DrawingContext ctx = new DrawingContext(bitmap.CreateDrawingContext(null)))
                {
                    testImage.Drawing.Draw(ctx);
                }
                bitmap.Save(memoryStream);
            }
            memoryStream.Seek(0, SeekOrigin.Begin);
            returnImage = new Bitmap(memoryStream);
        }
        return returnImage;
    }
}
