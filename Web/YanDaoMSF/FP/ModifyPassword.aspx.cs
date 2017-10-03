using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using SucLib.Common;
using SucLib.Data.Factory;
using SucLib.Data.IDal;

namespace YanDaoMSF.FP
{
    public partial class ModifyPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!SucCookie.Exists("username"))
                JsUtil.TipAndRedirect("登陆已过时，请重新登陆后再修改密码！", "UserLogin.aspx", "5");
        }

        [WebMethod]
        public static string Modify(string oldp, string newp, string code)
        {
            IDBHelp db = DBFactory.Create();
            string usern = SucCookie.Read("username");
            if (!SucCookie.Read("CheckCode").Equals(code.ToUpper()))
            {
                return "codeerror";
            }
            if (db.IsExists(string.Format(@"SELECT * FROM SUC_LOGIN WHERE LOGIN_NAME='{0}' AND PASSWORD='{1}'", usern, oldp)))
            {
                if (db.ExecuteNonQuery(string.Format(@"UPDATE SUC_LOGIN SET PASSWORD='{2}' WHERE LOGIN_NAME='{0}' AND PASSWORD={1}", usern, oldp, newp)) > 0)
                {
                    SucCookie.Delete("username");
                    return "ok";
                }
            }
            return "no";
        }
    }
}