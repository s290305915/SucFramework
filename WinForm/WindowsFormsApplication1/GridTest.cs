using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SucLib.Data.IDal;
using SucLib.Data.Factory;
using SucLib.Model;


namespace WindowsFormsApplication1
{
    public partial class GridTest : Form
    {
        public GridTest()
        {
            InitializeComponent();
        }

        IDBHelp db = DBFactory.Create();
        int Maxrows = 0;
        private void GridTest_Load(object sender, EventArgs e)
        {
            List<SUC_MODULE> models = new SUC_MODULE().FindAll();
            Maxrows = models.Count;
            int ids = 0;
            int? pids = 0;
            int? sords = 0;
            models.ForEach(x => {
                ids += x.ID;
                pids += x.PARENT_ID;
                sords += x.SHOW_ORDER;
            });
            models.Add(new SUC_MODULE()
            {
                ID = ids,
                PARENT_ID = pids,
                SHOW_ORDER = sords,
                NAME = "合计"
            });
            dgv_ts1.DataSource = models;
            dgv_ts1.Rows[2].Frozen = true;
            dgv_ts1.Columns[0].Frozen = true;
        }
    }
}
