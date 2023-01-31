using Microsoft.Extensions.DependencyInjection;
using ModularToolManagerModel.Services.Dependency;
using System;
using System.Linq;

namespace ModularToolManager.Services.Dependencies;

/// <summary>
/// Class to warp the microsoft services provider into a service
/// </summary>
internal sealed class MicrosoftDepdencyResolverService : IDependencyResolverService
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
        return GetDependency<T>(furtherInitilization: null);
    }

    /// <inheritdoc/>
    public T? GetDependency<T>(Action<T?>? furtherInitilization)
    {
        T? returnData = serviceProvider.GetService<T>() ?? ActivatorUtilities.CreateInstance<T>(serviceProvider);
        if (furtherInitilization is not null)
        {
            furtherInitilization(returnData);
        }
        return returnData;
    }

    /// <inheritdoc/>
    public T? GetDependency<T>(Func<IServiceProvider, T?> objectFactory)
    {
        if (objectFactory is null)
        {
            return default;
        }
        return objectFactory.Invoke(serviceProvider);
    }

    /// <inheritdoc/>
    public object? GetDependency(Type typeOf)
    {
        return serviceProvider.GetService(typeOf) ?? ActivatorUtilities.CreateInstance(serviceProvider, typeOf);
    }
}
