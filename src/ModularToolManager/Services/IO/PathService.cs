using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ModularToolManager.Services.IO
{
    internal class PathService : IPathService
    {
        /// <inheritdoc/>
        public DirectoryInfo GetApplicationPath()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public string GetApplicationPathString()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public FileInfo GetPluginPath()
        {
            FileInfo executable = new FileInfo(Assembly.GetExecutingAssembly().Location);
            string pluginPath = Path.Combine(executable.DirectoryName ?? Path.GetTempPath(), "plugins");
            return new FileInfo(pluginPath);
        }

        /// <inheritdoc/>
        public string GetPluginPathString()
        {
            return GetPluginPath().FullName;
        }

        /// <inheritdoc/>
        public FileInfo GetSettingsFilePath()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public string GetSettingsFilePathString()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public DirectoryInfo GetSettingsFolderPath()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public string GetSettingsFolderPathString()
        {
            throw new NotImplementedException();
        }
    }
}
