using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModularToolManger.Core
{
    public class Function
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string FilePath { get; set; }
    }

    public class FunctionsRoot
    {
        public List<Function> Functions { get; set; }
    }
}
