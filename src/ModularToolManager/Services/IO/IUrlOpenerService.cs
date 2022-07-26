using System;

namespace ModularToolManager.Services.IO;

/// <summary>
/// Interface used to describe a service to open urls in the browser
/// </summary>
internal interface IUrlOpenerService
{
    /// <summary>
    /// Open the url in the browser
    /// </summary>
    /// <param name="url">The url to open up in the browsers as a string</param>
    /// <returns>True if the url could be opened successful</returns>
    bool OpenUrl(string url);

    /// <summary>
    /// Open the url in the browser
    /// </summary>
    /// <param name="url">The url to open up in the browsers as a uri</param>
    /// <returns>True if the url could be opened successful</returns>
    bool OpenUrl(Uri url);
}
