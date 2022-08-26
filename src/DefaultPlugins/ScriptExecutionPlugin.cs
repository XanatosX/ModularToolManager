using ModularToolManagerPlugin.Attributes;
using ModularToolManagerPlugin.Models;
using ModularToolManagerPlugin.Plugin;
using ModularToolManagerPlugin.Services;
using System.Globalization;

namespace DefaultPlugins;

public class ScriptExecutionPlugin : AbstractFunctionPlugin
{
    private const string FALLBACK_TRANSLATION = "MISSING TRANSLATION";

    private readonly CultureInfo fallbackCulture;

    private readonly IPluginTranslationService? pluginTranslationService;

    [Setting("hide", ModularToolManagerPlugin.Enums.SettingType.Boolean, false)]
    public bool HideCmd { get; init; }

    public ScriptExecutionPlugin(IPluginTranslationService translationService)
    {
        fallbackCulture = CultureInfo.GetCultureInfo("en-EN");
        pluginTranslationService = translationService;
    }

    public override void Dispose()
    {
    }

    public override bool Execute(string parameters, string path)
    {
        return true;
    }

    public override string GetDisplayName()
    {
        return pluginTranslationService?.GetTranslationByKey("displayname", fallbackCulture) ?? "Script Execution";
    }

    public override Version GetVersion()
    {
        return Version.Parse("0.0.0.0");
    }

    public override bool IsOperationSystemValid()
    {
        return true;
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
}
