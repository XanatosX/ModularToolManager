namespace ModularToolManger.Forms
{
    partial class F_LanguageSelect
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.F_LanguageSelect_Language = new System.Windows.Forms.Label();
            this.Default_OK = new System.Windows.Forms.Button();
            this.Default_Abort = new System.Windows.Forms.Button();
            this.C_Languages = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // F_LanguageSelect_Language
            // 
            this.F_LanguageSelect_Language.AutoSize = true;
            this.F_LanguageSelect_Language.Location = new System.Drawing.Point(12, 9);
            this.F_LanguageSelect_Language.Name = "F_LanguageSelect_Language";
            this.F_LanguageSelect_Language.Size = new System.Drawing.Size(151, 13);
            this.F_LanguageSelect_Language.TabIndex = 0;
            this.F_LanguageSelect_Language.Text = "F_LanguageSelect_Language";
            // 
            // Default_OK
            // 
            this.Default_OK.Location = new System.Drawing.Point(13, 47);
            this.Default_OK.Name = "Default_OK";
            this.Default_OK.Size = new System.Drawing.Size(75, 23);
            this.Default_OK.TabIndex = 1;
            this.Default_OK.Text = "Default_OK";
            this.Default_OK.UseVisualStyleBackColor = true;
            this.Default_OK.Click += new System.EventHandler(this.Default_OK_Click);
            // 
            // Default_Abort
            // 
            this.Default_Abort.Location = new System.Drawing.Point(94, 47);
            this.Default_Abort.Name = "Default_Abort";
            this.Default_Abort.Size = new System.Drawing.Size(75, 23);
            this.Default_Abort.TabIndex = 2;
            this.Default_Abort.Text = "Default_Abort";
            this.Default_Abort.UseVisualStyleBackColor = true;
            this.Default_Abort.Click += new System.EventHandler(this.Default_Abort_Click);
            // 
            // C_Languages
            // 
            this.C_Languages.FormattingEnabled = true;
            this.C_Languages.Location = new System.Drawing.Point(169, 6);
            this.C_Languages.Name = "C_Languages";
            this.C_Languages.Size = new System.Drawing.Size(103, 21);
            this.C_Languages.TabIndex = 3;
            this.C_Languages.SelectedIndexChanged += new System.EventHandler(this.C_Languages_SelectedIndexChanged);
            // 
            // F_LanguageSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 82);
            this.Controls.Add(this.C_Languages);
            this.Controls.Add(this.Default_Abort);
            this.Controls.Add(this.Default_OK);
            this.Controls.Add(this.F_LanguageSelect_Language);
            this.Name = "F_LanguageSelect";
            this.Text = "F_LanguageSelect";
            this.Load += new System.EventHandler(this.F_LanguageSelect_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label F_LanguageSelect_Language;
        private System.Windows.Forms.Button Default_OK;
        private System.Windows.Forms.Button Default_Abort;
        private System.Windows.Forms.ComboBox C_Languages;
    }
}