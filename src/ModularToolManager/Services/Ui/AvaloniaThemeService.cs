using Avalonia;
using Avalonia.Media;
using Avalonia.Themes.Fluent;
using Microsoft.Extensions.Logging;
using ModularToolManager.Converters.Serialization;
using ModularToolManager.Models;
using ModularToolManager.Services.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace ModularToolManager.Services.Ui;
internal class AvaloniaThemeService : IThemeService
{
    private readonly ResourceReaderService resourceReaderService;
    private readonly ILogger<AvaloniaThemeService> logger;
    private readonly JsonSerializerOptions options;

    public AvaloniaThemeService(ResourceReaderService resourceReaderService, ILogger<AvaloniaThemeService> logger)
    {
        this.resourceReaderService = resourceReaderService;
        this.logger = logger;
        options = new();
        options.Converters.Add(new ColorConverter());
    }

    private IEnumerable<ApplicationStyle> LoadAllStyles()
    {
        IEnumerable<ApplicationStyle> returnStyles = Enumerable.Empty<ApplicationStyle>();
        try
        {
            returnStyles = JsonSerializer.Deserialize<IEnumerable<ApplicationStyle>>(resourceReaderService.GetResourceStream("buildInStyles.json") ?? Stream.Null, options) ?? returnStyles;
            foreach (var style in returnStyles)
            {
                style.Name = Properties.Resources.ResourceManager.GetString(style.NameTranslationKey ?? string.Empty) ?? style.NameTranslationKey;
                style.Description = Properties.Resources.ResourceManager.GetString(style.DescriptionTranslationKey ?? string.Empty) ?? style.DescriptionTranslationKey;
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Could not load build in themes for application!");
        }
        return returnStyles.DistinctBy(style => style.Id);
    }

    public IEnumerable<ApplicationStyle> GetAllStyles()
    {
        return LoadAllStyles().ToList();
    }

    public ApplicationStyle? GetStyleById(int id)
    {
        return LoadAllStyles().Where(style => style.Id == id).FirstOrDefault();
    }

    public void ChangeApplicationTheme(ApplicationStyle theme)
    {
        var loadedPair = Application.Current?.Resources.FirstOrDefault(resource => resource.Key as string == theme.ResourceKey);
        var fluentTheme = loadedPair?.Value as FluentTheme;
        var app = Application.Current;
        if (fluentTheme is not null && app is not null)
        {
            List<FluentTheme> themesToRemove = app.Styles.OfType<FluentTheme>().ToList();
            if (themesToRemove.Count > 1 || themesToRemove.FirstOrDefault() != fluentTheme)
            {
                app.Styles.RemoveAll(themesToRemove);
                app.Styles.Insert(0, fluentTheme);
            }
        }
    }
}
