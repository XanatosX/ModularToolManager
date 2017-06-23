using ModularToolManger.Core;
using ModularToolManger.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using JSONSettings;

namespace ModularToolManger
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Settings s = new Settings();
            s.AddNewField("ToolManager");
            s.AddKeyValue("ToolManager", "Test", "Hey");
            s.AddKeyValue("ToolManager", "Test2", false);
            s.Save();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            CentralLanguage.Initalize();
            Application.Run(new F_ToolManager());
        }
    }
}
