using ModularToolManagerPlugin.Services;
using System.Globalization;

namespace ModularToolManagerPlugin.Plugin;

public interface IFunctionPlugin : IDisposable
{
    bool Startup(
        IPluginTranslationService translationService,
        IFunctionSettingsService settingsService,
        OperatingSystem operatingSystem);

    void ChangeLanguage(CultureInfo culture);

    bool IsOperationSystemValid();

    string GetFunctionDisplayName();

    Version GetFunctionVersion();

    bool Execute(string parameters, string path);
}
