﻿using Microsoft.Extensions.Logging;
using ModularToolManagerModel.Services.Language;
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
public sealed class PluginTranslationService : IPluginTranslationService
{
    /// <summary>
    /// Regex to get the translation files from the resources
    /// </summary>
    private const string JSON_TRANSLATION_REGEX = @"[a-zA-Z\.]*\.Translations\.([a-z]{2}-[A-Z]{2}.json)";

    /// <summary>
    /// The language service to use
    /// </summary>
    private readonly ILanguageService languageService;

    /// <summary>
    /// The logger to use
    /// </summary>
    private readonly ILogger<PluginTranslationService> logger;

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    /// <param name="languageService">The langauge service to use</param>
    /// <param name="logger">The logger to use</param>
    public PluginTranslationService(ILanguageService languageService, ILogger<PluginTranslationService> logger)
    {
        this.languageService = languageService;
        this.logger = logger;
    }

    /// <inheritdoc/>
    public List<TranslationModel> GetAllTranslations(Assembly assemblyToUse)
    {
        return GetTranslationsFromFile(assemblyToUse, GetTranslationResourceByCulture(assemblyToUse, GetCurrentCulture()) ?? string.Empty);
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
        return GetTranslationManifests(assembly).FirstOrDefault(path => path.EndsWith(culture.Name.ToUpper() + ".json"));
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
    public string? GetTranslationByKey(string key)
    {
        return GetTranslationByKey(Assembly.GetCallingAssembly(), key);

    }

    /// <inheritdoc/>
    public string? GetTranslationByKey(Assembly assembly, string key)
    {
        string? translation = null;
        string cultureFile = GetCultureTranslationFile(assembly);
        try
        {
            translation = GetTranslationsFromFile(assembly, cultureFile)?
                                    .FirstOrDefault(translation => translation.Key == key)?
                                    .Value;
        }
        catch (Exception)
        {
            logger.LogError($"Could not find a translation file in assemlby {assembly.FullName} with key {key}");
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
        List<TranslationModel> translations = new();
        try
        {
            var data = JsonSerializer.Deserialize<Dictionary<string, string>>(LoadResourceData(assembly, cultureFile));
            foreach (var item in data)
            {
                translations.Add(new TranslationModel { Key = item.Key, Value = item.Value });
            }
        }
        catch (System.Exception e)
        {
            logger.LogError(e, "Error trying to parse translation file");
        }
        return translations;
    }

    /// <summary>
    /// Get the file to load the data from based on the given culture
    /// </summary>
    /// <param name="fallbackCulture">The fallback culture to use if no translation for the current culture can be found</param>
    /// <param name="assembly">The assembly to search the translation file in</param>
    /// <returns>Get the path to translation file of the given culture</returns>
    private string GetCultureTranslationFile(Assembly assembly)
    {
        string? cultureFile = GetTranslationResourceByCulture(assembly, GetCurrentCulture()) ?? GetTranslationResourceByCulture(assembly, GetFallbackLanguage());
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
        return languageService.GetCurrentLanguage() ?? CultureInfo.CurrentUICulture;
    }

    /// <inheritdoc/>
    public CultureInfo GetFallbackLanguage()
    {
        return languageService.GetFallbackLanguage() ?? CultureInfo.GetCultureInfo("eng");
    }
}
