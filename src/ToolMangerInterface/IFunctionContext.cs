using PluginInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolMangerInterface
{
    public interface IFunctionContext : PluginContext
    {
        string FilePath { get; set; }
    }
}
