using ModularToolManagerPlugin.Attributes;
using ModularToolManagerPlugin.Models;
using ModularToolManagerPlugin.Plugin;
using ModularToolManagerPlugin.Services;
using System.Diagnostics;
using System.Globalization;

namespace DefaultPlugins;

/// <summary>
/// Plugin to run scripts on windows maschines
/// </summary>
public class ScriptExecutionPlugin : AbstractFunctionPlugin
{
    /// <summary>
    /// Fallback text if a translation is missing
    /// </summary>
    private const string FALLBACK_TRANSLATION = "MISSING TRANSLATION";

    /// <summary>
    /// Fallback text for logging if a script could not be found
    /// </summary>
    private const string FALLBACK_SCRIPT_NOT_FOUND = "Could not find script '{0}' to execute";

    /// <summary>
    /// The fallback language to use
    /// </summary>
    private readonly CultureInfo fallbackCulture;

    /// <summary>
    /// Service to use for getting translation from this assembly
    /// </summary>
    private readonly IPluginTranslationService? pluginTranslationService;

    /// <summary>
    /// Logger service to use for logging purpose
    /// </summary>
    private readonly IPluginLoggerService? logger;

    /// <summary>
    /// Sould the execution be hidden
    /// </summary>
    [Setting("hide", ModularToolManagerPlugin.Enums.SettingType.Boolean, false)]
    public bool HideCmd { get; init; }

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    /// <param name="translationService">The translation service to use</param>
    /// <param name="logger">The logger service to use</param>
    public ScriptExecutionPlugin(IPluginTranslationService? translationService, IPluginLoggerService? logger)
    {
        fallbackCulture = CultureInfo.GetCultureInfo("en-EN");
        pluginTranslationService = translationService;
        this.logger = logger;
    }

    /// <inheritdoc/>
    public override bool Execute(string parameters, string path)
    {
        if (!File.Exists(path))
        {
            string baseMessage = pluginTranslationService?.GetTranslationByKey("error_cant_find_script_file", fallbackCulture) ?? FALLBACK_SCRIPT_NOT_FOUND;
            logger?.LogError(baseMessage, path);
            return false;
        }

        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = path,
        };
        Process.Start(startInfo);
        return true;
    }

    /// <inheritdoc/>
    public override string GetDisplayName()
    {
        return pluginTranslationService?.GetTranslationByKey("displayname", fallbackCulture) ?? "Script Execution";
    }

    /// <inheritdoc/>
    public override Version GetVersion()
    {
        return Version.Parse("0.1.0.0");
    }

    /// <inheritdoc/>
    public override bool IsOperationSystemValid()
    {
        return OperatingSystem.IsWindows();
    }

    /// <inheritdoc/>
    public override IEnumerable<FileExtension> GetAllowedFileEndings()
    {
        return GetWindowsExtensions().Where(extension => !string.IsNullOrEmpty(extension.Name) && !string.IsNullOrEmpty(extension.Extension))
                                     .OrderBy(item => item.Name);
    }

    /// <summary>
    /// Get all the windows extensions this plugin can run on
    /// </summary>
    /// <returns></returns>
    private IEnumerable<FileExtension> GetWindowsExtensions()
    {
        return new List<FileExtension>
        {
            new FileExtension(pluginTranslationService?.GetTranslationByKey("batch", fallbackCulture) ?? FALLBACK_TRANSLATION, ".bat"),
            new FileExtension(pluginTranslationService?.GetTranslationByKey("cmd", fallbackCulture) ?? FALLBACK_TRANSLATION, ".cmd"),
            new FileExtension(pluginTranslationService?.GetTranslationByKey("powershell", fallbackCulture) ?? FALLBACK_TRANSLATION, ".ps")
        };
    }

    /// <inheritdoc/>
    public override void Dispose()
    {
        //Nothing to dispose!
    }
}
