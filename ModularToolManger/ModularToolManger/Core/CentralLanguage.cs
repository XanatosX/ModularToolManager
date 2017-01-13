using jsonLanguage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Helper.GUI;

namespace ModularToolManger.Core
{
    public static class CentralLanguage
    {
        public static Language LanguageManager;

        public static void Initalize()
        {
            LanguageManager = new Language(new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName + @"\Language");
        }

        public static void SetupLanguage(this Form f)
        {
            f.Text = LanguageManager.GetText(f.Name);
            f.DoForEveryControl(SetLanguage);
        }

        private static bool SetLanguage(Control current)
        {
            if (current.GetType() == typeof(MenuStrip))
            {
                foreach (ToolStripMenuItem item in ((MenuStrip)current).Items)
                {
                    item.Text = LanguageManager.GetText(item.Name);
                    if (item.HasDropDownItems)
                        ForAllSubItems(item);
                }
            }
            else
                current.Text = LanguageManager.GetText(current.Name);
            return true;
        }

        private static void ForAllSubItems(ToolStripMenuItem current)
        {
            foreach (ToolStripMenuItem item in current.DropDownItems)
            {
                if (item.HasDropDownItems)
                    ForAllSubItems(item);
                else
                    item.Text = LanguageManager.GetText(item.Name);
            }
        }
    }
}
