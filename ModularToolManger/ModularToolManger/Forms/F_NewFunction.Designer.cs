namespace ModularToolManger.Forms
{
    partial class F_NewFunction
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
            this.F_NewFunction_L_Name = new System.Windows.Forms.Label();
            this.F_NewFunction_TB_Name = new System.Windows.Forms.TextBox();
            this.Default_OK = new System.Windows.Forms.Button();
            this.Default_Abort = new System.Windows.Forms.Button();
            this.F_NewFunction_L_Type = new System.Windows.Forms.Label();
            this.F_NewFunction_CB_Type = new System.Windows.Forms.ComboBox();
            this.Default_Open = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // F_NewFunction_L_Name
            // 
            this.F_NewFunction_L_Name.AutoSize = true;
            this.F_NewFunction_L_Name.Location = new System.Drawing.Point(13, 13);
            this.F_NewFunction_L_Name.Name = "F_NewFunction_L_Name";
            this.F_NewFunction_L_Name.Size = new System.Drawing.Size(128, 13);
            this.F_NewFunction_L_Name.TabIndex = 0;
            this.F_NewFunction_L_Name.Text = "F_NewFunction_L_Name";
            // 
            // F_NewFunction_TB_Name
            // 
            this.F_NewFunction_TB_Name.Location = new System.Drawing.Point(147, 10);
            this.F_NewFunction_TB_Name.Name = "F_NewFunction_TB_Name";
            this.F_NewFunction_TB_Name.Size = new System.Drawing.Size(125, 20);
            this.F_NewFunction_TB_Name.TabIndex = 1;
            // 
            // Default_OK
            // 
            this.Default_OK.Location = new System.Drawing.Point(16, 93);
            this.Default_OK.Name = "Default_OK";
            this.Default_OK.Size = new System.Drawing.Size(75, 23);
            this.Default_OK.TabIndex = 2;
            this.Default_OK.Text = "Default_OK";
            this.Default_OK.UseVisualStyleBackColor = true;
            this.Default_OK.Click += new System.EventHandler(this.Default_OK_Click);
            // 
            // Default_Abort
            // 
            this.Default_Abort.Location = new System.Drawing.Point(197, 93);
            this.Default_Abort.Name = "Default_Abort";
            this.Default_Abort.Size = new System.Drawing.Size(75, 23);
            this.Default_Abort.TabIndex = 3;
            this.Default_Abort.Text = "Default_Abort";
            this.Default_Abort.UseVisualStyleBackColor = true;
            this.Default_Abort.Click += new System.EventHandler(this.Default_Abort_Click);
            // 
            // F_NewFunction_L_Type
            // 
            this.F_NewFunction_L_Type.AutoSize = true;
            this.F_NewFunction_L_Type.Location = new System.Drawing.Point(13, 40);
            this.F_NewFunction_L_Type.Name = "F_NewFunction_L_Type";
            this.F_NewFunction_L_Type.Size = new System.Drawing.Size(124, 13);
            this.F_NewFunction_L_Type.TabIndex = 4;
            this.F_NewFunction_L_Type.Text = "F_NewFunction_L_Type";
            // 
            // F_NewFunction_CB_Type
            // 
            this.F_NewFunction_CB_Type.FormattingEnabled = true;
            this.F_NewFunction_CB_Type.Location = new System.Drawing.Point(147, 37);
            this.F_NewFunction_CB_Type.Name = "F_NewFunction_CB_Type";
            this.F_NewFunction_CB_Type.Size = new System.Drawing.Size(125, 21);
            this.F_NewFunction_CB_Type.TabIndex = 5;
            // 
            // Default_Open
            // 
            this.Default_Open.Location = new System.Drawing.Point(197, 64);
            this.Default_Open.Name = "Default_Open";
            this.Default_Open.Size = new System.Drawing.Size(75, 23);
            this.Default_Open.TabIndex = 6;
            this.Default_Open.Text = "Default_Open";
            this.Default_Open.UseVisualStyleBackColor = true;
            this.Default_Open.Click += new System.EventHandler(this.Default_Open_Click);
            // 
            // F_NewFunction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 127);
            this.Controls.Add(this.Default_Open);
            this.Controls.Add(this.F_NewFunction_CB_Type);
            this.Controls.Add(this.F_NewFunction_L_Type);
            this.Controls.Add(this.Default_Abort);
            this.Controls.Add(this.F_NewFunction_TB_Name);
            this.Controls.Add(this.F_NewFunction_L_Name);
            this.Controls.Add(this.Default_OK);
            this.Name = "F_NewFunction";
            this.Text = "F_NewFunction";
            this.Load += new System.EventHandler(this.F_NewFunction_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label F_NewFunction_L_Name;
        private System.Windows.Forms.TextBox F_NewFunction_TB_Name;
        private System.Windows.Forms.Button Default_OK;
        private System.Windows.Forms.Button Default_Abort;
        private System.Windows.Forms.Label F_NewFunction_L_Type;
        private System.Windows.Forms.ComboBox F_NewFunction_CB_Type;
        private System.Windows.Forms.Button Default_Open;
    }
}