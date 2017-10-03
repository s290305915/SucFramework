using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class WebLoadTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void bt_test_Click(object sender, EventArgs e)
        {
            var html = div_test.InnerHtml;// test_value.Value;
            div_test.InnerHtml = html + "这是后台页面按钮事件！！";
        }
    }
}