using ModularToolManagerPlugin.Services;

namespace ModularToolManagerPlugin.Plugin
{
    public interface IFunctionPlugin : IDisposable
    {
        bool Startup(
            IPluginTranslationService translationService,
            IFunctionSettingsService settingsService,
            OperatingSystem operatingSystem);

        bool IsOperationSystemValid();

        string GetFunctionDisplayName();

        Version GetFunctionVersion();

        bool Execute(string parameters, string path);
    }
}
