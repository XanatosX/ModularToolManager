using System.Collections.Generic;

namespace ModularToolManager.Strategies.Filters;

/// <summary>
/// An interface to define a filter for different situations
/// </summary>
/// <typeparam name="T">The type of data which should be filtered</typeparam>
/// <typeparam name="N">The type of the needle for filtering</typeparam>
public interface IFilterStrategy<T, N>
{
    /// <summary>
    /// Get the filtered items from the provided dataset based on the provided needle
    /// </summary>
    /// <param name="items">The items to filter</param>
    /// <param name="needle">The needle to search in the data set</param>
    /// <returns>A list with items matching this filter instance</returns>
    IEnumerable<T> GetFiltered(IEnumerable<T> items, N? needle);

    /// <summary>
    /// Get the filtered items from the provided dataset based on the provided needles
    /// </summary>
    /// <param name="items">The items to filter</param>
    /// <param name="needles">The needles to search in the data set</param>
    /// <returns>A list with items matching this filter instance</returns>
    IEnumerable<T> GetFiltered(IEnumerable<T> items, params N[] needles);
}