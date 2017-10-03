using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using SucLib.Common;

namespace YanDaoMSF.Admin.Handler
{
    /// <summary>
    /// TestService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class TestService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string Query()
        {
            string Code = Context.Request["code"];
            DataTable dtsql = GetDataTable(string.Format(@"SELECT SQL,PARA FROM {0}REMQUERY WHERE CODE='{1}'", "", Code));//.Rows[0][0].ToString();
            string strSql = dtsql.Rows[0][0].ToString();
            string para = dtsql.Rows[0][1].ToString();
            string[] paras = para.Split(',');
            foreach (string p in paras)
            {
                try
                {
                    strSql = strSql.Replace("{" + p + "}", Context.Request[p]);
                }
                catch
                {
                    strSql = strSql.Replace("{" + p + "}", "''");
                }
            }
            strSql = strSql.Replace("{PRE}", "");
            strSql = strSql.Replace("!", "'");
            return JsonHelper.DataTableToJSON(GetDataTable(strSql));
        }

        private DataTable GetDataTable(string sql)
        {
            DataSet ds = new QueryManager().Query(sql);
            return ds.Tables[0];
        }

    }
}
