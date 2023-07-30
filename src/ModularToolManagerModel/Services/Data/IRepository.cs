using ModularToolManagerModel.Services.Data;
using System;
using System.Collections.Generic;

namespace ModularToolManager.Services.Data;

/// <summary>
/// Repository for loading function files
/// </summary>
/// <typeparam name="T">The type of data this repository is used for</typeparam>
/// <typeparam name="I">The type of data for the identifier</typeparam>
internal interface IRepository<T, I> where T : IRepositoryDataSet<I>
{
    /// <summary>
    /// Get all the entries based on a filter from the repository
    /// </summary>
    /// <param name="filter">The filter to use for the repository items</param>
    /// <returns>A list with all the entries</returns>
    IEnumerable<T> GetAll(Func<T, bool> filter);

    /// <summary>
    /// Get all the entries from the repository
    /// </summary>
    /// <returns>All entries saved in the repository</returns>
    IEnumerable<T> GetAll();

    /// <summary>
    /// Find some entries based on there id
    /// </summary>
    /// <param name="id">The id of the entity to find</param>
    /// <returns>The found Id or null if notthing was found</returns>
    T? FindById(I id);

    /// <summary>
    /// Delete a given entity from the repository
    /// </summary>
    /// <param name="entity">The entity to delete</param>
    /// <returns>True if the deletion was successful</returns>
    bool Delete(T entity);

    /// <summary>
    /// Delete the given entity by id
    /// </summary>
    /// <param name="id">The id of the entity to delete</param>
    /// <returns>True if deletion was successful</returns>
    bool DeleteById(I id);

    /// <summary>
    /// Insert a new entity into the repository
    /// </summary>
    /// <param name="entity">The new entity to add</param>
    /// <returns>The newly added entity or null if something went wrong</returns>
    T? Insert(T entity);

    /// <summary>
    /// Update the given entity of the repository
    /// </summary>
    /// <param name="entity">The entity to update</param>
    /// <returns>The updatet entity if everything was fine</returns>
    T? Update(T entity);
}
