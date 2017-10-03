using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.CheckBoxDemo.style
{
    /// <summary>
    /// MutilFileHandler 的摘要说明
    /// </summary>
    public class MutilFileHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
            switch(HttpContext.Current.Request.QueryString["opt"].ToString())
            {
                case "ReadFile":
                    ReadFile(context);
                    break;
            }
        }

        public void ReadFile(HttpContext context)
        {
            //string uid = HttpContext.Current.Request.QueryString["uid"].ToString();
            HttpFileCollection files = HttpContext.Current.Request.Files;
            //这里只能用<input type="file" />才能有效果,因为服务器控件是HttpInputFile类型
            string msg = string.Empty;
            string filename = string.Empty; //路径
            string path = string.Empty;
            List<int> FileIds = new List<int>();    //存文件ID
            if(files.Count > 0)
            {
                for(int ii = 0;ii < files.Count;ii++)
                {
                    string type = files[ii].ContentType;
                    int size = files[ii].ContentLength / 1024 / 1024;
                    filename = files[ii].FileName;
                    try
                    {
                        string[] fnames = filename.Split('.');
                        fnames[0] = fnames[0].Replace("/", "").Replace("\\", "");
                        string newfname = fnames[0] + DateTime.Now.ToString("yyyyMMddHHmmss") + "." + fnames[1];
                        string respath = "../../Files/" + System.IO.Path.GetFileName(newfname);
                        path = HttpContext.Current.Server.MapPath("/Files/") + System.IO.Path.GetFileName(newfname);
                        files[ii].SaveAs(path);
                    }
                    catch(Exception ex)
                    {
                        msg += ex.Message;
                        msg += " 文件:" + filename + " 保存失败 ";
                        continue;
                    }
                }
            }
            context.Response.Write(string.IsNullOrEmpty(msg) ? "success" : msg);
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