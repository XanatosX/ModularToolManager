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
            //Test code for Password Decryption
            //PasswordManager PM = new PasswordManager();
            //PM.DecryptPassword(PM.EncryptPassword("Test", "Test"), "Keks");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            CentralLanguage.Initalize();
            Application.Run(new F_ToolManager());
        }
    }
}
