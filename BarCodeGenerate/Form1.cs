using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cobainsoft.Windows.Forms;
using ControlExs;

namespace BarCodeGenerate
{
    public partial class Form1 : FormEx
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Left = (Screen.PrimaryScreen.WorkingArea.Width - Width) / 2;
            this.Top = (Screen.PrimaryScreen.WorkingArea.Height - Height) / 2;
            pictureBox1.MaximumSize = new Size(this.Width, this.Height);
            this.tx_code.Text = "howareyou 123456";
            loadlist();
        }

        public void loadlist()
        {
            List<string> list = new List<string>();
            foreach(var e in Enum.GetValues(typeof(BarcodeType)))
            {
                string s = e.ToString();
                list.Add(s);
            }
            this.comboBox1.DataSource = list;
            this.comboBox1.SelectedIndex = 23;
        }

        /// <summary>
        /// 绘制条码底色,白色
        /// </summary>
        /// <param name="source">条码原型图片</param>
        private void DrawBackgroundColor(Image source)
        {
            Graphics g = Graphics.FromImage(source);
            Brush brush = new SolidBrush(Color.White);
            g.FillRectangle(brush, 0, 0, source.Width, source.Height);
            g.Dispose();
        }

        public Image BarCode(string data)
        {
            string file = Application.StartupPath + $"\\{DateTime.Now.ToString("hhmmss")}bar.jpg";
            var bar = new Cobainsoft.Windows.Forms.BarcodeControl();
            bar.AddOnCaption = null;
            bar.AddOnData = null;
            bar.BackColor = Color.White;
            bar.BarcodeType = BarcodeType.CODE39;
            bar.Font = new Font("Arial", 9F);
            bar.ForeColor = Color.Black;
            bar.HorizontalAlignment = BarcodeHorizontalAlignment.Center;
            bar.InvalidDataAction = InvalidDataAction.DisplayInvalid;
            bar.Location = new Point(3, 3);
            bar.LowerTopTextBy = 0F;
            bar.RaiseBottomTextBy = 0F;
            bar.Size = new System.Drawing.Size(218, 69);



            bar.BarcodeType = (BarcodeType)Enum.Parse(typeof(BarcodeType), comboBox1.Text); //Cobainsoft.Windows.Forms.BarcodeType.CODE128C;// 枚举 根据什么格式生成
            bar.ShowCode39StartStop = true;// code39是否有星号
            bar.Data = data; // 条码数据
            bar.AddOnData = "";// 日期
            bar.TextPosition = BarcodeTextPosition.Below; // 数据显示在条码底部
            bar.StretchText = true;
            bar.CopyRight = comboBox1.Text + " " + data;

            bar.Invalidate();
            //Stream stream = new FileStream("D://100.png", FileMode.Create, FileAccess.Write, FileShare.None);
            //bar.MakeImage(System.Drawing.Imaging.ImageFormat.Png, 1, 50, true, false, null, stream);
            bar.SaveImage(System.Drawing.Imaging.ImageFormat.Png, 2, 60, true, false, null, file);

            return Image.FromFile(file);
        }

        private void btn_general_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            if(File.Exists(Application.StartupPath + $"\\{DateTime.Now.ToString("hhmmss")}bar.jpg"))
                File.Delete(Application.StartupPath + $"\\{DateTime.Now.ToString("hhmmss")}bar.jpg");
            pictureBox1.Image = BarCode(tx_code.Text);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DirectoryInfo faildDirecotry = new System.IO.DirectoryInfo(Application.StartupPath);
            FileInfo[] filest = faildDirecotry.GetFiles("*.jpg");
            if(filest.Length > 0)
                for(int i = 0;i <= filest.Length;i++)
                {
                    try
                    {
                        File.Delete(filest[i].FullName);
                    }
                    catch(Exception) { continue; }
                }
        }
    }
}
