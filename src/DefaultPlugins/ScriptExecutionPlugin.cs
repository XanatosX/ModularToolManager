using ModularToolManagerPlugin.Attributes;
using ModularToolManagerPlugin.Models;
using ModularToolManagerPlugin.Plugin;
using ModularToolManagerPlugin.Services;
using System.Diagnostics;
using System.Globalization;

namespace DefaultPlugins;

/// <summary>
/// Plugin to run scripts
/// </summary>
public class ScriptExecutionPlugin : AbstractFunctionPlugin
{
    private const string FALLBACK_TRANSLATION = "MISSING TRANSLATION";

    private const string FALLBACK_SCRIPT_NOT_FOUND = "Could not find script '{0}' to execute";

    private readonly CultureInfo fallbackCulture;

    private readonly IPluginTranslationService? pluginTranslationService;
    private readonly IPluginLoggerService? logger;

    [Setting("hide", ModularToolManagerPlugin.Enums.SettingType.Boolean, false)]
    public bool HideCmd { get; init; }

    public ScriptExecutionPlugin(IPluginTranslationService? translationService, IPluginLoggerService? logger)
    {
        fallbackCulture = CultureInfo.GetCultureInfo("en-EN");
        pluginTranslationService = translationService;
        this.logger = logger;
    }

    public override bool Execute(string parameters, string path)
    {
        if (!File.Exists(path))
        {
            string baseMessage = pluginTranslationService?.GetTranslationByKey("error_cant_find_script_file", fallbackCulture) ?? FALLBACK_SCRIPT_NOT_FOUND;
            logger?.LogError(baseMessage, path);
            return false;
        }

        ProcessStartInfo startInfo = new ProcessStartInfo()
        {
            FileName = path,
        };
        Process.Start(startInfo);
        return true;
    }

    public override string GetDisplayName()
    {
        return pluginTranslationService?.GetTranslationByKey("displayname", fallbackCulture) ?? "Script Execution";
    }

    public override Version GetVersion()
    {
        return Version.Parse("0.1.0.0");
    }

    public override bool IsOperationSystemValid()
    {
        return OperatingSystem.IsWindows();
    }

    public override IEnumerable<FileExtension> GetAllowedFileEndings()
    {
        return GetWindowsExtensions().Where(extension => !string.IsNullOrEmpty(extension.Name) && !string.IsNullOrEmpty(extension.Extension))
                                     .OrderBy(item => item.Name);
    }

    private IEnumerable<FileExtension> GetWindowsExtensions()
    {
        return new List<FileExtension>()
        {
            new FileExtension(pluginTranslationService?.GetTranslationByKey("batch", fallbackCulture) ?? FALLBACK_TRANSLATION, ".bat"),
            new FileExtension(pluginTranslationService?.GetTranslationByKey("cmd", fallbackCulture) ?? FALLBACK_TRANSLATION, ".cmd"),
            new FileExtension(pluginTranslationService?.GetTranslationByKey("powershell", fallbackCulture) ?? FALLBACK_TRANSLATION, ".ps")
        };
    }
    public override void Dispose()
    {
    }
}
