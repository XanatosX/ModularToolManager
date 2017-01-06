using PluginInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolMangerInterface
{
    public interface IFunction : IPlugin
    {
        Dictionary<string, string> FileEndings { get; }
    }
}
