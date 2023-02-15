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
