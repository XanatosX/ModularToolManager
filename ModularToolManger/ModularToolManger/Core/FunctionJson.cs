using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToolMangerInterface;
using System.IO;

namespace ModularToolManger.Core
{
    public class Function
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string FilePath { get; set; }
        public int sortingSequence { get; set; }
        public bool ShowInNotification { get; set; }

        /// <summary>
        /// Trigger the action of the currently loaded function
        /// </summary>
        /// <param name="plugin">The current plugin to use</param>
        /// <returns></returns>
        public bool PerformeAction(IFunction plugin)
        {
            FileInfo FI = new FileInfo(FilePath);
            if (!FI.Exists || !plugin.FileEndings.ContainsValue(FI.Extension))
                return false;

            FunctionContext context = new FunctionContext(FilePath);

            return plugin.PerformeAction(context);
        }
    }

    /// <summary>
    /// Simple container class for the whole functions in the JSON-File
    /// </summary>
    public class FunctionsRoot
    {
        public List<Function> Functions { get; set; }
    }
}
