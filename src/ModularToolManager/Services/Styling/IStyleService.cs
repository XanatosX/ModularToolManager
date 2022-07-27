using Avalonia;
using Avalonia.Styling;
using System.Collections.Generic;

namespace ModularToolManager.Services.Styling;

/// <summary>
/// Service for getting styles from the avalonia application
/// </summary>
internal interface IStyleService
{
    /// <summary>
    /// Get all the styles included in the current app
    /// </summary>
    /// <returns></returns>
    IEnumerable<IStyle> GetCurrentAppIncludeStyles();

    /// <summary>
    /// Get all Styles in the current app
    /// </summary>
    /// <returns>A list with all the styles</returns>
    IEnumerable<Style> GetAllStylesWithinResource();

    /// <summary>
    /// Get all styles within the provided style
    /// </summary>
    /// <param name="style">The style to get the styles from</param>
    /// <returns>A list with styles</returns>
    IEnumerable<Style> GetAllStylesWithinResource(IStyle? style);

    /// <summary>
    /// Get a style of type T by name
    /// </summary>
    /// <typeparam name="T">The type of styles to get</typeparam>
    /// <param name="name">The name to search for</param>
    /// <returns>A single style or null if nothing found</returns>
    T? GetStyleByName<T>(string name) where T : IAvaloniaObject;

    /// <summary>
    /// Get a style of type T by name
    /// </summary>
    /// <typeparam name="T">The type of styles to get</typeparam>
    /// <param name="style">The style to search in</param>
    /// <param name="name">The name of the style to get</param>
    /// <returns>A single style or null if nothing found</returns>
    T? GetStyleByName<T>(IStyle? style, string name) where T : IAvaloniaObject;
}
