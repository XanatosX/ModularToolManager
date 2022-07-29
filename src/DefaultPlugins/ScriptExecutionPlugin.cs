using ModularToolManagerPlugin.Plugin;
using System.Globalization;

namespace DefaultPlugins;

public class ScriptExecutionPlugin : AbstractFunctionPlugin
{
    public override void Dispose()
    {

    }

    public override bool Execute(string parameters, string path)
    {
        return true;
    }

    public override string GetFunctionDisplayName()
    {
        return translationService.GetTranslationByKey("displayname", CultureInfo.GetCultureInfo("en-EN")) ?? "Script Execution";
    }

    public override Version GetFunctionVersion()
    {
        return Version.Parse("0.0.0.0");
    }

    public override bool IsOperationSystemValid()
    {
        return true;
    }
}
