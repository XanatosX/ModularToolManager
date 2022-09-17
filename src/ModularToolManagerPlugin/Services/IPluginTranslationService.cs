using ModularToolManagerPlugin.Attributes;
using ModularToolManagerPlugin.Models;
using System.Globalization;
using System.Reflection;

namespace ModularToolManagerPlugin.Services;

/// <summary>
/// Interface to get text translations for the plugin
/// </summary>
[PluginInjectable]
public interface IPluginTranslationService
{
    /// <summary>
    /// Get the fallback language of the main application
    /// </summary>
    /// <returns>The fallback culter informaton of the application</returns>
    CultureInfo GetFallbackLanguage();

    /// <summary>
    /// Get a list with all the languages in the plugin
    /// </summary>
    /// <returns>A list with all country codes available in the plugin</returns>
    List<string> GetLanguages();

    /// <summary>
    /// Get a list with all the translations in the plugin
    /// </summary>
    /// <returns>A list with all possible translations</returns>
    List<TranslationModel> GetAllTranslations();

    /// <summary>
    /// Get a list with all the translations in the plugin
    /// </summary>
    /// /// <param name="assemblyToUse">The assembly to get the translations from</param>
    /// <returns>A list with all possible translations</returns>
    List<TranslationModel> GetAllTranslations(Assembly assemblyToUse);

    /// <summary>
    /// Get the translation for a given key
    /// </summary>
    /// <param name="key">The key to request the translation from</param>
    /// <param name="fallbackCulture">The fallback langauge to use if the translation could not be found</param>
    /// <returns>The translation for the given key or the fallback value</returns>
    string? GetTranslationByKey(string key, CultureInfo fallbackCulture);

    /// <summary>
    /// Get the translation for a given key
    /// </summary>
    /// <param name="assembly">The assembly to get the translations from</param>
    /// <param name="key">The key to request the translation from</param>
    /// <param name="fallbackCulture">The fallback langauge to use if the translation could not be found</param>
    /// <returns>The translation for the given key or the fallback value</returns>
    string? GetTranslationByKey(Assembly assembly, string key, CultureInfo fallbackCulture);

    /// <summary>
    /// Get all the keys available in the translations
    /// </summary>
    /// <returns>A list with all keys of possible translations</returns>
    List<string> GetKeys();

    /// <summary>
    /// Get all the keys available in the translations
    /// </summary>
    /// <param name="assembly">The assembly to get the translations from</param>
    /// <returns>A list with all keys of possible translations</returns>
    List<string> GetKeys(Assembly assembly);
}
