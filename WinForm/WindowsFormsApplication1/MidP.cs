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
    public partial class MidP : Form
    {
        public MidP()
        {
            InitializeComponent();
        }
        MidC c = new MidC();
        Form1 f1 = Form1.f1;
        private void MidP_Load(object sender, EventArgs e)
        {
            c.TopLevel = false;
            this.panel1.Controls.Clear();
            this.panel1.Controls.Add(c);
            c.Show();
        }
        bool iscon = true;
        private void button1_Click(object sender, EventArgs e)
        {
            if(iscon)
            {
                iscon = !iscon;
                IsMdiContainer = false;     //把mdi父窗体属性关了
                this.panel1.Controls.Clear();   //把父窗体panel内容清空
                c.Close();                //父窗体内子窗体关闭了，
                c = new MidC();
                c.Show();       //在外部打开
            }
            else
            {
                iscon = !iscon;
                c.Close();
                c = new MidC();
                MidP_Load(sender, e);
            }
        }

        private void MidP_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(f1 != null)
                f1.Show();
        }
    }
}
