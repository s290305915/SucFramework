using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using SucLib.Common;
using SucLib.Data.Factory;
using SucLib.Data.IDal;

namespace YanDaoMSF.Admin.Master
{
    public partial class HeadPage : System.Web.UI.MasterPage
    {
        public string username;
        public string menu;
        IDBHelp db = DBFactory.Create();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (SucCookie.Exists("username"))
                username = SucCookie.Read("username");
            else
            {
                Response.Redirect("Login.aspx");
                return;
            }
            BindMenu();
        }

        /// <summary>
        /// 加载多级菜单
        /// </summary>
        public void BindMenu()
        {
            string userid = db.GetList(string.Format(@"SELECT ID FROM SUC_USER WHERE LOGIN_NAME='{0}'", SucCookie.Read("username")))[0];
            StringBuilder sb = new StringBuilder("<li><dl>");
            DataTable dt = db.GetDataTable(string.Format(@"
                                            SELECT * FROM SUC_MODULE WHERE ID IN(
                                            SELECT MODULE_ID FROM SUC_ROLE_MODULE WHERE MODULE_ID IN(
                                            SELECT ID FROM SUC_MODULE WHERE PARENT_ID=0)AND ROLE_ID=
                                            (SELECT ROLE_ID FROM SUC_USER WHERE ID={0}))", 1));
            foreach (DataRow dr in dt.Rows)
            {
                sb.Append(string.Format(@"<dt>{0}</dt>", dr["NAME"].ToString()));
                DataTable dt1 = db.GetDataTable(string.Format(@"SELECT * FROM SUC_MODULE WHERE ID IN(
                                            SELECT MODULE_ID FROM SUC_ROLE_MODULE WHERE MODULE_ID IN(
                                            SELECT ID FROM SUC_MODULE WHERE PARENT_ID={0})AND ROLE_ID=
                                            (SELECT ROLE_ID FROM SUC_USER WHERE ID={1}))", dr[0], 1));
                foreach (DataRow dr1 in dt1.Rows)
                {
                    sb.Append(string.Format("<dd><a href=\"{0}\">{1}</a></dd>", dr1[3], dr1[2]));
                }
            }
            sb.Append("</dl></li>");
            menu = sb.ToString();
        }
    }
}