using ModularToolManagerPlugin.Models;
using System.Globalization;

namespace ModularToolManagerPlugin.Plugin;

public interface IFunctionPlugin : IDisposable
{
    void ChangeLanguage(CultureInfo culture);

    bool IsOperationSystemValid();

    string GetFunctionDisplayName();

    Version GetFunctionVersion();

    bool Execute(string parameters, string path);

    IEnumerable<FileExtension> GetAllowedFileEndings();
}
