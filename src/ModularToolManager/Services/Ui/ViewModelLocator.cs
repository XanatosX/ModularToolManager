using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Logging;
using ModularToolManagerModel.Services.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ModularToolManager.Services.Ui;
public class ViewModelLocator : IViewModelLocatorService
{
    private string[] allowedNamespaces = new string[] { "ModularToolManager.ViewModels" };

    private readonly IDependencyResolverService dependencyResolverService;
    private readonly ILogger<ViewModelLocator> logger;

    public ViewModelLocator(IDependencyResolverService dependencyResolverService, ILogger<ViewModelLocator> logger)
    {
        this.dependencyResolverService = dependencyResolverService;
        this.logger = logger;
    }

    public ObservableObject? GetViewModel(string name)
    {
        Type type = GetViewModelType(name);
        return type is null ? null : dependencyResolverService?.GetDependency(type) as ObservableObject;
    }

    public Type? GetViewModelType(string name)
    {
        return Assembly.GetEntryAssembly()!
                       .GetTypes()
                       .Where(type => allowedNamespaces.Contains(type.Namespace))
                       .Where(type => type.IsAssignableTo(typeof(ObservableObject)))
                       .FirstOrDefault(type => type.Name == name);
    }
}
