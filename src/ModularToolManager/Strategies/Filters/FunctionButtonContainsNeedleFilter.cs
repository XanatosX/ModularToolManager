using System.Collections.Generic;
using System.Linq;
using ModularToolManager.ViewModels;

namespace ModularToolManager.Strategies.Filters;

/// <summary>
/// A function button filter which does a contains filter on the function name
/// </summary>
internal class FunctionButtonContainsNeedleFilter : IFunctionFilter
{
    /// <inheritdoc/>
    public IEnumerable<FunctionButtonViewModel> GetFiltered(IEnumerable<FunctionButtonViewModel> items, string? needle)
    {
        if (string.IsNullOrEmpty(needle))
        {
            return items;
        }
        return items.Where(function => (function.DisplayName ?? string.Empty).ToLower().Contains(needle.ToLower()));
    }

    /// <inheritdoc/>
    public IEnumerable<FunctionButtonViewModel> GetFiltered(IEnumerable<FunctionButtonViewModel> items, params string[] needles)
    {
        if (!needles.Any())
        {
            return items;
        }
        List<FunctionButtonViewModel> returnItems = new List<FunctionButtonViewModel>();
        foreach (var item in items)
        {
            bool containsAll = true;
            foreach (var needle in needles)
            {
                string displayName = item.DisplayName ?? string.Empty;
                if (!displayName.ToLower().Contains(needle.ToLower()))
                {
                    containsAll = false;
                    break;
                }
            }
            if (containsAll)
            {
                returnItems.Add(item);
            }
        }
        return returnItems;
    }
}