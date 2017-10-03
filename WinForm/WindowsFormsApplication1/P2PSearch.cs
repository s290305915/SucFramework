
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class P2PSearch : Form
    {
        int pageNum = 0;

        bool isrun = false;
        Boolean isFirst = true;
        String keyword;
        Thread myThread;
        //五个list分别存储：整个文本、名称、链接、大小、时间/格式
        List<string> list1 = new List<string>();
        List<string> list2 = new List<string>();
        List<string> list3 = new List<string>();
        List<string> list4 = new List<string>();
        List<string> list5 = new List<string>();
        Uri url;
        Stopwatch myWatch;
        public P2PSearch()
        {
            InitializeComponent();
            //this.skinEngine1.SkinFile = "DiamondBlue.ssk";
        }

        private void P2PSearch_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Form1.f1 != null)
            {
                Form1.f1.Show();
            }
        }

        private void P2PSearch_Load(object sender, EventArgs e)
        {
            btn_cancel.Enabled = false;
            tx_page.Text = "0";
            this.Left = (Screen.PrimaryScreen.WorkingArea.Width - Width) / 2;
            this.Top = (Screen.PrimaryScreen.WorkingArea.Height - Height) / 2;
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            tx_page.Text = "0";
            try
            {
                isFirst = true;
                if (!isrun)
                {
                    isrun = !isrun;
                }
                else
                {
                    throw new Exception("搜索正在进行中,请稍等！┗|*｀0′*|┛ ");
                }
                if (string.IsNullOrEmpty(tx_search.Text))
                {
                    throw new Exception("请先输入你要搜索的关键字！(*￣ω￣)");
                }
                btn_search.Enabled = false;
                btn_cancel.Enabled = true;
                keyword = tx_search.Text;
                tx_source.Text = "";
                lv_data.Items.Clear();
                pageNum = 0;
                list1 = new List<string>();
                list2 = new List<string>();
                list3 = new List<string>();
                list4 = new List<string>();
                list5 = new List<string>();

                myWatch = Stopwatch.StartNew();
                timer1.Start();

                myThread = new Thread(new ThreadStart(updateUI));
                myThread.Start();
            }
            catch (Exception ex)
            {
                Endrun(ex.Message);
            }
        }
        /// <summary>
        /// 进度条
        /// </summary>
        /// <param name="start"></param>
        /// <param name="prg"></param>
        private void Gopro(bool start, int prg = 1)
        {
            if (start)
            {
                while (prg <= 100)
                {
                    this.pro_search.Value = prg;
                    prg++;
                }
                Thread.Sleep(100);
            }
            else
            {
                pro_search.Value = 100;
            }
        }

        private void updateUI()
        {
            Task Tpro = Task.Factory.StartNew(() =>
            {
                this.Invoke(new Action(() =>
                {
                    Gopro(true, 1);
                }));
            });
            this.Invoke(new Action(() =>
            {
                tx_page.Enabled = false;
                tx_page.Text = pageNum + ""; //翻页功能未实现
            }));

            try
            {
                url = new Uri("http://www.torrentkitty.la/tk/" + keyword + "/1-" + pageNum + ".html");

                HttpWebRequest request = null;
                HttpWebResponse response = null;
                try
                {
                    request = (HttpWebRequest)WebRequest.Create(url);
                    request.UserAgent = ".NET Framework Test Client";
                    //request.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0; QQWubi 133; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; CIBA; InfoPath.2)";

                    response = (HttpWebResponse)request.GetResponse();
                }
                catch (WebException ex)
                {
                    //request = (HttpWebRequest)WebRequest.Create(url);
                    //request.UserAgent = ".NET Framework Test Client";
                    ////request.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0; QQWubi 133; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; CIBA; InfoPath.2)";

                    //response = (HttpWebResponse)request.GetResponse();
                }
                Stream stream = response.GetResponseStream();
                StreamReader sr = new StreamReader(stream);
                string str = sr.ReadToEnd();
                try
                {
                    str = str.Substring(str.IndexOf("<div class=\"list-area\">"), str.IndexOf("<div class=\"hot_keywords\">"));
                    str = str.Replace("\n", "").Replace("\t", "");    //去制表符回车换行等   .Replace("\r", "")
                    str = str.Replace("'+'", "").Replace("<b>", "").Replace(@"</b>", "");
                    str = str.Replace("attr p1", "attr p0");//.Replace("</p><div class=\"related\">", "<div class=\"related\">");
                    str = str.Replace("dt p1", "dt p0");
                }
                catch (Exception e)
                {
                    throw new Exception("处理获取到的信息失败！请检查关键字！~~(╯﹏╰)b");
                }
                string[] ordlist;
                sr.Close();
                stream.Close();
                response.Close();

                //算法实现 

                ordlist = System.Text.RegularExpressions.Regex.Split(str, "dt p0");

                foreach (String s in ordlist)
                {
                    list1.Add(s);
                }
                //htmlCode = str;
                MainProc();
                if (list1.Count == 0)
                {
                    if (isFirst)
                    {
                        throw new Exception("未搜索到任何信息，请切换关键字！ W(￣_￣)W");
                    }
                    myThread.Abort();
                }


                for (int k = 0; k < list2.Count; k++)
                {

                    ListViewItem listview = new ListViewItem("" + k);
                    listview.SubItems.Add(list2[k]);
                    listview.SubItems.Add(list4[k]);
                    listview.SubItems.Add(list5[k]);
                    listview.SubItems.Add(list3[k]);
                    this.Invoke(new Action(() =>
                    {
                        lv_data.Items.Add(listview);
                    }));
                }
                //myThread.Abort();
            }
            catch (Exception ex)
            {
                Endrun(ex.Message);
            }
            finally
            {
                Tpro.Dispose();
                Task.Factory.StartNew(() =>
                {
                    this.Invoke(new Action(() =>
                    {
                        Gopro(false);
                    }));
                });
                Endrun();
            }
        }

        private void MainProc()
        {
            try
            {
                foreach (string sourcestr in list1)
                {
                    string str = sourcestr;
                    string targetstr = string.Empty;
                    if (str.Length > 100)
                    {
                        if (str.Contains("list-area"))
                            continue;
                        //先去空格
                        str = str.Replace("&nbsp;", "");
                        //取汉字
                        targetstr = str.Substring(0, str.IndexOf("</a>"));
                        targetstr = targetstr.Substring(targetstr.LastIndexOf(">") + 1);
                        list2.Add(targetstr);
                        //名字取完 取链接
                        targetstr = str.Trim().Substring(0, str.LastIndexOf(">[磁力"));
                        targetstr = targetstr.Substring(0, targetstr.Length - 1);
                        targetstr = targetstr.Substring(targetstr.LastIndexOf("magnet"));
                        list3.Add(targetstr);
                        //取完链接取大小
                        targetstr = str.Trim().Substring(str.IndexOf("磁力"));
                        targetstr = targetstr.Substring(targetstr.IndexOf("<span>"));
                        string am1 = targetstr.Substring(0, targetstr.IndexOf("</span>"));
                        am1 = GetString("", am1);
                        targetstr = targetstr.Substring(targetstr.IndexOf(am1));
                        targetstr = targetstr.Substring(targetstr.IndexOf("<span>"));
                        string am2 = targetstr.Substring(0, targetstr.IndexOf("</span>"));
                        am2 = GetString("", am2);
                        list4.Add(am1 + "," + am2);
                        //最后放上日期

                        targetstr = targetstr.Substring(targetstr.IndexOf(am2));
                        targetstr = targetstr.Substring(targetstr.IndexOf("<span>"));
                        targetstr = targetstr.Substring(0, targetstr.IndexOf("</span>"));
                        targetstr = GetString("", targetstr);
                        list5.Add(targetstr);
                    }
                }
            }
            catch (Exception ex)
            {
                //果断吞了 
            }
        }

        public void Endrun(string message = "搜索完成！")
        {
            this.Invoke(new Action(() =>
            {
                tx_page.Enabled = true;
            }));
            isrun = false;
            myWatch.Stop();
            timer1.Stop();
            this.Invoke(new Action(() =>
            {
                lb_runtime.Text = "总运行：" + myWatch.Elapsed;
            }));
            if (isFirst)
                this.Invoke(new Action(() =>
                {
                    MessageBox.Show(this, message);
                }));
            this.Invoke(new Action(() =>
            {
                btn_search.Enabled = true;
                btn_cancel.Enabled = false;
            }));
            if (myThread.IsAlive)
                myThread.Abort();

            return;
        }

        /// <summary>
        /// 仅获取汉字
        /// </summary>
        /// <param name="mixstring"></param>
        /// <returns></returns>
        public string GetString(string mixstring, string am = "")
        {
            int currentcode = -1;
            string source = mixstring;
            string output = "";
            if (!string.IsNullOrEmpty(am))
            {
                //过滤掉<span>
                source = am;
                output = source.Substring(source.LastIndexOf('>') + 1);
                return output;
            }
            for (int i = 0; i < source.Length; i++)
            {
                currentcode = (int)source[i];
                if (currentcode > 19968 && currentcode < 40869)
                    output += source[i];

            }
            return output;
        }

        private void lv_data_SelectedIndexChanged(object sender, EventArgs e)
        {
            tx_source.Text = "";
            if (lv_data.SelectedItems.Count > 0)
            {
                try
                {
                    string Dsource = lv_data.SelectedItems[0].SubItems[4].Text;
                    this.Invoke(new Action(() =>
                    {
                        tx_source.Text = "链接地址：\"" + Dsource;
                    }));
                    Clipboard.SetDataObject(Dsource.Substring(0, Dsource.Length - 1));
                    MessageBox.Show(this, "链接地址已复制到剪贴板 (⊙v⊙)！");
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            try
            {
                if (!isrun)
                {
                    isrun = !isrun;
                    if (lv_data.Items.Count <= 0)
                    {
                        MessageBox.Show("当前没有进行搜索呢,请先搜索好伐 ののののののの！");
                        return;
                    }
                    isFirst = false;
                }
                else
                {
                    MessageBox.Show("搜索正在进行中,请稍等 ののののののの！");
                    return;
                }
                #region 勿用代码
                btn_cancel.Enabled = true;
                list1 = new List<string>();
                list2 = new List<string>();
                list3 = new List<string>();
                list4 = new List<string>();
                list5 = new List<string>();
                lv_data.Items.Clear();
                tx_source.Text = "";
                pageNum++;
                myThread = new Thread(new ThreadStart(updateUI));
                timer1.Start();
                myWatch.Start();
                myThread.Start();
                #endregion
            }
            catch (Exception ex)
            {
                Endrun(ex.Message);
            }
        }

        private void btn_reve_Click(object sender, EventArgs e)
        {
            if (!isrun)
            {
                if (lv_data.Items.Count <= 0)
                {
                    MessageBox.Show("当前没有进行搜索呢,请先搜索好伐 ののののののの！");
                    return;
                }
                isrun = !isrun;
            }
            else
            {
                MessageBox.Show("搜索正在进行中,请稍等 ののののののの！");
                return;
            }
            btn_cancel.Enabled = true;
            list1 = new List<string>();
            list2 = new List<string>();
            list3 = new List<string>();
            list4 = new List<string>();
            list5 = new List<string>();
            lv_data.Items.Clear();

            tx_source.Text = "";
            if (pageNum > 0)
            {
                pageNum--;
                myThread = new Thread(new ThreadStart(updateUI));
                timer1.Start();
                myWatch.Start();
                myThread.Start();
            }
            else
            {
                isFirst = true;
                MessageBox.Show(this, "已经第一页了 (O_O)?");
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            btn_cancel.Enabled = false;
            Endrun();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                lb_runtime.Text = "已运行:" + myWatch.Elapsed.ToString();
            }
            catch (Exception ex)
            { }
        }

        private void export_Click(object sender, EventArgs e)
        {
            string xp_txt = "";
            foreach (ListViewItem item in lv_data.Items)
            {
                xp_txt += "编号：" + item.SubItems[0].Text + "\r\n";
                xp_txt += "\n\t\t资源名称：" + item.SubItems[1].Text + "\r\n";
                xp_txt += "\n\t\t文件数\\大小：" + item.SubItems[2].Text + "\r\n";
                xp_txt += "\n\t\t发布时间：" + item.SubItems[3].Text + "\r\n";
                xp_txt += "\n\t\t链接地址：" + item.SubItems[4].Text + "\r\n\r\n";
            }
            string localFilePath;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "txt files(*.txt)|*.txt";
            sfd.RestoreDirectory = true;
            DialogResult result = sfd.ShowDialog();
            if (result == DialogResult.OK)
            {
                System.IO.FileStream fs;
                localFilePath = sfd.FileName.ToString();
                fs = (System.IO.FileStream)sfd.OpenFile();
                //fs = new FileStream(localFilePath, FileMode.Create, FileAccess.ReadWrite);
                StreamWriter sw;
                sw = new StreamWriter(fs);
                sw.Write(xp_txt);
                fs.Flush();
                sw.Close();
                fs.Close();
            }
            MessageBox.Show(this, "导出数据完成！ O(∩_∩)O~~");
        }

        private void tx_page_TextChanged(object sender, EventArgs e)
        {

        }

        private void tx_page_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    pageNum = Convert.ToInt32(tx_page.Text.Trim());
                }
                catch { MessageBox.Show(this, "不能输入奇怪的字符 (0_0)!!"); }
                if (!isrun)
                {
                    if (lv_data.Items.Count <= 0)
                    {
                        return;
                    }
                    isrun = !isrun;
                }
                else
                {
                    return;
                }
                btn_cancel.Enabled = true;
                list1 = new List<string>();
                list2 = new List<string>();
                list3 = new List<string>();
                list4 = new List<string>();
                list5 = new List<string>();
                lv_data.Items.Clear();

                tx_source.Text = "";
                if (pageNum >= 0)
                {
                    myThread = new Thread(new ThreadStart(updateUI));
                    timer1.Start();
                    myWatch.Start();
                    myThread.Start();
                }
                else
                {
                    isFirst = true;
                    MessageBox.Show(this, "页码不能穿越 (O_O)?");
                }
            }
        }
    }
}
