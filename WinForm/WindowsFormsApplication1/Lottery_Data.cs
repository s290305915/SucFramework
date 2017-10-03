using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace WindowsFormsApplication1
{
    public partial class Lottery_Data : Form
    {
        public Lottery_Data()
        {
            InitializeComponent();
        }
        bool isget = false;
        bool isover = false;
        WebBrowser web = new WebBrowser();
        List<string> all = new List<string>();
        List<Lottery> lotterys = new List<Lottery>();
        List<Lottery3D> l3ds = new List<Lottery3D>();
        string webcontent;
        void web_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser web = (WebBrowser)sender;
            HtmlElementCollection ElementCollection = web.Document.GetElementsByTagName("Table");
            foreach (HtmlElement item in ElementCollection)
            {
                webcontent += item.InnerHtml;
            }
            isover = true;
        }
        private void Lottery_Data_Load(object sender, EventArgs e)
        {
            //web.Navigate("http://datachart.500.com/ssq/history/newinc/history.php?start=99999&end=00000");  //获取所有数据
            ////http://datachart.500.com/ssq/history/newinc/history.php?limit=100&sort=0  //前100条
            ////http://datachart.500.com/ssq/history/newinc/history.php?start=99999&end=00000     //同第一条
            ////"http://datachart.500.com/ssq/history/history.shtml"  //默认50条
            //web.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(web_DocumentCompleted);
            List<string> lst = new List<string>() { "-请先选择-", "双色球", "福彩3D" };
            com_lot.DataSource = lst;
            com_lot.SelectedIndex = 0;
        }

        private void btn_get_Click(object sender, EventArgs e)
        {

            if (isget == false)
            {
                MessageBox.Show("请稍后，正在获取中！");
            }
            else
            {
                GetData();
            }
        }

        private void GetData()
        {
            if (isover == false)
            {
                MessageBox.Show("正在加载数据，请稍后！");
                return;
            }
            try
            {
                string str = webcontent; // sr.ReadToEnd();
                string[] oldlist;
                string content = "";
                if (is3d == true)
                {
                    str = str.Substring(str.IndexOf("奖金"));
                    str = str.Substring(str.IndexOf("<TR"));
                    content = str;
                    content = content.Substring(0, content.IndexOf("/TBODY>"));
                    content = content.Replace("\n", "").Replace("\t", "").Replace("\r", "").Replace("<!--<td>2</td>-->", "").Replace("&nbsp;", "");
                    oldlist = System.Text.RegularExpressions.Regex.Split(content, "TR");
                }
                else
                {
                    str = str.Substring(str.IndexOf("id=tdata"));
                    content = str;//.Substring(0, str.IndexOf("id=footer"));    //<TR class=t_tr1><!--<td>2</td>--> 分隔符
                    content = content.Substring(0, content.IndexOf("/TBODY>"));
                    content = content.Replace("\n", "").Replace("\t", "").Replace("\r", "").Replace("<!--<td>2</td>-->", "").Replace("&nbsp;", "");
                    oldlist = System.Text.RegularExpressions.Regex.Split(content, "t_tr1");
                }
                foreach (string s in oldlist)
                {
                    if (s.Length > 100)
                        all.Add(s.Trim());
                }
                //all.RemoveRange(100, all.Count - 100);
                if (all.Count() <= 0)
                {
                    MessageBox.Show("没有获取到数据！");
                    return;
                }
                ContentProc();
            }
            catch { }
        }

        private void ContentProc()
        {
            string str = "";
            string str1 = "";
            switch (com_lot.SelectedIndex)
            {
                case 1:
                    try
                    {
                        foreach (string sourcestr in all)
                        {
                            try
                            {
                                Lottery lot = new Lottery();
                                str = sourcestr;
                                str1 = HandTD(str);
                                lot.unit = Convert.ToInt32(HandOutTD(str1, false));    //期数
                                str = str.Replace(str1, "");
                                str1 = HandTD(str);
                                lot.red1 = Convert.ToInt32(HandOutTD(str1));
                                str = str.Replace(str1, "");
                                str1 = HandTD(str);
                                lot.red2 = Convert.ToInt32(HandOutTD(str1));
                                str = str.Replace(str1, "");
                                str1 = HandTD(str);
                                lot.red3 = Convert.ToInt32(HandOutTD(str1));
                                str = str.Replace(str1, "");
                                str1 = HandTD(str);
                                lot.red4 = Convert.ToInt32(HandOutTD(str1));
                                str = str.Replace(str1, "");
                                str1 = HandTD(str);
                                lot.red5 = Convert.ToInt32(HandOutTD(str1));
                                str = str.Replace(str1, "");
                                str1 = HandTD(str);
                                lot.red6 = Convert.ToInt32(HandOutTD(str1));
                                str = str.Replace(str1, "");
                                str1 = HandTD(str);
                                lot.blue = Convert.ToInt32(HandOutTD(str1));
                                //取时间
                                if (str.Length > 200)
                                    str = str.Substring(0, str.IndexOf("</TBODY"));
                                str = str.Substring(str.LastIndexOf("<TD"));
                                lot.date = Convert.ToDateTime(HandOutTD(str, false));


                                lotterys.Add(lot);
                                isget = true;
                            }
                            catch (Exception e) { }
                        }
                    }
                    catch (Exception e) { }
                    #region 双色球
                    dgv_lottery.DataSource = lotterys;
                    dgv_lottery.Columns[0].HeaderText = "期数";
                    dgv_lottery.Columns[1].HeaderText = "开奖日期";
                    dgv_lottery.Columns[2].HeaderText = "红1";
                    dgv_lottery.Columns[3].HeaderText = "红2";
                    dgv_lottery.Columns[4].HeaderText = "红3";
                    dgv_lottery.Columns[5].HeaderText = "红4";
                    dgv_lottery.Columns[6].HeaderText = "红5";
                    dgv_lottery.Columns[7].HeaderText = "红6";
                    dgv_lottery.Columns[8].HeaderText = "蓝球";
                    dgv_lottery.Columns[9].HeaderText = "红色遗漏";
                    dgv_lottery.Columns[10].HeaderText = "蓝色遗漏";
                    dgv_lottery.Columns[0].Width = 80;
                    dgv_lottery.Columns[1].Width = 80;
                    dgv_lottery.Columns[2].Width = 50;
                    dgv_lottery.Columns[3].Width = 50;
                    dgv_lottery.Columns[4].Width = 50;
                    dgv_lottery.Columns[5].Width = 50;
                    dgv_lottery.Columns[6].Width = 50;
                    dgv_lottery.Columns[7].Width = 50;
                    dgv_lottery.Columns[8].Width = 80;
                    dgv_lottery.Columns[9].Width = 80;
                    dgv_lottery.Columns[10].Width = 80;
                    #endregion
                    break;
                case 2:
                    foreach (string sourcestr in all)
                    {
                        try
                        {
                            Lottery3D l3d = new Lottery3D();
                            str = sourcestr;
                            str1 = HandTD(str);
                            l3d.unit = Convert.ToInt32(HandOutTD(str1, false));
                            str = str.Replace(str1, "");
                            str1 = HandTD(str);
                            l3d.number = HandOutTD(str1);
                            str = str.Replace(str1, "");
                            str1 = HandTD(str);
                            l3d.addtion = Convert.ToInt32(HandOutTD(str1));
                            str1 = str.Substring(str.LastIndexOf("<TD"));
                            l3d.date = Convert.ToDateTime(HandOutTD(str1, false));
                            string[] numbers = l3d.number.Split(' ');
                            l3d.number1 = Convert.ToInt32(numbers[0]);
                            l3d.number2 = Convert.ToInt32(numbers[1]);
                            l3d.number3 = Convert.ToInt32(numbers[2]);
                            l3ds.Add(l3d);
                        }
                        catch (Exception ex)
                        {
                            string mesg = "'" + sourcestr + "'";
                            continue;
                        }
                    }
                    dgv_lottery.DataSource = l3ds;
                    dgv_lottery.Columns[0].HeaderText = "期数";
                    dgv_lottery.Columns[1].HeaderText = "开奖号";
                    dgv_lottery.Columns[2].HeaderText = "和值";
                    dgv_lottery.Columns[3].HeaderText = "时间";
                    dgv_lottery.Columns[4].HeaderText = "球1";
                    dgv_lottery.Columns[5].HeaderText = "球2";
                    dgv_lottery.Columns[6].HeaderText = "球3";
                    dgv_lottery.Columns[0].Width = 80;
                    dgv_lottery.Columns[1].Width = 80;
                    dgv_lottery.Columns[2].Width = 80;
                    dgv_lottery.Columns[3].Width = 80;
                    dgv_lottery.Columns[4].Width = 50;
                    dgv_lottery.Columns[5].Width = 50;
                    dgv_lottery.Columns[6].Width = 50;
                    break;
            }
        }

        private string HandTD(string s)
        {
            if (is3d)
                return s.Substring(s.IndexOf("<TD"), s.IndexOf("</TD>") - 7);
            return s.Substring(s.IndexOf("<TD"), s.IndexOf("</TD>") + 4);
        }
        private string HandOutTD(string s, bool ball = true)
        {
            if (!ball)
            {
                if (s.Contains("t_tr1"))
                    s = s.Replace("<TD class=t_tr1", "<TD");
                s = s.Substring(4);
            }
            else
                s = s.Contains("t4>") ? s.Substring(s.IndexOf("t4>") + 3) : s.Substring(s.IndexOf("t2>") + 3);
            s = s.Substring(0, s.IndexOf("</T"));
            return s;
        }

        private void Lottery_Data_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Form1.f1 != null)
            {
                Form1.f1.Show();
                this.Hide();
                return;
            }
        }

        private void btn_export_Click(object sender, EventArgs e)
        {
            if (!isget)
            {
                MessageBox.Show("请先获取数据，再行导出！");
                return;
            }
            string isch = "";
            string[] chs;
            try
            {
                foreach (Control c in this.Controls)
                {
                    if (c is CheckBox)
                    {
                        CheckBox cb = c as CheckBox;
                        if (cb.Checked == true)
                            isch += cb.Name + ",";
                    }
                }
                if (isch.Length < 2)
                {
                    MessageBox.Show("请先选择要导出的项！");
                    return;
                }
                isch = isch.Substring(0, isch.Length - 1).Replace("chk_", "");
                chs = isch.Split(',');
                if (lotterys.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (Lottery lot in lotterys)
                    {
                        sb.Append(lot.date + " ");
                        switch (chs[0])
                        {
                            case "red1":
                                sb.Append(lot.red1 + "\r\n");
                                break;
                            case "red2":
                                sb.Append(lot.red2 + "\r\n");
                                break;
                            case "red3":
                                sb.Append(lot.red3 + "\r\n");
                                break;
                            case "red4":
                                sb.Append(lot.red4 + "\r\n");
                                break;
                            case "red5":
                                sb.Append(lot.red5 + "\r\n");
                                break;
                            case "red6":
                                sb.Append(lot.red6 + "\r\n");
                                break;
                            case "blue":
                                sb.Append(lot.blue + "\r\n");
                                break;
                        }
                    }
                    if (File.Exists(chs[0] + ".txt"))
                        File.Delete(chs[0] + ".txt");
                    FileStream fs = new FileStream(chs[0] + ".txt", FileMode.Create);
                    StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                    sw.Write(sb.ToString());
                    sw.Flush();
                    MessageBox.Show("导出完成！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public int chkcount = 0;
        private void chk_date_CheckedChanged(object sender, EventArgs e)
        {
            chk_date.Checked = true;
            CheckBox chk = sender as CheckBox;
            if (chkcount >= 2)
            {
                MessageBox.Show("只能选择日期和其中一列！");
                chk.Checked = false;
            }
            if (chk.CheckState == CheckState.Checked)
                chkcount += 1;
            else
                chkcount -= 1;
        }

        private void btn_excel_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            //return;
            if (!is3d)
            {
                dt = ToDataTable(lotterys);
                Lottery.BulkCopy("Lottery_Data", dt);
            }
            else
            {
                dt = ToDataTable(l3ds);
                Lottery.BulkCopy("[3D]", dt);
            }
            string filetime = DateTime.Now.ToString("yyyyMMddmm");
            string _webpath = AppDomain.CurrentDomain.BaseDirectory + "\\Excel\\Lottery";
            if (!Directory.Exists(_webpath))
                Directory.CreateDirectory(_webpath);
            string filename = "Lottery_" + filetime + ".xls";
            string path = _webpath + "\\" + filename;
            try
            {
                MemoryStream ms = new MemoryStream();
                IWorkbook workbook = new HSSFWorkbook();
                ISheet sheet = workbook.CreateSheet("数据1");
                //CellRangeAddress四个参数为：起始行，结束行，起始列，结束列
                IRow row1 = sheet.CreateRow(0);

                row1.CreateCell(0).SetCellValue("AA");
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        IRow row2 = sheet.CreateRow(i);
                        for (int j = 0; j < dt.Columns.Count; j++)
                            row2.CreateCell(j).SetCellValue(dt.Rows[i][j].ToString());
                    }
                }
                workbook.Write(ms);
                ms.Position = 0;
                ms.Close();
                ms.Flush();
                MusicPlayer.SaveToFile(ms, path);
                MessageBox.Show("导出完成！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 将List转换成DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static DataTable ToDataTable(IList<Lottery> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(Lottery));
            DataTable dt = new DataTable();
            for (int i = 0; i < properties.Count; i++)
            {
                PropertyDescriptor property = properties[i];
                dt.Columns.Add(property.Name, property.PropertyType);
            }
            object[] values = new object[properties.Count];
            foreach (Lottery item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = properties[i].GetValue(item);
                }
                dt.Rows.Add(values);
            }
            return dt;
        }
        /// <summary>
        /// 将List转换成DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static DataTable ToDataTable(IList<Lottery3D> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(Lottery3D));
            DataTable dt = new DataTable();
            for (int i = 0; i < properties.Count; i++)
            {
                PropertyDescriptor property = properties[i];
                dt.Columns.Add(property.Name, property.PropertyType);
            }
            object[] values = new object[properties.Count];
            foreach (Lottery3D item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = properties[i].GetValue(item);
                }
                dt.Rows.Add(values);
            }
            return dt;
        }
        bool is3d = false;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            webcontent = "";
            dgv_lottery.DataSource = null;
            string url = "";
            try
            {
                switch (com_lot.SelectedIndex)
                {
                    case 1:
                        is3d = false;
                        url = "http://datachart.500.com/ssq/history/newinc/history.php?start=99999&end=00000";
                        break;
                    case 2:
                        is3d = true;
                        url = "http://datachart.500.com/sd/history/inc/history.php?limit=1000000";
                        break;
                    default:
                        isget = false;
                        isover = false;
                        return;
                }
                isget = true;
                Task.Factory.StartNew(new Action(() =>
                {
                    this.Invoke(new Action(() =>
                    {
                        web.Navigate(url);
                        web.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(web_DocumentCompleted);
                    }));
                }));

                if (is3d == true)
                {
                    chk_blue.Enabled = false;
                    chk_red4.Enabled = false;
                    chk_red5.Enabled = false;
                    chk_red6.Enabled = false;
                }
                else
                {
                    chk_blue.Enabled = true;
                    chk_red4.Enabled = true;
                    chk_red5.Enabled = true;
                    chk_red6.Enabled = true;
                }
            }
            catch (Exception ex)
            { }
        }
    }

    public class Lottery
    {
        /// <summary>
        /// 期数
        /// </summary>
        public int unit
        {
            get;
            set;
        }
        /// <summary>
        /// 开奖日期
        /// </summary>
        public DateTime date
        {
            get;
            set;
        }
        /// <summary>
        /// 红球1
        /// </summary>
        public int red1
        {
            get;
            set;
        }
        /// <summary>
        /// 红球2
        /// </summary>
        public int red2
        {
            get;
            set;
        }
        /// <summary>
        /// 红球3
        /// </summary>
        public int red3
        {
            get;
            set;
        }
        /// <summary>
        /// 红球4
        /// </summary>
        public int red4
        {
            get;
            set;
        }
        /// <summary>
        /// 红球5
        /// </summary>
        public int red5
        {
            get;
            set;
        }
        /// <summary>
        /// 红球6
        /// </summary>
        public int red6
        {
            get;
            set;
        }
        /// <summary>
        /// 篮球
        /// </summary>
        public int blue
        {
            get;
            set;
        }
        /// <summary>
        /// 红色遗漏
        /// </summary>
        public string redmiss
        {
            get;
            set;
        }
        /// <summary>
        /// 蓝色遗漏
        /// </summary>
        public string bluemiss
        {
            get;
            set;
        }


        /// <summary>
        /// 大批量插入数据(2000每批次)
        /// 已采用整体事物控制
        /// </summary>
        /// <param name="tableName">数据库服务器上目标表名</param>
        /// <param name="dt">含有和目标数据库表结构完全一致(所包含的字段名完全一致即可)的DataTable</param>
        public static void BulkCopy(string tableName, DataTable dt)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = string.Format(@"DELETE FROM {0}", tableName);
                    cmd.Transaction = transaction;
                    cmd.ExecuteNonQuery();
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.Default, transaction))
                    {
                        bulkCopy.BatchSize = 200000;
                        bulkCopy.BulkCopyTimeout = 3000;
                        bulkCopy.DestinationTableName = tableName;
                        try
                        {
                            foreach (DataColumn col in dt.Columns)
                            {
                                bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                            }
                            bulkCopy.WriteToServer(dt);
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw ex;
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }
                }
            }
        }
    }

    public class Lottery3D
    {
        public int unit { get; set; }
        public string number { get; set; }
        public int addtion { get; set; }
        public DateTime date { get; set; }
        public int number1 { get; set; }
        public int number2 { get; set; }
        public int number3 { get; set; }
    }
}
