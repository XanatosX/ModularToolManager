using Microsoft.Extensions.DependencyInjection;
using ModularToolManagerModel.Services.Dependency;
using System;
using System.Linq;

namespace ModularToolManager.Services.Dependencies;

/// <summary>
/// Class to warp the microsoft services provider into a service
/// </summary>
internal class MicrosoftDepdencyResolverService : IDependencyResolverService
{
    private readonly IServiceProvider serviceProvider;

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    /// <param name="serviceProvider">The microsoft service provider</param>
    public MicrosoftDepdencyResolverService(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    /// <inheritdoc/>
    public T?[] GetDependencies<T>()
    {
        return serviceProvider.GetServices<T>().ToArray();
    }

    /// <inheritdoc/>
    public object?[] GetDependencies(Type typeOf)
    {
        return serviceProvider.GetServices(typeOf).ToArray();
    }

    /// <inheritdoc/>
    public T? GetDependency<T>()
    {
        return serviceProvider.GetService<T>() ?? ActivatorUtilities.CreateInstance<T>(serviceProvider);
    }

    /// <inheritdoc/>
    public object? GetDependency(Type typeOf)
    {
        return serviceProvider.GetService(typeOf) ?? ActivatorUtilities.CreateInstance(serviceProvider, typeOf);
    }
}
