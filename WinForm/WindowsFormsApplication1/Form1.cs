
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SucLib.Model;
using SucLib.Data.IDal;
using SucLib.Data.Factory;
using DBUtility;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public static Form1 f1;
        IDBHelp db = DBFactory.Create();
        public Form1()
        {
            InitializeComponent();
            this.skinEngine1.SkinFile = "DiamondBlue.ssk";
            f1 = this;
        }

#if 鼠标事件
        Point mouseOff;
        bool leftFlag;
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseOff = new Point(-e.X, -e.Y); //得到变量的值
                leftFlag = true;                  //点击左键按下时标注为true;
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                Point mouseSet = Control.MousePosition;
                mouseSet.Offset(mouseOff.X, mouseOff.Y);  //设置移动后的位置
                Location = mouseSet;
            }
        }
        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                leftFlag = false;//释放鼠标后标注为false;
            }
        }
#endif


        private void button1_Click(object sender, EventArgs e)
        {
            LogHelper.Write(typeof(Form1), "测试日志写入");
            try
            {
                //throw new Exception("测试错误日志");
                MessageBox.Show(button1.Text);
            }
            catch (Exception ex)
            {
                LogHelper.Write(typeof(Exception), ex);
            }
            //if(MessageBox.Show(this, "测试完成请按确认", "测试完成") == DialogResult.OK)
            //{
            //NativeConsole.AllocConsole();
            //Console.WriteLine("测试");
            //Console.ReadKey();
            //NativeConsole.FreeConsole();
            //}
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            t2.Enabled = false;
            t3.Enabled = false;
            t4.Enabled = false;
            this.Left = (Screen.PrimaryScreen.WorkingArea.Width - Width) / 2;
            this.Top = (Screen.PrimaryScreen.WorkingArea.Height - Height) / 2;
            string hname = System.Net.Dns.GetHostName();
            System.Net.IPAddress[] ips = System.Net.Dns.GetHostByName(hname).AddressList;
            string ip = "";
            foreach (System.Net.IPAddress i in ips)
            {
                ip += i.ToString() + "x";
            }
            MessageBox.Show(ip);
            //return;
            //////////////////////////////////////////////////////////
            bind_data();
        }

        public void bind_data()
        {
            try
            {
                List<SUC_USER> users = new List<SUC_USER>();
                users = new SUC_USER().FindAll();
                //
                //string sql = string.Format(@"UPDATE TB_USER SET NAME='" + users[0].user_name + "' WHERE ID=" + users[0].user_id);
                //等价于 UPDATE TB_USER SET NAME='张三' WHERE ID=1   ;

                dgv_data.DataSource = users;

                List<string> comlst = new List<string>();
                foreach (SUC_USER u in users)
                {
                    comlst.Add(u.NAME.Trim());
                }
                comlst.Add("adfa");
                comlst.Add("dfagg");
                comlst.Add("ccd");
                comlst.Add("adfffea");

                comboBox1.DataSource = comlst;
                //comboBox1.SelectedIndex = 2;
                comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //LogHelper.Write(ex);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show(this, "是否要退出程序", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                Application.ExitThread();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void dgv_data_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string content = dgv_data.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            string n = (sender as DataGridView).CurrentCell.Value.ToString();
            FrmSelect f = new FrmSelect(n);
            f.rtvalue += f_rtvalue;
            f.ShowDialog();
            //this.Hide();
            //MessageBox.Show(this, content, "内容", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void f_rtvalue(string value)
        {
            //MessageBox.Show(value);
            this.Invoke(new Action(() =>
            {
                dgv_data.CurrentCell.Value = value;
            })); try
            {
                dgv_data.CurrentCell = dgv_data.CurrentRow.Cells[dgv_data.CurrentCellAddress.X + 1];
            }
            catch { }
        }

        //protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        protected bool ProcessCmdKey1(ref Message msg, Keys keyData)
        {
            //return base.ProcessCmdKey(ref msg, keyData);
            button1.Text = keyData.ToString();
            return true;
        }

        private void btn_emule_Click(object sender, EventArgs e)
        {
            //btn_douban_Click(sender, e);
            P2PSearch p2p = new P2PSearch();
            p2p.Show();
            this.Hide();
        }

        private void btn_music_Click(object sender, EventArgs e)
        {
            //btn_douban_Click(sender, e);
            try
            {
                MusicPlayer mp = new MusicPlayer();
                mp.Show();
                this.Hide();
            }
            catch { }
        }

        private void btn_douban_Click(object sender, EventArgs e)
        {
            //QQButton b = sender as QQButton;
            //foreach (Control c in this.Controls)
            //{
            //    if (c.Text.Equals(b.Text))
            //    {
            //        MessageBox.Show(c.Text + "," + c.GetType());
            //    }
            //}

            Douban db = new Douban();
            db.Show();
            this.Hide();
        }

        private void btn_mids_Click(object sender, EventArgs e)
        {
            MidP m = new MidP();
            m.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Lottery_Data l = new Lottery_Data();
            l.Show();
            this.Hide();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            label1.Text = e.X + "," + e.Y;
        }

        private void dgv_data_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv_data.DataSource != null)
            {
                if (string.IsNullOrEmpty(this.dgv_data.Rows[e.RowIndex].Cells[0].ToString()))
                {
                    this.dgv_data.Rows[e.RowIndex].Cells[3].Value = ""; //类别
                    this.dgv_data.Rows[e.RowIndex].Cells[2].Value = "";//描述
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox1.SelectedValue.ToString()))
            {
                MessageBox.Show("为空！");
            }
            else
            {
                string nv = comboBox1.SelectedValue.ToString();
                DataTable dt = DBUtility.DbHelperSQL.Query(string.Format(@"SELECT FunctionName FROM tabF_Function where FunctionId in({0})", "1,2,3,4")).Tables[0];
                comboBox2.DataSource = dt;
                comboBox2.DisplayMember = "FunctionName";
                comboBox2.ValueMember = "FunctionName";
                comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = this.dgv_data.CurrentCell.RowIndex;
            if (string.IsNullOrEmpty(comboBox2.SelectedValue.ToString()))
            {
                MessageBox.Show("为空！");
            }
            else
            {
                string nv = comboBox2.SelectedValue.ToString();
                DataTable dt = DBUtility.DbHelperSQL.Query(string.Format(@"SELECT ModuleName FROM tabF_Module where State in({0})", "1,2,3,4")).Tables[0];
                comboBox3.DataSource = dt;
                comboBox3.DisplayMember = "ModuleName";
                comboBox3.ValueMember = "ModuleName";
                comboBox3.SelectedIndexChanged += comboBox3_SelectedIndexChanged;
                //comboBox3.Items.Clear();
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(comboBox3.SelectedValue.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Weather w = new Weather();
            w.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            TodayNews tn = new TodayNews();
            tn.Show();
            this.Hide();
        }

        private void btn_jokey_Click(object sender, EventArgs e)
        {
            Jokeys tn = new Jokeys();
            tn.Show();
            this.Hide();
        }

        private void dgv_data_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            int ii = 1;
            if (!string.IsNullOrEmpty(t4.Text))
            {
                if (string.IsNullOrEmpty(t3.Text))
                    ii *= 0;
                if (string.IsNullOrEmpty(t2.Text))
                    ii *= 0;
                if (string.IsNullOrEmpty(t1.Text))
                    ii *= 0;
            }
            if (!string.IsNullOrEmpty(t3.Text))
            {
                if (string.IsNullOrEmpty(t2.Text))
                    ii *= 0;
                if (string.IsNullOrEmpty(t1.Text))
                    ii *= 0;
            }
            if (!string.IsNullOrEmpty(t2.Text))
            {
                if (string.IsNullOrEmpty(t1.Text))
                    ii *= 0;
            }
            if (ii == 0)
            {
                MessageBox.Show("数据不合规范！");
                return;
            }
            MessageBox.Show("合格");
            return;

            int shouldbe = 1;
            if (t2.Enabled == true || !string.IsNullOrEmpty(t2.Text))
                shouldbe *= 2;
            if (t3.Enabled == true || !string.IsNullOrEmpty(t3.Text))
                shouldbe *= 3;
            if (t4.Enabled == true || !string.IsNullOrEmpty(t4.Text))
                shouldbe *= 4;
            int i = 1;
            if (!string.IsNullOrEmpty(t1.Text))
            {
                i *= string.IsNullOrEmpty(t2.Text) && t2.Enabled ? 1 : 2;
                i *= string.IsNullOrEmpty(t3.Text) && t3.Enabled ? 1 : 3;
                i *= string.IsNullOrEmpty(t4.Text) && t4.Enabled ? 1 : 4;
            }
            if (i != shouldbe)
            { MessageBox.Show("数据不合规范！"); return; }
            MessageBox.Show("合格");
        }

        private void t1_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(t1.Text.Trim()))
                t2.Enabled = true;
            else
            {
                t2.Enabled = true;
                t3.Enabled = true;
                t4.Enabled = true;
            }

        }

        private void t2_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(t2.Text.Trim()))
                t3.Enabled = true;
            else
            {
                t3.Enabled = false;
                t4.Enabled = false;
            }
        }

        private void t3_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(t3.Text.Trim()))
                t4.Enabled = true;
            else
                t4.Enabled = false;
        }

        private void t4_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_bdy_Click(object sender, EventArgs e)
        {
            BaiduYunSearch bdy = new BaiduYunSearch();
            bdy.Show();
            this.Hide();
        }
    }
}
