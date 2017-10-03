using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public delegate void ReturnValue(string value);
    public partial class FrmSelect : Form
    {
        public FrmSelect()
        {
            InitializeComponent();
        }
        string Namec;
        public event ReturnValue rtvalue;
        public FrmSelect(string name)
            : this()
        {
            Namec = name;
        }

        private void FrmSelect_Load(object sender, EventArgs e)
        {
            //MessageBox.Show(Name);
            this.Left = (Screen.PrimaryScreen.WorkingArea.Width - Width) / 2;
            this.Top = (Screen.PrimaryScreen.WorkingArea.Height - Height) / 2;
            this.Text = Namec;
            BindData();

        }

        private void BindData()
        {
            DataTable dt = new DataTable();
            DataColumn keyc = new DataColumn();
            keyc.DataType = System.Type.GetType("System.String");
            keyc.ColumnName = "key";
            DataColumn valuec = new DataColumn();
            valuec.DataType = System.Type.GetType("System.String");
            valuec.ColumnName = "value";
            dt.Columns.Add(keyc);
            dt.Columns.Add(valuec);
            dt.Rows.Add(new object[] { "xx", "ax" });
            dt.Rows.Add(new object[] { "vv", "bv" });
            dt.Rows.Add(new object[] { "cc", "ct" });
            dataGridView1.DataSource = dt;
            dataGridView1.RowHeadersVisible = false;

            //foreach (DataRow dr in dt.Rows)
            //{
            //    string v = dr[0].ToString();
            //    dr[0] = v.Substring(0, v.Length - 1) + "0";
            //}

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string v = (sender as DataGridView).CurrentCell.Value.ToString();
            rtvalue(v);
            this.Close();
        }

        private void FrmSelect_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Form1.f1 != null)
            {
                //Form1.f1.Show();
            }
        }
    }
}
