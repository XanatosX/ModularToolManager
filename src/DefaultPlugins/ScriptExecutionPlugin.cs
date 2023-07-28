using DefaultPlugins.Information;
using DefaultPlugins.ProcessStartStrategies;
using ModularToolManagerPlugin.Attributes;
using ModularToolManagerPlugin.Enums;
using ModularToolManagerPlugin.Models;
using ModularToolManagerPlugin.Plugin;
using ModularToolManagerPlugin.Services;
using System.Diagnostics;

namespace DefaultPlugins;

/// <summary>
/// Plugin to run scripts on windows maschines
/// </summary>
public sealed class ScriptExecutionPlugin : AbstractFunctionPlugin
{
    /// <summary>
    /// The plugin information
    /// </summary>
    private PluginInformation? pluginInformation;

    /// <summary>
    /// The factory to use for creating starter objects
    /// </summary>
    private DefaultScriptStarterFactory starterFactory;

    /// <summary>
    /// Fallback text if a translation is missing
    /// </summary>
    private const string FALLBACK_TRANSLATION = "MISSING TRANSLATION";

    /// <summary>
    /// Fallback text for logging if a script could not be found
    /// </summary>
    private const string FALLBACK_SCRIPT_NOT_FOUND = "Could not find script '{0}' to execute";

    /// <summary>
    /// Service to use for getting translation from this assembly
    /// </summary>
    private readonly IPluginTranslationService? translationService;

    /// <summary>
    /// Logger service to use for logging purpose
    /// </summary>
    private readonly IPluginLoggerService<ScriptExecutionPlugin>? loggingService;

    /// <summary>
    /// Sould the execution be hidden
    /// </summary>
    [Setting("hide", ModularToolManagerPlugin.Enums.SettingType.Boolean, false)]
    public bool HideCmd { get; set; }

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    /// <param name="translationService">The translation service to use</param>
    /// <param name="loggingService">The logger service to use</param>
    public ScriptExecutionPlugin(IPluginTranslationService? translationService, IPluginLoggerService<ScriptExecutionPlugin>? loggingService)
    {
        this.translationService = translationService;
        this.loggingService = loggingService;
        loggingService?.LogTrace($"Instance was created");
        starterFactory = new DefaultScriptStarterFactory((severity, message) => loggingService?.Log(severity, message, string.Empty));
    }

    /// <inheritdoc/>
    public override void ResetSettings()
    {
        loggingService?.LogTrace("Reset settings");
        HideCmd = false;
    }

    /// <inheritdoc/>
    public override bool Execute(string parameters, string path)
    {
        loggingService?.LogTrace($"Execute plugin with path attribute '{path}' including the following parameters '{parameters}'");
        if (!File.Exists(path))
        {
            string baseMessage = translationService?.GetTranslationByKey("error_cant_find_script_file") ?? FALLBACK_SCRIPT_NOT_FOUND;
            loggingService?.LogError(baseMessage, path);
            return false;
        }

        ProcessStartInfo? startInfo = starterFactory.CreateStartInfo(path)?.GetStartInfo(parameters, path);
        if (startInfo is null)
        {
            loggingService?.LogWarning("Could not get a starter for the operation system!");
            return false;
        }
        Process.Start(startInfo);
        loggingService?.LogTrace($"Executing of plugin done");
        return true;
    }

    /// <inheritdoc/>
    public override string GetDisplayName()
    {
        loggingService?.LogTrace($"Requested display name");
        return translationService?.GetTranslationByKey("script-displayname") ?? "Script Execution";
    }

    /// <inheritdoc/>
    public override bool IsOperationSystemValid()
    {
        loggingService?.LogTrace($"Checked if os is valid");
        return OperatingSystem.IsWindows() || OperatingSystem.IsLinux();
    }

    /// <inheritdoc/>
    public override IEnumerable<FileExtension> GetAllowedFileEndings()
    {
        loggingService?.LogTrace($"Get allowed file extensions");
        if (OperatingSystem.IsLinux())
        {
            return GetLinuxExtensions().Where(extension => !string.IsNullOrEmpty(extension.Name) && !string.IsNullOrEmpty(extension.Extension))
                                       .OrderBy(item => item.Name);
        }
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
            new FileExtension(translationService?.GetTranslationByKey("batch") ?? FALLBACK_TRANSLATION, "bat"),
            new FileExtension(translationService?.GetTranslationByKey("cmd") ?? FALLBACK_TRANSLATION, "cmd"),
            new FileExtension(translationService?.GetTranslationByKey("powershell") ?? FALLBACK_TRANSLATION, "ps")
        };
    }

    private IEnumerable<FileExtension> GetLinuxExtensions()
    {
        loggingService?.LogTrace("Create linux extensions");
        return new List<FileExtension> {
            new FileExtension("shell", "sh")
        };
    }

    /// <inheritdoc/>
    public override void Dispose()
    {
        loggingService?.LogTrace("Dispose plugin");
    }

    /// <inheritdoc/>
    public override PluginInformation GetPluginInformation()
    {
        pluginInformation = pluginInformation ?? PluginInformationFactory.Instance.GetPluginInformation(translationService?.GetTranslationByKey("script-description") ?? FALLBACK_TRANSLATION);
        return pluginInformation;
    }
}
