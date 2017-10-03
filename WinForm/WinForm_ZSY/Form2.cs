using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace WinForm_ZSY
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.fontDialog1.ShowDialog() == DialogResult.OK)
            {
                this.label1.Font = this.fontDialog1.Font;
                ft = this.fontDialog1.Font.Name;
                sz = this.fontDialog1.Font.Size.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.panel1.BackColor = this.colorDialog1.Color;
                bc = this.colorDialog1.Color.ToArgb().ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string path = strpath + "\\set.ini";
            string content = string.Format(@"FC:{0},BC:{1},FT:{2},SZ:{3}", fc, bc, ft, sz);
            File.Delete(path);
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
            byte[] data = System.Text.Encoding.Default.GetBytes(content);
            fs.Write(data, 0, data.Length);
            fs.Flush();
            fs.Close();

            MessageBox.Show("保存成功！");
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (this.colorDialog2.ShowDialog() == DialogResult.OK)
            {
                this.label1.ForeColor = this.colorDialog2.Color;
                fc = this.colorDialog2.Color.ToArgb().ToString();
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.Left = (Screen.PrimaryScreen.WorkingArea.Width - Width) / 2;
            this.Top = (Screen.PrimaryScreen.WorkingArea.Height - Height) / 2;
            Readset();
        }

        string strpath = System.Environment.CurrentDirectory;
        string fc;
        string bc;
        string ft;
        string sz;
        private void Readset()
        {
            string path = strpath + "\\set.ini";
            StreamReader sr = new StreamReader(path, Encoding.Default);
            string sets = sr.ReadLine();
            string[] set = sets.Split(',');
            sr.Close();

            fc = set[0];
            fc = fc.Split(':')[1];
            bc = set[1];
            bc = bc.Split(':')[1];
            ft = set[2];
            ft = ft.Split(':')[1];
            sz = set[3];
            sz = sz.Split(':')[1];

            this.label1.ForeColor = Color.FromArgb(Convert.ToInt32(fc));
            this.panel1.BackColor = Color.FromArgb(Convert.ToInt32(bc));
            this.label1.Font = new Font(ft, Convert.ToInt32(sz));
        }
    }
}
