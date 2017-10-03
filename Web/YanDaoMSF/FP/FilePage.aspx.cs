using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SucLib.Common;
using SucLib.Data.Factory;
using SucLib.Data.IDal;

namespace YanDaoMSF.FP.HTML
{
    public partial class FilePage : System.Web.UI.Page
    {
        IDBHelp db = DBFactory.Create();
        public int schType = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string UserN = SucCookie.Read("username");
                if (!string.IsNullOrEmpty(UserN))
                {
                    lk_loginstate.Text = UserN;
                    lk_quitlogin.Visible = true;
                    lk_modifypwd.Visible = false;
                    lk_upload.Text = "上传文件";
                }
                Bind_FileList();
            }

        }

        public void Bind_FileList()
        {
            DataTable dt = db.GetDataTable(string.Format(@"SELECT A.ID,A.NAME,B.NAME USER_NAME,A.BROWNUM,A.PUBLISH_DATE,B.UNIT
                                                            FROM SUC_FILES AS A
                                                            LEFT JOIN( 
                                                            SELECT * FROM SUC_USER) AS B
                                                            ON A.USER_ID=B.ID"));
            //rp_filelist.DataSource = dt;
            //rp_filelist.DataBind();

            //下拉列表绑定
            List<string> drs = new List<string>() { 
            "标题"};//,"单位","作者"
            dr_searchtype.DataSource = drs;
            dr_searchtype.DataBind();
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
            else
            {
                Response.Redirect("UserLogin.aspx");
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

        protected void dr_searchtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            schType = dr_searchtype.SelectedIndex;
        }

        protected void lk_modifypwd_Click(object sender, EventArgs e)
        {
            JsUtil.LocationNewHref("ModifyPassword.aspx");
        }
    }
}