using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Weather : Form
    {
        public Weather()
        {
            InitializeComponent();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            string City = tx_city.Text.Trim();
            string requestUrl = @"http://ws.webxml.com.cn/WebServices/WeatherWebService.asmx/getWeatherbyCityName?theCityName=" + City;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(requestUrl));
            request.Method = "GET";
            request.ContentLength = 0;
            request.ContentType = "text/xml; charset=utf-8";
            request.Host = "ws.webxml.com.cn";
            try
            {
                //request.UserAgent = ".NET Framework Test Client";
                request.UserAgent = @"Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0; QQWubi 133; SLCC2;
                .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729;
                Media Center PC 6.0; CIBA; InfoPath.2)";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader read = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string result = read.ReadToEnd();
                lb_result.Text = FormateStr(result);
            }
            catch (Exception ex)
            { }
        }
        private string FormateStr(string str)
        {
            str = str.Substring(str.IndexOf("<string>"), str.Length - (str.IndexOf("<string>")));
            str = str.Replace("</ArrayOfString>", "").Replace("<string>", "").Replace("</string>", "").Replace("<string />", "");
            return str;
        }


        private void Weather_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Form1.f1 != null)
            {
                Form1.f1.Show();
            }
        }

        private void Weather_Load(object sender, EventArgs e)
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true); //双缓冲

            this.Left = (Screen.PrimaryScreen.WorkingArea.Width - Width) / 2;
            this.Top = (Screen.PrimaryScreen.WorkingArea.Height - Height) / 2;
        }
    }
}
