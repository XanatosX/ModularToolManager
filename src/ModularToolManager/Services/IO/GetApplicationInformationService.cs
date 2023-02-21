using Microsoft.Extensions.Logging;
using ModularToolManager.Models;
using ModularToolManagerModel.Services.Language;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text.Json;

namespace ModularToolManager.Services.IO;

/// <summary>
/// Class to get all the information about the application
/// </summary>
internal class GetApplicationInformationService
{
    /// <summary>
    /// The service used to load data about the application
    /// </summary>
    private readonly ResourceReaderService readerService;

    /// <summary>
    /// Logger to use to log any problems
    /// </summary>
    private readonly ILogger<GetApplicationInformationService> logger;

    /// <summary>
    /// The current language service
    /// </summary>
    private readonly ILanguageService languageService;

    /// <summary>
    /// The file to load as a license
    /// </summary>
    private const string LICENSE_FILE_NAME = "LICENSE";

    /// <summary>
    /// The cached license if already loaded
    /// </summary>
    private string? cachedLicense;

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    /// <param name="readerService">The reader service to use</param>
    /// <param name="logger">The logging instance to use</param>
    /// <param name="languageService">The language service to use</param>
    public GetApplicationInformationService(ResourceReaderService readerService, ILogger<GetApplicationInformationService> logger, ILanguageService languageService)
    {
        this.readerService = readerService;
        this.logger = logger;
        this.languageService = languageService;
    }

    /// <summary>
    /// Get the license for the project
    /// </summary>
    /// <returns>The license information</returns>
    public string? GetLicense()
    {
        cachedLicense ??= readerService.GetResourceData(LICENSE_FILE_NAME);
        return cachedLicense;
    }

    /// <summary>
    /// Get the version of the application
    /// </summary>
    /// <returns>The version of the project or null</returns>
    public Version? GetVersion()
    {
        var assembly = Assembly.GetExecutingAssembly();
        Version? returnVersion = null;
        try
        {

            var versionString = FileVersionInfo.GetVersionInfo(assembly.Location).ProductVersion;
            returnVersion = new Version(versionString ?? "0.0.0");
        }
        catch (Exception e)
        {
            logger.LogError(e, "Could not get version from assembly");
        }

        return returnVersion;
    }

    /// <summary>
    /// Get the dependencies for this application
    /// </summary>
    /// <returns>A list with all the dependencies</returns>
    public IEnumerable<DependencyModel> GetDependencies()
    {
        IEnumerable<DependencyModel> returnDependencies = Enumerable.Empty<DependencyModel>();
        using (Stream? stream = readerService.GetResourceStream("dependencies.json"))
        {
            if (stream is not null && stream != Stream.Null)
            {
                returnDependencies = JsonSerializer.Deserialize<IEnumerable<DependencyModel>>(stream) ?? returnDependencies;
            }
        }

        return returnDependencies;
    }

    /// <summary>
    /// Get the url to the github repository
    /// </summary>
    /// <returns>The link to the github repository</returns>
    public string GetGithubUrl() => Properties.Properties.GitHubUrl;

    /// <summary>
    /// Get all the hotkeys for the application
    /// </summary>
    /// <returns>A list with all the hotkeys</returns>
    public IEnumerable<HotkeyModel> GetHotkeys()
    {
        IEnumerable<HotkeyModel> loadedHotkeys = Enumerable.Empty<HotkeyModel>();
        using (Stream? stream = readerService.GetResourceStream("hotkeys.json"))
        {
            if (stream is not null && stream != Stream.Null)
            {
                loadedHotkeys = JsonSerializer.Deserialize<IEnumerable<HotkeyModel>>(stream) ?? loadedHotkeys;
            }
        }
        List<HotkeyModel> returnHotkeys = new();
        foreach (var hotkey in loadedHotkeys)
        {
            var currentLangauge = languageService.GetCurrentLanguage();
            returnHotkeys.Add(new HotkeyModel
            {
                Name = Properties.Resources.ResourceManager.GetString(hotkey.Name ?? string.Empty, currentLangauge),
                Description = Properties.Resources.ResourceManager.GetString(hotkey.Description ?? string.Empty, currentLangauge),
                WorkingOn = Properties.Resources.ResourceManager.GetString(hotkey.WorkingOn ?? string.Empty, currentLangauge),
                Keys = hotkey.Keys?.Select(key => Properties.Resources.ResourceManager.GetString(key ?? string.Empty))
                                  .OfType<string>()
                                  .ToList() ?? new(),
                OrderId = hotkey.OrderId,
            });
        }

        return returnHotkeys;
    }
}
