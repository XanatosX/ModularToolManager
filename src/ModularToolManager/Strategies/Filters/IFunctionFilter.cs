using ModularToolManager.ViewModels;

namespace ModularToolManager.Strategies.Filters;

/// <summary>
/// A filter strategy for filtering <see cref="FunctionButtonViewModel"/>s.
/// </summary>
public interface IFunctionFilter : IFilterStrategy<FunctionButtonViewModel, string>
{
}