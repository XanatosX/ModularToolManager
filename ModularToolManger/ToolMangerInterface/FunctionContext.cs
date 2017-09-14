using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolMangerInterface
{
    public class FunctionContext : IFunctionContext
    {
        private string _filePath;
        public string FilePath
        {
            get
            {
                return _filePath;
            }
            set
            {
                _filePath = value;
            }
        }

        public string SaveFile
        {
            get
            {
                return null;
            }
            set
            {
                SaveFile = null;
            }
        }

        public FunctionContext(string path)
        {
            _filePath = path;
        }
    }
}
