using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SucLib.Data.Dal;
using SucLib.Data.Factory;
using SucLib.Data.IDal;

namespace Calc
{
    public partial class FormDataTest : Form
    {
        public FormDataTest()
        {
            InitializeComponent();
        }

        IDBHelp db = DBFactory.Create();

        private void FormDataTest_Load(object sender, EventArgs e)
        {
            DataTable dt = db.GetDataTable("SELECT * FROM SUC_MODULE");
            string msg = string.Join(",", dt.AsEnumerable().Select(x => x.Field<string>("NAME")));
            MessageBox.Show(msg);
        }
    }
}
