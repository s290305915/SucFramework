using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SucLib.Model;
using SucLib.Data.Factory;
using SucLib.Data.IDal;
using System.Data;
using SucLib.Core;

namespace WebApplication1
{
    public partial class SliderLayout : System.Web.UI.Page
    {
        IDBHelp db = DBFactory.Create();
        protected void Page_Load(object sender, EventArgs e)
        {
            //List<tabA_Account_Bank> bak = new tabA_Account_Bank().FindAll();
            //foreach (tabA_Account_Bank a in bak)
            //    Response.Write(a.BankName + ">>>" + a.State + "%}|0{d}\a");
        }
    }
}