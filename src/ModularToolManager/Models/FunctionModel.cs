using ModularToolManagerPlugin.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularToolManager.Models
{
    internal class FunctionModel
    {
        string UniqueIdentifier { get; init; }

        IFunctionPlugin Plugin { get; init; }

        int SortOrder { get; }

        public FunctionModel(string identifier, int sortOrder, IFunctionPlugin plugin)
        {
            UniqueIdentifier = identifier;
            Plugin = plugin;
            SortOrder = sortOrder;
        }
    }
}
