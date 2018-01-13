using Helper.GUI;
using JSONSettings;
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
    public partial class F_OAuth : Form
    {
        private Settings _settings;
        private int _biggestLabel;
        private int _textFieldEndPos;

        private bool _g2g;
        public bool GoodToGO => _g2g;

        public F_OAuth(Settings settings)
        {
            _settings = settings;
            _g2g = false;
            _biggestLabel = 0;

            InitializeComponent();
        }

        private void Default_Abort_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Default_OK_Click(object sender, EventArgs e)
        {
            if (F_OAuth_TB_Password.Text != F_OAuth_TB_Password2.Text)
            {
                return;
            }

            if (F_OAuth_L_Password.Text == String.Empty || F_OAuth_TB_Password2.Text == String.Empty)
            {
                return;
            }



            PasswordManager pwManager = new PasswordManager();
            PasswordHasher hasher = new PasswordHasher();

            string password = hasher.GetHashedPassword(F_OAuth_TB_Password.Text);
            if (!hasher.CheckPassword(F_OAuth_TB_Password.Text, password))
            {
                return;
            }

            _settings.AddOrChangeKeyValue("OAuthPassword", password);
            _settings.AddOrChangeKeyValue("OauthKey", pwManager.EncryptPassword(F_OAuth_TB_Key.Text, F_OAuth_TB_Password.Text));
            _settings.AddOrChangeKeyValue("OAuthSecret", pwManager.EncryptPassword(F_OAuth_TB_Secret.Text, F_OAuth_TB_Password.Text));
            _settings.Save();
            _g2g = true;
            this.Close();
        }

        private bool SizeButtons(Control C)
        {
            C.SetWidthByTextLenght();
            return true;
        }

        private void F_OAuth_Load(object sender, EventArgs e)
        {
            SetLanguage();
            this.DoForEveryControl(typeof(Label), getBiggestLabel);
            SetupTextFields();

            this.DoForEveryControl(typeof(Button), SizeButtons);
            this.AlignMultipleControls(Default_OK, Default_Abort);
        }

        private void SetupTextFields()
        {
            _textFieldEndPos = F_OAuth_TB_Key.Location.X + F_OAuth_TB_Key.Size.Width;

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
            F_OAuth_TB_Key.Text = "";
            F_OAuth_TB_Secret.Text = "";
            F_OAuth_TB_Password.Text = "";
            F_OAuth_TB_Password2.Text = "";
        }
    }
}
