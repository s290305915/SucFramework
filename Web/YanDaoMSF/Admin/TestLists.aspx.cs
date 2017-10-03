using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SucLib.Common;
using SucLib.Data.Factory;
using SucLib.Data.IDal;

namespace YanDaoMSF.Admin
{
    public partial class TestLists : System.Web.UI.Page
    {
        public string username;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (SucCookie.Exists("username"))
                username = SucCookie.Read("username");
            else
                Response.Redirect("Login.aspx");
        }
    }
}