using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using ControlExs;

namespace WindowsFormsApplication1
{
    public partial class BaiduYunSearch : Form
    {
        public BaiduYunSearch()
        {
            InitializeComponent();
        }

        public class ListViewNF : System.Windows.Forms.ListView
        {
            public ListViewNF()
            {
                // 开启双缓冲
                this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);

                // Enable the OnNotifyMessage event so we get a chance to filter out 
                // Windows messages before they get to the form's WndProc
                this.SetStyle(ControlStyles.EnableNotifyMessage, true);
            }
            protected override void OnNotifyMessage(Message m)
            {
                //Filter out the WM_ERASEBKGND message
                if (m.Msg != 0x14)
                {
                    base.OnNotifyMessage(m);
                }

            }
        }
        public struct strPCFileinfo
        {
            public string strName;
            public string strSize;
            public string strOwner;
            public string strTime;
            public string strCount;
            public string strRemark;
            public string strDownloadUrl;
        };
        public static object locker = new object();//添加一个对象作为锁

        private delegate void SearchResultCallBack(int index, strPCFileinfo file);
        private SearchResultCallBack searchResultCallBack;
        private void SearchResultMethod(int index, strPCFileinfo file)//往listview控件添加信息
        {
            ListViewItem firstrecord = new ListViewItem(index.ToString());
            firstrecord.SubItems.Add(file.strName);
            firstrecord.SubItems.Add(file.strSize);
            firstrecord.SubItems.Add(file.strOwner);
            firstrecord.SubItems.Add(file.strTime);
            firstrecord.SubItems.Add(file.strCount);
            firstrecord.SubItems.Add(file.strRemark);
            listView1.Items.Add(firstrecord);
        }


        int index;
        strPCFileinfo[] fileinfo;
        string htmlCompare = null;
        int page;

        private bool IsValideMethod(string url)//判断文件是否有效
        {
            // DateTime start = DateTime.Now;
            //url = @"http://pan.baidu.com/share/link?shareid=1407451583&uk=2318901111";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "GET";
            req.ContentType = "application/x-www-form-urlencoded";
            req.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)";
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            Stream ReceiveStream = res.GetResponseStream();
            Encoding encode = System.Text.Encoding.UTF8;
            StreamReader sr = new StreamReader(ReceiveStream, encode);

            string strResult = "";
            Char[] read = new Char[512];
            int count = sr.Read(read, 0, 512);

            while (count > 0)
            {
                String str = new String(read, 0, count);
                strResult += str;
                count = sr.Read(read, 0, 256);
            }
            MatchCollection match3 = Regex.Matches(strResult, "<title>百度云 网盘-链接不存在</title>");
            try
            {
                if (match3[0].Success)
                    return false;
                else
                    return true;
            }
            catch { }
            return true;
        }

        private void RegexMatchResult()//核心代码，正则匹配所有符合要求的资源列表
        {
            //}\"[\s]*?(href=\"http://.*\")+?[^(文件名)]*立即关注 （匹配文件夹）
            //string strPat ="}\"[\\s]*?(href=\"http://.*\")+?[\\s\\S]*?文件名:(.+?) 文件大小:(.+?) 分享者:(.+?) 分享时间:(.+?) 下载次数:(.+?)</div>";
            index = page = 0;
            fileinfo = new strPCFileinfo[2000];
            string strPat = "}\"[\\s]*?(href=\"http://.*\")+?[^(立即关注)]*?文件名:(.+?) 文件大小:(.+?) 分享者:(.+?) 分享时间:(.+?) 下载次数:(.+?)</div>";
            Regex reg = new Regex(strPat);
            WebClient client = new WebClient();
            client.Proxy = null;
            client.Encoding = Encoding.GetEncoding("utf-8");
            
# if 参数解释
            //www.daysou.com/s?q=中国&start=0&isget=1&tp=all&cl=0&filetype=1080p&line=0
            q:参数名（查询关键字）
            isget:未知
            tp:网盘类型
                    "all" 所有网盘
                    "gn" 国内网盘
                    "gw" 国外网盘
                    "baipan" 百度网盘
                    "dbank"华为网盘
                    "kuaichuan"迅雷快传
                    "xuanfeng"QQ 旋风
                    "kuaipan"金山快盘
                    "360yun"360 云盘
                    "weiyun"腾讯微云
                    "ctdisk"城通网盘
                    "qjwm"千军万马
                    "weibo"新浪微盘
                    "tianyi"天翼网盘
                    "letv"乐视云盘
                    "yunfile"YunFile网盘
                    "caiyun"移动彩云网盘
                    "vdisk"vdisk威盘
                    "115"115 网盘
                    "everbox"可乐云盘
                    "hotfile"hotfile.com
                    "rapidshare"rapidshare.com
                    "oron"oron.coms
                    "uploaded"uploaded.to
                    "easy-share"easy-share.com
                    "uploading"uploading.com
                    "turbobit"turbobit.net
                    "fileserve"fileserve.com
                    "enterupload"enterupload.com
                    "4shared"4shared.com
            cl:栏目：
                网盘0 网页1 微博2 微信4 文档3
            line:线路 
                引擎1-0,引擎2-1,引擎3-2,加密-3
            filetype:
                1080p
                720p
                mkv
                mp4
                torrent
                apk
                rmvb
                3gp
                ts
                avi
                wma
                mp3
                flac
                ape
                epub
                pdf
                ppt
                doc
                txt
                iso
                exe
                rar
                zip
                7z
# endif
            string strUrl = string.Format(@"http://www.daysou.com/s?q={0}&start=0&isget=1&tp=all&cl=0&line=0", tx_keyword.Text, page * 10);
            string html = client.DownloadString(strUrl);
            bool IsSame = true;
            //string html = client.DownloadString(@"http://www.baidu.com/s?wd=site%3Apan.baidu.com%20易语言&pn=10&ie=utf-8");
            lock (locker)//锁 
            {
                strPCFileinfo[] fileinfocompare = new strPCFileinfo[10];
                while (IsSame && (page <= 2000))
                {
                    MatchCollection matches = Regex.Matches(html, strPat);
                    //Trace.WriteLine("hello");
                    //fileinfo = new strPCFileinfo[matches.Count];

                    for (int i = 0; i < matches.Count; i++)
                    {
                        if (matches[i].Success)
                        {
                            //Trace.WriteLine(matches[i].Value);
                            int j = index;
                            index++;
                            string strMatch = matches[i].Value;
                            strMatch = strMatch.Replace("<em>", "");
                            strMatch = strMatch.Replace("</em>", "");
                            strMatch = strMatch.Replace("</div>", "");
                            strMatch = strMatch.Replace(" ", "");
                            //strMatch = strMatch.Replace("\n", "");
                            //Regex reg1 = new Regex("[a-zA-z]+://[^\\s]*");
                            MatchCollection match = Regex.Matches(strMatch, "[a-zA-z]+://[^\\s]*");
                            fileinfo[j].strDownloadUrl = match[0].Value.Substring(0, match[0].Value.IndexOf("\""));
                            fileinfo[j].strName = strMatch.Substring(strMatch.IndexOf("文件名") + 4);
                            fileinfo[j].strName = fileinfo[j].strName.Substring(0, fileinfo[j].strName.IndexOf("文件大小"));

                            fileinfo[j].strSize = strMatch.Substring(strMatch.IndexOf("文件大小") + 5);
                            fileinfo[j].strSize = fileinfo[j].strSize.Substring(0, fileinfo[j].strSize.IndexOf("分享者"));

                            fileinfo[j].strOwner = strMatch.Substring(strMatch.IndexOf("分享者") + 4);
                            fileinfo[j].strOwner = fileinfo[j].strOwner.Substring(0, fileinfo[j].strOwner.IndexOf("分享时间"));

                            fileinfo[j].strTime = strMatch.Substring(strMatch.IndexOf("分享时间") + 5);
                            fileinfo[j].strTime = fileinfo[j].strTime.Substring(0, fileinfo[j].strTime.IndexOf("下载次数"));

                            fileinfo[j].strCount = strMatch.Substring(strMatch.IndexOf("下载次数") + 5);
                            MatchCollection match2 = Regex.Matches(fileinfo[j].strCount, "[0-9]+"); try
                            {
                                if (match2[0].Success)
                                {
                                    fileinfo[j].strCount = match2[0].Value;
                                }
                                else
                                    fileinfo[j].strCount = "...";
                            }
                            catch (Exception ex)
                            {
                                // MessageBox.Show(ex.Message);
                            }
                            if (fileinfo[j].strSize == "-")
                            {
                                fileinfo[j].strRemark = "文件夹";
                            }
                            else
                            {
                                if (IsValideMethod(fileinfo[j].strDownloadUrl) == false)
                                    fileinfo[j].strRemark = "该资源已失效";
                                else
                                    fileinfo[j].strRemark = " ";
                            }
                            if (fileinfocompare[0].strOwner == fileinfo[j].strOwner && fileinfocompare[0].strTime == fileinfo[j].strTime)
                            {
                                IsSame = false;
                            }
                            else
                            {
                                fileinfocompare[i] = fileinfo[j];
                                //fileinfo[i].strSize = fileinfo[i].strSize.Substring(0, fileinfo[i].strSize.IndexOf("分享者"));
                                listView1.Invoke(searchResultCallBack, index, fileinfo[j]);
                                //Trace.WriteLine(strMatch);
                            }

                        }
                    } htmlCompare = html;
                    page = page + 10;
                    string strUrl1 = string.Format(@"http://www.baidu.com/s?wd=site%3Apan.baidu.com%20{0}&pn={1}&ie=utf-8", tx_keyword.Text, page);
                    html = client.DownloadString(strUrl1);
                }
            } //锁        

        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            //RegexMatch();
            searchResultCallBack = new SearchResultCallBack(SearchResultMethod);
            Thread[] SearchResultThread = new Thread[5];
            for (int p = 0; p < 1; p++)
            {
                SearchResultThread[p] = new Thread(RegexMatchResult);
                SearchResultThread[p].Start();
            }
            //SearchResultThread.Start();
        }

        //右击打开资源地址事件
        [DllImport("shell32.dll")]
        public static extern int ShellExecute(IntPtr hwnd, StringBuilder lpszOp, StringBuilder lpszFile, StringBuilder lpszParams, StringBuilder lpszDir, int FsShowCmd);
        private void 打开资源地址ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count > 0)
            {
                // 转到下载页ToolStripMenuItem.Visible = true;
                int index = listView1.SelectedIndices[0];//选中第index+1行
                ShellExecute(IntPtr.Zero, new StringBuilder("Open"), new StringBuilder(fileinfo[index].strDownloadUrl), new StringBuilder(""), new StringBuilder(""), 1);

            }
        }

        private void BaiduYunSearch_Load(object sender, EventArgs e)
        {

        }

        private void BaiduYunSearch_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(Form1.f1 != null)
            {
                Form1.f1.Show();
                this.Hide();
                return;
            }
            Form1.f1 = new Form1();
            Form1.f1.Show();
            this.Hide();
        }
    }
}
