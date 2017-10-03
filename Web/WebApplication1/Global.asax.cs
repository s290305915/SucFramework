using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Configuration;

namespace WebApplication1
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            HttpRuntimeSection runTime = (HttpRuntimeSection)WebConfigurationManager.GetSection("system.web/httpRuntime");
            try
            {
                //maxRequestLength 为整个页面的大小，不仅仅是上传文件的大小，所以扣除 100KB 的大小，
                //maxRequestLength单位为KB
                int maxRequestLength = (runTime.MaxRequestLength) * 1024;
                if(Request.ContentLength > maxRequestLength)
                { //注意这里可以跳转，可以直接终止；在VS里调试时候得不到想要的结果，通过IIS才能得到想要的结果；FW4.0经典或集成都没问题
                    Response.Write("请求大小" + Request.ContentLength);
                    Response.End();
                }
            }
            catch { }
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

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}