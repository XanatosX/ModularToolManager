namespace ModularToolManger.Forms
{
    partial class F_ReportBug
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
            this.components = new System.ComponentModel.Container();
            this.Default_Send = new System.Windows.Forms.Button();
            this.Default_Abort = new System.Windows.Forms.Button();
            this.F_ReportBug_L_Title = new System.Windows.Forms.Label();
            this.F_ReportBug_TB_Title = new System.Windows.Forms.TextBox();
            this.F_ReportBug_L_Content = new System.Windows.Forms.Label();
            this.F_ReportBug_TB_Content = new System.Windows.Forms.TextBox();
            this.F_ReportBug_LV_Files = new System.Windows.Forms.ListView();
            this.CH_Files = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CMS_LV_Files = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.F_ReportBug_LV_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.F_ReportBug_L_Kind = new System.Windows.Forms.Label();
            this.F_Report_Bug_CB_Kind = new System.Windows.Forms.ComboBox();
            this.F_Report_Bug_CB_Priority = new System.Windows.Forms.ComboBox();
            this.F_ReportBug_L_Priority = new System.Windows.Forms.Label();
            this.CMS_LV_Files.SuspendLayout();
            this.SuspendLayout();
            // 
            // Default_Send
            // 
            this.Default_Send.Location = new System.Drawing.Point(12, 427);
            this.Default_Send.Name = "Default_Send";
            this.Default_Send.Size = new System.Drawing.Size(75, 23);
            this.Default_Send.TabIndex = 0;
            this.Default_Send.Text = "Default_Send";
            this.Default_Send.UseVisualStyleBackColor = true;
            this.Default_Send.Click += new System.EventHandler(this.Default_Send_Click);
            // 
            // Default_Abort
            // 
            this.Default_Abort.Location = new System.Drawing.Point(415, 427);
            this.Default_Abort.Name = "Default_Abort";
            this.Default_Abort.Size = new System.Drawing.Size(75, 23);
            this.Default_Abort.TabIndex = 1;
            this.Default_Abort.Text = "Default_Abort";
            this.Default_Abort.UseVisualStyleBackColor = true;
            this.Default_Abort.Click += new System.EventHandler(this.Default_Abort_Click);
            // 
            // F_ReportBug_L_Title
            // 
            this.F_ReportBug_L_Title.AutoSize = true;
            this.F_ReportBug_L_Title.Location = new System.Drawing.Point(13, 13);
            this.F_ReportBug_L_Title.Name = "F_ReportBug_L_Title";
            this.F_ReportBug_L_Title.Size = new System.Drawing.Size(108, 13);
            this.F_ReportBug_L_Title.TabIndex = 2;
            this.F_ReportBug_L_Title.Text = "F_ReportBug_L_Title";
            // 
            // F_ReportBug_TB_Title
            // 
            this.F_ReportBug_TB_Title.Location = new System.Drawing.Point(127, 10);
            this.F_ReportBug_TB_Title.Name = "F_ReportBug_TB_Title";
            this.F_ReportBug_TB_Title.Size = new System.Drawing.Size(363, 20);
            this.F_ReportBug_TB_Title.TabIndex = 3;
            // 
            // F_ReportBug_L_Content
            // 
            this.F_ReportBug_L_Content.AutoSize = true;
            this.F_ReportBug_L_Content.Location = new System.Drawing.Point(13, 40);
            this.F_ReportBug_L_Content.Name = "F_ReportBug_L_Content";
            this.F_ReportBug_L_Content.Size = new System.Drawing.Size(125, 13);
            this.F_ReportBug_L_Content.TabIndex = 4;
            this.F_ReportBug_L_Content.Text = "F_ReportBug_L_Content";
            // 
            // F_ReportBug_TB_Content
            // 
            this.F_ReportBug_TB_Content.Location = new System.Drawing.Point(127, 37);
            this.F_ReportBug_TB_Content.Multiline = true;
            this.F_ReportBug_TB_Content.Name = "F_ReportBug_TB_Content";
            this.F_ReportBug_TB_Content.Size = new System.Drawing.Size(363, 241);
            this.F_ReportBug_TB_Content.TabIndex = 5;
            // 
            // F_ReportBug_LV_Files
            // 
            this.F_ReportBug_LV_Files.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.CH_Files});
            this.F_ReportBug_LV_Files.ContextMenuStrip = this.CMS_LV_Files;
            this.F_ReportBug_LV_Files.Location = new System.Drawing.Point(127, 338);
            this.F_ReportBug_LV_Files.MultiSelect = false;
            this.F_ReportBug_LV_Files.Name = "F_ReportBug_LV_Files";
            this.F_ReportBug_LV_Files.Size = new System.Drawing.Size(363, 78);
            this.F_ReportBug_LV_Files.TabIndex = 6;
            this.F_ReportBug_LV_Files.UseCompatibleStateImageBehavior = false;
            this.F_ReportBug_LV_Files.View = System.Windows.Forms.View.Details;
            // 
            // CMS_LV_Files
            // 
            this.CMS_LV_Files.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.F_ReportBug_LV_Delete});
            this.CMS_LV_Files.Name = "CMS_LV_Files";
            this.CMS_LV_Files.Size = new System.Drawing.Size(198, 26);
            this.CMS_LV_Files.Opening += new System.ComponentModel.CancelEventHandler(this.CMS_LV_Files_Opening);
            // 
            // F_ReportBug_LV_Delete
            // 
            this.F_ReportBug_LV_Delete.Name = "F_ReportBug_LV_Delete";
            this.F_ReportBug_LV_Delete.Size = new System.Drawing.Size(197, 22);
            this.F_ReportBug_LV_Delete.Text = "F_ReportBug_LV_Delete";
            this.F_ReportBug_LV_Delete.Click += new System.EventHandler(this.F_ReportBug_LV_Delete_Click);
            // 
            // F_ReportBug_L_Kind
            // 
            this.F_ReportBug_L_Kind.AutoSize = true;
            this.F_ReportBug_L_Kind.Location = new System.Drawing.Point(12, 287);
            this.F_ReportBug_L_Kind.Name = "F_ReportBug_L_Kind";
            this.F_ReportBug_L_Kind.Size = new System.Drawing.Size(109, 13);
            this.F_ReportBug_L_Kind.TabIndex = 7;
            this.F_ReportBug_L_Kind.Text = "F_ReportBug_L_Kind";
            // 
            // F_Report_Bug_CB_Kind
            // 
            this.F_Report_Bug_CB_Kind.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.F_Report_Bug_CB_Kind.FormattingEnabled = true;
            this.F_Report_Bug_CB_Kind.Location = new System.Drawing.Point(127, 284);
            this.F_Report_Bug_CB_Kind.Name = "F_Report_Bug_CB_Kind";
            this.F_Report_Bug_CB_Kind.Size = new System.Drawing.Size(363, 21);
            this.F_Report_Bug_CB_Kind.TabIndex = 8;
            this.F_Report_Bug_CB_Kind.SelectedIndexChanged += new System.EventHandler(this.F_Report_Bug_CB_Kind_SelectedIndexChanged);
            // 
            // F_Report_Bug_CB_Priority
            // 
            this.F_Report_Bug_CB_Priority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.F_Report_Bug_CB_Priority.FormattingEnabled = true;
            this.F_Report_Bug_CB_Priority.Location = new System.Drawing.Point(127, 311);
            this.F_Report_Bug_CB_Priority.Name = "F_Report_Bug_CB_Priority";
            this.F_Report_Bug_CB_Priority.Size = new System.Drawing.Size(363, 21);
            this.F_Report_Bug_CB_Priority.TabIndex = 9;
            this.F_Report_Bug_CB_Priority.SelectedIndexChanged += new System.EventHandler(this.F_Report_Bug_CB_Priority_SelectedIndexChanged);
            // 
            // F_ReportBug_L_Priority
            // 
            this.F_ReportBug_L_Priority.AutoSize = true;
            this.F_ReportBug_L_Priority.Location = new System.Drawing.Point(13, 314);
            this.F_ReportBug_L_Priority.Name = "F_ReportBug_L_Priority";
            this.F_ReportBug_L_Priority.Size = new System.Drawing.Size(119, 13);
            this.F_ReportBug_L_Priority.TabIndex = 10;
            this.F_ReportBug_L_Priority.Text = "F_ReportBug_L_Priority";
            // 
            // F_ReportBug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 462);
            this.Controls.Add(this.F_ReportBug_L_Priority);
            this.Controls.Add(this.F_Report_Bug_CB_Priority);
            this.Controls.Add(this.F_Report_Bug_CB_Kind);
            this.Controls.Add(this.F_ReportBug_L_Kind);
            this.Controls.Add(this.F_ReportBug_LV_Files);
            this.Controls.Add(this.F_ReportBug_TB_Content);
            this.Controls.Add(this.F_ReportBug_L_Content);
            this.Controls.Add(this.F_ReportBug_TB_Title);
            this.Controls.Add(this.F_ReportBug_L_Title);
            this.Controls.Add(this.Default_Abort);
            this.Controls.Add(this.Default_Send);
            this.Name = "F_ReportBug";
            this.Text = "F_ReportBug";
            this.Load += new System.EventHandler(this.F_ReportBug_Load);
            this.CMS_LV_Files.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Default_Send;
        private System.Windows.Forms.Button Default_Abort;
        private System.Windows.Forms.Label F_ReportBug_L_Title;
        private System.Windows.Forms.TextBox F_ReportBug_TB_Title;
        private System.Windows.Forms.Label F_ReportBug_L_Content;
        private System.Windows.Forms.TextBox F_ReportBug_TB_Content;
        private System.Windows.Forms.ListView F_ReportBug_LV_Files;
        private System.Windows.Forms.Label F_ReportBug_L_Kind;
        private System.Windows.Forms.ComboBox F_Report_Bug_CB_Kind;
        private System.Windows.Forms.ComboBox F_Report_Bug_CB_Priority;
        private System.Windows.Forms.Label F_ReportBug_L_Priority;
        private System.Windows.Forms.ColumnHeader CH_Files;
        private System.Windows.Forms.ContextMenuStrip CMS_LV_Files;
        private System.Windows.Forms.ToolStripMenuItem F_ReportBug_LV_Delete;
    }
}