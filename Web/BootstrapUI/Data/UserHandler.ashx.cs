using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using SucLib.Core;
using SucLib.Data.Factory;
using SucLib.Data.IDal;
using SucLib.Model;

namespace BootstrapUI.Data
{
    /// <summary>
    /// UserHandler 的摘要说明
    /// </summary>
    public class UserHandler : IHttpHandler
    {
        IDBHelp db = DBFactory.Create();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");

            switch (HttpContext.Current.Request["opt"].ToString())
            {
                case "GetUserList":
                    GetUserList(context);
                    break;
            }
        }

        private void GetUserList(HttpContext context)
        {
            List<SUC_USER> us = new SUC_USER().FindAll();
            StringBuilder sb = new StringBuilder("[");
            Type Type_Table = typeof(SUC_USER);
            PropertyInfo[] ProList = Type_Table.GetProperties();
            // 反射实体的所有属性
            foreach (SUC_USER u in us)
            {
                sb.Append("{");
                string k = "", v = "";
                foreach (PropertyInfo i in ProList)
                {
                    object[] objAttrs = i.GetCustomAttributes(typeof(DataMapAttribute), true);
                    if (objAttrs.Length > 0)
                    {
                        DataMapAttribute attr = objAttrs[0] as DataMapAttribute;
                        k = attr.Column;
                        try { v = i.GetValue(u, null).ToString(); }
                        catch { v = ""; }
                        sb.AppendFormat("\"{0}\":\"{1}\",", k, v);
                    }
                }
                sb.Remove(sb.Length - 1, 1);
                sb.Append("},");
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append("]");
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