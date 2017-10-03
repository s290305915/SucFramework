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
    public partial class Form1 : Form
    {
        string dengshi = string.Empty;
        public Form1()
        {
            InitializeComponent();
        }

        private void eq_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            switch (btn.Name)
            {
                case "one":
                case "two":
                case "three":
                case "four":
                case "five":
                case "six":
                case "seven":
                case "eight":
                case "nine":
                case "zero":
                case "add":
                case "div":
                case "cheng":
                case "chu":
                case "dot":
                case "eq":
                    tx_result.Text += btn.Text.Trim();
                    dengshi = tx_result.Text;
                    break;
                case "Clear":
                    tx_result.Text = "";
                    dengshi = string.Empty;
                    break;
                case "Delete":
                    if (dengshi.Length > 0)
                    {
                        tx_result.Text = dengshi.Substring(0, dengshi.Length - 1);
                        dengshi = tx_result.Text;
                    }
                    break;
                case "ClearE":
                    break;

            }
            Task.Factory.StartNew(() =>
            {
                todo();
            });
        }


        public event ShowMsg sm;    //声明一个事件
        private void eq_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tx_result.Text.Trim()))
                MessageBox.Show("为空");
        }
        SyncShow ss;
        private void todo()
        {
            try
            {
                if (ss == null)
                {
                    ss = new SyncShow();
                    ss.Show();
                }
                if (sm != null) //判断事件是否被注册
                    sm(dengshi);
            }
            catch { }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //SyncShow ss = new SyncShow();
            //ss.Show();
        }
    }
    public delegate void ShowMsg(string message); //声明一个委托
}
