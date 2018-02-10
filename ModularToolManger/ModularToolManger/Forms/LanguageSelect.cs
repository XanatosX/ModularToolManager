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
using System.Windows.Forms;

namespace ModularToolManger.Forms
{
    public partial class F_LanguageSelect : Form
    {
        private int _endPos;
        private bool _settingUp;
        private string _oldLanguage;

        private Settings _settings;
        public Settings Settings => _settings;

        public F_LanguageSelect(Settings settings)
        {
            _settings = settings;
            InitializeComponent();
        }

        private void F_LanguageSelect_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            _endPos = C_Languages.Location.X + C_Languages.Width;
            _oldLanguage = CentralLanguage.LanguageManager.Name;
            F_LanguageSelect_Language.AutoSize = true;
            _settingUp = false;
            SetupDesign();
        }
        private void SetupDesign()
        {
            _settingUp = true;
            this.SetupLanguage();
            GUIHelper.SetWidthByTextLenght(Default_OK, Default_Abort);
            this.AlignMultipleControls(Default_OK, Default_Abort);

            C_Languages.Location = new Point(F_LanguageSelect_Language.Location.X + F_LanguageSelect_Language.Width + 10, C_Languages.Location.Y);
            C_Languages.Size = new Size(_endPos - C_Languages.Location.X, C_Languages.Height);
            C_Languages.Text = "";
            C_Languages.Items.Clear();

            foreach (string item in CentralLanguage.LanguageManager.AvailableLanguages)
            {
                C_Languages.Items.Add(item);
            }

            for (int i = 0; i < C_Languages.Items.Count; i++)
            {
                string CurrentItem = C_Languages.Items[i].ToString();
                if (CurrentItem == CentralLanguage.LanguageManager.Name)
                {
                    C_Languages.SelectedIndex = i;
                    break;
                }
            }
            _settingUp = false;
        }

        private void C_Languages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_settingUp)
            {
                return;
            }
            if (CentralLanguage.LanguageManager.SetLanguageByName(C_Languages.SelectedItem.ToString()))
            {
                SetupDesign();
            }
        }
        private void Default_Abort_Click(object sender, EventArgs e)
        {
            CentralLanguage.LanguageManager.SetLanguageByName(_oldLanguage);
            this.Close();
        }
        private void Default_OK_Click(object sender, EventArgs e)
        {
            _settings.AddOrChangeKeyValue("Language", CentralLanguage.LanguageManager.CountryCode);
            this.Close();
        }
    }
}
