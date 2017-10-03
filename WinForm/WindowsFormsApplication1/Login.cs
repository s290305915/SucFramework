using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SucLib.Data.IDal;
using SucLib.Data.Factory;
using SucLib.Model;

namespace WindowsFormsApplication1
{
    public partial class Login : Form
    {
        IDBHelp db = DBFactory.Create();
        SUC_USER metUser = new SUC_USER();
        public static SUC_USER user;
        public Form1 f1;
        public Login()
        {
            InitializeComponent();
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            for(int k = 0;k < 10;k++)
            {
                for(int i = -3;i <= 3;i++)  //左三圈
                {
                    Top += i;
                    for(int j = i;j <= 3;j++)
                        Left += j;
                    for(int j = i;j >= -3;j--)
                        Left += j;
                }
                for(int i = -3;i <= 3;i++)  //右三圈
                {
                    Top += -i;
                    for(int j = i;j <= 3;j++)
                        Left += j;
                    for(int j = i;j >= -3;j--)
                        Left += j;
                }
            }
            tm1.Stop();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            //if (f1 == null)
            //    f1 = new Form1();
            //f1.Show();
            //this.Hide();
            //return;
            /////////////////////////////////////////////////////////////
            if(string.IsNullOrEmpty(tx_username.Text.Trim()) || string.IsNullOrEmpty(tx_password.Text.Trim()))
            {
                MessageBox.Show(this, "填写完先 ╭(╯^╰)╮");
                return;
            }
            try
            {
                user = new SUC_USER().FindSingleByCondition(new SUC_USER() { LOGIN_NAME = tx_username.Text.Trim() });
                //user.LOGIN.PASSWORD = tx_password.Text.Trim();
                if(user == null)
                {
                    MessageBox.Show(this, "用户不存在 ( ⊙ o ⊙ )！");
                    return;
                }

                if(user.LOGIN.PASSWORD == tx_password.Text.Trim())
                {
                    //成功了呗
                    if(f1 == null)
                        f1 = new Form1();
                    f1.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show(this, "用户名或密码错误！ ╮(╯▽╰)╭");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(this, "未能连接到数据库！ ╮(╯▽╰)╭");
            }
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            tm1.Start();
            tx_password.Text = "";
            tx_username.Text = "";
            chk_remember.Checked = false;
            user = null;
        }

        private void chk_remember_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {
            tx_password.Text = "admin";
            tx_username.Text = "1";

            tm1.Interval = 5;//动作频率，这里单位为ms
            tm1.Start();
            showpa = false;

            this.BackColor = Color.White;
            this.TransparencyKey = Color.White;

            //this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.TransparencyKey = Color.Transparent;
            // BitmapRegion bitmapregion = new BitmapRegion();
            // BitmapRegion.CreateControlRegion(this, Resources._54a28aeb026e4);

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true); //双缓冲

            this.Left = (Screen.PrimaryScreen.WorkingArea.Width - Width) / 2;
            this.Top = (Screen.PrimaryScreen.WorkingArea.Height - Height) / 2;
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(MessageBox.Show(this, "是否要退出程序", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                Application.ExitThread();
            }
            else
            {
                e.Cancel = true;
            }
        }

        #region 无边框移动MyRegion

        Point mouseOff;
        bool leftFlag;
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                mouseOff = new Point(-e.X, -e.Y); //得到变量的值
                leftFlag = true;                  //点击左键按下时标注为true;
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if(leftFlag)
            {
                Point mouseSet = Control.MousePosition;
                mouseSet.Offset(mouseOff.X, mouseOff.Y);  //设置移动后的位置
                Location = mouseSet;
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            if(leftFlag)
            {
                leftFlag = false;//释放鼠标后标注为false;
            }
        }

        #endregion

        #region 显示panelMyRegion

        bool showpa;
        private void panel1_MouseEnter(object sender, EventArgs e)
        {
            if(showpa == false)
            {

                showpa = true;
            }
        }

        private void panel1_MouseHover(object sender, EventArgs e)
        {

        }

        private void panel1_MouseLeave(object sender, EventArgs e)
        {
            if(showpa == true)
            {

                showpa = false;
            }
        }
        #endregion


        private void label1_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }
    }
}
