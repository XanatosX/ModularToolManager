using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularToolManager.Services.Ui;
public interface IImageService
{
    Bitmap? CreateBitmap(StreamGeometry streamGeometry) => CreateBitmap(streamGeometry, new SolidColorBrush(Color.FromRgb(0, g: 0, 0)));

    Bitmap? CreateBitmap(StreamGeometry streamGeometry, Brush brush);
}
