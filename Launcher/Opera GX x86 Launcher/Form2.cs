﻿using System.Windows.Forms;

namespace Opera_GX_x86_Launcher
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, System.EventArgs e)
        {
            if (radioButton1.Checked)
            {
                System.IO.File.WriteAllText(@"Opera GX x86\Profile.txt", "--user-data-dir=\"profile\"");
                this.Close();
            }
            if (radioButton2.Checked)
            {
                System.IO.File.WriteAllText(@"Opera GX x86\Profile.txt", "--user-data-dir=\"Opera GX x86\\profile\"");
                this.Close();
            }
            if (radioButton3.Checked)
            {
                System.IO.File.WriteAllText(@"Opera GX x86\Profile.txt", "");
                this.Close();
            }
        }
    }
}
