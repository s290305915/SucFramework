using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using SucLib.Common;

namespace YanDaoMSF
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            string Message = "\n\nURL:\n " + Request.Path + "\n\nMESSAGE:\n " + Server.GetLastError().Message + "\n\nSTACK TRACE:\n " +
                Server.GetLastError().StackTrace;
            string logName = "Application";
            //写入日志;

            Server.ClearError();
            //Response.Redirect("../Error.html");
            Message = Message.Replace("\n", "<br />");
            if (SucLib.Common.CacheUtil.IsExist("Error"))
                SucLib.Common.CacheUtil.Remove("Error");
            SucLib.Common.CacheUtil.Insert("Error", Message, 30);
            LogHelper.Write(Message);
            Response.Redirect("Error.aspx");
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}