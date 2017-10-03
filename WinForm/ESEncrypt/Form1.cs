using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace ESEncrypt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static string sKey = "asia123?";

        private void Form1_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            BitmapRegion bitmap = new BitmapRegion();
            BitmapRegion.CreateControlRegion(this, Properties.Resources.EncryptForm_02_01); //根据固定格式和颜色来创建变规则窗体 
            tx_enin.Focus();
        }

        private void aa()
        {
        }

        private Point mouse_Offset;
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouse_Offset.X, mouse_Offset.Y);
                Location = mousePos;
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            mouse_Offset = new Point(-e.X, -e.Y);
        }

        public string DESEnCode(string pToEncrypt)
        {
            string pwd = EM.Security.DESEncrypt.Encrypt(pToEncrypt);
            return pwd;

            DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider();
            byte[] bytes = Encoding.GetEncoding("UTF-8").GetBytes(pToEncrypt);
            dESCryptoServiceProvider.Key = Encoding.ASCII.GetBytes(Form1.sKey);
            dESCryptoServiceProvider.IV = Encoding.ASCII.GetBytes(Form1.sKey);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, dESCryptoServiceProvider.CreateEncryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(bytes, 0, bytes.Length);
            cryptoStream.FlushFinalBlock();
            StringBuilder stringBuilder = new StringBuilder();
            byte[] array = memoryStream.ToArray();
            for (int i = 0; i < array.Length; i++)
            {
                byte b = array[i];
                stringBuilder.AppendFormat("{0:X2}", b);
            }
            stringBuilder.ToString();
            return stringBuilder.ToString();
        }
        public string DESDeCode(string pToDecrypt)
        {
            string pwd = EM.Security.DESEncrypt.Decrypt(pToDecrypt);
            return pwd;

            DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider();
            byte[] array = new byte[pToDecrypt.Length / 2];
            for (int i = 0; i < pToDecrypt.Length / 2; i++)
            {
                int num = Convert.ToInt32(pToDecrypt.Substring(i * 2, 2), 16);
                array[i] = (byte)num;
            }
            dESCryptoServiceProvider.Key = Encoding.ASCII.GetBytes(Form1.sKey);
            dESCryptoServiceProvider.IV = Encoding.ASCII.GetBytes(Form1.sKey);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, dESCryptoServiceProvider.CreateDecryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(array, 0, array.Length);
            cryptoStream.FlushFinalBlock();
            StringBuilder stringBuilder = new StringBuilder();
            return Encoding.Default.GetString(memoryStream.ToArray());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(tx_enin.Text))
                {
                    string enout = DESEnCode(tx_enin.Text);//(tx_denin.Text); //
                    tx_enout.Text = enout;
                }
                else
                { tx_enout.Text = ""; }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(tx_denin.Text))
                {
                    string denout = DESDeCode(tx_denin.Text);
                    tx_denout.Text = denout;
                }
                else
                { tx_denout.Text = ""; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Application.ExitThread();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string file = "";
            OpenFileDialog op = new OpenFileDialog();
            if (DialogResult.OK == op.ShowDialog())
            {
                file = op.FileName;
            }

            Bitmap bmp = new Bitmap(file);
            //this.pictureBox1.Image = bmp;

            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] arr = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(arr, 0, (int)ms.Length);
            ms.Close();
            string strbaser64 = Convert.ToBase64String(arr);
            textBox1.Text = strbaser64;



            byte[] arra = Convert.FromBase64String(strbaser64);
            MemoryStream msa = new MemoryStream(arra);
            System.Drawing.Bitmap bmpa = new System.Drawing.Bitmap(msa);
            this.pictureBox1.Image = bmpa;

            return;
            int i = 0;
            int result = 1;
            for (i = 5; i > 0; i--)
            {
                result *= i;
            }
            Console.WriteLine(result);
            return;
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = true;
            dlg.Title = "选择要转换的base64编码的文本";
            dlg.Filter = "txt files|*.txt";
            if (DialogResult.OK == dlg.ShowDialog())
            {
                for (int j = 0; j < dlg.FileNames.Length; j++)
                {
                    Base64StringToImage(dlg.FileNames[j].ToString());
                }

            }
        }

        //base64编码的文本 转为    图片
        private void Base64StringToImage(string txtFileName)
        {
            try
            {
                string g = Guid.NewGuid().ToString();
                FileStream ifs = new FileStream(txtFileName, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(ifs);
                string time = DateTime.Now.ToString("yyyyMMddhhssmm");
                String inputStr = sr.ReadToEnd();
                byte[] arr = Convert.FromBase64String(inputStr);
                MemoryStream ms = new MemoryStream(arr);
                Bitmap bmp = new Bitmap(ms);

                bmp.Save(txtFileName + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                ms.Close();
                sr.Close();
                ifs.Close();
                string newfname = "aa_" + Guid.NewGuid().ToString() + ".jpg";
                string filename = @"c:/" + newfname;
                bmp.Save(filename, System.Drawing.Imaging.ImageFormat.Jpeg);
                if (File.Exists(txtFileName))
                { }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Base64StringToImage 转换失败\nException：" + ex.Message);
            }
        }
    }
}
