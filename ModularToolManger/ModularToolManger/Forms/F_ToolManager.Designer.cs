namespace ModularToolManger.Forms
{
    partial class F_ToolManager
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.F_ToolManager_File = new System.Windows.Forms.ToolStripMenuItem();
            this.F_ToolManager_Langauge = new System.Windows.Forms.ToolStripMenuItem();
            this.F_ToolManager_Close = new System.Windows.Forms.ToolStripMenuItem();
            this.F_ToolManager_NI_Taskliste = new System.Windows.Forms.NotifyIcon(this.components);
            this.F_ToolManager_NewFunction = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.F_ToolManager_File});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(284, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // F_ToolManager_File
            // 
            this.F_ToolManager_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.F_ToolManager_Langauge,
            this.F_ToolManager_NewFunction,
            this.F_ToolManager_Close});
            this.F_ToolManager_File.Name = "F_ToolManager_File";
            this.F_ToolManager_File.Size = new System.Drawing.Size(124, 20);
            this.F_ToolManager_File.Text = "F_ToolManager_File";
            // 
            // F_ToolManager_Langauge
            // 
            this.F_ToolManager_Langauge.Name = "F_ToolManager_Langauge";
            this.F_ToolManager_Langauge.Size = new System.Drawing.Size(232, 22);
            this.F_ToolManager_Langauge.Text = "F_ToolManager_Langauge";
            this.F_ToolManager_Langauge.Click += new System.EventHandler(this.F_ToolManager_Langauge_Click);
            // 
            // F_ToolManager_Close
            // 
            this.F_ToolManager_Close.Name = "F_ToolManager_Close";
            this.F_ToolManager_Close.Size = new System.Drawing.Size(232, 22);
            this.F_ToolManager_Close.Text = "F_ToolManager_Close";
            // 
            // F_ToolManager_NI_Taskliste
            // 
            this.F_ToolManager_NI_Taskliste.Text = "F_ToolManager_NI_Taskliste";
            this.F_ToolManager_NI_Taskliste.Visible = true;
            // 
            // F_ToolManager_NewFunction
            // 
            this.F_ToolManager_NewFunction.Name = "F_ToolManager_NewFunction";
            this.F_ToolManager_NewFunction.Size = new System.Drawing.Size(232, 22);
            this.F_ToolManager_NewFunction.Text = "F_ToolManager_NewFunction";
            this.F_ToolManager_NewFunction.Click += new System.EventHandler(this.F_ToolManager_NewFunction_Click);
            // 
            // F_ToolManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "F_ToolManager";
            this.Text = "F_ToolManager";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem F_ToolManager_File;
        private System.Windows.Forms.NotifyIcon F_ToolManager_NI_Taskliste;
        private System.Windows.Forms.ToolStripMenuItem F_ToolManager_Langauge;
        private System.Windows.Forms.ToolStripMenuItem F_ToolManager_Close;
        private System.Windows.Forms.ToolStripMenuItem F_ToolManager_NewFunction;
    }
}

