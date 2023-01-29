using Microsoft.Extensions.Logging;
using ModularToolManager.Properties;
using ModularToolManager.Services.Settings;
using ModularToolManagerModel.Services.IO;
using ModularToolManagerModel.Services.Language;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

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
    /// The settings service used to save the data to
    /// </summary>
    private readonly ISettingsService settingsService;

    /// <summary>
    /// The logger to use
    /// </summary>
    private readonly ILogger<ResourceCultureService> logger;

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    /// <param name="pathService">The path service to use</param>
    /// <param name="logger">The logger to use</param>
    public ResourceCultureService(IPathService pathService, ISettingsService settingsService, ILogger<ResourceCultureService> logger)
    {
        this.pathService = pathService;
        this.settingsService = settingsService;
        this.logger = logger;
    }

    /// <inheritdoc/>
    public void ChangeLanguage(CultureInfo? newCulture)
    {
        if (newCulture is null)
        {
            logger.LogError("Trying to switch to new language of null!");
            return;
        }
        logger.LogTrace($"Requesting language change to {newCulture.Name}");
        if (!ValidLanguage(newCulture))
        {
            logger.LogTrace($"Requesting language is not valid!");
            return;
        }
        logger.LogTrace($"Set new language!");
        Properties.Resources.Culture = newCulture;
        settingsService.ChangeSettings(settings => settings.CurrentLanguage = newCulture);
        availableCultures = null;
    }

    /// <inheritdoc/>
    public bool ValidLanguage(CultureInfo culture)
    {
        logger.LogTrace($"Check if language {culture.Name} is valid");
        return GetAvailableCultures().Contains(culture);
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
        if (!availableCultures.Contains(CultureInfo.GetCultureInfo(Properties.Properties.FallbackLanguage)))
        {
            availableCultures.Add(CultureInfo.GetCultureInfo(Properties.Properties.FallbackLanguage));
        }
        availableCultures.OrderBy(culture => culture.DisplayName);
        return availableCultures;
    }

    /// <inheritdoc/>
    public CultureInfo? GetCurrentLanguage()
    {
        return settingsService.GetApplicationSettings().CurrentLanguage;
    }

    /// <inheritdoc/>
    public CultureInfo? GetFallbackLanguage()
    {
        CultureInfo? returnCulture = null;
        try
        {
            returnCulture = CultureInfo.GetCultureInfo(Properties.Properties.FallbackLanguage);
        }
        catch (System.Exception)
        {
        }
        return returnCulture;
    }
}
