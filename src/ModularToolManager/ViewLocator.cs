using Avalonia.Controls;
using Avalonia.Controls.Templates;
using CommunityToolkit.Mvvm.ComponentModel;
using ModularToolManagerModel.Services.Dependency;
using System;

namespace ModularToolManager
{
    /// <summary>
    /// Class to locate Views for given ViewModels
    /// </summary>
    public class ViewLocator : IDataTemplate
    {
        /// <summary>
        /// The dependency resolver service to use
        /// </summary>
        private readonly IDependencyResolverService dependencyResolverService;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="dependencyResolverService">The dependency resolver service to use</param>
        public ViewLocator(IDependencyResolverService dependencyResolverService)
        {
            this.dependencyResolverService = dependencyResolverService;
        }

        /// <inheritdoc/>
        public Control Build(object? data)
        {
            if (data is null)
            {
                return new TextBlock { Text = "Request data was null" };
            }
            var name = data.GetType().FullName!.Replace("ViewModel", "View");
            var type = Type.GetType(name);

            if (type != null)
            {
                var control = dependencyResolverService is null ? (Control)Activator.CreateInstance(type)! : (Control)dependencyResolverService!.GetDependency(type)!;
                return control ?? (Control)Activator.CreateInstance(type)!;
            }
            else
            {
                return new TextBlock { Text = "Not Found: " + name };
            }
        }

        /// <inheritdoc/>
        public bool Match(object? data)
        {
            return data is ObservableObject;
        }
    }
}
