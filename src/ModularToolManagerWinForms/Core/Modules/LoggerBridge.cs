using Logging;
using PluginCommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModularToolManger.Core.Modules
{
    public class LoggerBridge : Module
    {
        public override void init()
        {
            addType("log");
            addType("LogWarning");
            addType("LogError");
            addType("LogCritical");

            base.init();
        }

        public override void Notified(MessageData DataSet)
        {
            LogLevel curLevel = LogLevel.Unknown;
            switch (DataSet.Type)
            {
                case "log":
                    curLevel = LogLevel.Information;
                    break;
                case "logwarning":
                    curLevel = LogLevel.Warning;
                    break;
                case "logerror":
                    curLevel = LogLevel.Error;
                    break;
                case "logcritical":
                    curLevel = LogLevel.Critical;
                    break;
                default:
                    break;
            }
            CentralLogging.AppDebugLogger.WriteLine(DataSet.Data.ToString(), curLevel);
        }
    }
}
