using ModularToolManagerModel.Services.IO;
using ModularToolManagerModel.Services.Language;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ModularToolManager.Services.Language;

/// <summary>
/// Implementation of the language service
/// </summary>
internal class ResourceCultureService : ILanguageService
{
    /// <summary>
    /// All the available cultures
    /// </summary>
    private List<CultureInfo>? availableCultures;

    /// <summary>
    /// The path service to use
    /// </summary>
    private readonly IPathService pathService;

    public ResourceCultureService(IPathService pathService)
    {
        this.pathService = pathService;
    }

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
        return availableCultures is null ? false : availableCultures.Contains(culture);
    }

    /// <inheritdoc/>
    public List<CultureInfo> GetAvailableCultures()
    {
        if (availableCultures != null)
        {
            return availableCultures;
        }

        string applicationLocation = pathService.GetApplicationExecutableString();
        string resoureFileName = Path.GetFileNameWithoutExtension(applicationLocation) + ".resources.dll";
        DirectoryInfo? rootDirectory = pathService.GetApplicationPath();
        availableCultures = rootDirectory?.GetDirectories()
                                         .Where(dir => CultureInfo.GetCultures(CultureTypes.AllCultures).Any(culture => culture.Name == dir.Name))
                                         .Where(dir => File.Exists(Path.Combine(dir.FullName, resoureFileName)))
                                         .Select(dir => CultureInfo.GetCultureInfo(dir.Name))
                                         .ToList() ?? new();
        if (!availableCultures.Contains(CultureInfo.GetCultureInfo("en")))
        {
            availableCultures.Add(CultureInfo.GetCultureInfo("en"));
        }
        availableCultures.OrderBy(culture => culture.DisplayName);
        return availableCultures;
    }
}
