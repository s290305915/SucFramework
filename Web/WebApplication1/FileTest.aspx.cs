using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class FileTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["userId"] = "123";
        }

        protected void btn_xeff_Click(object sender, EventArgs e)
        {
            HttpFileCollection files = Request.Files;
            if(files.Count > 0)
            {
                for(int ii = 0;ii < files.Count;ii++)
                {
                    string type = files[ii].ContentType;
                    int size = files[ii].ContentLength / 1024 / 1024;
                    string filename = files[ii].FileName;
                    try
                    {
                        //context.Response.Write(files[0].ContentLength);
                        //return;
                        string[] fnames = filename.Split('.');
                        fnames[0] = fnames[0].Replace("/", "").Replace("\\", "");
                        string nname = filename.Substring(0, filename.LastIndexOf('.') - 1);
                        nname = nname.Trim().Replace(".", "_").Replace(" ", "_");
                        string newfname = nname + DateTime.Now.ToString("yyyyMMddHHmmss") + "." + fnames[fnames.Count() - 1];
                        string respath = "../../Files/" + System.IO.Path.GetFileName(newfname);
                        string path = HttpContext.Current.Server.MapPath("/Files/") + System.IO.Path.GetFileName(newfname);
                        files[ii].SaveAs(path.Trim());
                    }
                    catch { }
                }
            }
        }
    }
}