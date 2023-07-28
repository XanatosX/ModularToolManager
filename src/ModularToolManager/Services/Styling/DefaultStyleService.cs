using Avalonia;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Styling;
using System.Collections.Generic;
using System.Linq;

namespace ModularToolManager.Services.Styling;

/// <summary>
/// Default service for getting styles
/// </summary>
internal class DefaultStyleService : IStyleService
{
    /// <inheritdoc/>
    public IEnumerable<Style> GetAllStylesWithinResource() => GetAllStylesWithinResource(GetCurrentAppIncludeStyles().FirstOrDefault());

    /// <inheritdoc/>
    public IEnumerable<Style> GetAllStylesWithinResource(IStyle? style)
    {
        List<IStyle> returnStyles = new();
        if (style == null)
        {
            return Enumerable.Empty<Style>();
        }
        foreach (IStyle cStyle in style.Children)
        {
            if (style.Children.Count > 0)
            {
                returnStyles.Add(cStyle);
                returnStyles.AddRange(GetAllStylesWithinResource(cStyle));
            }
        }

        return returnStyles.OfType<Style>()
                           .Where(style => style!.Resources.Count > 0);

    }

    /// <inheritdoc/>
    public IEnumerable<IStyle> GetCurrentAppIncludeStyles()
    {
        var styles = App.Current.Styles.Where(style => style.GetType() == typeof(Styles)).ToList(); ;
        var types = styles.Select(style => style.GetType());
        return App.Current?.Styles.Where(style => style.GetType() == typeof(Styles)) ?? Enumerable.Empty<IStyle>();
    }

    /// <inheritdoc/>
    public T? GetStyleByName<T>(string name) where T : AvaloniaObject => GetStyleByName<T>(GetCurrentAppIncludeStyles().FirstOrDefault(), name);

    /// <inheritdoc/>
    public T? GetStyleByName<T>(IStyle? style, string name) where T : AvaloniaObject
    {
        var test = GetCurrentAppIncludeStyles();
        return GetAllStylesWithinResource(style)
               .Where(style => style.Resources.Count > 0)
               .Where(style => style.Resources.ContainsKey(name))
               .Where(style => style.Resources[name] != null && style.Resources[name]!.GetType() == typeof(T))
               .Select(style => (T)style.Resources[name]!)
               .FirstOrDefault();
    }
}
