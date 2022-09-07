using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularToolManager.Services.Ui;
internal interface IWindowVisibilityService
{
    void CloseWindow(Window window);

    void CloseApplication();

    void ToggleWindowVisiblity(Window window);

    void ToggleMainWindowVisiblity();
}
