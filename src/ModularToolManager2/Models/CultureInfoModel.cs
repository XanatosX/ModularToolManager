using System.Globalization;

namespace ModularToolManager2.Models;

/// <summary>
/// Model for culture information
/// </summary>
/// <param name="DisplayName">The display name of the culture information</param>
/// <param name="Culture">The real culture information</param>
public record CultureInfoModel(string DisplayName, CultureInfo Culture);
