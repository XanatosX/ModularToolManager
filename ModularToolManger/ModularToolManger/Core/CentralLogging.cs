﻿using Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ModularToolManger.Core
{
    //@TODO find a non static way of doing this ...
    public static class CentralLogging
    {
        public static FileLogger AppDebugLogger = new FileLogger(Path.GetTempPath(), "ModularToolManager_Debug.log");//new BasicLogger(Path.GetTempPath(), "ModularToolManager_Debug.log");
    }
}
