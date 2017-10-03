using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WinForm_ZSY
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public int ScreenWidth = 0;
        private void Form1_Load(object sender, EventArgs e)
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true); //双缓冲

            Rectangle rect = new Rectangle();
            rect = Screen.GetWorkingArea(this);
            int w = rect.Width;//屏幕宽
            ScreenWidth = w;
            int h = rect.Height;//屏幕高

            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;

            AppBarStates currentState = GetTaskbarState();
            SetTaskbarState(AppBarStates.AutoHide);

            Write(strpath + "\\1.txt");
            Build();
        }
        Label l_time;
        Label l_con;
        Panel pcc;
        Panel pcccopy;
        string strpath = System.Environment.CurrentDirectory;
        public void Build()
        {

            string setpath = strpath + "\\set.ini";
            StreamReader sr = new StreamReader(setpath, Encoding.Default);
            string sets = sr.ReadLine();
            string[] set = sets.Split(',');
            sr.Close();
            string fc = set[0];
            string bc = set[1];
            string ftx = set[2];
            string szx = set[3];


            Size sz = new System.Drawing.Size(350, 60);
            Font ft = new Font("SimSun", 30);
            GroupBox gb = new GroupBox();
            gb.Height = Convert.ToInt32(this.Height * 0.3);
            gb.Width = this.Width;
            gb.BackColor = Color.White;
            this.Controls.Add(gb);


            string logpath = strpath + "\\log.jpg";
            PictureBox pclog = new PictureBox();
            Bitmap logBitmap = new Bitmap(logpath);
            pclog.BackgroundImage = logBitmap;
            pclog.Size = new System.Drawing.Size(60, 60);
            pclog.Location = new Point(Convert.ToInt32(this.Width * 0.07), 15);
            pclog.BackgroundImageLayout = ImageLayout.Zoom;
            gb.Controls.Add(pclog);


            Label l_log = new Label();
            l_log.Text = "西 南 油 气 田 分 公 司 蜀 南 气 矿 信 息 自 控 中 心";
            l_log.Size = new Size(this.Width, 45);
            l_log.Font = new Font(ft.FontFamily, ft.Size, FontStyle.Bold);
            l_log.ForeColor = Color.Red;
            l_log.Location = new Point(Convert.ToInt32(this.Width * 0.13), 25);
            gb.Controls.Add(l_log);


            Label l_log22 = new Label();
            l_log22.Text = "安 全 生 产";// +days[0] + "天";
            l_log22.Size = sz;
            l_log22.Font = new Font(ft.FontFamily, 35);
            l_log22.Location = new Point(Convert.ToInt32(this.Width * 0.2), 90);
            gb.Controls.Add(l_log22);


            Label l_log2 = new Label();
            string txtpath = strpath + "\\1.txt";
            sr = new StreamReader(txtpath, Encoding.Default);
            string[] days = sr.ReadLine().Split('|');
            sr.Close();
            l_log2.Text = days[0] + " 天";
            l_log2.Size = sz;
            l_log2.Font = new Font(ft.FontFamily, 35);
            l_log2.Location = new Point(Convert.ToInt32(this.Width * 0.2) + 40, 160);
            gb.Controls.Add(l_log2);





            Label l_time11 = new Label();
            l_time11.Text = DateTime.Now.ToString("yyyy 年 MM 月 dd 日");
            l_time11.Size = new Size(sz.Width, 40);
            l_time11.Font = new Font(ft.FontFamily, 25, FontStyle.Bold);
            l_time11.Location = new Point(Convert.ToInt32(this.Width * 0.6), 80);
            gb.Controls.Add(l_time11);

            Label l_time1 = new Label();
            l_time1.Text = Week();
            l_time1.Size = new Size(sz.Width, 40);
            l_time1.Font = new Font(ft.FontFamily, 25, FontStyle.Bold);
            l_time1.Location = new Point(Convert.ToInt32(this.Width * 0.6) + 50, 125);
            gb.Controls.Add(l_time1);

            l_time = new Label();
            l_time.Size = new Size(sz.Width, 35);
            l_time.Font = new Font(ft.FontFamily, 25, FontStyle.Bold);
            l_time.Location = new Point(Convert.ToInt32(this.Width * 0.6) + 30, 170);
            gb.Controls.Add(l_time);




            pcc = new Panel();
            pcc.Size = new Size(this.Width, Convert.ToInt32(this.Height * 0.7) + 40);
            pcc.Location = new Point(0, Convert.ToInt32(this.Height * 0.3));
            pcc.BackColor = Color.Transparent;
            this.Controls.Add(pcc);


            //图片路径
            string imgpath = strpath + "\\1.jpg";
            try
            {
                Bitmap myBitmap = new Bitmap(imgpath);
                pcc.BackgroundImage = myBitmap;
                pcc.BackgroundImageLayout = ImageLayout.Stretch;
            }
            catch
            {
                pcc.BackColor = Color.FromArgb(Convert.ToInt32(bc.Split(':')[1]));
            }

            timer1.Start();
            string conpath = strpath + "\\2.txt";
            sr = new StreamReader(conpath, Encoding.Default);
            string con = sr.ReadToEnd();
            sr.Close();

            l_con = new Label();
            //l_con.Size = pc.Size;
            l_con.AutoSize = true;
            l_con.MaximumSize = new System.Drawing.Size(this.Width, 0);
            //l_con.Dock = DockStyle.Fill;
            l_con.BackColor = Color.Transparent;
            l_con.ForeColor = Color.FromArgb(Convert.ToInt32(fc.Split(':')[1]));
            l_con.Font = new Font(ftx.Split(':')[1], Convert.ToInt32(szx.Split(':')[1]));
            l_con.Location = new Point(0, 0);
            l_con.Text = con;
            l_con.BringToFront();
            pcc.Controls.Add(l_con);

        }
        string[] picfiles;
        PictureBox pic;
        public void MovePic()
        {
            pcc.BackColor = Color.White;
            string picpath = strpath + "\\pic\\";
            picfiles = Directory.GetFiles(picpath, "*.*");
            pic = new PictureBox();
            pcc.Controls.Add(pic);
            pic.BringToFront();
            Bitmap b = new Bitmap(picfiles[0]);
            pic.Image = b;
            pic.AutoSize = true;
            pic.Size = pcc.Size;
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
            pic.Location = new Point(-ScreenWidth, 0);
            timer3.Start();
        }
         
        private int GetSpeed()
        {
            int spd = 10;
            string speedpath = strpath + "\\speed.ini";
            StreamReader sr = new StreamReader(speedpath, Encoding.Default);
            string speed = sr.ReadLine();
            Int32.TryParse(speed, out spd);
            return spd;
        }

        int curr = 1;
        bool ischage = false;
        private void timer3_Tick(object sender, EventArgs e)
        {
            try
            {
                if (ischage)
                {
                    pcc.Controls.Clear();
                    this.timer3.Interval = 100;
                    pic = new PictureBox();
                    ischage = false;
                    if (curr == picfiles.Length)
                        curr = 0;
                    pic = new PictureBox();
                    pcc.Controls.Add(pic);
                    pic.BringToFront();
                    Bitmap b = new Bitmap(picfiles[curr]);
                    pic.Image = b;
                    pic.AutoSize = true;
                    pic.Size = pcc.Size;
                    pic.SizeMode = PictureBoxSizeMode.StretchImage;
                    pic.Location = new Point(-ScreenWidth, 0);
                    curr++;
                }
                this.pic.Location = new Point(this.pic.Location.X + GetSpeed(), 0);
                if (pic.Left >= 0)  //容差
                {
                    pcc.BackgroundImage = pic.Image;
                    this.timer3.Interval = 2000;
                    ischage = true;
                }
            }
            catch { }
        }


        bool isrun;
        bool ispc = false;
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                SetTaskbarState(AppBarStates.AlwaysOnTop);
            }
            else if (e.KeyCode == Keys.F12)
            {
                Form2 f2 = new Form2();
                f2.ShowDialog();
            }
            else if (e.KeyCode == Keys.Space)
            {
                if (ispc)   //图片滚动 文字无效
                {
                    //加图片滚动timer
                    return;
                }
                else
                {
                    if (!isrun)
                    {
                        timer2.Start();
                    }
                    else
                    {
                        timer2.Stop();
                        isrun = false;
                        l_con.Top = top;
                    }
                }
            }
            else if (e.KeyCode == Keys.F2)
            {
                if (!ispc)
                {
                    ispc = !ispc;
                    pcccopy = new Panel();
                    foreach (Control c in pcc.Controls)
                        pcccopy.Controls.Add(c);
                    pcc.Controls.Clear();
                    MovePic();
                }
                else
                {
                    ispc = !ispc;
                    pcc.Controls.Clear();
                    foreach (Control c in pcccopy.Controls)
                        pcc.Controls.Add(c);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            l_time.Text = DateTime.Now.ToString("HH : mm : ss");
        }

        public void Write(string path)
        {
            StreamReader sr = new StreamReader(path, Encoding.Default);
            string dayss = sr.ReadLine();
            string[] days = dayss.Split('|');
            sr.Close();
            int day = Convert.ToInt32(days[0]);
            if (Convert.ToDateTime(days[1]).ToShortDateString() == DateTime.Now.ToShortDateString())
                return;
            else
                File.Delete(path);
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
            byte[] data = System.Text.Encoding.Default.GetBytes((day + 1).ToString() + "|" + DateTime.Now.ToString());
            fs.Write(data, 0, data.Length);
            fs.Flush();
            fs.Close();
        }

        public string Week()
        {
            string weekstr = DateTime.Now.DayOfWeek.ToString();
            switch (weekstr)
            {
                case "Monday": weekstr = "星 期 一"; break;
                case "Tuesday": weekstr = "星 期 二"; break;
                case "Wednesday": weekstr = "星 期 三"; break;
                case "Thursday": weekstr = "星 期 四"; break;
                case "Friday": weekstr = "星 期 五"; break;
                case "Saturday": weekstr = "星 期 六"; break;
                case "Sunday": weekstr = "星 期 日"; break;
            }
            return weekstr;
        }

        #region appbar

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindow(string strClassName, string strWindowName);

        [DllImport("shell32.dll")]
        public static extern UInt32 SHAppBarMessage(UInt32 dwMessage, ref APPBARDATA pData);

        public enum AppBarMessages
        {
            New =
            0x00000000,
            Remove =
            0x00000001,
            QueryPos =
            0x00000002,
            SetPos =
            0x00000003,
            GetState =
            0x00000004,
            GetTaskBarPos =
            0x00000005,
            Activate =
            0x00000006,
            GetAutoHideBar =
            0x00000007,
            SetAutoHideBar =
            0x00000008,
            WindowPosChanged =
            0x00000009,
            SetState =
            0x0000000a
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct APPBARDATA
        {
            public UInt32 cbSize;
            public IntPtr hWnd;
            public UInt32 uCallbackMessage;
            public UInt32 uEdge;
            public Rectangle rc;
            public Int32 lParam;
        }

        public enum AppBarStates
        {
            /// <summary>
            /// 自动隐藏
            /// </summary>
            AutoHide =
            0x00000001,
            /// <summary>
            /// 放在最前
            /// </summary>
            AlwaysOnTop =
            0x00000002
        }

        /// <summary>
        ///设置当前任务栏状态
        /// </summary>
        /// <param name="option">要设置的状态</param>
        public void SetTaskbarState(AppBarStates option)
        {
            APPBARDATA msgData = new APPBARDATA();
            msgData.cbSize = (UInt32)Marshal.SizeOf(msgData);
            msgData.hWnd = FindWindow("System_TrayWnd", null);
            msgData.lParam = (Int32)(option);
            SHAppBarMessage((UInt32)AppBarMessages.SetState, ref msgData);
        }

        /// <summary>
        /// 获取当前任务栏状态
        /// </summary>
        /// <returns></returns>
        public AppBarStates GetTaskbarState()
        {
            APPBARDATA msgData = new APPBARDATA();
            msgData.cbSize = (UInt32)Marshal.SizeOf(msgData);
            msgData.hWnd = FindWindow("System_TrayWnd", null);
            return (AppBarStates)SHAppBarMessage((UInt32)AppBarMessages.GetState, ref msgData);
        }

        #endregion
        int top = 0;
        private void timer2_Tick(object sender, EventArgs e)
        {
            isrun = true;
            l_con.Top = l_con.Top - 3;
            if (l_con.Bottom < 0)
                l_con.Top = pcc.Height;
            top = l_con.Top;
        }

        /// <summary>
        /// 处理消息以防止背景抖动
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }


    }
}
