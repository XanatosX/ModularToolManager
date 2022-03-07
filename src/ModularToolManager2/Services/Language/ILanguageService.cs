using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularToolManager2.Services.Language
{
    public interface ILanguageService
    {
        List<CultureInfo> GetAvailableCultures();

        void ChangeLanguage(CultureInfo newCulture);

        bool ValidLanguage(CultureInfo culture);
    }
}
