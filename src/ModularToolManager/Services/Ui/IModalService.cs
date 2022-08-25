using Avalonia.Controls;
using ModularToolManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularToolManager.Services.Ui
{
    public interface IModalService
    {
        Task ShowModalWindowAsync(ShowWindowModel modalData, Window parent);
    }
}
