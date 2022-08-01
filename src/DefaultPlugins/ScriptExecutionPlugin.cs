using ModularToolManagerPlugin.Models;
using ModularToolManagerPlugin.Plugin;
using System.Globalization;

namespace DefaultPlugins;

public class ScriptExecutionPlugin : AbstractFunctionPlugin
{
    private readonly CultureInfo fallbackCulture;

    public ScriptExecutionPlugin()
    {
        fallbackCulture = CultureInfo.GetCultureInfo("en-EN");
    }

    public override void Dispose()
    {
    }

    public override bool Execute(string parameters, string path)
    {
        return true;
    }

    public override string GetFunctionDisplayName()
    {
        return translationService.GetTranslationByKey("displayname", fallbackCulture) ?? "Script Execution";
    }

    public override Version GetFunctionVersion()
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
            new FileExtension(translationService.GetTranslationByKey("batch", fallbackCulture), ".bat"),
            new FileExtension(translationService.GetTranslationByKey("cmd", fallbackCulture), ".cmd"),
            new FileExtension(translationService.GetTranslationByKey("powershell", fallbackCulture), ".ps")
        };
    }
}
