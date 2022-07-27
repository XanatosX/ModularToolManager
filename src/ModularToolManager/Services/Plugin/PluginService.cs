using ModularToolManagerPlugin.Plugin;
using ModularToolManagerPlugin.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ModularToolManager.Services.Plugin
{
    internal class PluginService : IPluginService
    {
        private string pluginPath;
        private readonly IPluginTranslationService pluginTranslationService;
        private readonly IFunctionSettingsService functionSettingsService;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="pluginTranslationService">The plugin translation service to use</param>
        /// <param name="functionSettingsService">The function settings service to use</param>
        public PluginService(IPluginTranslationService pluginTranslationService, IFunctionSettingsService functionSettingsService)
        {
            FileInfo executable = new FileInfo(Assembly.GetExecutingAssembly().Location);
            pluginPath = Path.Combine(executable.DirectoryName, "plugins");
            this.pluginTranslationService = pluginTranslationService;
            this.functionSettingsService = functionSettingsService;
        }

        /// <inheritdoc/>
        public List<IFunctionPlugin> GetAvailablePlugins()
        {
            List<string> pluginPaths = GetPlugins();
            List<IFunctionPlugin> plugins = pluginPaths.Select(pluginPath => LoadAssemblySavely(pluginPath))
                                                       .Where(assembly => assembly is not null)
                                                       .SelectMany(assembly => GetValidPlugins(assembly!))
                                                       .Select(pluginType => ActivatePlugin(pluginType))
                                                       .ToList();

            return new List<IFunctionPlugin>();
        }

        public Assembly? LoadAssemblySavely(string path)
        {
            try
            {
                Assembly possiblePlugin = Assembly.LoadFrom(path);
                return possiblePlugin;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public List<Type> GetValidPlugins(Assembly assembly)
        {
            return assembly.GetTypes().Where(type => type.IsVisible)
                                      .Where(type => type.GetInterfaces()
                                      .Contains(typeof(IFunctionPlugin)))
                                      .Where(type => type.GetConstructors().Any(constructor => constructor.GetParameters().Length == 0))
                                      .ToList();
        }

        public IFunctionPlugin ActivatePlugin(Type pluginType)
        {
            IFunctionPlugin plugin = (IFunctionPlugin)Activator.CreateInstance(pluginType)!;
            plugin.Startup(pluginTranslationService, functionSettingsService, Environment.OSVersion);
            return plugin;
        }



        private List<string> GetPlugins()
        {
            return Directory.GetFiles(pluginPath)
                            .ToList()
                            .Select(file => new FileInfo(file))
                            .Where(file => file.Extension.ToLower() == ".dll")
                            .Select(file => file.FullName)
                            .ToList();
        }
    }
}
