using Helper.GUI;
using ModularToolManger.Core;
using PluginInterface;
using PluginManager;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using ToolMangerInterface;
using JSONSettings;
using ModularToolManger.Core.Modules;
using System.Text.RegularExpressions;

namespace ModularToolManger.Forms
{
    public partial class F_ToolManager : Form
    {
        private Manager _pluginManager;
        private FunctionsManager _functionManager;
        private int _startOffset = 25;
        private int _baseScrollValue;
        private int _maxHeight;
        private int _minWidth;
        private Point _location;
        private bool _forceClose;
        private bool _hidden;
        private bool _searchbarAdded;

        private int _newValue;

        private string _functionsPath;

        //private int _lastContextListButton;
        private Settings _settingsContainer;

        private LanguageCom _languageConnector;
        private LoggerBridge _loggingBridge;

        public F_ToolManager()
        {
            InitializeComponent();
            _hidden = false;
            _forceClose = false;
            _searchbarAdded = false;
            KeyPreview = true;

            //_lastContextListButton = 0;

            Default_Show.Visible = _hidden;

            _minWidth = this.Size.Width;
            _maxHeight = Screen.FromControl(this).Bounds.Height / 4;
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MinimizeBox = false;

            F_ToolManager_NI_Taskliste.ContextMenuStrip = F_ToolManager_TasklisteContext;

            SetupPluginManager();

            File.Delete(CentralLogging.AppDebugLogger.LogFile);
            CentralLogging.AppDebugLogger.WriteLine("Searching modules at: " + new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName + @"\Modules", Logging.LogLevel.Information);

            SetupPlugins();
        }

        private void SetupPluginManager()
        {
            _pluginManager = new Manager();
            SetupModules();
            _pluginManager.Messenger.Register(_languageConnector);
            _pluginManager.Messenger.Register(_loggingBridge);
        }

        private void SetupModules()
        {
            _languageConnector = new LanguageCom();
            _loggingBridge = new LoggerBridge();
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

            SetupSettingsFile();
            if (_settingsContainer.GetBoolValue("Borderless"))
            {
                FormBorderStyle = FormBorderStyle.None;
            }
            MouseWheel += F_ToolManager_MouseWheel;
            SetLanguage();
            SetupButtons();


            setScrollSpeed();
            F_ToolManager_Hide.Visible = _settingsContainer.GetBoolValue("Borderless");



        }
        private void setScrollSpeed()
        {
            _baseScrollValue = 3;
            _baseScrollValue = _settingsContainer.GetIntValue("ScrollSpeed");

            if (_baseScrollValue < F_ToolManager_ScrollBar.Minimum)
                _baseScrollValue = F_ToolManager_ScrollBar.Minimum;
            if (_baseScrollValue > F_ToolManager_ScrollBar.Maximum)
                _baseScrollValue = F_ToolManager_ScrollBar.Maximum;
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
        private void SetupSettingsFile()
        {
            _settingsContainer = new Settings(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\ToolManager\settings.json");
            _settingsContainer.AddNewField("ToolManager");
            SetupSettingsForPlugins();
        }
        private void SetupSettingsForPlugins()
        {
            for (int i = 0; i < _pluginManager.PluginCount; i++)
            {
                _settingsContainer.AddNewField(_pluginManager.LoadetPlugins[i].UniqueName);
                try
                {
                    IFunction function = (IFunction)_pluginManager.LoadetPlugins[i];

                    foreach (IPluginSetting setting in function.Settings.AllSettings)
                    {
                        SettingsType type = SettingsType.Error;
                        string curSettings = _settingsContainer.GetValue(function.UniqueName, setting.Key, out type);
                        switch (type)
                        {
                            case SettingsType.String:
                                function.Settings.UpdateValue(setting.Key, curSettings);
                                break;
                            case SettingsType.Bool:
                                bool Bvalue = false;
                                if (bool.TryParse(curSettings, out Bvalue))
                                    function.Settings.UpdateValue(setting.Key, Bvalue);
                                break;
                            case SettingsType.Int:
                                int Ivalue = 0;
                                if (int.TryParse(curSettings, out Ivalue))
                                    function.Settings.UpdateValue(setting.Key, Ivalue);
                                break;
                            case SettingsType.Float:
                                float Fvalue = 0;
                                if (float.TryParse(curSettings, out Fvalue))
                                    function.Settings.UpdateValue(setting.Key, Fvalue);
                                break;
                            case SettingsType.Error:
                                break;
                            default:
                                break;
                        }
                        //
                    }

                }
                catch (Exception)
                {
                }
            }
        }
        private void SetLanguage()
        {
            string Language = _settingsContainer.GetValue("Language");
            CentralLanguage.LanguageManager.SetLanguageByCountyCode(Language);
            this.SetupLanguage();
            F_ToolManager_NI_Taskliste.Text = CentralLanguage.LanguageManager.GetText(F_ToolManager_NI_Taskliste.Tag.ToString());
            SetLanguageForContextStrip(F_ToolManager_ButtonContext);
            SetLanguageForContextStrip(F_ToolManager_TasklisteContext);
        }
        private void SetupButtons(string filter = ".*")
        {
            this.DoForEveryControl(typeof(Button), DeleteButton);
            F_ToolManager_NI_Taskbar_Buttons.DropDownItems.Clear();
            F_ToolManager_NI_Taskbar_Buttons.Visible = false;
            F_ToolManager_ScrollBar.Visible = false;
            Button lastButton = null;
            int buttonsAdded = 0;

            if (_functionManager.Functions.Count > 0)
            {
                _functionManager.Functions.Sort((function1, function2) => (function1.sortingSequence.CompareTo(function2.sortingSequence)));
                for (int i = 0; i < _functionManager.Functions.Count; i++)
                {
                    Function currentFunction = _functionManager.Functions[i];
                    Regex regex = new Regex(filter, RegexOptions.IgnoreCase);
                    if (!regex.IsMatch(currentFunction.Name))
                    {
                        continue;
                    }
                    Button newButton = createButton(currentFunction);
                    newButton.Location = new Point(0, _startOffset + buttonsAdded * 25 + newButton.Size.Height);
                    this.Controls.Add(newButton);
                    lastButton = newButton;
                    buttonsAdded++;

                    if (currentFunction.ShowInNotification)
                        addNewToolStripMenuItem(currentFunction);
                    
                }

                if (F_ToolManager_NI_Taskbar_Buttons.DropDownItems.Count > 0)
                    F_ToolManager_NI_Taskbar_Buttons.Visible = true;
            }

            List<Control> buttons = this.GetAllControls(typeof(Button));
            if (buttons.Count == 0)
            {
                MoveToPosition();
                return;
            }
            CalculateFormSize(lastButton, buttons);

        }

        private void addNewToolStripMenuItem(Function currentFunction)
        {
            ToolStripMenuItem newItem = new ToolStripMenuItem(currentFunction.Name)
            {
                Name = currentFunction.ID,
                Text = currentFunction.Name,
                Tag = currentFunction,
            };
            newItem.Click += NewItem_Click;
            F_ToolManager_NI_Taskbar_Buttons.DropDownItems.Add(newItem);

        }
        private Button createButton(Function currentFunction)
        {
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
            newButton.SetWidthByTextLenght();

            return newButton;
        }
        private void CalculateFormSize(Button lastButton, List<Control> buttons)
        {
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
        }

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

        private void NewItem_Click(object sender, EventArgs e)
        {
            if (sender.GetType() == typeof(ToolStripMenuItem))
            {
                ToolStripMenuItem TSMI = (ToolStripMenuItem)sender;
                if (TSMI.Tag.GetType() == typeof(Function))
                {
                    Function func = (Function)TSMI.Tag;
                    IPlugin currentPlugin = _pluginManager.GetPluginByName(func.Type);
                    if (currentPlugin == null)
                        return;
                    if (currentPlugin.ContainsInterface(typeof(IFunction)))
                        func.PerformeAction((IFunction)currentPlugin);
                }
            }
        }
        private void F_ToolManager_Settings_Click(object sender, EventArgs e)
        {
            F_Settings settingsForm = new F_Settings(_settingsContainer, _pluginManager.LoadetPlugins);
            Hide();
            F_ToolManager_NI_Taskbar_Close.Enabled = false;
            settingsForm.ShowDialog();
            if (settingsForm.Save)
                _settingsContainer = settingsForm.Settings;
            Show();
            _settingsContainer.Save();

            if (_settingsContainer.GetBoolValue("KeepOnTop"))
                TopMost = true;
            else
                TopMost = false;

            ShowInTaskbar = (!_settingsContainer.GetBoolValue("HideInTaskbar"));
            F_ToolManager_NI_Taskbar_Close.Enabled = true;

            F_ToolManager_Hide.Visible = _settingsContainer.GetBoolValue("Borderless");
            if (_settingsContainer.GetBoolValue("Borderless"))
                FormBorderStyle = FormBorderStyle.None;
            else
                FormBorderStyle = FormBorderStyle.Fixed3D;

            setScrollSpeed();
        }
        private void F_ToolManager_Shown(object sender, EventArgs e)
        {
            if (_settingsContainer.GetBoolValue("StartMinimized"))
            {
                Hide();
                _hidden = true;
                Default_Show.Visible = _hidden;
            }
            ShowInTaskbar = (!_settingsContainer.GetBoolValue("HideInTaskbar"));
        }
        private void F_ToolManager_MouseWheel(object sender, MouseEventArgs e)
        {
            if (!F_ToolManager_ScrollBar.Visible)
                return;



            int Test = e.Delta < 0 ? _baseScrollValue : -_baseScrollValue;
            Test = F_ToolManager_ScrollBar.Value + Test;
            if (Test < 0)
                return;
            if (Test > F_ToolManager_ScrollBar.Maximum)
                return;

            _newValue = Test;
            this.DoForEveryControl(typeof(Button), OffsetButton);
            F_ToolManager_ScrollBar.Value = Test;
        }
        private void defaultCloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _forceClose = true;
            this.Close();
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

                    if (_searchbarAdded && _settingsContainer.GetBoolValue("DisableSearchByButton"))
                    {
                        List<Control> textBoxes = this.GetAllControls(typeof(TextBox));
                        foreach (Control curControl in textBoxes)
                        {
                            RemoveSearchbar((object)curControl);
                        }
                        
                    }
                }
            }
        }
        private void F_ToolManager_Langauge_Click(object sender, EventArgs e)
        {
            F_LanguageSelect LanguageSelector = new F_LanguageSelect(_settingsContainer);
            Hide();
            F_ToolManager_NI_Taskbar_Close.Enabled = false;
            LanguageSelector.ShowDialog();
            _settingsContainer = LanguageSelector.Settings;
            _settingsContainer.Save();
            Show();
            SetLanguage();
            SetupButtons();
            _languageConnector.LanguageChanged();
            F_ToolManager_NI_Taskbar_Close.Enabled = true;
        }
        private void F_ToolManager_NewFunction_Click(object sender, EventArgs e)
        {
            F_NewFunction NewFunction = new F_NewFunction(ref _pluginManager);
            Hide();
            F_ToolManager_NI_Taskbar_Close.Enabled = false;
            NewFunction.ShowDialog();
            if (NewFunction.NewFunction != null)
            {
                _functionManager.AddNewFunction(NewFunction.NewFunction);
                _functionManager.Save();

                SetupButtons();
            }
            Show();
            F_ToolManager_NI_Taskbar_Close.Enabled = true;
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

            _settingsContainer.Save();
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

                if (_settingsContainer.GetBoolValue("KeepOnTop"))
                    TopMost = true;
                else
                    TopMost = false;
            }
            else
            {
                TopMost = true;
                if (!_settingsContainer.GetBoolValue("KeepOnTop"))
                    TopMost = false;
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

        protected override void WndProc(ref Message message)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_MOVE = 0xF010;
        
            switch (message.Msg)
            {
                case WM_SYSCOMMAND:
                    int command = message.WParam.ToInt32() & 0xfff0;
                    if (command == SC_MOVE)
                        return;
                    break;
            }
            base.WndProc(ref message);
        }

        private void F_ToolManager_Hide_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private void F_ToolManager_ReportBug_Click(object sender, EventArgs e)
        {
            Hide();
            F_ToolManager_NI_Taskbar_Close.Enabled = false;
            F_ReportBug BugReporter = new F_ReportBug(_settingsContainer);
            BugReporter.Show();
            Show();
            F_ToolManager_NI_Taskbar_Close.Enabled = true;
        }

        private void F_ToolManager_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox searchBox;
            if (!_searchbarAdded)
            {
                searchBox = new TextBox();

                searchBox.Location = new Point(0, F_MainMenuStrip.Height);
                searchBox.Size = new Size(Width, 21);
                searchBox.Tag = "SearchBox";
                searchBox.Text = e.KeyChar.ToString();
                searchBox.TabIndex = 999;
                searchBox.KeyPress += SearchBox_KeyPress;

                Controls.Add(searchBox);
                _searchbarAdded = true;

                searchBox.Focus();
                int textLenght = searchBox.Text.Length;
                searchBox.Select(textLenght, textLenght);
                SetupButtons(buildRegex(searchBox.Text));
            }            
        }
        private TextBox getSearchboxBySender(object sender)
        {
            if (sender.GetType() == typeof(TextBox))
            {
                TextBox curTextBox = (TextBox)sender;
                if (curTextBox.Tag.ToString() == "SearchBox")
                {
                    return curTextBox;
                }
            }
            return null;
        }
        private string buildRegex(string value)
        {
            value = value.Replace("?", ".");
            value = value.Replace("*", ".*");
            return value;
        }
        private void SearchBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                RemoveSearchbar(sender);
                return;
            }
            TextBox curTextBox = getSearchboxBySender(sender);
            if (curTextBox == null)
            {
                return;
            }
            SetupButtons(buildRegex(curTextBox.Text));
        }

        private void RemoveSearchbar(object sender)
        {
            TextBox curTextBox = getSearchboxBySender(sender);
            if (curTextBox == null)
            {
                return;
            }
            Controls.Remove(curTextBox);
            SetupButtons();
            _searchbarAdded = false;
        }
    }
}
