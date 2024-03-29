﻿namespace ModularToolManagerModel.Services.Serialization;

/// <summary>
/// Interface to define serialization options for a given serialization interface
/// </summary>
/// <typeparam name="T">The type of serialization to provide</typeparam>
public interface ISerializationOptionFactory<out T> where T : class
{
    /// <summary>
    /// Create a new set of option from type T for the given serialization serive
    /// </summary>
    /// <returns>Option of type T</returns>
    T CreateOptions();
}
