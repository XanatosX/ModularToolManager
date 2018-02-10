namespace ModularToolManger.Forms
{
    partial class F_Settings
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.TP_Common = new System.Windows.Forms.TabPage();
            this.F_Settings_L_ScrollSpeed = new System.Windows.Forms.Label();
            this.F_Settings_TB_ScrollSpeed = new System.Windows.Forms.TrackBar();
            this.F_Settings_CB_BorderLess = new System.Windows.Forms.CheckBox();
            this.F_Settings_CB_AutoStart = new System.Windows.Forms.CheckBox();
            this.F_Settings_CB_HideInTaskbar = new System.Windows.Forms.CheckBox();
            this.F_Settings_CB_StartMinimized = new System.Windows.Forms.CheckBox();
            this.F_Settings_CB_KeepOnTop = new System.Windows.Forms.CheckBox();
            this.Default_OK = new System.Windows.Forms.Button();
            this.Default_Abort = new System.Windows.Forms.Button();
            this.B_OAuth = new System.Windows.Forms.Button();
            this.F_Settings_CB_DisableSearchByButtonClick = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.TP_Common.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.F_Settings_TB_ScrollSpeed)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.TP_Common);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(363, 329);
            this.tabControl1.TabIndex = 0;
            // 
            // TP_Common
            // 
            this.TP_Common.Controls.Add(this.F_Settings_CB_DisableSearchByButtonClick);
            this.TP_Common.Controls.Add(this.F_Settings_L_ScrollSpeed);
            this.TP_Common.Controls.Add(this.F_Settings_TB_ScrollSpeed);
            this.TP_Common.Controls.Add(this.F_Settings_CB_BorderLess);
            this.TP_Common.Controls.Add(this.F_Settings_CB_AutoStart);
            this.TP_Common.Controls.Add(this.F_Settings_CB_HideInTaskbar);
            this.TP_Common.Controls.Add(this.F_Settings_CB_StartMinimized);
            this.TP_Common.Controls.Add(this.F_Settings_CB_KeepOnTop);
            this.TP_Common.Location = new System.Drawing.Point(4, 22);
            this.TP_Common.Name = "TP_Common";
            this.TP_Common.Padding = new System.Windows.Forms.Padding(3);
            this.TP_Common.Size = new System.Drawing.Size(355, 303);
            this.TP_Common.TabIndex = 0;
            this.TP_Common.Tag = "";
            this.TP_Common.Text = "TP_Common";
            this.TP_Common.UseVisualStyleBackColor = true;
            // 
            // F_Settings_L_ScrollSpeed
            // 
            this.F_Settings_L_ScrollSpeed.AutoSize = true;
            this.F_Settings_L_ScrollSpeed.Location = new System.Drawing.Point(5, 141);
            this.F_Settings_L_ScrollSpeed.Name = "F_Settings_L_ScrollSpeed";
            this.F_Settings_L_ScrollSpeed.Size = new System.Drawing.Size(132, 13);
            this.F_Settings_L_ScrollSpeed.TabIndex = 6;
            this.F_Settings_L_ScrollSpeed.Text = "F_Settings_L_ScrollSpeed";
            // 
            // F_Settings_TB_ScrollSpeed
            // 
            this.F_Settings_TB_ScrollSpeed.Location = new System.Drawing.Point(8, 157);
            this.F_Settings_TB_ScrollSpeed.Name = "F_Settings_TB_ScrollSpeed";
            this.F_Settings_TB_ScrollSpeed.Size = new System.Drawing.Size(339, 45);
            this.F_Settings_TB_ScrollSpeed.TabIndex = 5;
            this.F_Settings_TB_ScrollSpeed.Tag = "ScrollSpeed";
            // 
            // F_Settings_CB_BorderLess
            // 
            this.F_Settings_CB_BorderLess.AutoSize = true;
            this.F_Settings_CB_BorderLess.Location = new System.Drawing.Point(8, 98);
            this.F_Settings_CB_BorderLess.Name = "F_Settings_CB_BorderLess";
            this.F_Settings_CB_BorderLess.Size = new System.Drawing.Size(155, 17);
            this.F_Settings_CB_BorderLess.TabIndex = 4;
            this.F_Settings_CB_BorderLess.Tag = "Borderless";
            this.F_Settings_CB_BorderLess.Text = "F_Settings_CB_BorderLess";
            this.F_Settings_CB_BorderLess.UseVisualStyleBackColor = true;
            // 
            // F_Settings_CB_AutoStart
            // 
            this.F_Settings_CB_AutoStart.AutoSize = true;
            this.F_Settings_CB_AutoStart.Location = new System.Drawing.Point(8, 75);
            this.F_Settings_CB_AutoStart.Name = "F_Settings_CB_AutoStart";
            this.F_Settings_CB_AutoStart.Size = new System.Drawing.Size(146, 17);
            this.F_Settings_CB_AutoStart.TabIndex = 3;
            this.F_Settings_CB_AutoStart.Tag = "AutoStart";
            this.F_Settings_CB_AutoStart.Text = "F_Settings_CB_AutoStart";
            this.F_Settings_CB_AutoStart.UseVisualStyleBackColor = true;
            this.F_Settings_CB_AutoStart.CheckedChanged += new System.EventHandler(this.F_Settings_CB_AutoStart_CheckedChanged);
            // 
            // F_Settings_CB_HideInTaskbar
            // 
            this.F_Settings_CB_HideInTaskbar.AutoSize = true;
            this.F_Settings_CB_HideInTaskbar.Location = new System.Drawing.Point(8, 52);
            this.F_Settings_CB_HideInTaskbar.Name = "F_Settings_CB_HideInTaskbar";
            this.F_Settings_CB_HideInTaskbar.Size = new System.Drawing.Size(172, 17);
            this.F_Settings_CB_HideInTaskbar.TabIndex = 2;
            this.F_Settings_CB_HideInTaskbar.Tag = "HideInTaskbar";
            this.F_Settings_CB_HideInTaskbar.Text = "F_Settings_CB_HideInTaskbar";
            this.F_Settings_CB_HideInTaskbar.UseVisualStyleBackColor = true;
            // 
            // F_Settings_CB_StartMinimized
            // 
            this.F_Settings_CB_StartMinimized.AutoSize = true;
            this.F_Settings_CB_StartMinimized.Location = new System.Drawing.Point(8, 29);
            this.F_Settings_CB_StartMinimized.Name = "F_Settings_CB_StartMinimized";
            this.F_Settings_CB_StartMinimized.Size = new System.Drawing.Size(170, 17);
            this.F_Settings_CB_StartMinimized.TabIndex = 1;
            this.F_Settings_CB_StartMinimized.Tag = "StartMinimized";
            this.F_Settings_CB_StartMinimized.Text = "F_Settings_CB_StartMinimized";
            this.F_Settings_CB_StartMinimized.UseVisualStyleBackColor = true;
            // 
            // F_Settings_CB_KeepOnTop
            // 
            this.F_Settings_CB_KeepOnTop.AutoSize = true;
            this.F_Settings_CB_KeepOnTop.Location = new System.Drawing.Point(8, 6);
            this.F_Settings_CB_KeepOnTop.Name = "F_Settings_CB_KeepOnTop";
            this.F_Settings_CB_KeepOnTop.Size = new System.Drawing.Size(160, 17);
            this.F_Settings_CB_KeepOnTop.TabIndex = 0;
            this.F_Settings_CB_KeepOnTop.Tag = "KeepOnTop";
            this.F_Settings_CB_KeepOnTop.Text = "F_Settings_CB_KeepOnTop";
            this.F_Settings_CB_KeepOnTop.UseVisualStyleBackColor = true;
            this.F_Settings_CB_KeepOnTop.CheckedChanged += new System.EventHandler(this.F_Settings_CB_KeepOnTop_CheckedChanged);
            // 
            // Default_OK
            // 
            this.Default_OK.Location = new System.Drawing.Point(12, 338);
            this.Default_OK.Name = "Default_OK";
            this.Default_OK.Size = new System.Drawing.Size(75, 23);
            this.Default_OK.TabIndex = 1;
            this.Default_OK.Text = "Default_OK";
            this.Default_OK.UseVisualStyleBackColor = true;
            this.Default_OK.Click += new System.EventHandler(this.Default_OK_Click);
            // 
            // Default_Abort
            // 
            this.Default_Abort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Default_Abort.Location = new System.Drawing.Point(276, 338);
            this.Default_Abort.Name = "Default_Abort";
            this.Default_Abort.Size = new System.Drawing.Size(75, 23);
            this.Default_Abort.TabIndex = 2;
            this.Default_Abort.Text = "Default_Abort";
            this.Default_Abort.UseVisualStyleBackColor = true;
            this.Default_Abort.Click += new System.EventHandler(this.Default_Abort_Click);
            // 
            // B_OAuth
            // 
            this.B_OAuth.Location = new System.Drawing.Point(137, 338);
            this.B_OAuth.Name = "B_OAuth";
            this.B_OAuth.Size = new System.Drawing.Size(75, 23);
            this.B_OAuth.TabIndex = 7;
            this.B_OAuth.Text = "B_OAuth";
            this.B_OAuth.UseVisualStyleBackColor = true;
            this.B_OAuth.Click += new System.EventHandler(this.B_OAuth_Click);
            // 
            // F_Settings_CB_DisableSearchByButtonClick
            // 
            this.F_Settings_CB_DisableSearchByButtonClick.AutoSize = true;
            this.F_Settings_CB_DisableSearchByButtonClick.Location = new System.Drawing.Point(8, 121);
            this.F_Settings_CB_DisableSearchByButtonClick.Name = "F_Settings_CB_DisableSearchByButtonClick";
            this.F_Settings_CB_DisableSearchByButtonClick.Size = new System.Drawing.Size(237, 17);
            this.F_Settings_CB_DisableSearchByButtonClick.TabIndex = 7;
            this.F_Settings_CB_DisableSearchByButtonClick.Tag = "DisableSearchByButton";
            this.F_Settings_CB_DisableSearchByButtonClick.Text = "F_Settings_CB_DisableSearchByButtonClick";
            this.F_Settings_CB_DisableSearchByButtonClick.UseVisualStyleBackColor = true;
            // 
            // F_Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(363, 373);
            this.Controls.Add(this.B_OAuth);
            this.Controls.Add(this.Default_Abort);
            this.Controls.Add(this.Default_OK);
            this.Controls.Add(this.tabControl1);
            this.Name = "F_Settings";
            this.Text = "F_Settings";
            this.Load += new System.EventHandler(this.F_Settings_Load);
            this.tabControl1.ResumeLayout(false);
            this.TP_Common.ResumeLayout(false);
            this.TP_Common.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.F_Settings_TB_ScrollSpeed)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage TP_Common;
        private System.Windows.Forms.CheckBox F_Settings_CB_KeepOnTop;
        private System.Windows.Forms.Button Default_OK;
        private System.Windows.Forms.CheckBox F_Settings_CB_StartMinimized;
        private System.Windows.Forms.CheckBox F_Settings_CB_HideInTaskbar;
        private System.Windows.Forms.CheckBox F_Settings_CB_AutoStart;
        private System.Windows.Forms.Button Default_Abort;
        private System.Windows.Forms.CheckBox F_Settings_CB_BorderLess;
        private System.Windows.Forms.Label F_Settings_L_ScrollSpeed;
        private System.Windows.Forms.TrackBar F_Settings_TB_ScrollSpeed;
        private System.Windows.Forms.Button B_OAuth;
        private System.Windows.Forms.CheckBox F_Settings_CB_DisableSearchByButtonClick;
    }
}