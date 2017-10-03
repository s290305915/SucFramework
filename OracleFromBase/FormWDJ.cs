using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace OracleFromBase
{
    public partial class FormWDJ : Form
    {
        public FormWDJ()
        {
            InitializeComponent();
        }
        Timer timer;

        private void FormWDJ_Load(object sender, EventArgs e)
        {
            webBrowser1.ScriptErrorsSuppressed = true;
            webBrowser1.Hide();
            tx_msg.AppendText("开始执行！\r\n");
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 10000;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            GetData();
        }

        public void GetData()
        {
            DataTable dt = AccessHelper.DataTable("select * from `设备1` order by `时间` desc");
            if(dt != null)
            {
                tx_msg.AppendText("连接Access数据库成功！\r\n");
                string host = ConfigurationManager.AppSettings["host"].ToString();
                if(dt.Rows.Count > 0)
                {
                    tx_msg.AppendText("数据条数：" + dt.Rows.Count + "\r\n");
                    DataRow dr = dt.Rows[0];
                    tx_msg.AppendText($"取最近一条数据  时间：{dr[2]} 温度：{dr[3]} \r\n");
                    lb_data.Text = dr[3].ToString();
                    decimal d = Convert.ToDecimal(dr[3].ToString());
                    tx_msg.AppendText("正在将数据添加到服务器！\r\n");
                    //string sql = $"update device set TEMP='{d}' where DEVICEID=113";          //烘干箱
                    try
                    {
                        //new QueryManager().Execute(sql);
                        string url = $"http://{host}/Manager/BJFacktoryMonitor_Other_A?wd={d}";

                        webBrowser1.Navigate(new Uri(url));
                        //GetUrltoHtml(url, "UTF8");
                        tx_msg.AppendText("更新成功！\r\n");
                    }
                    catch(Exception ex)
                    {
                        tx_msg.AppendText("更新出错：" + ex.Message + "\r\n");
                    }
                }
                else
                {
                    tx_msg.AppendText("数据为空！\r\n");
                }
            }
        }

    }
}
