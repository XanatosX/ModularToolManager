using FuzzySharp;
using ModularToolManager.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace ModularToolManager.Strategies.Filters;

/// <summary>
/// A function button filter which does fuzzy matching on the function name
/// </summary>
internal class FunctionButtonFuzzyFilter : IFunctionFilter
{
    /// <inheritdoc/>
    public IEnumerable<FunctionButtonViewModel> GetFiltered(IEnumerable<FunctionButtonViewModel> items, string? needle)
    {
        if (string.IsNullOrEmpty(needle))
        {
            return items;
        }
        return needle.Length < 3 ? DoContainsMatch(items, needle) : DoFuzzyMatch(items, needle);
    }

    /// <summary>
    /// Method to do a simple contains match
    /// </summary>
    /// <param name="items">The items to search through</param>
    /// <param name="needle">The needle to search for</param>
    /// <returns>The filtered data</returns>
    private IEnumerable<FunctionButtonViewModel> DoContainsMatch(IEnumerable<FunctionButtonViewModel> items, string needle)
    {
        return items.Where(function => (function.DisplayName ?? string.Empty).ToLower().Contains(needle.ToLower()));
    }

    /// <summary>
    /// The fuzzy search algorithm
    /// </summary>
    /// <param name="items">The items to search through</param>
    /// <param name="needle">The needle to search for</param>
    /// <returns></returns>
    private IEnumerable<FunctionButtonViewModel> DoFuzzyMatch(IEnumerable<FunctionButtonViewModel> items, string needle)
    {
        var results = Process.ExtractAll(needle, items.Select(item => item.DisplayName), null, null, 60);
        if (results is null)
        {
            return items;
        }
        List<FunctionButtonViewModel> returnList = new List<FunctionButtonViewModel>();
        foreach (var result in results)
        {
            returnList.Add(items.ElementAt(result.Index));
        }
        return returnList.OfType<FunctionButtonViewModel>();
    }

    /// <inheritdoc/>
    public IEnumerable<FunctionButtonViewModel> GetFiltered(IEnumerable<FunctionButtonViewModel> items, params string[] needles)
    {
        return GetFiltered(items, string.Join(string.Empty, needles));
    }
}