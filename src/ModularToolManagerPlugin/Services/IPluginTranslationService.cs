using ModularToolManagerPlugin.Models;
using System.Globalization;

namespace ModularToolManagerPlugin.Services
{
    public interface IPluginTranslationService
    {
        List<string> GetLanguages();

        List<TranslationModel> GetAllTranslations();

        string? GetTranslationByKey(string key, CultureInfo fallbackCulture);

        List<string> GetKeys();
    }
}
