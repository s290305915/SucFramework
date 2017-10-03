using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calc
{
    public partial class SyncShow : Form
    {
        public SyncShow()
        {
            Form1 form1 = new Form1();
            form1.sm += new ShowMsg(GetMsg);//注册事件
            //form1.Show(); //反写
            InitializeComponent();
        }

        private void GetMsg(string message)
        {
            this.Invoke(new Action(()=>{
            
            label1.Text = message;
            }));
        }

        private void SyncShow_Load(object sender, EventArgs e)
        {

        }
    }
}
