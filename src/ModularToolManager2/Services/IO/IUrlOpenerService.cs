using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularToolManager2.Services.IO
{
    internal interface IUrlOpenerService
    {
        bool OpenUrl(string url);

        bool OpenUrl(Uri url);
    }
}
