using ModularToolManagerModel.Services.Data;
using ModularToolManagerPlugin.Models;
using ModularToolManagerPlugin.Plugin;
using System.Text.Json.Serialization;

namespace ModularToolManager.Models
{
    /// <summary>
    /// Model for a Function stored by the user
    /// </summary>
    public class FunctionModel : IRepositoryDataSet<string>
    {
        [JsonPropertyName("UniqueIdentifier")]
        public string Id { get; init; }

        /// <summary>
        /// Unique identifier for the function model
        /// </summary>
        [JsonIgnore]
        [Obsolete("Please use the Id field instead")]
        public string UniqueIdentifier => Id;

        /// <summary>
        /// The name of the function to display
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// The description of the function
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// The plugin to use to execute the function
        /// </summary>
        public IFunctionPlugin? Plugin { get; set; }

        /// <summary>
        /// The path to run the with the function plugin
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// The parameters of the function to use for the excecution
        /// </summary>
        public string Parameters { get; set; }

        /// <summary>
        /// The sort order of the function
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// The settings for this specific function
        /// </summary>
        public IEnumerable<SettingModel> Settings { get; set; }

        /// <summary>
        /// Create a new instance of ths class
        /// </summary>
        public FunctionModel() : this(string.Empty, null, string.Empty, string.Empty, int.MaxValue)
        {
        }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="displayName">The name of the function to display</param>
        /// <param name="plugin">The plugin to use</param>
        /// <param name="path">The path to execute with the plugin</param>
        /// <param name="parameters">The parameters to provide before execution</param>
        /// <param name="sortOrder">The sort order of the function</param>
        public FunctionModel(string displayName, IFunctionPlugin? plugin, string path, string parameters, int sortOrder)
        {
            Id = Guid.NewGuid().ToString().Replace("-", string.Empty);
            DisplayName = displayName;
            Description = null;
            Plugin = plugin;
            Path = path;
            Parameters = parameters;
            SortOrder = sortOrder;
        }

        /// <summary>
        /// Is this plugin valid and ready to use
        /// </summary>
        /// <returns>True if all required fields are filled</returns>
        public bool IsUseable()
        {
            bool valid = Plugin is not null;
            valid &= !string.IsNullOrEmpty(DisplayName);
            valid &= !string.IsNullOrEmpty(Path);
            valid &= File.Exists(Path);
            return valid;
        }
    }
}
