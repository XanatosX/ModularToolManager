using DefaultPlugins.Information;
using ModularToolManagerPlugin.Attributes;
using ModularToolManagerPlugin.Enums;
using ModularToolManagerPlugin.Models;
using ModularToolManagerPlugin.Plugin;
using ModularToolManagerPlugin.Services;
using System.Diagnostics;

namespace DefaultPlugins;

/// <summary>
/// Plugin for executing binaries on the system
/// </summary>
public sealed class BinaryExecutionPlugin : AbstractFunctionPlugin
{
    /// <summary>
    /// The plugin information
    /// </summary>
    private PluginInformation? pluginInformation;

    /// <summary>
    /// The translation service to use
    /// </summary>
    private readonly IPluginTranslationService translationService;

    /// <summary>
    /// The logging service to use
    /// </summary>
    private readonly IPluginLoggerService<BinaryExecutionPlugin> loggingService;

    /// <summary>
    /// Fallback text if a translation is missing
    /// </summary>
    private const string FALLBACK_TRANSLATION = "MISSING TRANSLATION";

    /// <summary>
    /// Fallback text for logging if a script could not be found
    /// </summary>
    private const string FALLBACK_SCRIPT_NOT_FOUND = "Could not find binary file '{0}' to run";

    /// <summary>
    /// Setting if the application should be startet as a administrator
    /// </summary>
    [Setting("adminRequired", SettingType.Boolean, false)]
    public bool RunAsAdministrator { get; set; }

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    /// <param name="translationService">The plugin translation service to use</param>
    /// <param name="loggingService">The logger service to use</param>
    public BinaryExecutionPlugin(IPluginTranslationService translationService, IPluginLoggerService<BinaryExecutionPlugin> loggingService)
    {
        this.translationService = translationService;
        this.loggingService = loggingService;
    }

    /// <inheritdoc/>
    public override void ResetSettings()
    {
        RunAsAdministrator = false;
    }

    /// <inheritdoc/>
    public override bool Execute(string parameters, string path)
    {
        loggingService?.LogTrace($"Execute plugin with path attribute '{path}' including the following parameters '{parameters}'");
        if (!File.Exists(path))
        {
            string baseMessage = translationService?.GetTranslationByKey("error_cant_find_binary_file") ?? FALLBACK_SCRIPT_NOT_FOUND;
            loggingService?.LogError(baseMessage, path);
            return false;
        }

        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = path,
            Arguments = parameters
        };
        if (RunAsAdministrator)
        {
            startInfo.Verb = "runas";
            startInfo.UseShellExecute = true;
        }
        try
        {
            Process.Start(startInfo);
        }
        catch (Exception e)
        {
            loggingService?.LogError($"Could not run plugin with path {path} and parameters {parameters}");
            loggingService?.LogError(e.Message);
        }

        loggingService?.LogTrace($"Executing of plugin done");
        return true;
    }

    /// <inheritdoc/>
    public override IEnumerable<FileExtension> GetAllowedFileEndings()
    {
        return GetWindowsExtensions();
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
            new FileExtension(translationService?.GetTranslationByKey("executable") ?? FALLBACK_TRANSLATION, "exe"),

        };
    }

    /// <inheritdoc/>
    public override string GetDisplayName()
    {
        loggingService?.LogTrace($"Requested display name");
        return translationService?.GetTranslationByKey("binary-displayname") ?? "Start executable binary file";
    }

    /// <inheritdoc/>
    public override bool IsOperationSystemValid()
    {
        loggingService?.LogTrace($"Checked if os is valid");
        return OperatingSystem.IsWindows();
    }

    /// <inheritdoc/>
    public override void Dispose()
    {
        loggingService?.LogTrace("Dispose plugin");
    }

    /// <inheritdoc/>
    public override PluginInformation GetPluginInformation()
    {
        pluginInformation = pluginInformation ?? PluginInformationFactory.Instance.GetPluginInformation(translationService?.GetTranslationByKey("binary-description") ?? FALLBACK_TRANSLATION);
        return pluginInformation;
    }
}
