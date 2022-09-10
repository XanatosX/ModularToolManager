using ModularToolManagerModel.Services.Dependency;
using Splat;
using System;
using System.Linq;

namespace ModularToolManager.Services.Dependencies;

/// <summary>
/// Wrapper service for the splat locator
/// </summary>
internal class SplatDepdendencyResolverWrapperService : IDependencyResolverService
{
    /// <inheritdoc/>
    public T?[] GetDependencies<T>()
    {
        return Locator.Current.GetServices<T>().ToArray();
    }

    /// <inheritdoc/>
    public object?[] GetDependencies(Type typeOf)
    {
        return Locator.Current.GetServices(typeOf).ToArray();
    }

    /// <inheritdoc/>
    public T? GetDependency<T>()
    {
        return Locator.Current.GetService<T>();
    }

    /// <inheritdoc/>
    public object? GetDependency(Type typeOf)
    {
        return Locator.Current.GetService(typeOf);
    }
}
