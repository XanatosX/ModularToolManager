using BitbucketAPI;
using Helper.GUI;
using JSONSettings;
using ModularToolManger.Core;
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
    //ToDo This need's to use the GitHub issue creation instead of the Bitbucket one
    public partial class F_ReportBug : Form
    {
        private int _biggestLabel;
        private int _textFieldEndPos;

        readonly Settings _settings;

        readonly HashSet<string> _allowedFiletypes;
        readonly int _maxFileSize;
        readonly int _allFileSize;

        private Kind _curKind;
        private Priority _curPriority;



        public F_ReportBug(Settings settings)
        {
            _settings = settings;
            _curKind = Kind.bug;
            _curPriority = Priority.trivial;
            InitializeComponent();
            _biggestLabel = 0;

            _allowedFiletypes = new HashSet<string>();
            SetupFiletypes();
            _maxFileSize = 1000;

            _maxFileSize *= 1000;

            _allFileSize = 3000;
            _allFileSize *= 1000;

        }

        private void SetupFiletypes()
        {
            _allowedFiletypes.Add(".txt");
            _allowedFiletypes.Add(".png");
            _allowedFiletypes.Add(".pdf");
            _allowedFiletypes.Add(".jpg");
            _allowedFiletypes.Add(".jpeg");
        }

        private void F_ReportBug_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.MinimizeBox = false;
            this.MaximizeBox = false;

            SetLanguage();
            SetupComboBoxes();
            this.DoForEveryControl(typeof(Label), GetBiggestLabel);
            SetupTextFields();

            this.DoForEveryControl(typeof(Button), SizeButtons);
            this.AlignMultipleControls(Default_Send, Default_Abort);


            F_ReportBug_LV_Files.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            CH_Files.Text = CentralLanguage.LanguageManager.GetText("F_ReportBug_L_LV_Files");

            SetupDragDrop();
        }

        private void SetLanguageForContextStrip(ContextMenuStrip CMS)
        {
            for (int i = 0; i < CMS.Items.Count; i++)
            {
                ToolStripItem CurrentTSI = CMS.Items[i];
                CurrentTSI.Text = CentralLanguage.LanguageManager.GetText(CurrentTSI.Name);
            }
        }

        private void SetupDragDrop()
        {
            F_ReportBug_LV_Files.AllowDrop = true;
            F_ReportBug_LV_Files.DragDrop += F_ReportBug_LV_Files_DragDrop;
            F_ReportBug_LV_Files.DragEnter += F_ReportBug_LV_Files_DragEnter;
        }

        private void F_ReportBug_LV_Files_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void F_ReportBug_LV_Files_DragDrop(object sender, DragEventArgs e)
        {
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            foreach (string s in FileList)
            {
                FileInfo fi = new FileInfo(s);
                if (fi.Length > _maxFileSize)
                {
                    continue;
                }
                if (!_allowedFiletypes.Contains(fi.Extension.ToLower()))
                {
                    continue;
                }
                if (GetCompleteFileSize() > _allFileSize)
                {
                    continue;
                }

                F_ReportBug_LV_Files.Items.Add(new ListViewItem
                {
                    Text = fi.Name,
                    Tag = fi,
                });
            }

        }

        private HashSet<FileInfo> GetFiles()
        {
            HashSet<FileInfo> files = new HashSet<FileInfo>();
            foreach (ListViewItem item in F_ReportBug_LV_Files.Items)
            {
                if (item.Tag == null || item.Tag.GetType() != typeof(FileInfo))
                {
                    continue;
                }
                FileInfo fi = (FileInfo)item.Tag;
                files.Add(fi);
            }
            return files;
        }

        private int GetCompleteFileSize()
        {
            int Return = 0;
            foreach (ListViewItem item in F_ReportBug_LV_Files.Items)
            {
                if (item.Tag == null || item.Tag.GetType() != typeof(FileInfo))
                {
                    continue;
                }
                FileInfo fi = (FileInfo)item.Tag;
                Return += (int)fi.Length;
            }
            return Return;
        }

        private bool SizeButtons(Control C)
        {
            C.SetWidthByTextLenght();
            return true;
        }

        private void SetLanguage()
        {
            this.SetupLanguage();
            SetLanguageForContextStrip(CMS_LV_Files);
            F_ReportBug_TB_Title.Text = "";
            F_ReportBug_TB_Content.Text = "";
            F_Report_Bug_CB_Kind.Text = "";
            F_Report_Bug_CB_Priority.Text = "";
        }

        private void SetupComboBoxes()
        {
            SetupKindComboBox();
            SetupPriorityComboBox();
        }
        private void SetupKindComboBox()
        {
            F_Report_Bug_CB_Kind.Items.Add(CentralLanguage.LanguageManager.GetText("F_ReportBug_L_bug"));
            F_Report_Bug_CB_Kind.Items.Add(CentralLanguage.LanguageManager.GetText("F_ReportBug_L_enhancement"));
            F_Report_Bug_CB_Kind.SelectedIndex = 0;
        }
        private void SetupPriorityComboBox()
        {
            F_Report_Bug_CB_Priority.Items.Add(CentralLanguage.LanguageManager.GetText("F_ReportBug_L_Priority_Trivial"));
            F_Report_Bug_CB_Priority.Items.Add(CentralLanguage.LanguageManager.GetText("F_ReportBug_L_Priority_Minor"));
            F_Report_Bug_CB_Priority.Items.Add(CentralLanguage.LanguageManager.GetText("F_ReportBug_L_Priority_Major"));
            F_Report_Bug_CB_Priority.Items.Add(CentralLanguage.LanguageManager.GetText("F_ReportBug_L_Priority_Critical"));

            F_Report_Bug_CB_Priority.SelectedIndex = 0;
        }

        private bool GetBiggestLabel(Control B)
        {
            if (B.Location.X + B.Size.Width > _biggestLabel)
            {
                _biggestLabel = B.Location.X + B.Size.Width;
            }
            return true;
        }

        private void SetupTextFields()
        {
            _textFieldEndPos = F_ReportBug_TB_Title.Location.X + F_ReportBug_TB_Title.Size.Width;

            this.DoForEveryControl(typeof(TextBox), SetTextFieldSize);
            this.DoForEveryControl(typeof(ListView), SetTextFieldSize);
            this.DoForEveryControl(typeof(ComboBox), SetTextFieldSize);
        }

        private bool SetTextFieldSize(Control B)
        {
            B.Location = new Point(_biggestLabel, B.Location.Y);
            B.Size = new Size(_textFieldEndPos - _biggestLabel, B.Size.Height);

            return true;
        }

        private void Default_Abort_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Default_Send_Click(object sender, EventArgs e)
        {
            if (F_ReportBug_TB_Title.Text == String.Empty || F_ReportBug_TB_Content.Text == String.Empty)
            {
                return;
            }

            PasswordHasher hasher = new PasswordHasher();

            string key = _settings.GetValue("OauthKey");
            string secret = _settings.GetValue("OAuthSecret");

            if (key == string.Empty || secret == string.Empty)
            {
                F_OAuth OAuthEntry = new F_OAuth(_settings);

                OAuthEntry.ShowDialog();
                if (!OAuthEntry.GoodToGO)
                {
                    return;
                } 
                key = _settings.GetValue("OauthKey");
                secret = _settings.GetValue("OAuthSecret");
            }

            F_Password passwordForm = new F_Password();
            passwordForm.ShowDialog();

            string realPassword = _settings.GetValue("OAuthPassword");

            if (!hasher.CheckPassword(passwordForm.Password, realPassword))
            {
                MessageBox.Show(CentralLanguage.LanguageManager.GetText("Message_Wrong_Password_Text"), CentralLanguage.LanguageManager.GetText("Message_Wrong_Password_Title"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            RepositoryData repository = new RepositoryData("XanatosX", "modulartoolmanager");

            PasswordManager pwManager = new PasswordManager();
            OAuth authentication = new OAuth(pwManager.DecryptPassword(key, passwordForm.Password), pwManager.DecryptPassword(secret, passwordForm.Password));
            if (authentication.ResponseData == null)
            {
                return;
            }
               
            
            Issue issue = new Issue(repository, authentication.ResponseData);
            HashSet<FileInfo> files = GetFiles();
            List<string> uploadFiles = new List<string>();
            foreach (FileInfo fi in files)
            {
                if (!File.Exists(fi.FullName))
                {
                    continue;
                }
                uploadFiles.Add(fi.FullName);
            }


            string UploadWindowTitle = CentralLanguage.LanguageManager.GetText("Message_Upload_Status_Title");
            if (issue.CreateIssue(new IssueCreateData(F_ReportBug_TB_Title.Text, F_ReportBug_TB_Content.Text, _curPriority, _curKind), uploadFiles.ToArray()))
            {
                MessageBox.Show(CentralLanguage.LanguageManager.GetText("Message_Upload_Status_Succeded"), UploadWindowTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                return;
            }

            MessageBox.Show(CentralLanguage.LanguageManager.GetText("Message_Upload_Status_Failed"), UploadWindowTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void F_Report_Bug_CB_Kind_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (F_Report_Bug_CB_Kind.SelectedIndex == 1)
            {
                F_Report_Bug_CB_Priority.SelectedIndex = 0;
                F_Report_Bug_CB_Priority.Enabled = false;
                _curKind = Kind.enhancement;
                _curPriority = Priority.trivial;
            }
            else
            {
                F_Report_Bug_CB_Priority.Enabled = true;
                _curKind = Kind.bug;
            }
        }

        private void F_Report_Bug_CB_Priority_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (F_Report_Bug_CB_Priority.SelectedIndex)
            {
                case 0:
                    _curPriority = Priority.trivial;
                    break;
                case 1:
                    _curPriority = Priority.minor;
                    break;
                case 2:
                    _curPriority = Priority.major;
                    break;
                case 3:
                    _curPriority = Priority.critical;
                    break;
                default:
                    break;
            }
        }

        private void CMS_LV_Files_Opening(object sender, CancelEventArgs e)
        {
            if (F_ReportBug_LV_Files.Items.Count == 0 || F_ReportBug_LV_Files.SelectedItems.Count == 0)
            {
                CMS_LV_Files.Visible = false;
                e.Cancel = true;
            }
        }

        private void F_ReportBug_LV_Delete_Click(object sender, EventArgs e)
        {
            F_ReportBug_LV_Files.Items.Remove(F_ReportBug_LV_Files.SelectedItems[0]);
        }
    }
}
