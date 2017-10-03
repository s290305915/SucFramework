
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Web.Script.Serialization;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.Collections;

namespace WindowsFormsApplication1
{
    public partial class MusicPlayer : Form
    {
        public MusicPlayer()
        {
            InitializeComponent();
        }

        string apikey = "6fa6bc20eb8b5d6a9175619728fa37e1";
        string apiname = Login.user.NAME.Trim();
        string area = "成都";
        string botname = "机器人";
        private void MusicPlayer_Load(object sender, EventArgs e)
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true); //双缓冲

            this.Left = (Screen.PrimaryScreen.WorkingArea.Width - Width) / 2;
            this.Top = (Screen.PrimaryScreen.WorkingArea.Height - Height) / 2;
        }

        private void MusicPlayer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(Form1.f1 != null)
            {
                Form1.f1.Show();
            }
        }

        private void qqButton1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(tx_keyword.Text.Trim()))
            {
                MessageBox.Show(this, "请先输入再发送");
                return;
            }
            string keyword = tx_keyword.Text;
            chat(keyword, true);
            string requestUrl = "http://www.tuling123.com/openapi/api?key=" + apikey + "&info=" + keyword + "&loc=" + area + "&userid=" + apiname;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(requestUrl));
            request.Method = "POST";
            try
            {
                //request.UserAgent = ".NET Framework Test Client";
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0; QQWubi 133; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; CIBA; InfoPath.2)";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader read = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string str = read.ReadToEnd();
                chat(str);
            }
            catch(Exception ex)
            {
            }
        }

        public void chat(string msg, bool isyourself = false)
        {
            try
            {
                if(isyourself)
                {
                    lstb_chat.Items.Add("\r\n你说：\r\t\t" + msg);
                    return;
                }
                MessageObj<ListCnt> model = new JavaScriptSerializer().Deserialize<MessageObj<ListCnt>>(msg);
                string json = new JavaScriptSerializer().Serialize(model);
                switch(model.code)
                {
                    case 100000:
                        lstb_chat.Items.Add("\r\n" + botname + "：\r\t\t" + model.text);
                        break;
                    case 200000:
                        lstb_chat.Items.Add("\r\n" + botname + "：\r\t\t" + model.text);
                        lstb_chat.Items.Add("\r\n\r\t链接：" + model.url);
                        break;
                    case 302000:
                        lstb_chat.Items.Add("\r\n" + botname + "：\r\t\t" + model.text);
                        lstb_chat.Items.Add("\r\t\t\t==============================");
                        foreach(ListCnt lc in model.list)
                        {
                            lstb_chat.Items.Add("\r\n\r\t标题：" + lc.article);
                            lstb_chat.Items.Add("\r\n\r\t来源：" + lc.source);
                            lstb_chat.Items.Add("\r\n\r\t链接：" + lc.detailurl);
                            lstb_chat.Items.Add("\r\t\t\t==============================");
                        }
                        break;
                    case 308000:
                        lstb_chat.Items.Add("\r\n" + botname + "：\r\t\t" + model.text);
                        lstb_chat.Items.Add("\r\t\t\t==============================");
                        foreach(ListCnt lc in model.list)
                        {
                            lstb_chat.Items.Add("\r\n\r\t菜名：" + lc.name);
                            lstb_chat.Items.Add("\r\n\r\t菜谱信息：" + lc.info);
                            lstb_chat.Items.Add("\r\n\r\t链接：" + lc.detailurl);
                            lstb_chat.Items.Add("\r\t\t\t==============================");
                        }
                        break;
                }
            }
            finally
            {
                lstb_chat.Items.Add("\r\n");
                if(lstb_chat.Items.Count > 10)
                    lstb_chat.SelectedIndex = lstb_chat.Items.Count - 1;
            }
        }

        private void btn_export_Click(object sender, EventArgs e)
        {
            string filetime = DateTime.Now.ToString("yyyyMMddmm");
            string _webpath = AppDomain.CurrentDomain.BaseDirectory + "\\Excel\\EndMarketing";
            if(!Directory.Exists(_webpath))
                Directory.CreateDirectory(_webpath);
            string filename = "(年终营销活动)_" + filetime + ".xls";
            string path = _webpath + "\\" + filename;

            MemoryStream ms = new MemoryStream();
            IWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet("年终营销活动-");
            //CellRangeAddress四个参数为：起始行，结束行，起始列，结束列
            IRow row1 = sheet.CreateRow(0);

            row1.CreateCell(0).SetCellValue("AA");
            if(lstb_chat.Items.Count > 0)
            {
                for(int i = 0;i < lstb_chat.Items.Count;i++)
                {
                    IRow row2 = sheet.CreateRow(i);
                    row2.CreateCell(1).SetCellValue(lstb_chat.Items[i].ToString());
                }
            }
            workbook.Write(ms);
            ms.Position = 0;
            ms.Close();
            ms.Flush();
            SaveToFile(ms, path);
        }

        public static void SaveToFile(MemoryStream ms, string fileName)
        {
            using(FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                byte[] data = ms.ToArray();

                fs.Write(data, 0, data.Length);
                fs.Flush();

                data = null;
            }
        }
    }


    public class MessageObj<T> where T : ListCnt
    {
        //public enum Ecode { 文本类 = 100000, 链接类 = 200000, 新闻类 = 302000, 菜谱类 = 308000 }
        public int code
        {
            get;
            set;
        }
        public string text
        {
            get;
            set;
        }
        public string url
        {
            get;
            set;
        }
        public List<T> list
        {
            get;
            set;
        }
    }

    public class ListCnt
    {
        public string name
        {
            get;
            set;
        }
        public string info
        {
            get;
            set;
        }
        public string article
        {
            get;
            set;
        }
        public string source
        {
            get;
            set;
        }
        public string icon
        {
            get;
            set;
        }
        public string detailurl
        {
            get;
            set;
        }
    }
}
