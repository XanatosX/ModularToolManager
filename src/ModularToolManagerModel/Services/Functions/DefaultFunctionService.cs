using ModularToolManager.Models;
using ModularToolManager.Services.Data;

namespace ModularToolManagerModel.Services.Functions;

/// <summary>
/// Default implementation of the IFunctionService
/// </summary>
internal class DefaultFunctionService : IFunctionService
{
    /// <summary>
    /// The repository to use
    /// </summary>
    private readonly IRepository<FunctionModel, string> repository;

    /// <inheritdoc/>
    public DefaultFunctionService(IRepository<FunctionModel, string> repository)
    {
        this.repository = repository;
    }

    /// <inheritdoc/>
    public bool AddFunction(FunctionModel function)
    {
        return repository.Insert(function) is not null;
    }

    /// <inheritdoc/>
    public void DeleteFunction(string identifier)
    {
        repository.DeleteById(identifier);
    }

    /// <inheritdoc/>
    public List<FunctionModel> GetAvailableFunctions()
    {
        return repository.GetAll().Where(function => function.Plugin is not null).ToList();
    }

    /// <inheritdoc/>
    public FunctionModel? GetFunction(string identifier)
    {
        return repository.FindById(identifier);
    }

    /// <inheritdoc/>
    public bool ReplaceFunction(FunctionModel function)
    {
        return repository.Update(function) is not null;
    }

    /// <inheritdoc/>
    public bool UpdateFunction(string identifier, Action<FunctionModel> updateMethod)
    {
        var function = GetFunction(identifier);
        if (function is null)
        {
            return false;
        }
        updateMethod?.Invoke(function);
        return ReplaceFunction(function);
    }

    /// <inheritdoc/>
    public IEnumerable<bool> UpdateFunction(IEnumerable<string> identifiers, Action<FunctionModel> updateMethod)
    {
        List<bool> result = new();
        foreach (var identifier in identifiers)
        {
            result.Add(UpdateFunction(identifier, updateMethod));
        }
        return result;
    }
}
