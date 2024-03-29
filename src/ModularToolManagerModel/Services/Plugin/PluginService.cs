﻿using Microsoft.Extensions.Logging;
using ModularToolManagerModel.Services.Dependency;
using ModularToolManagerModel.Services.IO;
using ModularToolManagerPlugin.Attributes;
using ModularToolManagerPlugin.Plugin;
using ModularToolManagerPlugin.Services;
using System.Reflection;
using System.Reflection.Metadata;

namespace ModularToolManagerModel.Services.Plugin;

/// <summary>
/// Plugin service for loading plugins from dlls
/// </summary>
public sealed class PluginService : IPluginService
{
    /// <summary>
    /// Service for getting application paths
    /// </summary>
    private readonly IPathService? pathService;

    /// <summary>
    /// The dependency resolver to use
    /// </summary>
    private readonly IDependencyResolverService dependencyResolver;

    /// <summary>
    /// The logging service to use
    /// </summary>
    private readonly ILogger<PluginService>? loggingService;

    /// <summary>
    /// A list with all the plugins currently available
    /// </summary>
    private readonly List<IFunctionPlugin> plugins;

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    /// <param name="pluginTranslationFactoryService">The plugin translation service to use</param>
    /// <param name="functionSettingsService">The function settings service to use</param>
    public PluginService(
        IPathService? pathService,
        IDependencyResolverService dependencyResolver,
        ILogger<PluginService>? loggingService
        )
    {
        this.pathService = pathService;
        this.dependencyResolver = dependencyResolver;
        this.loggingService = loggingService;
        plugins = new List<IFunctionPlugin>();
    }

    /// <inheritdoc/>
    public List<IFunctionPlugin> GetAvailablePlugins()
    {
        if (plugins.Count == 0)
        {
            List<IFunctionPlugin> dataToAdd = GetPlugins().Select(pluginPath => LoadAssemblySavely(pluginPath))
                                                          .Where(assembly => assembly is not null)
                                                          .SelectMany(assembly => GetValidPlugins(assembly!))
                                                          .Select(pluginType => ActivatePlugin(pluginType))
                                                          .Where(plugin => plugin is not null)
                                                          .Where(plugin => plugin!.IsOperationSystemValid())
                                                          .OfType<IFunctionPlugin>()
                                                          .ToList() ?? new();
            plugins.AddRange(dataToAdd);
        }

        return plugins;
    }

    /// <summary>
    /// Load the assembly 
    /// </summary>
    /// <param name="path">The path of the assembly to load</param>
    /// <returns>The assembly to load or null if loading failed</returns>
    private Assembly? LoadAssemblySavely(string path)
    {
        Assembly? assembly = null;
        try
        {
            loggingService?.LogTrace($"Trying to load assembly {path}");
            assembly = Assembly.LoadFrom(path);
        }
        catch (Exception e)
        {
            loggingService?.LogTrace($"Loading assembly {path} did fail with errroe {e.Message}");
        }
        return assembly;

    }

    /// <summary>
    /// Get all the valid plugin types from a given assembly
    /// </summary>
    /// <param name="assembly">The assembly to get the plugin types from</param>
    /// <returns>A list with all the types</returns>
    private List<Type> GetValidPlugins(Assembly assembly)
    {
        return assembly.GetTypes().Where(type => type.IsVisible)
                                  .Where(type => type.GetInterfaces()
                                  .Contains(typeof(IFunctionPlugin)))
                                  .Where(type => type.GetConstructors().Any(constructor => IsConstructorValid(constructor)))
                                  .ToList();
    }

    /// <summary>
    /// Is the constructor of the plugin valid for injection
    /// </summary>
    /// <param name="constructor">The constructor to check</param>
    /// <returns>True if is a valid constructor</returns>
    private bool IsConstructorValid(ConstructorInfo constructor)
    {
        return !constructor.GetParameters().Select(param => param.ParameterType)
                                           .Any(type => type.GetCustomAttribute(typeof(PluginInjectableAttribute)) is null);
    }

    /// <summary>
    /// Activate a plugin and call the startup method
    /// </summary>
    /// <param name="pluginType">The plugin type to activate and start</param>
    /// <returns>The newly created ready to use plugin</returns>
    private IFunctionPlugin? ActivatePlugin(Type pluginType)
    {
        IFunctionPlugin? plugin = null;
        try
        {
            ConstructorInfo? constructor = pluginType.GetConstructors().FirstOrDefault();
            object?[] dependencies = constructor?.GetParameters().Where(parameter => parameter.ParameterType.GetCustomAttribute<PluginInjectableAttribute>() is not null)
                                                                 .Select(parameter => dependencyResolver.GetDependency(parameter.ParameterType))
                                                                 .ToArray() ?? Array.Empty<object>();

            loggingService?.LogInformation($"Activation for plugin of type {pluginType.FullName} imminent");

            var parameters = constructor?.GetParameters().Select(parameter => parameter.ParameterType.FullName);
            loggingService?.LogInformation($"Required parameters for constructor: {string.Join(",", parameters ?? Enumerable.Empty<string>())}");
            IEnumerable<string> objectInstances = dependencies?.Select(dependency => dependency?.GetType().FullName)
                                                               .Where(data => !string.IsNullOrEmpty(data))
                                                               .OfType<string>() ?? Enumerable.Empty<string>();
            loggingService?.LogInformation($"Instances used for filling up: {string.Join(", ", objectInstances)}");

            plugin = (IFunctionPlugin)Activator.CreateInstance(pluginType, dependencies)!;
        }
        catch (Exception e)
        {
            loggingService?.LogError($"Activation of plugin {pluginType.FullName} did fail: {e.Message}");
        }

        return plugin;
    }


    /// <summary>
    /// Get all the plugin paths on the plugin directory
    /// </summary>
    /// <returns>A list with all the plugins</returns>
    private List<string> GetPlugins()
    {
        string pluginDirectory = pathService?.GetPluginPathString() ?? string.Empty;
        if (!Directory.Exists(pluginDirectory))
        {
            loggingService?.LogError($"Could not find plugin directory on path {pluginDirectory} nothing was loaded");
            return Enumerable.Empty<string>().ToList();
        }
        return Directory.GetFiles(pluginDirectory)
                        .ToList()
                        .Select(file => new FileInfo(file))
                        .Where(file => file.Extension.ToLower() == ".dll")
                        .Select(file => file.FullName)
                        .ToList();
    }
}
