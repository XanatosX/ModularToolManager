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
using System.Configuration;
using System.Windows.Forms;
using ToolMangerInterface;
using static System.Collections.Specialized.NameObjectCollectionBase;

namespace ModularToolManger.Forms
{
    public partial class F_ToolManager : Form
    {
        private Manager _pluginManager;
        private FunctionsManager _functionManager;
        private int _startOffset = 25;
        private int _maxHeight;
        private int _minWidth;
        private Point _location;
        private bool _forceClose;
        private bool _hidden;

        private int _newValue;

        private string _functionsPath;

        private int _lastContextListButton;

        public F_ToolManager()
        {
            InitializeComponent();
            _hidden = false;
            _forceClose = false;
            _lastContextListButton = 0;

            Default_Show.Visible = _hidden;

            _minWidth = this.Size.Width;
            _maxHeight = Screen.FromControl(this).Bounds.Height / 4;
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MinimizeBox = false;

            F_ToolManager_NI_Taskliste.ContextMenuStrip = F_ToolManager_TasklisteContext;

            _pluginManager = new Manager();


            File.Delete(CentralLogging.AppDebugLogger.LogFile);
            CentralLogging.AppDebugLogger.WriteLine("Searching modules at: " + new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName + @"\Modules", Logging.LogLevel.Information);

            SetupPlugins();

        }
        private void SetupPlugins()
        {
            List<string> allowedTypes = LoadPlugins();
            LoadFunctions(allowedTypes);
        }
        private List<string> LoadPlugins()
        {
            _pluginManager.Initialize(new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName + @"\Modules");
            _pluginManager.Error += _pluginManager_Error;
            _pluginManager.LoadPlugins();

            List<string> allowedTypes = new List<string>();
            foreach (IPlugin currentPlugin in _pluginManager.LoadetPlugins)
            {
                if (currentPlugin.ContainsInterface(typeof(IFunction)))
                {
                    allowedTypes.Add(((IFunction)currentPlugin).UniqueName);
                }
            }
            return allowedTypes;
        }
        private void LoadFunctions(List<string> allowedTypes)
        {
            _functionsPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\ToolManager\";

            CentralLogging.AppDebugLogger.WriteLine("Searching functions at: " + _functionsPath, Logging.LogLevel.Information);

            _functionManager = new FunctionsManager(_functionsPath + "functions.json", allowedTypes);
            _functionManager.Load();
        }
        private void _pluginManager_Error(object sender, ErrorData e)
        {
            CentralLogging.AppDebugLogger.Log(e.Message, Logging.LogLevel.Error);
            CentralLogging.AppDebugLogger.Log(e.ErrorException.Message, Logging.LogLevel.Error);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetLanguage();
            SetupButtons();
        }
        private void MoveToPosition()
        {
            int LocX, LocY;
            Rectangle ScreenSize = Screen.FromControl(this).WorkingArea;
            LocX = ScreenSize.Width - Size.Width;
            LocY = ScreenSize.Height - Size.Height;
            _location = new Point(LocX, LocY);
            Location = _location;
        }
        private void SetLanguage()
        {
            this.SetupLanguage();
            SetLanguageForContextStrip(F_ToolManager_ButtonContext);
            SetLanguageForContextStrip(F_ToolManager_TasklisteContext);
        }
        private void SetupButtons()
        {
            this.DoForEveryControl(typeof(Button), DeleteButton);
            F_ToolManager_ScrollBar.Visible = false;
            Button lastButton = null;

            if (_functionManager.Functions.Count > 0)
            {
                for (int i = 0; i < _functionManager.Functions.Count; i++)
                {
                    Function currentFunction = _functionManager.Functions[i];
                    MenuItem newMenuItem = new MenuItem()
                    {
                        Name = currentFunction.ID + "_MenuItem",
                        Text = currentFunction.Name,
                        Visible = true,
                        Tag = currentFunction,
                    };
                    Button newButton = new Button()
                    {
                        Name = currentFunction.ID,
                        Text = currentFunction.Name,
                        Visible = true,
                        Tag = currentFunction,
                        Size = new Size(10, 23),
                        Location = new Point(10, 10),
                    };
                    newButton.Click += F_ToolManager_Click;
                    newButton.ContextMenuStrip = F_ToolManager_ButtonContext;
                    newButton.Location = new Point(0, _startOffset + i * 25 + newButton.Size.Height);
                    this.Controls.Add(newButton);
                    newButton.SetWidthByTextLenght();
                    if (i == _functionManager.Functions.Count - 1)
                        lastButton = newButton;
                }
            }



            List<Control> buttons = this.GetAllControls(typeof(Button));
            if (buttons.Count == 0)
            {
                MoveToPosition();
                return;
            }


            int BiggestValue = 0;
            int newWidth = 0;
            int newHeight = 0;
            if (lastButton != null)
                newHeight = lastButton.Location.Y + lastButton.Size.Height + _startOffset * 2;
            if (newHeight > _maxHeight)
            {
                F_ToolManager_ScrollBar.Maximum = newHeight - _maxHeight;
                F_ToolManager_ScrollBar.Value = 0;
                newHeight = _maxHeight;
                F_ToolManager_ScrollBar.Visible = true;

            }
            for (int i = 0; i < buttons.Count; i++)
            {
                if (buttons[i].Size.Width > BiggestValue)
                    BiggestValue = buttons[i].Size.Width;
            }
            newWidth = BiggestValue;
            if (newWidth < _minWidth)
                newWidth = _minWidth;
            this.Size = new Size(newWidth, newHeight);



            this.DoForEveryControl(typeof(Button), CenterButton);

            MoveToPosition();
        } //Needs cleanup!

        private bool CenterButton(Control B)
        {
            B.SetWidthByTextLenght();
            this.AlignMultipleControls(B);
            return true;
        }
        private bool DeleteButton(Control B)
        { 
            if (B.GetType() != typeof(Button))
                return false;
            this.Controls.Remove(((Button)B));
            return true;
        }
        private bool OffsetButton(Control B)
        {
            int Offset = _newValue - F_ToolManager_ScrollBar.Value;
            B.Location = new Point(B.Location.X, B.Location.Y - Offset);
            return true;
        }

        private void F_ToolManager_Click(object sender, EventArgs e)
        {
            if (sender.GetType() == typeof(Button))
            {
                Button B = (Button)sender;
                if (B.Tag.GetType() == typeof(Function))
                {
                    Function func = (Function)B.Tag;
                    IPlugin currentPlugin = _pluginManager.GetPluginByName(func.Type);
                    if (currentPlugin == null)
                        return;
                    if (currentPlugin.ContainsInterface(typeof(IFunction)))
                        func.PerformeAction((IFunction)currentPlugin);
                }
            }
        }
        private void F_ToolManager_Langauge_Click(object sender, EventArgs e)
        {
            F_LanguageSelect LanguageSelector = new F_LanguageSelect();
            Hide();
            LanguageSelector.ShowDialog();
            Show();
            SetLanguage();
            SetupButtons();
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

                SetupButtons();
            }
            Show();
        }
        private void F_ToolManager_ButtonContext_Edit_Click(object sender, EventArgs e)
        {
            if (sender.GetType() != typeof(ToolStripMenuItem))
                return;

            Button B = GetButtonFromTSMI((ToolStripMenuItem)sender);
            Function currentFunction = GetFunctionFromButton(B);
            F_NewFunction EditFunction = new F_NewFunction(ref _pluginManager, currentFunction);
            this.Hide();
            EditFunction.ShowDialog();
            if (EditFunction.NewFunction != null)
            {
                _functionManager.DeleteFunction(currentFunction);
                _functionManager.AddNewFunction(EditFunction.NewFunction);
                _functionManager.Save();
            }
            this.Show();
            SetupButtons();
        }
        private void F_ToolManager_ScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            _newValue = e.NewValue;
            this.DoForEveryControl(typeof(Button), OffsetButton);
        }
        private void F_ToolManager_ButtonContext_Delete_Click(object sender, EventArgs e)
        {
            if (sender.GetType() != typeof(ToolStripMenuItem))
                return;

            Button B = GetButtonFromTSMI((ToolStripMenuItem)sender);
            _functionManager.DeleteFunction(GetFunctionFromButton(B));
            SetupButtons();
        }
        private void F_ToolManager_Move(object sender, EventArgs e)
        {
            Location = _location;
        }
        private void F_ToolManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_forceClose)
            {
                e.Cancel = true;
                Hide();
                _hidden = true;
                Default_Show.Visible = _hidden;
                return;
            }
            F_ToolManager_NI_Taskliste.Visible = false;
        }
        private void defaultCloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _forceClose = true;
            this.Close();
        }
        private void F_ToolManager_NI_Taskliste_Click(object sender, EventArgs e)
        {
            if (e.GetType() == typeof(MouseEventArgs))
            {
                MouseEventArgs NewE = (MouseEventArgs)e;
                if (NewE.Button == MouseButtons.Right)
                    return;
            }
            if (_hidden)
            {
                _hidden = false;
                this.MoveToPosition();
                this.Show();
                Default_Show.Visible = _hidden;
            }
        }
        private void F_ToolManager_NI_Taskliste_Close_Click(object sender, EventArgs e)
        {
            _forceClose = true;
            this.Close();
        }
        private void defaultShowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            F_ToolManager_NI_Taskliste_Click(sender, e);
        }

        private Button GetButtonFromTSMI(ToolStripMenuItem tsmi)
        {
            if (tsmi != null)
            {
                ToolStrip owner = tsmi.GetCurrentParent();
                if (owner.GetType() == typeof(ContextMenuStrip))
                {
                    Control Source = ((ContextMenuStrip)owner).SourceControl;
                    if (Source != null && Source.GetType() == typeof(Button))
                        return (Button)Source;
                }
            }
            return null;
        }
        private Function GetFunctionFromButton(Button currentButton)
        {
            if (currentButton.Tag.GetType() == typeof(Function))
                return (Function)currentButton.Tag;
            return null;
        }
        private void SetLanguageForContextStrip(ContextMenuStrip CMS)
        {
            for (int i = 0; i < CMS.Items.Count; i++)
            {
                ToolStripItem CurrentTSI = CMS.Items[i];
                CurrentTSI.Text = CentralLanguage.LanguageManager.GetText(CurrentTSI.Name);
            }
        }
    }
}
