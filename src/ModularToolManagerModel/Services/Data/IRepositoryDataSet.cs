namespace ModularToolManagerModel.Services.Data;

/// <summary>
/// Interface to define a data set as saveable via the IRepository interface
/// </summary>
/// <typeparam name="I">The type of id to save the data with</typeparam>
internal interface IRepositoryDataSet<I>
{
    /// <summary>
    /// The id for the given object
    /// </summary>
    I Id { get; init; }
}
