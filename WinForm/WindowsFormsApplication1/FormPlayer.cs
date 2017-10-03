using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class FormPlayer : Form
    {
        public FormPlayer()
        {
            InitializeComponent();
        }

        private void FormPlayer_Load(object sender, EventArgs e)
        {
            string mdpath = App.AppPath + "1.wmv";
            this.axWindowsMediaPlayer1.URL = mdpath;
            this.axWindowsMediaPlayer1.Ctlcontrols.play();

            Process p = new Process();
            p.StartInfo.FileName = "wmplayer.exe";
            p.StartInfo.Arguments = mdpath;
            p.Start();
        }
    }
}
