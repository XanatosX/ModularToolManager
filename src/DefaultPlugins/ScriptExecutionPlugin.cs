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
    private readonly IPluginTranslationService? translationsService;

    /// <summary>
    /// Logger service to use for logging purpose
    /// </summary>
    private readonly IPluginLoggerService<ScriptExecutionPlugin>? loggingService;

    /// <summary>
    /// Sould the execution be hidden
    /// </summary>
    [Setting("hide", ModularToolManagerPlugin.Enums.SettingType.Boolean, false)]
    public bool HideCmd { get; init; }

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    /// <param name="translationService">The translation service to use</param>
    /// <param name="loggingService">The logger service to use</param>
    public ScriptExecutionPlugin(IPluginTranslationService? translationService, IPluginLoggerService<ScriptExecutionPlugin>? loggingService)
    {
        fallbackCulture = CultureInfo.GetCultureInfo("en-EN");
        translationsService = translationService;
        this.loggingService = loggingService;
        loggingService?.LogTrace($"Instance was created");
    }

    /// <inheritdoc/>
    public override bool Execute(string parameters, string path)
    {
        loggingService?.LogTrace($"Execute plugin with path attribute '{path}' including the following parameters '{parameters}'");
        if (!File.Exists(path))
        {
            string baseMessage = translationsService?.GetTranslationByKey("error_cant_find_script_file", fallbackCulture) ?? FALLBACK_SCRIPT_NOT_FOUND;
            loggingService?.LogError(baseMessage, path);
            return false;
        }

        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = path,
        };
        Process.Start(startInfo);
        loggingService?.LogTrace($"Executing of plugin done");
        return true;
    }

    /// <inheritdoc/>
    public override string GetDisplayName()
    {
        loggingService?.LogTrace($"Requested display name");
        return translationsService?.GetTranslationByKey("script-displayname", fallbackCulture) ?? "Script Execution";
    }

    /// <inheritdoc/>
    public override Version GetVersion()
    {
        loggingService?.LogTrace($"Requested version");
        return Version.Parse("0.1.0.0");
    }

    /// <inheritdoc/>
    public override bool IsOperationSystemValid()
    {
        loggingService?.LogTrace($"Checked if os is valid");
        return OperatingSystem.IsWindows();
    }

    /// <inheritdoc/>
    public override IEnumerable<FileExtension> GetAllowedFileEndings()
    {
        loggingService?.LogTrace($"Get allowed file extensions");
        return GetWindowsExtensions().Where(extension => !string.IsNullOrEmpty(extension.Name) && !string.IsNullOrEmpty(extension.Extension))
                                     .OrderBy(item => item.Name);
    }

    /// <summary>
    /// Get all the windows extensions this plugin can run on
    /// </summary>
    /// <returns></returns>
    private IEnumerable<FileExtension> GetWindowsExtensions()
    {
        loggingService?.LogTrace($"Create windows extensions");
        return new List<FileExtension>
        {
            new FileExtension(translationsService?.GetTranslationByKey("batch", fallbackCulture) ?? FALLBACK_TRANSLATION, "bat"),
            new FileExtension(translationsService?.GetTranslationByKey("cmd", fallbackCulture) ?? FALLBACK_TRANSLATION, "cmd"),
            new FileExtension(translationsService?.GetTranslationByKey("powershell", fallbackCulture) ?? FALLBACK_TRANSLATION, "ps")
        };
    }

    /// <inheritdoc/>
    public override void Dispose()
    {
        loggingService?.LogTrace("Dispose plugin");
        //Nothing to dispose!
    }
}
