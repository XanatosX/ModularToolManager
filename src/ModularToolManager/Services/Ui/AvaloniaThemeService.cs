using Avalonia;
using Avalonia.Themes.Fluent;
using Microsoft.Extensions.Logging;
using ModularToolManager.Converters.Serialization;
using ModularToolManager.Models;
using ModularToolManager.Services.IO;
using ModularToolManagerModel.Services.Language;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace ModularToolManager.Services.Ui;

/// <summary>
/// Service used to switch themes for the application
/// </summary>
internal class AvaloniaThemeService : IThemeService
{
    /// <summary>
    /// The resource loader to get embedded resources
    /// </summary>
    private readonly ResourceReaderService resourceReaderService;

    /// <summary>
    /// The logger to use
    /// </summary>
    private readonly ILogger<AvaloniaThemeService> logger;

    /// <summary>
    /// The language service to use
    /// </summary>
    private readonly ILanguageService languageService;

    /// <summary>
    /// The options used for serialization
    /// </summary>
    private readonly JsonSerializerOptions options;

    /// <summary>
    /// Create a new instance of this object
    /// </summary>
    /// <param name="resourceReaderService">Service required to load embedded resources</param>
    /// <param name="logger">The logger to use</param>
    /// <param name="languageService">The language service to use</param>
    public AvaloniaThemeService(ResourceReaderService resourceReaderService, ILogger<AvaloniaThemeService> logger, ILanguageService languageService)
    {
        this.resourceReaderService = resourceReaderService;
        this.logger = logger;
        this.languageService = languageService;
        options = new();
        options.Converters.Add(new ColorConverter());
    }

    /// <summary>
    /// Load all the styles from the embedded resources
    /// </summary>
    /// <returns></returns>
    private IEnumerable<ApplicationStyle> LoadAllStyles()
    {
        IEnumerable<ApplicationStyle> returnStyles = Enumerable.Empty<ApplicationStyle>();
        try
        {
            var cultureInfo = languageService.GetCurrentLanguage();
            returnStyles = JsonSerializer.Deserialize<IEnumerable<ApplicationStyle>>(resourceReaderService.GetResourceStream("buildInStyles.json") ?? Stream.Null, options) ?? returnStyles;
            foreach (var style in returnStyles)
            {
                style.Name = Properties.Resources.ResourceManager.GetString(style.NameTranslationKey ?? string.Empty, cultureInfo) ?? style.NameTranslationKey;
                style.Description = Properties.Resources.ResourceManager.GetString(style.DescriptionTranslationKey ?? string.Empty, cultureInfo) ?? style.DescriptionTranslationKey;
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Could not load build in themes for application!");
        }
        return returnStyles.DistinctBy(style => style.Id);
    }

    /// <inheritdoc/>
    public IEnumerable<ApplicationStyle> GetAllStyles()
    {
        return LoadAllStyles().ToList();
    }

    /// <inheritdoc/>
    public ApplicationStyle? GetStyleById(int id)
    {
        return LoadAllStyles().Where(style => style.Id == id).FirstOrDefault();
    }

    /// <inheritdoc/>
    public void ChangeApplicationTheme(ApplicationStyle theme)
    {
        var app = Application.Current;
        if (app is not null)
        {
            var loadedTheme = app.Styles.OfType<FluentTheme>().FirstOrDefault();
            if (loadedTheme is not null)
            {
                loadedTheme.Mode = theme.Mode;
            }
        }
    }
}
