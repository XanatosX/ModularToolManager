﻿using Helper.GUI;
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
using ToolMangerInterface;

namespace ModularToolManger.Forms
{
    public partial class F_Settings : Form
    {
        readonly Settings _settings;
        readonly List<IPlugin> _availablePlugins;
        public Settings Settings => _settings;
        private bool _save;
        public bool Save => _save;
        private bool _starting;

        private int yStart;
        private int xStart;
        private int nextOffset;
        private int curY;


        public F_Settings(Settings settings, List<IPlugin> plugins) : this(settings)
        {
            _availablePlugins = plugins;
        }
        public F_Settings(Settings settings)
        {
            _settings = settings;
            InitializeComponent();
            _save = false;
            _starting = true;
        }

        private void F_Settings_Load(object sender, EventArgs e)
        {
            xStart = 8;
            yStart = 6;
            
            nextOffset = 5;
            curY = yStart;

            this.SetupLanguage();
            this.DoForEveryControl(typeof(Button), SizeButtons);
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            SetupTabs();
            SetupEntries();
            _starting = false;


            this.AlignMultipleControls(Default_OK, B_OAuth, Default_Abort);
        }

        private bool SizeButtons(Control C)
        {
            C.SetWidthByTextLenght();
            return true;
        }

        private void SetupTabs()
        {
            if (_availablePlugins == null)
            {
                return;
            }
                

            for (int i = 0; i < _availablePlugins.Count; i++)
            {
                IPlugin plugin = _availablePlugins[i];
                TabPage basicDesign = tabControl1.TabPages[0];
                TabPage newPage = new TabPage
                {
                    BackColor = basicDesign.BackColor,
                    BackgroundImage = basicDesign.BackgroundImage,
                    BorderStyle = basicDesign.BorderStyle,
                    UseVisualStyleBackColor = basicDesign.UseVisualStyleBackColor,
                };
                newPage.Text = plugin.DisplayName;
                newPage.Name = plugin.UniqueName;
                newPage.Tag = "Added";


                SetupTabPage(newPage, (IFunction)plugin);

                if (newPage.Controls.Count > 0)
                {
                    tabControl1.TabPages.Add(newPage);
                }
                    
            }
        }

        private void SetupTabPage(Control Tab, IFunction curFunction)
        {
            foreach (IPluginSetting curSettings in curFunction.Settings.AllSettings)
            {
                if (curSettings.objectType == typeof(bool))
                {
                    CheckBox CB = new CheckBox
                    {
                        Text = curSettings.DisplayName,
                        Tag = curSettings.Key,
                        Checked = (bool)curSettings.Value,
                        Location = new Point(xStart, curY),
                    };
                    Tab.Controls.Add(CB);
                    curY += CB.Size.Height + nextOffset;
                }
                else
                {
                    Tab.Controls.Add(new Label
                    {
                        Text = curSettings.Key,
                    });
                }
            

            }
        }

        private void SetupEntries()
        {
            this.DoForEveryControl((Control TP) =>
            {
                if (TP.GetType() == typeof(TabPage))
                {
                    TP.DoForEveryControl((Control c) =>
                    {
                        string Name = _settings.DefaultApp;
                        if (c.GetType() == typeof(Label))
                        {
                            return true;
                        }

                        if (TP.Tag.ToString() == "Added")
                        {
                            Name = TP.Name;
                        }
                        IPlugin curPlugin = GetPluginByName(Name);
                        IFunction curFunction = null;
                        if (curPlugin != null)
                        {
                            curFunction = (IFunction)curPlugin;
                        }

                        string SettingsKey = c.Tag.ToString();
                        if (c.GetType() == typeof(CheckBox))
                        {
                            CheckBox cb = (CheckBox)c;
                            cb.Checked = _settings.GetBoolValue(Name, SettingsKey);

                            if (curFunction != null)
                            {
                                curFunction.Settings.UpdateValue(SettingsKey, cb.Checked);
                            }
                        }
                        if (c.GetType() == typeof(TrackBar))
                        {
                            TrackBar tbar = (TrackBar)c;
                            int loadetValue = Settings.GetIntValue(Name, SettingsKey) < tbar.Minimum ? tbar.Minimum : Settings.GetIntValue(Name, SettingsKey);
                            loadetValue = loadetValue > tbar.Maximum ? tbar.Maximum : loadetValue;
                            tbar.Value = loadetValue;
                        }
                        return true;
                    
                    });
                }

                return true;
            });
        }

        private IPlugin GetPluginByName(string uniqueName)
        {
            foreach (IPlugin plugin in _availablePlugins)
            {
                if (plugin.UniqueName == uniqueName)
                {
                    return plugin;
                }
                    
            }
            return null;
        }

        private void Default_OK_Click(object sender, EventArgs e)
        {
            this.DoForEveryControl((Control TP) => {
                if (TP.GetType() == typeof(TabPage))
                    TP.DoForEveryControl((Control c) =>
                    {
                        if (c.GetType() == typeof(Label))
                            return true;
                        string Name = _settings.DefaultApp;
                        if (TP.Tag.ToString() == "Added")
                        {
                            Name = TP.Name;
                            UpdatePlugins(Name, (TabPage)TP);
                        }
                            
                        string SettingsName = c.Tag.ToString();
                        if (SettingsName == "")
                        {
                            return false;
                        }
                            

                        if (c.GetType() == typeof(CheckBox))
                        {
                            CheckBox box = (CheckBox)c;
                            Settings.AddOrChangeKeyValue(Name, SettingsName, box.Checked);
                        }
                        if (c.GetType() == typeof(TrackBar))
                        {
                            TrackBar tbar = (TrackBar)c;
                            Settings.AddOrChangeKeyValue(Name, SettingsName, tbar.Value);
                        }
                        return true;
                    });
                return true;
            });
            _save = true;



            this.Close();
        }

        private void UpdatePlugins(string pluginName, TabPage page)
        {
            page.DoForEveryControl((Control c) =>
            {
                if (c.GetType() == typeof(Label))
                    return true;
                IPlugin curPlugin = GetPluginByName(pluginName);
                IFunction curFunction = null;
                if (curPlugin != null)
                {
                    try
                    {
                        curFunction = (IFunction)curPlugin;
                    }
                    catch (Exception)
                    {
                    }
                }
                string SettingsKey = c.Tag.ToString();
                if (c.GetType() == typeof(CheckBox))
                {
                    CheckBox cb = (CheckBox)c;

                    if (curFunction != null)
                        curFunction.Settings.UpdateValue(SettingsKey, cb.Checked);
                }

                return true;
            });
        }

        private void F_Settings_CB_AutoStart_CheckedChanged(object sender, EventArgs e)
        {
            if (_starting)
                return;
            string TargetFolder = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            string Name = "Tool Manager.url";
            string ShortcutFile = $"{TargetFolder}\\{Name}";
            if (F_Settings_CB_AutoStart.Checked)
            {
                CreateShortcut(ShortcutFile);
            }
            else
            {
                DeleteShortcut(ShortcutFile);
            }
                
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
            {
                File.Delete(ShortcutFile);
            }
            
        }

        private void Default_Abort_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void B_OAuth_Click(object sender, EventArgs e)
        {
            F_OAuth oauthManager = new F_OAuth(_settings);
            oauthManager.ShowDialog();
        }
    }
}
