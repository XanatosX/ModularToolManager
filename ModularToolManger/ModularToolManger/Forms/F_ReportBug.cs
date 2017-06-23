using ModularToolManger.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ModularToolManger.Forms
{
    public partial class F_ReportBug : Form
    {
        public F_ReportBug()
        {
            InitializeComponent();
        }

        private void F_ReportBug_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.MinimizeBox = false;
            this.MaximizeBox = false;



            SetLanguage();
        }

        private void SetLanguage()
        {
            this.SetupLanguage();
        }

        private void Default_Abort_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Default_Send_Click(object sender, EventArgs e)
        {
            //OAuth authentification = new OAuth()
        }
    }
}
