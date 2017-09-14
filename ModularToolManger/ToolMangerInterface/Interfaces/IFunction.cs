using PluginInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolMangerInterface
{
    public interface IFunction : ISettingPlugin
    {
        Dictionary<string, string> FileEndings { get; }
    }
}
