using Helper.GUI;
using ModularToolManger.Core;
using PluginInterface;
using PluginManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using ToolMangerInterface;

namespace ModularToolManger.Forms
{
    public partial class F_ToolManager : Form
    {
        private Manager _pluginManager;
        private FunctionsManager _functionManager;

        private string _functionsPath;

        public F_ToolManager()
        {
            InitializeComponent();
            _pluginManager = new Manager();
            _pluginManager.Initialize(new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName + @"\Modules");
            _pluginManager.LoadPlugins();

            List<string> allowedTypes = new List<string>();
            foreach (IPlugin currentPlugin in _pluginManager.LoadetPlugins)
            {
                if (currentPlugin.ContainsInterface(typeof(IFunction)))
                {
                    allowedTypes.Add(((IFunction)currentPlugin).UniqueName);
                }
            }

            _functionsPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\ToolManager\";

            _functionManager = new FunctionsManager(_functionsPath + "functions.json", allowedTypes);
            _functionManager.Load();

            foreach (Function F in _functionManager.Functions)
            {
                Button B = new Button()
                {
                    Name = F.Name,
                    Tag = F,
                    Visible = true,
                    Location = new Point(0, 0),
                
                };
                B.Click += F_ToolManager_Click;
                this.Controls.Add(B);
            }
            this.DoForEveryControl(typeof(Button), CenterButton);

        }

        private bool CenterButton(Control B)
        {
            B.SetWidthByTextLenght();
            this.AlignMultipleControls(B);
            return true;
        }

        private void F_ToolManager_Click(object sender, EventArgs e)
        {
            if (sender.GetType() == typeof(Button))
            {
                Button B = (Button)sender;

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.SetupLanguage();
        }

        private void F_ToolManager_Langauge_Click(object sender, EventArgs e)
        {
            F_LanguageSelect LanguageSelector = new F_LanguageSelect();
            Hide();
            LanguageSelector.ShowDialog();
            Show();
            this.SetupLanguage();
        }

        private void F_ToolManager_NewFunction_Click(object sender, EventArgs e)
        {
            F_NewFunction NewFunction = new F_NewFunction(ref _pluginManager);
            Hide();
            NewFunction.ShowDialog();
            if (NewFunction.NewFunction != null)
            {
                _functionManager.AddNewFunction(NewFunction.NewFunction);
                _functionManager.Save();
            }
            Show();
        }
    }
}
