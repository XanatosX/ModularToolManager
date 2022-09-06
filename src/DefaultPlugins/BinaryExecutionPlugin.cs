using ModularToolManagerPlugin.Models;
using ModularToolManagerPlugin.Plugin;
using ModularToolManagerPlugin.Services;
using System.Diagnostics;
using System.Globalization;

namespace DefaultPlugins;
public class BinaryExecutionPlugin : AbstractFunctionPlugin
{
    private readonly IPluginTranslationService translationService;
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
    /// The fallback language to use
    /// </summary>
    private readonly CultureInfo fallbackCulture;

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
    public override bool Execute(string parameters, string path)
    {
        loggingService?.LogTrace($"Execute plugin with path attribute '{path}' including the following parameters '{parameters}'");
        if (!File.Exists(path))
        {
            string baseMessage = translationService?.GetTranslationByKey("error_cant_find_binary_file", fallbackCulture) ?? FALLBACK_SCRIPT_NOT_FOUND;
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
            new FileExtension(translationService?.GetTranslationByKey("executable", fallbackCulture) ?? FALLBACK_TRANSLATION, "exe"),

        };
    }

    /// <inheritdoc/>
    public override string GetDisplayName()
    {
        loggingService?.LogTrace($"Requested display name");
        return translationService?.GetTranslationByKey("binary-displayname", fallbackCulture) ?? "Script Execution";
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
    public override void Dispose()
    {
        loggingService?.LogTrace("Dispose plugin");
    }
}
