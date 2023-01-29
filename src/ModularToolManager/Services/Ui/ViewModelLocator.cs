using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Logging;
using ModularToolManagerModel.Services.Dependency;
using System;
using System.Linq;
using System.Reflection;

namespace ModularToolManager.Services.Ui;

/// <summary>
/// Class to use for loacint views
/// </summary>
public class ViewModelLocator : IViewModelLocatorService
{
    /// <summary>
    /// All the namespaces which are allowed for views to be stored in
    /// </summary>
    private readonly string[] allowedNamespaces = new[] { "ModularToolManager.ViewModels" };

    /// <summary>
    /// Service used to resolve depedencies
    /// </summary>
    private readonly IDependencyResolverService dependencyResolverService;

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    /// <param name="dependencyResolverService">The dependency resolver Service to use</param>
    public ViewModelLocator(IDependencyResolverService dependencyResolverService)
    {
        this.dependencyResolverService = dependencyResolverService;
    }

    /// <inheritdoc/>
    public ObservableObject? GetViewModel(string name)
    {
        Type? type = GetViewModelType(name);
        return type is null ? null : dependencyResolverService?.GetDependency(type) as ObservableObject;
    }

    /// <inheritdoc/>
    public Type? GetViewModelType(string name)
    {
        return Assembly.GetEntryAssembly()!
                       .GetTypes()
                       .Where(type => allowedNamespaces.Contains(type.Namespace))
                       .Where(type => type.IsAssignableTo(typeof(ObservableObject)))
                       .FirstOrDefault(type => type.Name == name);
    }
}
