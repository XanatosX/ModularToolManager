using Helper.GUI;
using JSONSettings;
using Microsoft.Win32;
using ModularToolManger.Core;
using PluginInterface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ModularToolManger.Forms
{
    public partial class F_Settings : Form
    {
        private Settings _settings;
        private List<IPlugin> _availablePlugins;
        public Settings Settings => _settings;
        public bool Save;
        private bool _starting;

        public F_Settings(Settings settings, List<IPlugin> plugins = null)
        {
            _settings = settings;
            _availablePlugins = plugins;
            InitializeComponent();
            Save = false;
            _starting = true;
        }

        private void F_Settings_Load(object sender, EventArgs e)
        {
            this.SetupLanguage();
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            SetupTabs();
            SetupEntries();
            _starting = false;
        }

        private void SetupTabs()
        {
            if (_availablePlugins == null)
                return;

            for (int i = 0; i < _availablePlugins.Count; i++)
            {
                IPlugin plugin = _availablePlugins[i];
                TabPage basicDesign = tabControl1.TabPages[0];
                TabPage newPage = new TabPage()
                {
                    BackColor = basicDesign.BackColor,
                    BackgroundImage = basicDesign.BackgroundImage,
                    BorderStyle = basicDesign.BorderStyle,
                    UseVisualStyleBackColor = basicDesign.UseVisualStyleBackColor,
                };
                newPage.Text = plugin.DisplayName;
                newPage.Name = plugin.UniqueName;
                newPage.Tag = "Added";
                if (newPage.Controls.Count > 0)
                    tabControl1.TabPages.Add(newPage);
            }
        }

        private void SetupEntries()
        {
            this.DoForEveryControl((Control TP) =>
            {
                if (TP.GetType() == typeof(TabPage))
                    TP.DoForEveryControl((Control c) =>
                    {
                        string Name = _settings.DefaultApp;
                        if (TP.Tag.ToString() == "Added")
                            Name = TP.Name;
                        string SettingsKey = c.Tag.ToString();
                        if (c.GetType() == typeof(CheckBox))
                        {
                            CheckBox cb = (CheckBox)c;
                            cb.Checked = _settings.GetBoolValue(Name, SettingsKey);
                        }
                        return true;
                    });
                return true;
            });
        }

        private void F_Settings_CB_KeepOnTop_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Default_OK_Click(object sender, EventArgs e)
        {
            this.DoForEveryControl((Control TP) => {
                if (TP.GetType() == typeof(TabPage))
                    TP.DoForEveryControl((Control c) =>
                    {
                        string Name = _settings.DefaultApp;
                        if (TP.Tag.ToString() == "Added")
                            Name = TP.Name;
                        string SettingsName = c.Tag.ToString();
                        if (SettingsName == "")
                            return false;

                        if (c.GetType() == typeof(CheckBox))
                        {
                            CheckBox box = (CheckBox)c;
                            Settings.AddOrChangeKeyValue(Name, SettingsName, box.Checked);
                        }
                        return true;
                    });
                return true;
            });
            Save = true;
            this.Close();
        }

        private void F_Settings_CB_AutoStart_CheckedChanged(object sender, EventArgs e)
        {
            //Disabled to prevent virus warning
            //RegistryKey AutostartKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            //if (F_Settings_CB_AutoStart.Checked)
            //{
            //    AutostartKey.SetValue("ToolManagerAutoStart", Application.ExecutablePath.ToString());
            //    AutostartKey.Close();
            //}
            //else
            //{
            //    AutostartKey.DeleteValue("ToolManagerAutoStart");
            //}

            //compromise solution
            if (_starting)
                return;
            string TargetFolder = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            string Name = "Tool Manager.url";
            string ShortcutFile = $"{TargetFolder}\\{Name}";
            if (F_Settings_CB_AutoStart.Checked)
                CreateShortcut(ShortcutFile);
            else
                DeleteShortcut(ShortcutFile);

        }

        private void CreateShortcut(string ShortcutFile)
        {

            string app = Application.ExecutablePath.ToString();

            using (StreamWriter writer = new StreamWriter(ShortcutFile))
            {
                writer.WriteLine("[InternetShortcut]");
                writer.WriteLine("URL=file:///" + app);
                writer.WriteLine("IconIndex=0");
                string icon = app.Replace('\\', '/');
                writer.WriteLine("IconFile=" + icon);
            }
        }

        private void DeleteShortcut(string ShortcutFile)
        {
            if (File.Exists(ShortcutFile))
            File.Delete(ShortcutFile);
        }

        private void Default_Abort_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
