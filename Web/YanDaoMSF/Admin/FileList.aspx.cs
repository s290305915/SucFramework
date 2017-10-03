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
    public partial class FileList : System.Web.UI.Page
    {
        IDBHelp db = DBFactory.Create();
        public IList<string> funcs;
        public string filePath;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!SucCookie.Exists("username"))
                JsUtil.ShowMsg("登录已过时，请重新登陆！", "Login.aspx");
            GetFunction();
        }

        private void GetFunction()
        {
            funcs = db.GetList(string.Format(@"SELECT CODE,NAME FROM SUC_FUNCTION WHERE ID IN(
                                            SELECT FUNCTION_ID FROM SUC_ROLE_FUNCTION WHERE ROLE_ID=(
                                            SELECT ROLE_ID FROM SUC_USER WHERE LOGIN_NAME='{0}'))", SucCookie.Read("username")));
        }

    }
}