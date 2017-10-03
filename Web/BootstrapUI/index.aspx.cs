using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BootstrapUI
{
    public partial class index : System.Web.UI.Page
    {
        public string s = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            string p = tx_val.Value;
            s = p + "xxx";
            //Server.Transfer("index.aspx");
            //Response.Write("<script language='javascript'>window.location.href='index.aspx'</script>");
            //Response.Redirect("index.aspx");
            //Server.Execute("");
        }

        public string getp()
        {
            string p = tx_val.Value;
            return p + "xxx";
        }
    }
}