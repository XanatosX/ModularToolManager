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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_ToolManager));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.F_ToolManager_File = new System.Windows.Forms.ToolStripMenuItem();
            this.F_ToolManager_Langauge = new System.Windows.Forms.ToolStripMenuItem();
            this.F_ToolManager_Settings = new System.Windows.Forms.ToolStripMenuItem();
            this.F_ToolManager_NewFunction = new System.Windows.Forms.ToolStripMenuItem();
            this.F_ToolManager_Hide = new System.Windows.Forms.ToolStripMenuItem();
            this.Default_Close = new System.Windows.Forms.ToolStripMenuItem();
            this.F_ToolManager_ReportBug = new System.Windows.Forms.ToolStripMenuItem();
            this.F_ToolManager_NI_Taskliste = new System.Windows.Forms.NotifyIcon(this.components);
            this.F_ToolManager_ScrollBar = new System.Windows.Forms.VScrollBar();
            this.F_ToolManager_ButtonContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.F_ToolManager_ButtonContext_Edit = new System.Windows.Forms.ToolStripMenuItem();
            this.F_ToolManager_ButtonContext_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.F_ToolManager_TasklisteContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Default_Show = new System.Windows.Forms.ToolStripMenuItem();
            this.F_ToolManager_NI_Taskbar_Buttons = new System.Windows.Forms.ToolStripMenuItem();
            this.F_ToolManager_NI_Taskbar_Close = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.F_ToolManager_ButtonContext.SuspendLayout();
            this.F_ToolManager_TasklisteContext.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.F_ToolManager_File,
            this.F_ToolManager_ReportBug});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(243, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // F_ToolManager_File
            // 
            this.F_ToolManager_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.F_ToolManager_Langauge,
            this.F_ToolManager_Settings,
            this.F_ToolManager_NewFunction,
            this.F_ToolManager_Hide,
            this.Default_Close});
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
            // F_ToolManager_Settings
            // 
            this.F_ToolManager_Settings.Name = "F_ToolManager_Settings";
            this.F_ToolManager_Settings.Size = new System.Drawing.Size(232, 22);
            this.F_ToolManager_Settings.Text = "F_ToolManager_Settings";
            this.F_ToolManager_Settings.Click += new System.EventHandler(this.F_ToolManager_Settings_Click);
            // 
            // F_ToolManager_NewFunction
            // 
            this.F_ToolManager_NewFunction.Name = "F_ToolManager_NewFunction";
            this.F_ToolManager_NewFunction.Size = new System.Drawing.Size(232, 22);
            this.F_ToolManager_NewFunction.Text = "F_ToolManager_NewFunction";
            this.F_ToolManager_NewFunction.Click += new System.EventHandler(this.F_ToolManager_NewFunction_Click);
            // 
            // F_ToolManager_Hide
            // 
            this.F_ToolManager_Hide.Name = "F_ToolManager_Hide";
            this.F_ToolManager_Hide.Size = new System.Drawing.Size(232, 22);
            this.F_ToolManager_Hide.Text = "F_ToolManager_Hide";
            this.F_ToolManager_Hide.Click += new System.EventHandler(this.F_ToolManager_Hide_Click);
            // 
            // Default_Close
            // 
            this.Default_Close.Name = "Default_Close";
            this.Default_Close.Size = new System.Drawing.Size(232, 22);
            this.Default_Close.Text = "Default_Close";
            this.Default_Close.Click += new System.EventHandler(this.defaultCloseToolStripMenuItem_Click);
            // 
            // F_ToolManager_ReportBug
            // 
            this.F_ToolManager_ReportBug.Name = "F_ToolManager_ReportBug";
            this.F_ToolManager_ReportBug.Size = new System.Drawing.Size(162, 20);
            this.F_ToolManager_ReportBug.Text = "F_ToolManager_ReportBug";
            this.F_ToolManager_ReportBug.Click += new System.EventHandler(this.F_ToolManager_ReportBug_Click);
            // 
            // F_ToolManager_NI_Taskliste
            // 
            this.F_ToolManager_NI_Taskliste.Icon = ((System.Drawing.Icon)(resources.GetObject("F_ToolManager_NI_Taskliste.Icon")));
            this.F_ToolManager_NI_Taskliste.Tag = "F_ToolManager_NI_Taskliste";
            this.F_ToolManager_NI_Taskliste.Text = "F_ToolManager_NI_Taskliste";
            this.F_ToolManager_NI_Taskliste.Visible = true;
            this.F_ToolManager_NI_Taskliste.Click += new System.EventHandler(this.F_ToolManager_NI_Taskliste_Click);
            // 
            // F_ToolManager_ScrollBar
            // 
            this.F_ToolManager_ScrollBar.Dock = System.Windows.Forms.DockStyle.Right;
            this.F_ToolManager_ScrollBar.Location = new System.Drawing.Point(226, 24);
            this.F_ToolManager_ScrollBar.Name = "F_ToolManager_ScrollBar";
            this.F_ToolManager_ScrollBar.Size = new System.Drawing.Size(17, 237);
            this.F_ToolManager_ScrollBar.TabIndex = 1;
            this.F_ToolManager_ScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.F_ToolManager_ScrollBar_Scroll);
            // 
            // F_ToolManager_ButtonContext
            // 
            this.F_ToolManager_ButtonContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.F_ToolManager_ButtonContext_Edit,
            this.F_ToolManager_ButtonContext_Delete});
            this.F_ToolManager_ButtonContext.Name = "F_ToolManager_ButtonContext";
            this.F_ToolManager_ButtonContext.Size = new System.Drawing.Size(277, 48);
            // 
            // F_ToolManager_ButtonContext_Edit
            // 
            this.F_ToolManager_ButtonContext_Edit.Name = "F_ToolManager_ButtonContext_Edit";
            this.F_ToolManager_ButtonContext_Edit.Size = new System.Drawing.Size(276, 22);
            this.F_ToolManager_ButtonContext_Edit.Text = "F_ToolManager_ButtonContext_Edit";
            this.F_ToolManager_ButtonContext_Edit.Click += new System.EventHandler(this.F_ToolManager_ButtonContext_Edit_Click);
            // 
            // F_ToolManager_ButtonContext_Delete
            // 
            this.F_ToolManager_ButtonContext_Delete.Name = "F_ToolManager_ButtonContext_Delete";
            this.F_ToolManager_ButtonContext_Delete.Size = new System.Drawing.Size(276, 22);
            this.F_ToolManager_ButtonContext_Delete.Text = "F_ToolManager_ButtonContext_Delete";
            this.F_ToolManager_ButtonContext_Delete.Click += new System.EventHandler(this.F_ToolManager_ButtonContext_Delete_Click);
            // 
            // F_ToolManager_TasklisteContext
            // 
            this.F_ToolManager_TasklisteContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Default_Show,
            this.F_ToolManager_NI_Taskbar_Buttons,
            this.F_ToolManager_NI_Taskbar_Close});
            this.F_ToolManager_TasklisteContext.Name = "F_ToolManager_TasklisteContext";
            this.F_ToolManager_TasklisteContext.Size = new System.Drawing.Size(266, 70);
            this.F_ToolManager_TasklisteContext.Text = "F_ToolManager_TasklisteContext";
            // 
            // Default_Show
            // 
            this.Default_Show.Name = "Default_Show";
            this.Default_Show.Size = new System.Drawing.Size(265, 22);
            this.Default_Show.Text = "Default_Show";
            this.Default_Show.Click += new System.EventHandler(this.defaultShowToolStripMenuItem_Click);
            // 
            // F_ToolManager_NI_Taskbar_Buttons
            // 
            this.F_ToolManager_NI_Taskbar_Buttons.Name = "F_ToolManager_NI_Taskbar_Buttons";
            this.F_ToolManager_NI_Taskbar_Buttons.Size = new System.Drawing.Size(265, 22);
            this.F_ToolManager_NI_Taskbar_Buttons.Text = "F_ToolManager_NI_Taskbar_Buttons";
            // 
            // F_ToolManager_NI_Taskbar_Close
            // 
            this.F_ToolManager_NI_Taskbar_Close.Name = "F_ToolManager_NI_Taskbar_Close";
            this.F_ToolManager_NI_Taskbar_Close.Size = new System.Drawing.Size(265, 22);
            this.F_ToolManager_NI_Taskbar_Close.Text = "F_ToolManager_NI_Taskbar_Close";
            this.F_ToolManager_NI_Taskbar_Close.Click += new System.EventHandler(this.F_ToolManager_NI_Taskliste_Close_Click);
            // 
            // F_ToolManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(243, 261);
            this.Controls.Add(this.F_ToolManager_ScrollBar);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "F_ToolManager";
            this.Text = "F_ToolManager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.F_ToolManager_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.F_ToolManager_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.F_ToolManager_ButtonContext.ResumeLayout(false);
            this.F_ToolManager_TasklisteContext.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem F_ToolManager_File;
        private System.Windows.Forms.NotifyIcon F_ToolManager_NI_Taskliste;
        private System.Windows.Forms.ToolStripMenuItem F_ToolManager_Langauge;
        private System.Windows.Forms.ToolStripMenuItem F_ToolManager_NewFunction;
        private System.Windows.Forms.VScrollBar F_ToolManager_ScrollBar;
        private System.Windows.Forms.ContextMenuStrip F_ToolManager_ButtonContext;
        private System.Windows.Forms.ToolStripMenuItem F_ToolManager_ButtonContext_Edit;
        private System.Windows.Forms.ToolStripMenuItem F_ToolManager_ButtonContext_Delete;
        private System.Windows.Forms.ToolStripMenuItem Default_Close;
        private System.Windows.Forms.ContextMenuStrip F_ToolManager_TasklisteContext;
        private System.Windows.Forms.ToolStripMenuItem F_ToolManager_NI_Taskbar_Close;
        private System.Windows.Forms.ToolStripMenuItem Default_Show;
        private System.Windows.Forms.ToolStripMenuItem F_ToolManager_Settings;
        private System.Windows.Forms.ToolStripMenuItem F_ToolManager_NI_Taskbar_Buttons;
        private System.Windows.Forms.ToolStripMenuItem F_ToolManager_Hide;
        private System.Windows.Forms.ToolStripMenuItem F_ToolManager_ReportBug;
    }
}

