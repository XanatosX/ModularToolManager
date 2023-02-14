using ModularToolManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularToolManager.Services.Ui;
public interface IThemeService
{
    IEnumerable<ApplicationStyle> GetAllStyles();

    void ChangeApplicationTheme(ApplicationStyle theme);

    ApplicationStyle? GetStyleById(int id);
}
