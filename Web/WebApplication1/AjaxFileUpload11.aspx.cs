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
    public partial class AjaxFileUpload11 : System.Web.UI.Page
    {
        IDBHelp db = DBFactory.Create();
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = db.GetDataTable("SELECT * FROM SUC_MODULE");
            StringBuilder sb = new StringBuilder();
            foreach(DataRow dr in dt.Rows)
            {
                sb.AppendFormat("<input type='checkbox' value='{0}' name='a' />{0}<br/>", dr["NAME"].ToString());
            }
            ret.InnerHtml = sb.ToString();
        }
    }
}