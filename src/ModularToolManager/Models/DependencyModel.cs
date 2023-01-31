namespace ModularToolManager.Models;

/// <summary>
/// Model for a single dependency
/// </summary>
internal class DependencyModel
{
    /// <summary>
    /// The name of the dependency
    /// </summary>
    public string? Name { get; init; }

    /// <summary>
    /// The version of the dependency
    /// </summary>
    public string? Version { get; init; }

    /// <summary>
    /// The path to the license of the dependency
    /// </summary>
    public string? LicenseUrl { get; init; }

    /// <summary>
    /// The path to the project url of the dependency
    /// </summary>
    public string? ProjectUrl { get; init; }
}
