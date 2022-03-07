using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ModularToolManager2.Services.Language
{
    internal class ResourceCultureService : ILanguageService
    {
        private List<CultureInfo>? availableCultures;

        public void ChangeLanguage(CultureInfo newCulture)
        {
            if (!ValidLanguage(newCulture))
            {
                return;
            }
            Properties.Resources.Culture = newCulture;
            availableCultures = null;
        }

        public bool ValidLanguage(CultureInfo culture)
        {
            return availableCultures.Contains(culture);
        }

        List<CultureInfo> ILanguageService.GetAvailableCultures()
        {
            if (availableCultures != null)
            {
                return availableCultures;
            }
            string applicationLocation = Assembly.GetExecutingAssembly().Location;
            string resoureFileName = Path.GetFileNameWithoutExtension(applicationLocation) + ".resources.dll";
            DirectoryInfo rootDirectory = new DirectoryInfo(Path.GetDirectoryName(applicationLocation));
            availableCultures = rootDirectory.GetDirectories()
                                             .Where(dir => CultureInfo.GetCultures(CultureTypes.AllCultures).Any(culture => culture.Name == dir.Name))
                                             .Where(dir => File.Exists(Path.Combine(dir.FullName, resoureFileName)))
                                             .Select(dir => CultureInfo.GetCultureInfo(dir.Name))
                                             .ToList();
            if (!availableCultures.Contains(CultureInfo.GetCultureInfo("en")))
            {
                availableCultures.Add(CultureInfo.GetCultureInfo("en"));
            }
            availableCultures.OrderBy(culture => culture.DisplayName);
            return availableCultures;
        }
    }
}
