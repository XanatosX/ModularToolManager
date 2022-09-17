using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace ModularToolManager.Services.Ui;

/// <summary>
/// Service to locate view models by name
/// </summary>
public interface IViewModelLocatorService
{
    /// <summary>
    /// Get the type of a given view model
    /// </summary>
    /// <param name="name">The view model to generate</param>
    /// <returns>The type of the requested model</returns>
    Type? GetViewModelType(string name);

    /// <summary>
    /// Get the view model from a given name
    /// </summary>
    /// <param name="name">The name of the view model to search</param>
    /// <returns>A class instance if it could be created</returns>
    ObservableObject? GetViewModel(string name);
}
