using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using Jk;

namespace WindowsFormsApplication1
{
    public partial class Jokeys : Form
    {
        public Jokeys()
        {
            InitializeComponent();
        }

        string key = "e47266216bde57a9b019a857a4457287";
        private void Jokeys_Load(object sender, EventArgs e)
        {
            this.Left = (Screen.PrimaryScreen.WorkingArea.Width - Width) / 2;
            this.Top = (Screen.PrimaryScreen.WorkingArea.Height - Height) / 2;
            GetContext();
            lb_content.DoubleClick += lb_content_DoubleClick;
        }

        private void lb_content_DoubleClick(object sender, EventArgs e)
        {
            ListBox lb = sender as ListBox;
            MessageBox.Show(lb.SelectedItem.ToString());
        }

        JavaScriptSerializer JSS = new JavaScriptSerializer();
        public void GetContext()
        {
            Jokey j = new Jokey();
            string url = "http://japi.juhe.cn/joke/content/text.from?key=e47266216bde57a9b019a857a4457287&page=1&pagesize=10";
            //string url = "https://openapi.ky-express.com/kyeopenapi/Find_WEB_LogisticsYD_More";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url));
            request.Method = "get";
            //request.ContentLength = 0;
            request.ContentType = "text/html;charset=utf-8";

#if nousecode
            request.Headers.Add("key", "value");
            byte[] paras;
            paras = System.Text.Encoding.UTF8.GetBytes("kye=10017&accesskey=AD01ED5848AE13956573FB489CB660ED&ydNumber=88800156392,88800559805");
            request.ContentLength = paras.Length;
            Stream writer = request.GetRequestStream();
            //将请求参数写入流
            writer.Write(paras, 0, paras.Length);
            writer.Close();
#endif
            //request.Host = "v.juhe.cn";
            try
            {
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:47.0) Gecko/20100101 Firefox/47.0";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader read = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string result = read.ReadToEnd();
                j = JSS.Deserialize<Jokey>(result);
                j = null;
                j = (Jokey)JSS.Deserialize(result, typeof(Jokey));
                foreach (Jk.Data d in j.result.data)
                {
                    lb_content.Items.Add(d.content);
                }
            }
            catch (Exception ex)
            { }
        }
    }

}

namespace Jk
{

    public class Jokey
    {
        public string error_code { get; set; }
        public string reason { get; set; }
        public Jk.Result result { get; set; }

    }

    public class Result
    {
        public string stat { get; set; }
        public List<Jk.Data> data { get; set; }
    }

    public class Data
    {
        public string content { get; set; }
        public string hashId { get; set; }
        public string unixtime { get; set; }
        public DateTime updatetime { get; set; }
        public string url { get; set; }
    }
}
