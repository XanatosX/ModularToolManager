namespace ModularToolManger.Forms
{
    partial class F_OAuth
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
            this.F_OAuth_L_Key = new System.Windows.Forms.Label();
            this.F_OAuth_L_Secret = new System.Windows.Forms.Label();
            this.F_OAuth_TB_Key = new System.Windows.Forms.TextBox();
            this.F_OAuth_TB_Secret = new System.Windows.Forms.TextBox();
            this.Default_OK = new System.Windows.Forms.Button();
            this.Default_Abort = new System.Windows.Forms.Button();
            this.F_OAuth_L_Password = new System.Windows.Forms.Label();
            this.F_OAuth_L_Password2 = new System.Windows.Forms.Label();
            this.F_OAuth_TB_Password = new System.Windows.Forms.TextBox();
            this.F_OAuth_TB_Password2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // F_OAuth_L_Key
            // 
            this.F_OAuth_L_Key.AutoSize = true;
            this.F_OAuth_L_Key.Location = new System.Drawing.Point(13, 13);
            this.F_OAuth_L_Key.Name = "F_OAuth_L_Key";
            this.F_OAuth_L_Key.Size = new System.Drawing.Size(85, 13);
            this.F_OAuth_L_Key.TabIndex = 7;
            this.F_OAuth_L_Key.Text = "F_OAuth_L_Key";
            // 
            // F_OAuth_L_Secret
            // 
            this.F_OAuth_L_Secret.AutoSize = true;
            this.F_OAuth_L_Secret.Location = new System.Drawing.Point(13, 40);
            this.F_OAuth_L_Secret.Name = "F_OAuth_L_Secret";
            this.F_OAuth_L_Secret.Size = new System.Drawing.Size(98, 13);
            this.F_OAuth_L_Secret.TabIndex = 8;
            this.F_OAuth_L_Secret.Text = "F_OAuth_L_Secret";
            // 
            // F_OAuth_TB_Key
            // 
            this.F_OAuth_TB_Key.Location = new System.Drawing.Point(142, 10);
            this.F_OAuth_TB_Key.Name = "F_OAuth_TB_Key";
            this.F_OAuth_TB_Key.Size = new System.Drawing.Size(219, 20);
            this.F_OAuth_TB_Key.TabIndex = 1;
            // 
            // F_OAuth_TB_Secret
            // 
            this.F_OAuth_TB_Secret.Location = new System.Drawing.Point(142, 37);
            this.F_OAuth_TB_Secret.Name = "F_OAuth_TB_Secret";
            this.F_OAuth_TB_Secret.Size = new System.Drawing.Size(219, 20);
            this.F_OAuth_TB_Secret.TabIndex = 2;
            // 
            // Default_OK
            // 
            this.Default_OK.Location = new System.Drawing.Point(12, 127);
            this.Default_OK.Name = "Default_OK";
            this.Default_OK.Size = new System.Drawing.Size(75, 23);
            this.Default_OK.TabIndex = 5;
            this.Default_OK.Text = "Default_OK";
            this.Default_OK.UseVisualStyleBackColor = true;
            this.Default_OK.Click += new System.EventHandler(this.Default_OK_Click);
            // 
            // Default_Abort
            // 
            this.Default_Abort.Location = new System.Drawing.Point(286, 127);
            this.Default_Abort.Name = "Default_Abort";
            this.Default_Abort.Size = new System.Drawing.Size(75, 23);
            this.Default_Abort.TabIndex = 6;
            this.Default_Abort.Text = "Default_Abort";
            this.Default_Abort.UseVisualStyleBackColor = true;
            this.Default_Abort.Click += new System.EventHandler(this.Default_Abort_Click);
            // 
            // F_OAuth_L_Password
            // 
            this.F_OAuth_L_Password.AutoSize = true;
            this.F_OAuth_L_Password.Location = new System.Drawing.Point(13, 66);
            this.F_OAuth_L_Password.Name = "F_OAuth_L_Password";
            this.F_OAuth_L_Password.Size = new System.Drawing.Size(113, 13);
            this.F_OAuth_L_Password.TabIndex = 9;
            this.F_OAuth_L_Password.Text = "F_OAuth_L_Password";
            // 
            // F_OAuth_L_Password2
            // 
            this.F_OAuth_L_Password2.AutoSize = true;
            this.F_OAuth_L_Password2.Location = new System.Drawing.Point(13, 92);
            this.F_OAuth_L_Password2.Name = "F_OAuth_L_Password2";
            this.F_OAuth_L_Password2.Size = new System.Drawing.Size(119, 13);
            this.F_OAuth_L_Password2.TabIndex = 10;
            this.F_OAuth_L_Password2.Text = "F_OAuth_L_Password2";
            // 
            // F_OAuth_TB_Password
            // 
            this.F_OAuth_TB_Password.Location = new System.Drawing.Point(142, 63);
            this.F_OAuth_TB_Password.Name = "F_OAuth_TB_Password";
            this.F_OAuth_TB_Password.PasswordChar = '*';
            this.F_OAuth_TB_Password.Size = new System.Drawing.Size(219, 20);
            this.F_OAuth_TB_Password.TabIndex = 3;
            // 
            // F_OAuth_TB_Password2
            // 
            this.F_OAuth_TB_Password2.Location = new System.Drawing.Point(142, 89);
            this.F_OAuth_TB_Password2.Name = "F_OAuth_TB_Password2";
            this.F_OAuth_TB_Password2.PasswordChar = '*';
            this.F_OAuth_TB_Password2.Size = new System.Drawing.Size(219, 20);
            this.F_OAuth_TB_Password2.TabIndex = 4;
            // 
            // F_OAuth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 162);
            this.Controls.Add(this.F_OAuth_TB_Password2);
            this.Controls.Add(this.F_OAuth_TB_Password);
            this.Controls.Add(this.F_OAuth_L_Password2);
            this.Controls.Add(this.F_OAuth_L_Password);
            this.Controls.Add(this.Default_Abort);
            this.Controls.Add(this.Default_OK);
            this.Controls.Add(this.F_OAuth_TB_Secret);
            this.Controls.Add(this.F_OAuth_TB_Key);
            this.Controls.Add(this.F_OAuth_L_Secret);
            this.Controls.Add(this.F_OAuth_L_Key);
            this.Name = "F_OAuth";
            this.Text = "F_OAuth";
            this.Load += new System.EventHandler(this.F_OAuth_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label F_OAuth_L_Key;
        private System.Windows.Forms.Label F_OAuth_L_Secret;
        private System.Windows.Forms.TextBox F_OAuth_TB_Key;
        private System.Windows.Forms.TextBox F_OAuth_TB_Secret;
        private System.Windows.Forms.Button Default_OK;
        private System.Windows.Forms.Button Default_Abort;
        private System.Windows.Forms.Label F_OAuth_L_Password;
        private System.Windows.Forms.Label F_OAuth_L_Password2;
        private System.Windows.Forms.TextBox F_OAuth_TB_Password;
        private System.Windows.Forms.TextBox F_OAuth_TB_Password2;
    }
}