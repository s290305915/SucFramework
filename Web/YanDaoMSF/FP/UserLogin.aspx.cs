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
    public partial class UserLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        [WebMethod]
        public static string checkUser(string usern, string userp, string code)
        {
            IDBHelp db = DBFactory.Create();
            if (!SucCookie.Read("CheckCode").Equals(code.ToUpper()))
            {
                return "codeerror";
            }
            if (db.IsExists(string.Format(@"SELECT * FROM SUC_USER WHERE LOGIN_NAME='{0}'", usern)))
            {
                    if (db.IsExists(string.Format(@"SELECT * FROM SUC_LOGIN WHERE LOGIN_NAME='{0}' AND PASSWORD={1}", usern, userp)))
                    {
                        SucCookie.Add("username", usern, 30);
                        return "ok";
                    }
            }
            return "no";
        }

    }
}