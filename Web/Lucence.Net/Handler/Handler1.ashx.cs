using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SucLib.Model;
using SucLib.Data.Factory;
using SucLib.Data.IDal;
using System.Text;

namespace Lucence.Net.Handler
{
    /// <summary>
    /// Handler1 的摘要说明
    /// </summary>
    public class Handler1 : IHttpHandler
    {
        IDBHelp db = DBFactory.Create();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            switch (context.Request.QueryString["opt"].ToString())
            {
                case "test":
                    string t = context.Request.Form["title"];
                    SUC_NEWS n = new SUC_NEWS();
                    n = n.FindAll().Where(x => x.TITLE.Contains(t)).ToList()[0];
                    context.Response.Write(n.CONTENT);
                    break;
                case "list":
                    GetList(context);
                    break;
            }
            //context.Response.Write("Hello World");
        }

        private void GetList(HttpContext context)
        {
            string t = context.Request.QueryString["t"].ToString();
            List<SUC_NEWS> ns = new SUC_NEWS().FindAll().Where(x => x.TITLE.Contains(t)).ToList();
            System.Web.Script.Serialization.JavaScriptSerializer jscriptSeri = new System.Web.Script.Serialization.JavaScriptSerializer();
            StringBuilder sb = new StringBuilder();
            jscriptSeri.Serialize(ns, sb);
            context.Response.Write(sb.ToString());

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}