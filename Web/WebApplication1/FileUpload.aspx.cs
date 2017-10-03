﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SucLib.Common;
using SucLib.Data.Factory;
using SucLib.Data.IDal;

namespace WebApplication1
{
    public partial class FileUpload : System.Web.UI.Page
    {

        IDBHelp db = DBFactory.Create();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_up_Click(object sender, EventArgs e)
        {
            if (file_up.HasFile)
            {
                try
                {
                    if (file_up.FileContent.Length > 0)
                    {
                        string filename = file_up.FileName;
                        string ext = System.IO.Path.GetExtension(filename);
                        DateTime dt = DateTime.Now;
                        string newname = dt.ToString("yyyyMMddHHmmssffff") + ext;
                        string path = "~/Files/" + newname;
                        file_up.SaveAs(System.Web.HttpContext.Current.Server.MapPath(path));
                        string usern = SucCookie.Read("username");
                        if (!string.IsNullOrEmpty(usern))
                        {
                            DataTable userDT = db.GetDataTable(string.Format(@"SELECT * FROM SUC_USER WHERE LOGIN_NAME='{0}'", usern));
                            if (userDT.Rows.Count > 0)
                            {
                                DateTime publicdate = DateTime.Now;
                                //string type = sComb.Value;
                                string fromwhere = userDT.Rows[0]["Unit"].ToString();
                                string userid = userDT.Rows[0]["ID"].ToString();
                                string filesize = (file_up.PostedFile.ContentLength / 1000).ToString() + "kb";
                                string filepath = path;
                                //string gradeclass = sTree.Value;
                                //db.ExecuteNonQuery(string.Format(@"INSERT INTO SUC_FILES (NAME, USER_ID,BROWNUM,TYPE,FROMWHERE,DOWNLOADNUM,FILETYPE,FILEPATH,FILESIZE,GRADE_CLASS,PUBLISH_DATE) VALUES ('" + tname.Text + "','" + userid + "','0','" + ext + "','" + fromwhere + "','0','" + type + "','" + filepath + "','" + filesize + "','" + gradeclass + "',GETDATE())"));
                                JsUtil.ShowMsg("上传成功！", "../Default.aspx");
                                return;
                            }
                            else
                            {
                                JsUtil.ShowMsg("该用户不存在！");
                                return;
                            }
                        }
                        else
                        {
                            JsUtil.ShowMsg("请重新登录！");
                            return;
                        }
                    }
                    else
                    {
                        JsUtil.ShowMsg("请选择要上传的文件！");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    JsUtil.ShowMsg("上传失败，请重新上传！");
                    return;

                }
            }
            JsUtil.ShowMsg(" 您还没有选择文件或您选择的文件大小为0，请先选择文件！");
        }
    }
}