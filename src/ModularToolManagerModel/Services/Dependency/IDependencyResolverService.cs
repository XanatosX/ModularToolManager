
namespace ModularToolManagerModel.Services.Dependency;

/// <summary>
/// Service interface for getting dependencies
/// </summary>
public interface IDependencyResolverService
{
    /// <summary>
    /// Get a dependecy of type T
    /// </summary>
    /// <typeparam name="T">The type of the dependency to get</typeparam>
    /// <returns>An instance of type T or null if nothing found</returns>
    T? GetDependency<T>();

    /// <summary>
    /// Get all dependencies of type T
    /// </summary>
    /// <typeparam name="T">The type of the dependencies to get</typeparam>
    /// <returns>A list with instances of the type T or an empty one if nothing found</returns>
    T?[] GetDependencies<T>();

    /// <summary>
    /// Get the dependency of the provided type
    /// </summary>
    /// <param name="typeOf">The type of the dependency to get</param>
    /// <returns>A single instances of the object or an empty one if nothing found</returns>
    object? GetDependency(Type typeOf);

    /// <summary>
    /// Get all dependencies of the provided type
    /// </summary>
    /// <typeparam name="T">The type of the dependencies to get</typeparam>
    /// <returns>A list with instances of the objects or an empty one if nothing found</returns>
    object?[] GetDependencies(Type typeOf);
}
