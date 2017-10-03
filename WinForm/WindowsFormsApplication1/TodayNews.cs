using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class TodayNews : Form
    {
        public TodayNews()
        {
            InitializeComponent();
        }

        public ArrayList NewsType = new ArrayList();
        int o = 0;
        //string apikey = "6fa6bc20eb8b5d6a9175619728fa37e1";   //图灵机器人在用
        string apikey = "4d17eb32465ef9444963d264dac1fd65";
        string openid = "JH7ddd4d0957e7ec33e379396474e1c2d2";
        string apiname = Login.user.NAME.Trim();
        private void TodayNews_Load(object sender, EventArgs e)
        {
            this.Left = (Screen.PrimaryScreen.WorkingArea.Width - Width) / 2;
            this.Top = (Screen.PrimaryScreen.WorkingArea.Height - Height) / 2;
            BindType();
            cmb_type.DataSource = NewsType;
            cmb_type.DisplayMember = "Key";
            cmb_type.ValueMember = "Value";
            cmb_type.DropDownStyle = ComboBoxStyle.DropDownList;
            cmb_type.SelectedIndex = 0;
            GetNews();
        }
        JavaScriptSerializer JSS = new JavaScriptSerializer();
        private void GetNews()
        {
            o += 1;
            News n = new News();
            //请求地址：v.juhe.cn/toutiao/index
            //请求参数：type=&key=4d17eb32465ef9444963d264dac1fd65
            //请求方式：GET
            string url = "http://v.juhe.cn/toutiao/index?key=4d17eb32465ef9444963d264dac1fd65";
            url += cmb_type.SelectedIndex > 0 ? "&type=" + cmb_type.SelectedValue : "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url));
            request.Method = "GET";
            request.ContentLength = 0;
            request.ContentType = "application/json;charset=utf-8";
            request.Host = "v.juhe.cn";
            try
            {
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:47.0) Gecko/20100101 Firefox/47.0";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader read = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string result = read.ReadToEnd();
                n = (News)JSS.Deserialize(result, typeof(News));
                RenderToLayout(n);
            }
            catch (Exception ex)
            { }
        }

        private void RenderToLayout(News n)
        {
            this.flowLayoutPanel1.Controls.Clear();
            List<Data> data = n.result.data;
            for (int i = 0; i < data.Count; i++)
            {
                Data d = data[i];
                LinkLabel Title = new LinkLabel();
                Title.Size = new Size(flowLayoutPanel1.Width - 30, 30);
                Title.Text = d.title;
                Title.Links[0].LinkData = d.url;
                Title.Click += Title_Click;
                PictureBox picture = new PictureBox();
                picture.SizeMode = PictureBoxSizeMode.StretchImage;
                picture.ImageLocation = d.thumbnail_pic_s;
                PictureBox picture1 = new PictureBox();
                picture1.SizeMode = PictureBoxSizeMode.StretchImage;
                picture1.ImageLocation = d.thumbnail_pic_s02;
                PictureBox picture2 = new PictureBox();
                picture2.SizeMode = PictureBoxSizeMode.StretchImage;
                picture2.ImageLocation = d.thumbnail_pic_s03;
                FlowLayoutPanel f = new FlowLayoutPanel();
                f.Width = flowLayoutPanel1.Width - 30;
                f.Controls.Add(Title);
                f.Controls.Add(picture);
                f.Controls.Add(picture1);
                f.Controls.Add(picture2);
                this.flowLayoutPanel1.Controls.Add(f);
            }
        }

        private void Title_Click(object sender, EventArgs e)
        {
            LinkLabel Title = sender as LinkLabel;
            //System.Diagnostics.Process.Start("iexplore.exe", Title.Links[0].LinkData.ToString());
            web_content.Navigate(Title.Links[0].LinkData.ToString());
        }

        private void TodayNews_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Form1.f1 != null)
            {
                Form1.f1.Show();
            }
        }

        private void cmb_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetNews();
        }

        private void BindType()
        {
            NewsType.Add(new DictionaryEntry("头条", "top"));
            NewsType.Add(new DictionaryEntry("社会", "shehui"));
            NewsType.Add(new DictionaryEntry("国内", "guonei"));
            NewsType.Add(new DictionaryEntry("国际", "guoji"));
            NewsType.Add(new DictionaryEntry("娱乐", "yule"));
            NewsType.Add(new DictionaryEntry("体育", "tiyu"));
            NewsType.Add(new DictionaryEntry("军事", "junshi"));
            NewsType.Add(new DictionaryEntry("科技", "keji"));
            NewsType.Add(new DictionaryEntry("财经", "caijing"));
            NewsType.Add(new DictionaryEntry("时尚", "shishang"));
        }
    }

    public class News
    {
        public string reason { get; set; }
        public Result result { get; set; }
        public string error_code { get; set; }
    }

    public class Result
    {
        public string stat { get; set; }
        public List<Data> data { get; set; }
    }

    public class Data
    {
        public string title { get; set; }
        public DateTime date { get; set; }
        public string author_name { get; set; }
        public string thumbnail_pic_s { get; set; }
        public string thumbnail_pic_s02 { get; set; }
        public string thumbnail_pic_s03 { get; set; }
        public string url { get; set; }
        public string uniquekey { get; set; }
        public string type { get; set; }
        public string realtype { get; set; }
    }

}
