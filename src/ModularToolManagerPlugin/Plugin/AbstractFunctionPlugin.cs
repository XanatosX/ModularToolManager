using ModularToolManagerPlugin.Models;
using ModularToolManagerPlugin.Services;
using System;
using System.Globalization;

namespace ModularToolManagerPlugin.Plugin;

public abstract class AbstractFunctionPlugin : IFunctionPlugin
{
    protected CultureInfo? currentCulture;

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
}
