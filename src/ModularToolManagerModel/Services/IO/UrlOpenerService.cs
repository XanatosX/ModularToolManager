using System;
using System.Diagnostics;

namespace ModularToolManagerModel.Services.IO;

/// <summary>
/// Implementation of the url opener service
/// </summary>
public class UrlOpenerService : IUrlOpenerService
{
    /// <inheritdoc/>
    public bool OpenUrl(string url)
    {
        try
        {
            return OpenUrl(new Uri(url));
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <inheritdoc/>
    public bool OpenUrl(Uri url)
    {
        try
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo()
            {
                UseShellExecute = true,
                FileName = url.OriginalString
            };
            _ = Process.Start(processStartInfo);
        }
        catch (Exception)
        {
            return false;
        }
        return true;
    }
}
