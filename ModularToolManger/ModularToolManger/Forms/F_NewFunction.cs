using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Helper.GUI;
using ModularToolManger.Core;
using PluginManager;
using PluginInterface;
using ToolMangerInterface;
using System.IO;

namespace ModularToolManger.Forms
{
    public partial class F_NewFunction : Form
    {
        private Manager _pluginManager;
        private int _endPos;
        private int _startPos;

        private Function _returnFunction;
        public Function NewFunction
        {
            get
            {
                return _returnFunction;
            }
        }
        private bool _editMode;

        private bool _firstOpen;

        //private Dictionary<string, IFunction> _functions;

        public F_NewFunction(ref Manager pluginManager)
        {
            InitializeComponent();
            _pluginManager = pluginManager;
            _startPos = 0;
            _returnFunction = null;
            _editMode = false;
            _firstOpen = true;
        }
        public F_NewFunction(ref Manager pluginManager, Function _functionToEdit)
        {
            InitializeComponent();
            _pluginManager = pluginManager;
            _startPos = 0;
            _returnFunction = _functionToEdit;
            _editMode = true;
            _firstOpen = true;
        }
        private void F_NewFunction_Load(object sender, EventArgs e)
        {
            this.SetupLanguage();
            this.DoForEveryControl(F_NewFunction_TB_Name, ClearTextBox);
            MinimizeBox = false;
            MaximizeBox = false;
            TB_filePath.ReadOnly = true;
            Default_Open.Enabled = false;
            Default_OK.Enabled = false;
            FormBorderStyle = FormBorderStyle.Fixed3D;


            GUIHelper.SetWidthByTextLenght(Default_OK, Default_Abort);
            this.AlignMultipleControls(Default_OK, Default_Abort);

            F_NewFunction_CB_Type.Text = "";
            F_NewFunction_CB_Type.Items.Clear();



            for (int i = 0; i < _pluginManager.LoadetPlugins.Count; i++)
            {
                IPlugin currentIPlugin = _pluginManager.LoadetPlugins[i];
                if (currentIPlugin.ContainsInterface(typeof(IFunction)))
                {
                    F_NewFunction_CB_Type.Items.Add(currentIPlugin.DisplayName);
                }
            }
            if (F_NewFunction_CB_Type.Items.Count > 0)
            {
                Default_Open.Enabled = true;
                Default_OK.Enabled = true;
                F_NewFunction_CB_Type.SelectedIndex = 0;
                Default_Open.Tag = _pluginManager.LoadetPlugins[0];
            }

            List<Control> Labels = this.GetAllControls(typeof(Label));
            for (int i = 0; i < Labels.Count; i++)
            {
                Control currentLabel = Labels[i];
                if (currentLabel.Width > _startPos)
                    _startPos = currentLabel.Width;
            }
            _startPos += 25;

            _endPos = F_NewFunction_TB_Name.Location.X + F_NewFunction_TB_Name.Size.Width;
            F_NewFunction_TB_Name.Location = new Point(_startPos, F_NewFunction_TB_Name.Location.Y);
            F_NewFunction_TB_Name.Size = new Size(_endPos - F_NewFunction_TB_Name.Location.X, F_NewFunction_TB_Name.Height);

            F_NewFunction_CB_Type.Location = new Point(_startPos, F_NewFunction_CB_Type.Location.Y);
            F_NewFunction_CB_Type.Size = new Size(_endPos - F_NewFunction_CB_Type.Location.X, F_NewFunction_CB_Type.Height);

            F_New_Function_CB_ShowInTaskList.Location = new Point(_startPos, F_New_Function_CB_ShowInTaskList.Location.Y);

            F_NewFunction_CB_Type.SelectedIndexChanged += F_NewFunction_CB_Type_SelectedIndexChanged;


            if (_editMode)
                FillFields();
        }

        private void FillFields()
        {
            F_NewFunction_TB_Name.Text = _returnFunction.Name;
            int _selectIndex = 0;

            if (_pluginManager.LoadetPlugins.Count < F_NewFunction_CB_Type.Items.Count)
                return;
            for (int i = 0; i < F_NewFunction_CB_Type.Items.Count; i++)
            {
                IPlugin plugin = _pluginManager.LoadetPlugins[i];
                object CBI = F_NewFunction_CB_Type.Items[i];
                if (plugin.UniqueName == _returnFunction.Type)
                {
                    _selectIndex = i;
                    Default_Open.Tag = plugin;
                    F_New_Function_CB_ShowInTaskList.Checked = _returnFunction.ShowInNotification;
                    TB_filePath.Text = _returnFunction.FilePath;
                    break;
                }
                    
            }
            F_NewFunction_CB_Type.SelectedIndex = _selectIndex;
            this.Tag = _returnFunction.FilePath;
        }
        private bool ClearTextBox(Control TB_current)
        {
            TB_current.Text = "";
            return true;
        }

        private string SetupFilter(Dictionary<string, string> extensions)
        {
            string ReturnString = String.Empty;
            HashSet<string> entries = new HashSet<string>();
            string LastEntry = CentralLanguage.LanguageManager.GetText("Default_All") + " ";
            foreach (string key in extensions.Keys)
            {
                ReturnString += String.Format("{0} (*{1})|*{1}|", key, extensions[key]);
                entries.Add(extensions[key]);
            }

            string front = String.Empty;
            string selections = " ";
            foreach (string type in entries)
            {
                front += $"*{type} ";
                selections += $"*{type}; ";
            }
            front = front.Remove(front.Length - 1);
            selections = selections.Remove(selections.Length - 1);
            LastEntry += $"({front})|{selections}";
            return ReturnString + LastEntry;
        }

        private void F_NewFunction_CB_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_editMode && !_firstOpen)
                TB_filePath.Text = string.Empty;

            if (_pluginManager.PluginCount >= F_NewFunction_CB_Type.SelectedIndex)
                Default_Open.Tag = _pluginManager.LoadetPlugins[F_NewFunction_CB_Type.SelectedIndex];

            _firstOpen = false;
        }
        private void Default_Open_Click(object sender, EventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();
            if (Default_Open.Tag == null)
                return;
            OFD.Filter = SetupFilter(((IFunction)Default_Open.Tag).FileEndings);
            if (_returnFunction != null && _returnFunction.FilePath != string.Empty)
            {
                FileInfo currentFile = new FileInfo(_returnFunction.FilePath);
                if (!OFD.Filter.Contains(currentFile.Extension))
                    return;
                OFD.InitialDirectory = currentFile.DirectoryName;
                OFD.FileName = currentFile.Name;
                string[] split = OFD.Filter.Split('|');
                for (int i = 0; i < split.Length; i += 2)
                {
                    if (split[i].Contains(currentFile.Extension) || split[i + 1].Contains(currentFile.Extension))
                    {
                        OFD.FilterIndex = i;

                        break;
                    }
                }
            }
            if (OFD.ShowDialog() == DialogResult.OK)
            {
                this.Tag = OFD.FileName;
                TB_filePath.Text = OFD.FileName;
            }
        }
        private void Default_OK_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(F_NewFunction_TB_Name.Text))
                return;
            if (String.IsNullOrEmpty(F_NewFunction_CB_Type.Text))
                return;
            if (Tag != null && Tag.GetType() == typeof(string))
            {
                _returnFunction = new Function()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = F_NewFunction_TB_Name.Text,
                    ShowInNotification = F_New_Function_CB_ShowInTaskList.Checked,
                    Type = _pluginManager.LoadetPlugins[F_NewFunction_CB_Type.SelectedIndex].UniqueName,
                    FilePath = (string)Tag
                };
            }
            Close();
        }
        private void Default_Abort_Click(object sender, EventArgs e)
        {
            _returnFunction = null;
            this.Close();
        }
    }
}
