using Microsoft.Extensions.Logging;
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

    /// <summary>
    /// The logger to use
    /// </summary>
    private readonly ILogger<ResourceCultureService> logger;

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    /// <param name="pathService">The path service to use</param>
    /// <param name="logger">The logger to use</param>
    public ResourceCultureService(IPathService pathService, ILogger<ResourceCultureService> logger)
    {
        this.pathService = pathService;
        this.logger = logger;
    }

    /// <inheritdoc/>
    public void ChangeLanguage(CultureInfo newCulture)
    {
        logger.LogTrace($"Requesting language change to {newCulture.Name}");
        if (!ValidLanguage(newCulture))
        {
            logger.LogTrace($"Requesting language is not valid!");
            return;
        }
        logger.LogTrace($"Set new language!");
        Properties.Resources.Culture = newCulture;
        availableCultures = null;
    }

    /// <inheritdoc/>
    public bool ValidLanguage(CultureInfo culture)
    {
        logger.LogTrace($"Check if language {culture.Name} is valid");
        return availableCultures is null ? false : availableCultures.Contains(culture);
    }

    /// <inheritdoc/>
    public List<CultureInfo> GetAvailableCultures()
    {
        if (availableCultures != null)
        {
            logger.LogTrace("Return cached cultures");
            return availableCultures;
        }
        logger.LogTrace("Load available cultures from resources");
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
