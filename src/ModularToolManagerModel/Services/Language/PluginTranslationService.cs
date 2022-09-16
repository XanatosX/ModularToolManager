using ModularToolManagerPlugin.Models;
using ModularToolManagerPlugin.Services;
using System.Globalization;
using System.Reflection;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace ModularToolManagerModel.Services.Language;

/// <summary>
/// Plugin translation service to get translations inside of the plugins
/// </summary>
public sealed class PluginTranslationService : IPluginTranslationService
{
    /// <summary>
    /// Regex to get the translation files from the resources
    /// </summary>
    private const string JSON_TRANSLATION_REGEX = @"[a-zA-Z\.]*\.Translations\.([a-z]{2}-[A-Z]{2}.json)";

    /// <inheritdoc/>
    public List<TranslationModel> GetAllTranslations(Assembly assemblyToUse)
    {
        return GetTranslationsFromFile(assemblyToUse, GetTranslationResourceByCulture(assemblyToUse, GetCurrentCulture()));
    }

    /// <inheritdoc/>
    public List<TranslationModel> GetAllTranslations()
    {
        return GetAllTranslations(Assembly.GetCallingAssembly());

    }

    /// <inheritdoc/>
    public List<string> GetKeys()
    {
        return GetKeys(Assembly.GetCallingAssembly());

    }

    /// <inheritdoc/>
    public List<string> GetKeys(Assembly assembly)
    {
        return GetTranslationsFromFile(
            assembly,
            GetTranslationResourceByCulture(assembly, GetCurrentCulture()) ?? string.Empty
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

    /// <summary>
    /// Get all the valid resource file names
    /// </summary>
    /// <param name="assembly">The assembly to search the files in</param>
    /// <returns>A list with all resource file paths</returns>
    private IEnumerable<string> GetTranslationManifests(Assembly assembly)
    {
        return assembly.GetManifestResourceNames()
                       .Where(path => IsValidTranslationPath(path));
    }

    /// <summary>
    /// Check if a given string is a valid translation path
    /// </summary>
    /// <param name="path">The path to check if valid</param>
    /// <returns>True if the path is a valid translation path</returns>
    private bool IsValidTranslationPath(string path)
    {
        Regex regex = new Regex(JSON_TRANSLATION_REGEX);
        return regex.IsMatch(path);
    }

    /// <summary>
    /// Get the resource path by culture
    /// </summary>
    /// <param name="assembly">The assembly to search through</param>
    /// <param name="culture">The culture to get the resource file for</param>
    /// <returns>A string with the filepath or null if nothing was found</returns>
    private string? GetTranslationResourceByCulture(Assembly assembly, CultureInfo culture)
    {
        return GetTranslationManifests(assembly).FirstOrDefault(path => path.EndsWith(culture.Name + ".json"));
    }

    /// <summary>
    /// Extract the culture information from the resource path
    /// </summary>
    /// <param name="path">The resource path to get the culture information from</param>
    /// <returns>The culture information as string</returns>
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
        return GetTranslationByKey(Assembly.GetCallingAssembly(), key, fallbackCulture);

    }

    /// <inheritdoc/>
    public string? GetTranslationByKey(Assembly assembly, string key, CultureInfo fallbackCulture)
    {
        string? translation = null;
        string cultureFile = GetCultureTranslationFile(fallbackCulture, assembly);
        try
        {
            translation = GetTranslationsFromFile(assembly, cultureFile)?
                                    .FirstOrDefault(translation => translation.Key == key)?
                                    .Value;
        }
        catch (Exception)
        {
            //No translation found, keep as null
        }
        return translation;
    }

    /// <summary>
    /// Get all the translations from the given file
    /// </summary>
    /// <param name="assembly">The assembly to get the translations from</param>
    /// <param name="cultureFile">The culture info for the resource file to get</param>
    /// <returns>A list with all possible translations</returns>
    private List<TranslationModel> GetTranslationsFromFile(Assembly assembly, string cultureFile)
    {
        return JsonSerializer.Deserialize<List<TranslationModel>>(LoadResourceData(assembly, cultureFile));
    }

    /// <summary>
    /// Get the file to load the data from based on the given culture
    /// </summary>
    /// <param name="fallbackCulture">The fallback culture to use if no translation for the current culture can be found</param>
    /// <param name="assembly">The assembly to search the translation file in</param>
    /// <returns>Get the path to translation file of the given culture</returns>
    private string GetCultureTranslationFile(CultureInfo fallbackCulture, Assembly assembly)
    {
        string? cultureFile = GetTranslationResourceByCulture(assembly, GetCurrentCulture());
        cultureFile = cultureFile ?? GetTranslationResourceByCulture(assembly, fallbackCulture);
        return cultureFile ?? string.Empty;
    }

    /// <summary>
    /// Load the data from the resource and return it as a sring
    /// </summary>
    /// <param name="assembly">The assembly to load the data from</param>
    /// <param name="path">The resource file to load</param>
    /// <returns>The data in the resource file or a empty string if nothing could be loaded</returns>
    private string LoadResourceData(Assembly assembly, string path)
    {
        string returnData = string.Empty;
        if (string.IsNullOrEmpty(path))
        {
            return returnData;
        }

        using (Stream? stream = assembly.GetManifestResourceStream(path))
        {
            if (stream is not null)
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    returnData = reader.ReadToEnd();
                }
            }
        }
        return returnData;
    }

    /// <summary>
    /// Get the current culture of the main application
    /// </summary>
    /// <returns>The current culture information of the main application</returns>
    private CultureInfo GetCurrentCulture()
    {
        //@TODO use correct culture as soon as we save it!
        CultureInfo culture = CultureInfo.CurrentUICulture;
        return culture;
    }

    /// <inheritdoc/>
    public CultureInfo GetFallbackLanguage()
    {
        //@Todo add fallback language logic here
        return null;
    }
}
