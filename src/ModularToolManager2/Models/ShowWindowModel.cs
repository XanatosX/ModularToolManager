using Avalonia.Controls;
using ModularToolManager2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularToolManager2.Models
{
    public record ShowWindowModel(ModalWindowViewModel ViewModel, WindowStartupLocation StartupLocation)
    {
    }
}
