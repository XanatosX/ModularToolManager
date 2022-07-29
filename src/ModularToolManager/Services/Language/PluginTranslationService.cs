using DynamicData.Aggregation;
using ModularToolManagerPlugin.Models;
using ModularToolManagerPlugin.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace ModularToolManager.Services.Language;

/// <summary>
/// Plugin translation service to get translations inside of the plugins
/// </summary>
public class PluginTranslationService : IPluginTranslationService
{
    private const string JSON_TRANSLATION_REGEX = @"[a-zA-Z\.]*\.Translations\.([a-z]{2}-[A-Z]{2}.json)";

    /// <inheritdoc/>
    public List<TranslationModel> GetAllTranslations()
    {
        Assembly assembly = Assembly.GetCallingAssembly();
        return GetTranslationsFromFile(assembly, GetCorrectFile(assembly, GetCurrentCulture()));
    }

    /// <inheritdoc/>
    public List<string> GetKeys()
    {
        Assembly assembly = Assembly.GetCallingAssembly();
        return GetTranslationsFromFile(
                assembly,
                GetCorrectFile(assembly, GetCurrentCulture())
            ).Select(translation => translation.Key)
             .ToList();
    }

    /// <inheritdoc/>
    public List<string> GetLanguages()
    {
        return GetTranslationManifests(Assembly.GetCallingAssembly()).Select(path => GetLanguageFromPath(path))
                                               .Where(path => !string.IsNullOrEmpty(path))
                                               .ToList();


    }

    private IEnumerable<string> GetTranslationManifests(Assembly assembly)
    {
        return assembly.GetManifestResourceNames()
                       .Where(path => IsValidTranslationPath(path));
    }

    private bool IsValidTranslationPath(string path)
    {
        Regex regex = new Regex(JSON_TRANSLATION_REGEX);
        return regex.IsMatch(path);
    }

    private string? GetCorrectFile(Assembly assembly, CultureInfo culture)
    {

        return GetTranslationManifests(assembly).FirstOrDefault(path => path.EndsWith(culture.Name + ".json"));
    }

    private string GetLanguageFromPath(string path)
    {
        string returnLanguage = string.Empty;
        Regex regex = new Regex(JSON_TRANSLATION_REGEX);
        foreach (Match match in regex.Matches(path))
        {
            GroupCollection group = match.Groups;
            returnLanguage = group.Count == 2 ? group[1].Value : returnLanguage;

        }
        return returnLanguage.Replace(".json", string.Empty);

    }

    /// <inheritdoc/>
    public string? GetTranslationByKey(string key, CultureInfo fallbackCulture)
    {
        string? translation = null;
        Assembly assembly = Assembly.GetCallingAssembly();
        string cultureFile = GetCultureTranslationFile(fallbackCulture, assembly);
        try
        {
            translation = GetTranslationsFromFile(assembly, cultureFile)?
                                    .FirstOrDefault(translation => translation.Key == key)?
                                    .Value;
        }
        catch (Exception)
        {
        }

        return translation;
    }

    private List<TranslationModel> GetTranslationsFromFile(Assembly assembly, string cultureFile)
    {
        return JsonSerializer.Deserialize<List<TranslationModel>>(LoadResourceData(assembly, cultureFile));
    }

    private string GetCultureTranslationFile(CultureInfo fallbackCulture, Assembly assembly)
    {
        string cultureFile = GetCorrectFile(assembly, GetCurrentCulture());
        return cultureFile ?? GetCorrectFile(assembly, fallbackCulture);
    }

    private string LoadResourceData(Assembly assembly, string path)
    {
        string returnData = string.Empty;
        if (string.IsNullOrEmpty(path))
        {
            return returnData;
        }

        using (Stream stream = assembly.GetManifestResourceStream(path))
        {
            using (StreamReader reader = new StreamReader(stream))
            {
                returnData = reader.ReadToEnd();
            }
        }
        return returnData;
    }

    private CultureInfo GetCurrentCulture()
    {
        //@TODO use correct culture as soon as we save it!
        CultureInfo culture = CultureInfo.CurrentCulture;
        return culture;
    }
}
