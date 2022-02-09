namespace ModularToolManger.Forms
{
    partial class F_Password
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
            this.F_Password_L_Password = new System.Windows.Forms.Label();
            this.F_Password_TB_Password = new System.Windows.Forms.TextBox();
            this.Default_OK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // F_Password_L_Password
            // 
            this.F_Password_L_Password.AutoSize = true;
            this.F_Password_L_Password.Location = new System.Drawing.Point(13, 13);
            this.F_Password_L_Password.Name = "F_Password_L_Password";
            this.F_Password_L_Password.Size = new System.Drawing.Size(129, 13);
            this.F_Password_L_Password.TabIndex = 0;
            this.F_Password_L_Password.Text = "F_Password_L_Password";
            // 
            // F_Password_TB_Password
            // 
            this.F_Password_TB_Password.Location = new System.Drawing.Point(148, 10);
            this.F_Password_TB_Password.Name = "F_Password_TB_Password";
            this.F_Password_TB_Password.PasswordChar = '*';
            this.F_Password_TB_Password.Size = new System.Drawing.Size(218, 20);
            this.F_Password_TB_Password.TabIndex = 1;
            this.F_Password_TB_Password.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.F_Password_TB_Password_KeyPress);
            // 
            // Default_OK
            // 
            this.Default_OK.Location = new System.Drawing.Point(131, 36);
            this.Default_OK.Name = "Default_OK";
            this.Default_OK.Size = new System.Drawing.Size(75, 23);
            this.Default_OK.TabIndex = 2;
            this.Default_OK.Text = "Default_OK";
            this.Default_OK.UseVisualStyleBackColor = true;
            this.Default_OK.Click += new System.EventHandler(this.Default_OK_Click);
            // 
            // F_Password
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 68);
            this.Controls.Add(this.Default_OK);
            this.Controls.Add(this.F_Password_TB_Password);
            this.Controls.Add(this.F_Password_L_Password);
            this.Name = "F_Password";
            this.Text = "F_Password";
            this.Load += new System.EventHandler(this.F_Password_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label F_Password_L_Password;
        private System.Windows.Forms.TextBox F_Password_TB_Password;
        private System.Windows.Forms.Button Default_OK;
    }
}