using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using SucLib.Data.IDal;     //需要改
using SucLib.Data.Factory;
using System.Data;

namespace TestWebService
{
    /// <summary>
    /// DataServicely 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class DataServicely : System.Web.Services.WebService
    {
        IDBHelp db = DBFactory.Create();    //需要改
        string sql = "";
        DataTable dt = new DataTable();
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        /// <summary>
        /// 家宽投诉
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public void JIAKUANTOUSU()
        {
            sql = string.Format(@"SELECT [TASKID]
      ,[SN]
      ,[APPLYTIME]
      ,[APPLYUSER]
      ,[BILLSOURCE]
      ,[CITY]
      ,[VILLAGE]
      ,[AREAFAULT]
      ,[COMMUNITYTYPE]
      ,[COMMUNITYNAME]
      ,[CUSTOMERTEL]
      ,[DISTRICT]
      ,[RESPONSIBLEUNIT]
      ,[EMOSDATE]
      ,[EMOSDELYTIME]
      ,[FAULTTYPE]
      ,[CUSTOMERACCOUNT]
      ,[SYMPTOM]
      ,[LIMITTIME]
      ,[PRESTEP]
      ,[FAULTDETAIL]
      ,[DELYHOURS]
      ,[CALLBACKOPERATION]
      ,[RETURNBACKREASON]
      ,[RESONSFORFAILURE]
      ,[WEATHERACHIVED]
      ,[WORKPERMIT]
      ,[ATTITUDESATISFY]
      ,[RESULTSATISFY]
      ,[OTHERPROBLEMS]
      ,[OTHERPROBLEMDEAL]
      ,[VIEWFAILUREMARK]
      ,[UNCOVERREASON]
  FROM [SUCMSF1].[dbo].[FORM_JSWH_WIDEBANDMAINT]");
            dt = db.GetDataTable(sql);
            HttpContext.Current.Response.Write(JsonHelper.DataTableToJSON(dt));
        }

        /// <summary>
        /// 专线投诉
        /// </summary>
        [WebMethod]
        public void ZHUANXIANTOUSU()
        {
            sql = string.Format(@"SELECT [TASKID]
      ,[SN]
      ,[APPLYTIME]
      ,[APPLYUSER]
      ,[LINENAME]
      ,[BILLSOURCE]
      ,[LINETYPE]
      ,[CUSTOMERTEL]
      ,[DISTRICT]
      ,[EOMSAPPLYTIME]
      ,[EOMSDELAYTIME]
      ,[COMMUNITYNAME]
      ,[RESPONSIBLEUNIT]
      ,[LIMITTIME]
      ,[ATTACHMENT]
      ,[FAULTDETAIL]
      ,[CALLBACKOPERATION]
      ,[WEATHERACHIVED]
      ,[ATTITUDESATISFY]
      ,[WORKPERMIT]
      ,[RESULTSATISFY]
      ,[OTHERPROBLEMDEAL]
      ,[DELYHOURS]
  FROM [SUCMSF1].[dbo].[FORM_JSWH_WIDEBANDMAINT_COMPLAINTSLINE]");
            dt = db.GetDataTable(sql);
            HttpContext.Current.Response.Write(JsonHelper.DataTableToJSON(dt));
        }

        /// <summary>
        /// Wlan投诉
        /// </summary>
        [WebMethod]
        public void WLANTOUSU()
        {
            sql = string.Format(@"SELECT [TASKID]
      ,[SN]
      ,[APPLYTIME]
      ,[APPLYUSER]
      ,[CUSTOMERNAME]
      ,[CUSTOMERTEL]
      ,[CUSTOMERAREA]
      ,[CELLNAME]
      ,[CELLLOCATION]
      ,[MARK]
      ,[APPLYTEL]
      ,[APPLYDEPT]
      ,[ID]
  FROM [SUCMSF1].[dbo].[FORM_JSWH_WIDEBANDMAINT_WECHATORDER]");
            dt = db.GetDataTable(sql);
            HttpContext.Current.Response.Write(JsonHelper.DataTableToJSON(dt));
        }

        /// <summary>
        /// 微信预约
        /// </summary>
        [WebMethod]
        public void WEIXINYUYUE()
        {
            sql = string.Format(@"SELECT [TASKID]
      ,[SN]
      ,[APPLYTIME]
      ,[APPLYUSER]
      ,[BILLSOURCE]
      ,[WLANNAME]
      ,[CUSTOMERTEL]
      ,[BILLCLASS]
      ,[EOMSAPPLYTIME]
      ,[EOMSDELAYTIME]
      ,[DISTRICT]
      ,[RESPONSIBLEUNIT]
      ,[ROOMNUM]
      ,[LIMITTIME]
      ,[FAULTDETAIL]
      ,[DELYHOURS]
      ,[CALLBACKOPERATION]
      ,[RETURNBACKREASON]
      ,[RESONSFORFAILURE]
      ,[WEATHERACHIVED]
      ,[WORKPERMIT]
      ,[ATTITUDESATISFY]
      ,[RESULTSATISFY]
      ,[OTHERPROBLEMS]
      ,[OTHERPROBLEMDEAL]
      ,[VIEWFAILUREMARK]
      ,[UNCOVERREASON]
  FROM [SUCMSF1].[dbo].[FORM_JSWH_WIDEBANDMAINT_WLAN]");
            dt = db.GetDataTable(sql);
            HttpContext.Current.Response.Write(JsonHelper.DataTableToJSON(dt));
        }
    }
}
