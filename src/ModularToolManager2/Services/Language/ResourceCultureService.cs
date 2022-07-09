using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ModularToolManager2.Services.Language;

/// <summary>
/// Implementation of the language service
/// </summary>
internal class ResourceCultureService : ILanguageService
{
    /// <summary>
    /// All the available cultures
    /// </summary>
    private List<CultureInfo>? availableCultures;

    /// <inheritdoc/>
    public void ChangeLanguage(CultureInfo newCulture)
    {
        if (!ValidLanguage(newCulture))
        {
            return;
        }
        Properties.Resources.Culture = newCulture;
        availableCultures = null;
    }

    /// <inheritdoc/>
    public bool ValidLanguage(CultureInfo culture)
    {
        return availableCultures.Contains(culture);
    }

    /// <inheritdoc/>
    public List<CultureInfo> GetAvailableCultures()
    {
        if (availableCultures != null)
        {
            return availableCultures;
        }
        string applicationLocation = Assembly.GetExecutingAssembly().Location;
        string resoureFileName = Path.GetFileNameWithoutExtension(applicationLocation) + ".resources.dll";
        DirectoryInfo rootDirectory = new DirectoryInfo(Path.GetDirectoryName(applicationLocation));
        availableCultures = rootDirectory.GetDirectories()
                                         .Where(dir => CultureInfo.GetCultures(CultureTypes.AllCultures).Any(culture => culture.Name == dir.Name))
                                         .Where(dir => File.Exists(Path.Combine(dir.FullName, resoureFileName)))
                                         .Select(dir => CultureInfo.GetCultureInfo(dir.Name))
                                         .ToList();
        if (!availableCultures.Contains(CultureInfo.GetCultureInfo("en")))
        {
            availableCultures.Add(CultureInfo.GetCultureInfo("en"));
        }
        availableCultures.OrderBy(culture => culture.DisplayName);
        return availableCultures;
    }
}
