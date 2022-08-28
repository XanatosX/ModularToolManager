using ModularToolManagerPlugin.Plugin;
using System;

namespace ModularToolManager.Models
{
    /// <summary>
    /// Model for a Function stored by the user
    /// </summary>
    internal class FunctionModel
    {
        public string UniqueIdentifier { get; init; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public IFunctionPlugin? Plugin { get; set; }

        public string Path { get; set; }

        public string Parameters { get; set; }

        public int SortOrder { get; set; }

        public FunctionModel() : this(Guid.NewGuid().ToString(), string.Empty, null, string.Empty, string.Empty, int.MaxValue)
        {
        }

        public FunctionModel(string uniqueIdentifier, string displayName, IFunctionPlugin plugin, string path, string parameters, int sortOrder)
        {
            UniqueIdentifier = uniqueIdentifier;
            DisplayName = displayName;
            Plugin = plugin;
            Path = path;
            Parameters = parameters;
            SortOrder = sortOrder;
        }

        public bool IsUseable()
        {
            bool valid = Plugin is not null;
            valid &= !string.IsNullOrEmpty(DisplayName);
            valid &= !string.IsNullOrEmpty(Path);
            return valid;
        }
    }
}
