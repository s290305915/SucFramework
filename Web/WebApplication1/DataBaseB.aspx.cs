using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SucLib.Data.Factory;
using SucLib.Data.IDal;

namespace WebApplication1
{
    public partial class DataBaseB : System.Web.UI.Page
    {
        public StringBuilder sb = new StringBuilder();
        IDBHelp db = DBFactory.Create();
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Request.QueryString["id"]);
            DataTable dt = db.GetDataTable(string.Format(@"SELECT * FROM SUC_USER WHERE ID={0}", id));
            foreach (DataColumn dc in dt.Columns)
            {
                sb.Append(dc.ColumnName + " : " + dt.Rows[0][dc].ToString());
                sb.Append("<br />");
            }
        }
    }
}