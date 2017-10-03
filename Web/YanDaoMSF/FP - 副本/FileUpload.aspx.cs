using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SucLib.Common;

namespace YanDaoMSF.FP
{
    public partial class FileUpload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
       

        protected void btn_upload_Click(object sender, EventArgs e)
        {
            //file_open.SaveAs("/Video/Files/"+file_open.FileName);
            if (file_open.HasFile)
            {
                try
                {
                    string filename = Path.GetFileName(file_open.FileName);
                    string filetype = filename.Substring(filename.LastIndexOf("."));
                    file_open.SaveAs(Server.MapPath("~/css/") + filename);
                    //保存到数据库

                    JsUtil.ShowMsg("上传文件成功！", "FilePage.aspx");
                }
                catch (Exception ex)
                {
                    JsUtil.ShowMsg("上传失败，请重新上传！","FileUpload.aspx");

                }
            }
            string t = "";
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            file_open = new System.Web.UI.WebControls.FileUpload();
        }
    }
}