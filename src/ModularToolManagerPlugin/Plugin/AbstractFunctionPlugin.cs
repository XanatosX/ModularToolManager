using ModularToolManagerPlugin.Models;
using ModularToolManagerPlugin.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularToolManagerPlugin.Plugin;

public abstract class AbstractFunctionPlugin : IFunctionPlugin
{
    protected IPluginTranslationService translationService;
    protected IFunctionSettingsService settingsService;
    protected OperatingSystem operatingSystem;
    protected CultureInfo currentCulture;

    public void ChangeLanguage(CultureInfo culture)
    {
        currentCulture = culture;
    }

    public abstract void Dispose();

    public abstract bool Execute(string parameters, string path);

    public abstract IEnumerable<FileExtension> GetAllowedFileEndings();

    public abstract string GetFunctionDisplayName();

    public abstract Version GetFunctionVersion();

    public abstract bool IsOperationSystemValid();

    public virtual bool Startup(IPluginTranslationService translationService, IFunctionSettingsService settingsService, OperatingSystem operatingSystem)
    {
        this.translationService = translationService;
        this.settingsService = settingsService;
        this.operatingSystem = operatingSystem;

        return true;
    }
}
