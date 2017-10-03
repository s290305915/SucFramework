using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SucLib.Core;
using SucLib.Common;
using System.Data;
using QuerManager;

namespace BootstrapUI
{
    public partial class Comfirm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = new QueryManager().Query("select * from users").Tables[0];
        }
    }
}