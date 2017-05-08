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
            this.Default_Send = new System.Windows.Forms.Button();
            this.Default_Abort = new System.Windows.Forms.Button();
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
            // F_ReportBug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 462);
            this.Controls.Add(this.Default_Abort);
            this.Controls.Add(this.Default_Send);
            this.Name = "F_ReportBug";
            this.Text = "F_ReportBug";
            this.Load += new System.EventHandler(this.F_ReportBug_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Default_Send;
        private System.Windows.Forms.Button Default_Abort;
    }
}