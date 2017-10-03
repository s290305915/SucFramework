using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeGeneration
{
    public partial class Form2 : Form
    {
        bool isstart = false;
        bool isret = false;
        public List<string> tbnames;
        public string connection;
        string BaseFloder = "D:\\Code";
        //Data Source=.;Initial Catalog=SUCMSF;User ID=sa;Pwd=suchi12345
        public Form2(string db, List<string> tbs, string add, string uname, string pass,string conn)
        {
            InitializeComponent();
            //la_tb.Text = @"当前数据库：" + db +" 当前选择的表：" + string.Join(",", tbs.ToArray());
            lb_tbs.Text = "当前数据库：" + db;
            lbx_tbs.DataSource = tbs;
            tbnames = tbs;
            connection = conn;// string.Format(@"Data Source={0};Initial Catalog={1};User ID={2};Pwd={3}", add, db, uname, pass);
        }

        private void lbx_tbs_SelectedIndexChanged(object sender, EventArgs e)
        {
            toollb_tbname.Text = lbx_tbs.SelectedItem.ToString();
        }

        private void lbx_tbs_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Clipboard.SetDataObject(lbx_tbs.SelectedItem.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            isret = true;
            DialogResult res = MessageBox.Show(this, "确定要返回到上一步？", "提示", MessageBoxButtons.OKCancel);
            if(res == DialogResult.OK)
            {
                if(Form1.f1 != null)
                {
                    Form1.f1.Show();
                    this.Close();
                    return;
                }
                Form1.f1 = new Form1();
                Form1.f1.Show();
                this.Close();
            }
            else
                isret = false;
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(isstart)
            {
                MessageBox.Show("正在生成文件，请稍候！");
                e.Cancel = true;
                return;
            }
            if(!isret)
            {
                DialogResult res = MessageBox.Show(this, "点YES关闭此窗口，点NO退出程序", "提示", MessageBoxButtons.YesNoCancel);
                switch(res)
                {
                    case DialogResult.Yes:
                        if(Form1.f1 != null)
                        {
                            Form1.f1.Show();
                        }
                        break;
                    case DialogResult.No:
                        Application.ExitThread();
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
        }

        private void btn_floder_Click(object sender, EventArgs e)
        {
            dia_savefloder.ShowNewFolderButton = true;
            var dialogresult = dia_savefloder.ShowDialog();
            if(dialogresult == DialogResult.OK)
            {
                BaseFloder = dia_savefloder.SelectedPath;
                la_floder.Text = BaseFloder;
            }
            else
            {
                la_floder.Text += "窗口打开失败，默认文件夹：" + BaseFloder + "\\";
            }
        }

        private void btn_genarel_Click(object sender, EventArgs e)
        {
            if(!Directory.Exists(BaseFloder))
            {
                Directory.CreateDirectory(BaseFloder);
            }
            if(string.IsNullOrEmpty(BaseFloder))
            {
                MessageBox.Show("请先选择文件夹！");
                return;
            }
            if(string.IsNullOrEmpty(tx_namespc.Text.Trim()))
            {
                MessageBox.Show("请先输入命名空间！");
                return;
            }
            DataTable dt = new DataTable();
            foreach(string s in tbnames)
            {
                Task task = new Task(() => {


                });
                toollb_tbname.Text = s;
                la_curr.Text = "当前执行的表：" + s;
                dt = GetDataSet("EXEC sp_help {0}", s).Tables[1];
                dgv_fields.DataSource = dt;
                GenaralInfo g = new GenaralInfo()
                {
                    floder = BaseFloder,
                    tablename = s,
                    content = GenaralCodeText(dt, s)
                };
                ThreadPool.QueueUserWorkItem(new WaitCallback(WriteIntoFile), g);
                //WriteIntoFile(BaseFloder, s, GenaralCodeText(dt, s));
            }
            MessageBox.Show("生成成功！");
            System.Diagnostics.Process.Start("explorer.exe", BaseFloder);
        }

        #region StepOne BuildText

        public string GenaralCodeText(DataTable dt, string tbname)
        {
            string nap = tx_namespc.Text.Trim();
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("using System;\r\n");
            sb.AppendFormat("using System.Collections.Generic;\r\n");
            sb.AppendFormat("using System.Data;\r\n");
            sb.AppendFormat("using SucLib.Core;\r\n");
            sb.AppendFormat("using SucLib.Data.Factory;\r\n");
            sb.AppendFormat("using SucLib.Data.IDal;\r\n");//using
            sb.AppendFormat("namespace {0}\r\n", nap);
            sb.AppendFormat("{{\r\n");
            sb.AppendFormat("    /// <summary>\r\n");
            sb.AppendFormat("    /// {0}:实体类(属性说明自动提取数据库字段的描述信息) \r\n", nap);
            sb.AppendFormat("    /// </summary>\r\n");
            sb.AppendFormat("    [Serializable]\r\n");
            sb.AppendFormat("    [DataMap(TableName = \"{0}\")]\r\n", tbname);
            sb.AppendFormat("    public partial class {0} : DataBase<{0}>{{\r\n", tbname);
            sb.AppendFormat("       public {0}()\r\n", tbname);
            sb.AppendFormat("       {{ }}\r\n\r\n");
            sb.AppendFormat("    #region Model\r\n");

            foreach(DataRow dr in dt.Rows)
            {
                sb.AppendFormat("    private {1} _{0};\r\n", dr[0].ToString().ToLower(), GetType(dr[1].ToString()));
                sb.AppendFormat("    [DataMap(Column = \"{0}\")]\r\n", dr[0].ToString());
                sb.AppendFormat("    public {1} {0} \r\n", dr[0].ToString(), GetType(dr[1].ToString()));
                sb.AppendFormat("    {{ \r\n");
                sb.AppendFormat("     set {{ _{0} = value; }}\r\n", dr[0].ToString().ToLower());
                sb.AppendFormat("     get {{ return _{0}; }}\r\n", dr[0].ToString().ToLower());
                sb.AppendFormat("    }}\r\n\r\n", dr[0].ToString().ToLower());
            }
            sb.AppendFormat("   #endregion\r\n");
            sb.AppendFormat("   #region AutoMethod\r\n");
            sb.AppendFormat("     IDBHelp db = DBFactory.Create(); //实例化工厂\r\n");
            sb.AppendFormat("    public IList<{0}> Find(string Sql, params object[] args) \r\n", tbname);
            sb.AppendFormat("    {{\r\n");
            sb.AppendFormat("       return Find(string.Format(Sql, args));\r\n");
            sb.AppendFormat("    }}\r\n\r\n");
            sb.AppendFormat("    public IList<{0}> Find(string Sql) \r\n", tbname);
            sb.AppendFormat("    {{\r\n");
            sb.AppendFormat("        Sql = string.IsNullOrEmpty(Sql) ? \"\" : \" AND \" + Sql;\r\n");
            sb.AppendFormat("        DataTable dt = db.GetDataTable(\"SELECT * FROM {0} WHERE 1=1 \" + Sql);\r\n", tbname);
            sb.AppendFormat("        return EntityModel.ConvertTo<{0}>(dt); \r\n", tbname);
            sb.AppendFormat("     }}\r\n\r\n");
            sb.AppendFormat("   #endregion\r\n");
            sb.AppendFormat("   }}\r\n");
            sb.AppendFormat("}}");
            return sb.ToString();
        }

        public string GetType(string Dttype)
        {
            string newtype = "";
            switch(Dttype)
            {
                case "int":
                case "bigint":
                case "smallint":
                case "tinyint":
                    newtype = "int?";
                    break;
                case "varchar":
                case "text":
                case "char":
                case "nchar":
                case "ntext":
                case "nvarchar":
                case "varbinary":
                case "xml":
                    newtype = "string";
                    break;
                case "datetime":
                case "date":
                case "time":
                case "timestamp":
                    newtype = "DateTime";
                    break;
                case "decimal":
                case "float":
                case "numeric":
                case "money":
                    newtype = "Double";
                    break;
            }
            return newtype;
        }
        #endregion

        #region StepTwo WriteFile
        public void WriteIntoFile(object ginfo)
        {
            GenaralInfo g = (GenaralInfo)ginfo;
            string path = g.floder;
            string tbname = g.tablename;
            string content = g.content;
            string filename = path + "\\" + tbname + ".cs";
            FileStream fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(content);
            sw.Close();
            //return filename;
        }
        #endregion

        #region DataReader
        public DataSet GetDataSet(string commandText, params object[] args)
        {
            return GetDataSet(string.Format(commandText, args));
        }
        public DataSet GetDataSet(string commandText)
        {
            DataSet result;
            try
            {
                using(SqlConnection sqlConnection = new SqlConnection(this.connection))
                {
                    sqlConnection.Open();
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(commandText, sqlConnection);
                    DataSet dataset = new DataSet();
                    sqlDataAdapter.Fill(dataset);
                    result = dataset;
                }
            }
            catch(Exception ex)
            {
                string str = "";
                str = commandText;
                throw new Exception(str + " " + ex.Message);
            }
            return result;
        }
        #endregion


    }

    public class GenaralInfo
    {
        public string floder
        {
            get; set;
        }
        public string tablename
        {
            get; set;
        }
        public string content
        {
            get; set;
        }
    }
}
