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

        //private Dictionary<string, IFunction> _functions;

        public F_NewFunction(ref Manager pluginManager)
        {
            InitializeComponent();
            _pluginManager = pluginManager;
            _startPos = 0;
            _returnFunction = null;
        }

        private void F_NewFunction_Load(object sender, EventArgs e)
        {
            this.SetupLanguage();
            this.DoForEveryControl(F_NewFunction_TB_Name, ClearTextBox);
            MinimizeBox = false;
            MaximizeBox = false;
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

            F_NewFunction_CB_Type.SelectedIndexChanged += F_NewFunction_CB_Type_SelectedIndexChanged;

        }

        private void F_NewFunction_CB_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_pluginManager.PluginCount >= F_NewFunction_CB_Type.SelectedIndex)
                Default_Open.Tag = _pluginManager.LoadetPlugins[F_NewFunction_CB_Type.SelectedIndex];
        }

        private bool ClearTextBox(Control TB_current)
        {
            TB_current.Text = "";
            return true;
        }

        private void Default_Open_Click(object sender, EventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.Filter = SetupFilter(((IFunction)Default_Open.Tag).FileEndings);
            if (OFD.ShowDialog() == DialogResult.OK)
            {
                this.Tag = OFD.FileName;
            }
        }

        private string SetupFilter(Dictionary<string, string> extensions)
        {
            string ReturnString = String.Empty;
            foreach (string key in extensions.Keys)
            {
                ReturnString += String.Format("{0} (*{1})|*{1}|", key, extensions[key]);
            }
            ReturnString = ReturnString.Remove(ReturnString.Length - 1);

            return ReturnString;
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
                    Type = _pluginManager.LoadetPlugins[F_NewFunction_CB_Type.SelectedIndex].UniqueName,
                    FilePath = (string)Tag
                };
            }
            Close();           
        }

        private void Default_Abort_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
