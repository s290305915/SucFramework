using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SucLib.Data.IDal;
using SucLib.Data.Factory;
using SucLib.Common;
using System.Data;
using System.Text;
using System.IO;

namespace YanDaoMSF.FP
{
    public partial class FileContent : System.Web.UI.Page
    {
        public string Name;
        public int BrowNum;
        public int DownloadNum;
        public string PublishDate;
        public string FilePath;
        public string id;
        public string loadmedia;
        public string filetp;
        public string fileinfo;
        IDBHelp db = DBFactory.Create();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string UserN = SucCookie.Read("username");
                if (!string.IsNullOrEmpty(UserN))
                {
                    lk_loginstate.Text = UserN;
                    lk_quitlogin.Visible = true;
                    lk_modifypwd.Visible = false;
                }
                id = Request.QueryString["id"];
                if (SucCookie.Exists("fileid"))
                    SucCookie.Delete("fileid");
                SucCookie.Add("fileid", id, 10);
                GetInfo(id);
                LoadMedia();
                LoadInfo(id);
                AddBrowNum(id);
            }
        }

        public void AddBrowNum(string id)
        {
            int brownum = Convert.ToInt32(db.GetList(string.Format(@"SELECT BROWNUM FROM SUC_FILES WHERE ID={0}", id))[0]);
            db.ExecuteNonQuery(string.Format(@"UPDATE SUC_FILES SET BROWNUM={0} WHERE ID={1}", brownum + 1, id));
        }

        public void LoadInfo(string id)
        {
            string sql = string.Format(@"SELECT * FROM (
                                        SELECT * FROM (
                                        SELECT * FROM SUC_FILES A
                                        LEFT JOIN 
                                        (SELECT ID USERID,NAME USERNAME,UNIT FROM SUC_USER) B
                                        ON A.USER_ID=B.USERID)C
                                        LEFT JOIN 
                                        (SELECT ID CLASSID,NAME CLASSNAME FROM SUC_GRADE_CLASS)D
                                        ON C.GRADE_CLASS=D.CLASSID)E
                                        LEFT JOIN
                                        (SELECT ID TPID,NAME TPNAME FROM SUC_FILETYPE)F
                                        ON E.FILETYPE=F.TPID
                                        WHERE ID={0}", id);
            DataTable dt = db.GetDataTable(sql);
            StringBuilder sb = new StringBuilder("<tr>");
            sb.Append(string.Format("<td colspan=\"2\">来源:<a href=\"#\">{0}</a></td>", dt.Rows[0]["UNIT"].ToString()));
            sb.Append(string.Format("</tr>"));
            sb.Append(string.Format("<tr>"));
            sb.Append(string.Format("<td>版本:<a href=\"#\">{0}</a></td>", dt.Rows[0]["CLASSNAME"].ToString()));
            sb.Append(string.Format("<td>大小:<a href=\"#\">{0}</a></td>", dt.Rows[0]["FILESIZE"].ToString()));
            sb.Append(string.Format("</tr>"));
            sb.Append(string.Format("<tr>"));
            sb.Append(string.Format("<td>文件类型:<a href=\"#\">{0}</a></td>", dt.Rows[0]["TYPE"].ToString()));
            sb.Append(string.Format("<td>类别:<a href=\"#\">{0}</a></td>", dt.Rows[0]["TPNAME"].ToString()));
            sb.Append(string.Format("</tr>"));
            sb.Append(string.Format("<tr>"));
            sb.Append(string.Format("<td colspan=\"2\">发布时间:<a href=\"#\">{0}</a></td>", Convert.ToDateTime(dt.Rows[0]["PUBLISH_DATE"].ToString()).ToShortDateString()));
            sb.Append(string.Format("</tr>"));
            sb.Append(string.Format("<tr>"));
            sb.Append(string.Format("<td colspan=\"2\">主讲人:<a href=\"#\">{0}</a></td>", dt.Rows[0]["USERNAME"].ToString()));
            sb.Append(string.Format("</tr>"));
            sb.Append(string.Format("<tr>"));
            sb.Append(string.Format("<td colspan=\"2\">发布人:<a href=\"#\">{0}</a></td>", dt.Rows[0]["USERNAME"].ToString()));
            sb.Append(string.Format("</tr>"));
            fileinfo = sb.ToString();
        }

        public void LoadMedia()
        {
            switch (filetp.ToLower())
            {
                case ".wmv":
                case ".mp4":
                    StringBuilder sbv = new StringBuilder("<video id=\"example_video_1\" class=\"video-js vjs-default-skin\" controls preload=\"none\" width=\"710\" height=\"420\"");
                    sbv.Append(" poster=\"http://video-js.zencoder.com/oceans-clip.png\"");
                    sbv.Append(" data-setup=\"{}\">");
                    sbv.Append("<source src=\"../" + FilePath + "\" type=\"video/mp4\"/>");
                    sbv.Append("<track kind=\"captions\" src=\"demo.captions.vtt\" srclang=\"en\" label=\"English\"/>");
                    sbv.Append("<track kind=\"subtitles\" src=\"demo.captions.vtt\" srclang=\"en\" label=\"English\"/>");
                    sbv.Append("</video>");
                    loadmedia = sbv.ToString();
                    break;
                //case ".doc":
                //case ".pdf":
                case ""://.ppt
                    StringBuilder sbd = new StringBuilder("<object id=\"rppt\" class=\"FlashReaderObject\" classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,29,0\" height=\"500\" width=\"100%\">");
                    sbd.Append("<param name=\"Movie\" value=\"" + FilePath + "\">");
                    sbd.Append("<param name=\"wmode\" value=\"opaque\">");
                    sbd.Append("<param name=\"allowFullScreen\" value=\"true\">");
                    sbd.Append("<param name=\"quality\" value=\"high\">");
                    sbd.Append("<param name=\"SCALE\" value=\"exactfit\">");
                    sbd.Append("<embed src=\"" + FilePath + "\" id=\"rppt_embed\" allowfullscreen=\"true\" quality=\"high\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\" wmode=\"opaque\" type=\"application/x-shockwave-flash\" scale=\"exactfit\" height=\"5\" width=\"100%\">");
                    sbd.Append("</object>");
                    loadmedia = sbd.ToString();
                    break;
                default:
                    StringBuilder sbn = new StringBuilder("<h1 style='margin-top:10px;color:red;'>此文件暂时无法预览，仅支持下载（mp4,swf格式可支持在线预览）。</h1>");
                    loadmedia = sbn.ToString();
                    break;
            }
        }

        public void GetInfo(string id)
        {
            DataTable dt = db.GetDataTable(string.Format(@"SELECT * FROM SUC_FILES WHERE ID={0}", id));
            Name = dt.Rows[0]["NAME"].ToString();
            BrowNum = Convert.ToInt32(dt.Rows[0]["BROWNUM"].ToString());
            DownloadNum = Convert.ToInt32(dt.Rows[0]["DOWNLOADNUM"].ToString());
            PublishDate = Convert.ToDateTime(dt.Rows[0]["PUBLISH_DATE"].ToString()).ToShortDateString();
            FilePath = dt.Rows[0]["FILEPATH"].ToString();
            filetp = dt.Rows[0]["TYPE"].ToString();
        }

        protected void lk_upload_Click(object sender, EventArgs e)
        {
            //SucCookie.Add("username", "suchi", 30);
            //SucCookie.Delete("username");
            if (SucCookie.Exists("username"))
            {
                //Response.Write("<script>javascript:window.open  ('FileUpload.aspx','文件上传', 'height=700, width=1200')</script>");
                //Response.Write("<script language='javascirpt'>window.showModalDialog('FileUpload.aspx?', window, 'dialogWidth:800px;dialogHeight:440px;center:yes;status:no;scroll:yes;help:no');</script>");
                Response.Redirect("FileUpload.aspx");
                return;
            }
            Response.Redirect("UserLogin.aspx");
        }

        protected void lk_loginstate_Click(object sender, EventArgs e)
        {
            string UserN = SucCookie.Read("username");
            if (!string.IsNullOrEmpty(UserN))
            {
                JsUtil.ShowMsg("您已经成功登录！");
                return;
            }

            Response.Redirect("UserLogin.aspx");
        }

        protected void btn_download_Click(object sender, EventArgs e)
        {
            if (SucCookie.Exists("username"))
            {
                try
                {
                    id = SucCookie.Read("fileid");
                    FilePath = db.GetList(string.Format("SELECT FILEPATH FROM SUC_FILES WHERE ID={0}", id))[0];
                    Name = db.GetList(string.Format("SELECT NAME FROM SUC_FILES WHERE ID={0}", id))[0];
                    string fileName = Name + FilePath.Substring(FilePath.LastIndexOf('.'));//客户端保存的文件名
                    string filePath = Server.MapPath(FilePath);//路径

                    //以字符流的形式下载文件
                    FileStream fs = new FileStream(filePath, FileMode.Open);
                    byte[] bytes = new byte[(int)fs.Length];
                    fs.Read(bytes, 0, bytes.Length);
                    fs.Close();
                    Response.ContentType = "application/octet-stream";
                    //通知浏览器下载文件而不是打开
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                    Response.End();
                }
                catch (Exception ex)
                {
                    JsUtil.ShowMsg("文件保存出错，请联系网站管理员！");
                }
            }
            else
            {
                JsUtil.ShowMsg("您尚未登陆，请先登录然后才能下载！", "UserLogin.aspx");
            }
        }

        protected void lk_quitlogin_Click(object sender, EventArgs e)
        {
            if (SucCookie.Exists("username"))
            {
                SucCookie.Delete("username");
                lk_loginstate.Text = "请登陆";
                lk_quitlogin.Visible = false;
                lk_modifypwd.Visible = false;
            }
        }

        protected void lk_modifypwd_Click(object sender, EventArgs e)
        {
            JsUtil.LocationNewHref("ModifyPassword.aspx");
        }
    }
}