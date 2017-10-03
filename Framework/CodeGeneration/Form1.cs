using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeGeneration
{
    public partial class Form1 : Form
    {
        public static Form1 f1;
        public List<string> names;
        bool isalls = false;
        public Form1()
        {
            InitializeComponent();
            this.skinEngine1.SkinFile = "DiamondBlue.ssk";
            tx_add.Text = ".";
            tx_user.Text = "sa";
            tx_pass.Text = "";
            names = new List<string>();
        }

        public static string connstr = "";
        public static string name = "";
        public static string pass = "";
        public static string host = "";
        bool bconn = false;
        private void btn_conn_Click(object sender, EventArgs e)
        {
            try
            {

                if(!bconn)
                    bconn = true;
                else
                {
                    Redure();
                    return;
                }
                //Data Source=.;Initial Catalog=SUCMSF;User ID=sa;Pwd=suchi12345
                btn_conn.Text = "断开连接";
                host = tx_add.Text.Trim();
                tx_add.Enabled = false;
                name = tx_user.Text.Trim();
                tx_user.Enabled = false;
                pass = tx_pass.Text.Trim();
                tx_pass.Enabled = false;

                if(comboBox2.SelectedIndex == 0)
                {
                    connstr = string.Format(@"Data Source={0};Integrated Security=True", host);
                }
                else
                {
                    connstr = string.Format(@"Data Source={0};User ID={1};Pwd={2}", host, name, pass);//Initial Catalog={1};
                }
                using(SqlConnection conn = new SqlConnection(connstr))
                {
                    using(SqlCommand cmd = new SqlCommand("select name from sys.databases", conn))
                    {
                        conn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        while(dr.Read())
                        {
                            com_dbs.Items.Add(dr[0]);
                            //dr[0];
                        }
                    }
                }
                MessageBox.Show("连接成功！");

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                Redure();
                return;
            }
        }

        public void Redure()
        {
            bconn = false;
            btn_conn.Text = "连  接";
            tx_add.Enabled = true;
            tx_user.Enabled = true;
            tx_pass.Enabled = true;
            com_dbs.Items.Clear();
            com_dbs.Text = "";
            la_dbs.Controls.Clear();
            names = new List<string>();
        }

        private void com_dbs_SelectedIndexChanged(object sender, EventArgs e)
        {
            isalls = false;
            names = new List<string>();
            la_dbs.Controls.Clear();
            CheckBox cmbtb = new CheckBox();
            //Data Source=.;Initial Catalog=SUCMSF;User ID=sa;Pwd=suchi12345
            string db = com_dbs.Text.ToString();
            if(comboBox2.SelectedIndex == 0)
            {
                connstr = string.Format(@"Data Source={0};Initial Catalog={1};Integrated Security=True", host, db);
            }
            else
            {
                connstr = string.Format(@"Data Source={0};Initial Catalog={3};User ID={1};Pwd={2}", host, name, pass, db);//Initial Catalog={1};
            }
            using(SqlConnection conn = new SqlConnection(connstr))
            {
                using(SqlCommand cmd = new SqlCommand("select name from SysObjects where type='U'", conn))
                {
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while(dr.Read())
                    {
                        cmbtb = new CheckBox();
                        cmbtb.Parent = la_dbs;
                        cmbtb.Text = dr[0].ToString();
                        cmbtb.CheckedChanged += cmbtb_CheckedChanged;
                        //dr[0];
                        la_dbs.Controls.Add(cmbtb);
                    }
                }
            }
        }

        void cmbtb_CheckedChanged(object sender, EventArgs e)
        {
            if(isalls)
                return;
            CheckBox cbtb = sender as CheckBox;
            string tb = cbtb.Text;
            if(names.Contains(tb))
                names.Remove(tb);
            else
                names.Add(tb);
        }

        private void btn_nextstep_Click(object sender, EventArgs e)
        {
            if(names.Count > 0)
            {
                f1 = this;
                Form2 f2 = new Form2(com_dbs.Text.ToString(), names, host, name, pass, connstr);
                f2.Show();
                this.Hide();
                return;
            }
            MessageBox.Show("还未选取表！");
        }

        private void btn_checkall_Click(object sender, EventArgs e)
        {
            isalls = true;
            names = new List<string>();
            foreach(CheckBox chk in la_dbs.Controls)
            {
                chk.CheckState = CheckState.Checked;
                names.Add(chk.Text);
            }
            isalls = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tx_user.Enabled = false;
            tx_pass.Enabled = false;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.comboBox2.SelectedIndex == 1)
            {
                tx_user.Enabled = true;
                tx_pass.Enabled = true;
            }
            else
            {
                tx_user.Enabled = false;
                tx_pass.Enabled = false;
            }
            Redure();
        }
    }
}
