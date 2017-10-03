
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Douban : Form
    {
        public Douban()
        {
            InitializeComponent();
        }

        private void Douban_Load(object sender, EventArgs e)
        {
            //Rectangle ScreenArea = System.Windows.Forms.Screen.GetBounds(this);
            this.Left = (Screen.PrimaryScreen.WorkingArea.Width - Width) / 2;
            this.Top = (Screen.PrimaryScreen.WorkingArea.Height - Height) / 2;
            webBrowser1.ScriptErrorsSuppressed = true;
            this.Text = "豆瓣电台-聆听天籁";
            string url = "http://douban.fm/partner/baidu/doubanradio";
            webBrowser1.Navigate(url);
        }

        private void Douban_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Form1.f1 != null)
            {
                Form1.f1.Show();
                this.Hide();
                return;
            }
            Form1.f1 = new Form1();
            Form1.f1.Show();
            this.Hide();
        }

    }
}
