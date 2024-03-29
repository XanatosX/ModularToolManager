﻿using ModularToolManagerModel.Data;
using System.Collections.Generic;
using System.Globalization;

namespace ModularToolManagerModel.Services.Language;

/// <summary>
/// Interface to describe the language service
/// </summary>
public interface ILanguageService
{
    /// <summary>
    /// Get a list with all the available cultures
    /// </summary>
    /// <returns></returns>
    List<CultureInfo> GetAvailableCultures();

    /// <summary>
    /// Change the language for the service
    /// </summary>
    /// <param name="newCulture">The new culture to use</param>
    void ChangeLanguage(CultureInfo newCulture);

    /// <summary>
    /// Change the language for the service
    /// </summary>
    /// <param name="newCulture">The new culture to use</param>
    void ChangeLanguage(CultureInfoModel newCulture) => ChangeLanguage(newCulture.Culture);

    /// <summary>
    /// Check if the provided culture is a valid one
    /// </summary>
    /// <param name="culture">The culture to check if it is valid</param>
    /// <returns>True if the provided culture is valid</returns>
    bool ValidLanguage(CultureInfo culture);

    /// <summary>
    /// Check if the provided culture is a valid one
    /// </summary>
    /// <param name="culture">The culture to check if it is valid</param>
    /// <returns>True if the provided culture is valid</returns>
    void ValidLanguage(CultureInfoModel newCulture) => ValidLanguage(newCulture.Culture);

    /// <summary>
    /// Get the current language
    /// </summary>
    /// <returns>The current culture info</returns>
    CultureInfo? GetCurrentLanguage();

    /// <summary>
    /// Get the fallback language
    /// </summary>
    /// <returns>The fallback culture info</returns>
    CultureInfo? GetFallbackLanguage();
}
