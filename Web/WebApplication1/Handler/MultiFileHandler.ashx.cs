using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Handler
{
    /// <summary>
    /// MultiFileHandler 的摘要说明
    /// </summary>
    public class MultiFileHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
            if(context.Request.Files.Count > 0)
            {
                HttpFileCollection files = context.Request.Files;
                for(int i = 0;i < files.Count;i++)
                {
                    HttpPostedFile file = files[i];
                    string fname = context.Server.MapPath("~/uploads/" + file.FileName);
                    file.SaveAs(fname);
                }
                context.Response.ContentType = "text/plain";
                context.Response.Write("File Uploaded Successfully!");
            }
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