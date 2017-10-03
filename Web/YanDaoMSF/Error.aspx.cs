using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SucLib.Common;

namespace YanDaoMSF
{
    public partial class Error : System.Web.UI.Page
    {
        public string ErrorMsg = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (CacheUtil.IsExist("Error"))
                {
                    ErrorMsg = CacheUtil.Read("Error").ToString();
                    er_msg.InnerHtml = ErrorMsg;
                }
            }
        }

        protected void btn_export_Click(object sender, EventArgs e)
        {

        }

    }
}