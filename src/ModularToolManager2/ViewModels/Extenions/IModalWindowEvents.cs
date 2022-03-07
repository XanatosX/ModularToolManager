using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularToolManager2.ViewModels.Extenions
{
    public interface IModalWindowEvents
    {
        event EventHandler Closing;
    }
}
