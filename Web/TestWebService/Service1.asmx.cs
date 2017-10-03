using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Services;
using SucLib.Data;
using SucLib.Model;
using SucLib.Common;

namespace TestWebService
{
    /// <summary>
    /// Service1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class Service1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public List<SUC_USER> GetUser()
        {
            SUC_USER us = new SUC_USER();
            return us.FindAll();
        }

        [WebMethod]
        public void GetUserJson()
        {
            SUC_USER us = new SUC_USER();
            DataSet dsuser = us.FindAllTable();
            HttpContext.Current.Response.Write(JsonHelper.DataTableToJSON(dsuser.Tables[0]));
        }

        [WebMethod]
        public DataSet GetUserTable()
        {
            SUC_USER us = new SUC_USER();
            return us.FindAllTable();
        }

        [WebMethod]
        public SUC_USER user(string name)
        {
            SUC_USER us = new SUC_USER() { NAME = name };
            List<SUC_USER> uss = us.FindByCondition(us);
            if (uss == null || uss.Count == 0)
            {
                return null;
            }
            else
            {
                return uss[0];
            }
        }

    }
}