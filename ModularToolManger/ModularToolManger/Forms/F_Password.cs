using Helper.GUI;
using ModularToolManger.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModularToolManger.Forms
{
    public partial class F_Password : Form
    {
        private int _biggestLabel;
        private int _textFieldEndPos;

        private string _password;
        public string Password => _password;

        public F_Password()
        {
            _biggestLabel = 0;
            InitializeComponent();
        }

        private void F_Password_Load(object sender, EventArgs e)
        {
            SetLanguage();
            F_Password_TB_Password.Focus();

            this.DoForEveryControl(typeof(Label), getBiggestLabel);
            SetupTextFields();

            Default_OK.Center(this);
        }

        private void SetupTextFields()
        {
            _textFieldEndPos = F_Password_TB_Password.Location.X + F_Password_TB_Password.Size.Width;

            this.DoForEveryControl(typeof(TextBox), setTextFieldSize);
        }

        private bool setTextFieldSize(Control B)
        {
            B.Location = new Point(_biggestLabel, B.Location.Y);
            B.Size = new Size(_textFieldEndPos - _biggestLabel, B.Size.Height);

            return true;
        }

        private bool getBiggestLabel(Control B)
        {
            if (B.Location.X + B.Size.Width > _biggestLabel)
                _biggestLabel = B.Location.X + B.Size.Width;
            return true;
        }

        private void SetLanguage()
        {
            this.SetupLanguage();
            F_Password_TB_Password.Text = "";
        }

        private void Default_OK_Click(object sender, EventArgs e)
        {
            _password = F_Password_TB_Password.Text;
            this.Close();
        }

        private void F_Password_TB_Password_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
                Default_OK.PerformClick();
        }
    }
}
