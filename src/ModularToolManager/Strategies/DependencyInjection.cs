using Microsoft.Extensions.DependencyInjection;
using ModularToolManager.Strategies.Filters;

namespace ModularToolManager.Strategies;

/// <summary>
/// Dependency Injection for strategies
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Add all the strategies 
    /// </summary>
    /// <param name="collection">The collection to extend</param>
    /// <returns>The extended collection</returns>
    public static IServiceCollection AddStrategies(this IServiceCollection collection)
    {
        return collection.AddSingleton<IFunctionFilter, FunctionButtonFuzzyFilter>()
                         .AddSingleton<IFunctionFilter, FunctionButtonContainsNeedleFilter>();
    }
}
