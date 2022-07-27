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
        private List<IFunctionPlugin> plugins;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="pluginTranslationService">The plugin translation service to use</param>
        /// <param name="functionSettingsService">The function settings service to use</param>
        public PluginService(IPluginTranslationService pluginTranslationService, IFunctionSettingsService functionSettingsService)
        {
            FileInfo executable = new FileInfo(Assembly.GetExecutingAssembly().Location);
            pluginPath = Path.Combine(executable.DirectoryName ?? Path.GetTempPath(), "plugins");
            this.pluginTranslationService = pluginTranslationService;
            this.functionSettingsService = functionSettingsService;

            plugins = new List<IFunctionPlugin>();
        }

        /// <inheritdoc/>
        public List<IFunctionPlugin> GetAvailablePlugins()
        {
            if (plugins.Count == 0)
            {
                plugins.AddRange(GetPlugins().Select(pluginPath => LoadAssemblySavely(pluginPath))
                                             .Where(assembly => assembly is not null)
                                             .SelectMany(assembly => GetValidPlugins(assembly!))
                                             .Select(pluginType => ActivatePlugin(pluginType))
                                             .Where(plugin => plugin is not null)
                                             .Where(plugin => plugin.IsOperationSystemValid()));
            }

            return plugins;
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

        /// <summary>
        /// Get all the valid plugin types from a given assembly
        /// </summary>
        /// <param name="assembly">The assembly to get the plugin types from</param>
        /// <returns>A list with all the types</returns>
        public List<Type> GetValidPlugins(Assembly assembly)
        {
            return assembly.GetTypes().Where(type => type.IsVisible)
                                      .Where(type => type.GetInterfaces()
                                      .Contains(typeof(IFunctionPlugin)))
                                      .Where(type => type.GetConstructors().Any(constructor => constructor.GetParameters().Length == 0))
                                      .ToList();
        }

        /// <summary>
        /// Activate a plugin and call the startup method
        /// </summary>
        /// <param name="pluginType">The plugin type to activate and start</param>
        /// <returns>The newly created ready to use plugin</returns>
        public IFunctionPlugin? ActivatePlugin(Type pluginType)
        {
            IFunctionPlugin? plugin = null;
            try
            {
                plugin = (IFunctionPlugin)Activator.CreateInstance(pluginType)!;
                plugin?.Startup(pluginTranslationService, functionSettingsService, Environment.OSVersion);
            }
            catch (Exception)
            {
            }

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
