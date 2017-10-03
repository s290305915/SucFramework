using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using SucLib.Common;
using SucLib.Data.Factory;
using SucLib.Data.IDal;

namespace YanDaoMSF.Admin.Handler
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
            if (HttpContext.Current.Request["opt"] != null)
            {
                switch (HttpContext.Current.Request["opt"].ToString())
                {
                    case "GetUserList":
                        GetUserList(context);
                        break;
                    case "GetRole":
                        GetRole();
                        break;
                    case "save":
                        SaveUser();
                        break;
                    case "delete":
                        DeleteUser();
                        break;
                    case "check":
                        CheckUser();
                        break;
                }
            }
        }


        public void CheckUser()
        {
            HttpRequest request = HttpContext.Current.Request;
            string id = request.Form["id"];
            string name = request.Form["name"];
            if (string.IsNullOrEmpty(id))
            {
                HttpContext.Current.Response.Write(db.IsExists(string.Format("SELECT * FROM SUC_USER WHERE LOGIN_NAME='{0}'", name)) == false ? "success" : "no");
                return;
            }
            HttpContext.Current.Response.Write("success");
        }

        public void DeleteUser()
        {
            HttpRequest request = HttpContext.Current.Request;
            string id = request.Form["id"];
            if (Convert.ToInt32(db.ExecuteScalar(string.Format(@"SELECT COUNT(1) FROM SUC_USER"))) <= 1)
            {
                HttpContext.Current.Response.Write("success");
                return;
            }
            string logname = db.GetList(string.Format(@"SELECT LOGIN_NAME FROM SUC_USER WHERE ID={0}", id))[0];
            if (db.ExecuteNonQuery(string.Format(@"DELETE FROM SUC_USER WHERE ID={0}", id)) > 0)
                if (db.ExecuteNonQuery(string.Format(@"DELETE FROM SUC_LOGIN WHERE LOGIN_NAME='{0}'", logname)) > 0)
                {
                    HttpContext.Current.Response.Write("success");
                    return;
                }
            HttpContext.Current.Response.Write("no");
        }

        public void SaveUser()
        {
            HttpRequest request = HttpContext.Current.Request;
            string id = request.Form["id"];
            string name = request.Form["name"];
            string login_name = request.Form["login_name"];
            string unit = request.Form["unit"];
            string phoneno = request.Form["phoneno"];
            string role_id = request.Form["role_id"];
            bool is_password = request.Form["is_password"].ToString() == "1" ? true : false;
            string strsql = "";
            if (string.IsNullOrEmpty(id))
            {
                //ADD
                strsql = string.Format(@"INSERT INTO SUC_USER VALUES
                                        ('{0}','{1}',GETDATE(),0,GETDATE(),NULL,{2},'{3}','{4}','{5}',1)"
                    , name, login_name, role_id, phoneno, request.ServerVariables["LOCAL_ADDR"], unit);
                db.ExecuteNonQuery(strsql);
                strsql = string.Format(@"INSERT INTO SUC_LOGIN VALUES('{0}','1',0,NULL,NULL,GETDATE());", login_name);
                db.ExecuteNonQuery(strsql);
                HttpContext.Current.Response.Write("success");
                return;
            }
            //EDIT
            strsql = string.Format(@"UPDATE SUC_USER SET NAME='{1}',LOGIN_NAME ='{2}',ROLE_ID={3},PHONENO='{4}',UNIT='{5}'
                                        WHERE ID={0}", id, name, login_name, role_id, phoneno, unit);
            db.ExecuteNonQuery(strsql);
            if (is_password)
            {
                //MODIFY PASSWORD
                strsql = string.Format(@"UPDATE SUC_LOGIN SET PASSWORD='{0}' WHERE LOGIN_NAME='{1}'", 1, login_name);
                db.ExecuteNonQuery(strsql);
            }
            HttpContext.Current.Response.Write("success");
        }

        public void GetUserList(HttpContext context)
        {
            int rows = Convert.ToInt32(context.Request.Form["rows"]);
            int page = Convert.ToInt32(context.Request.Form["page"]);
            string s_name = context.Request.Form["s_name"];
            string sql = " select * from (select t.*,row_number() over(order by ID desc) as rowid from (";
            sql += string.Format(@"SELECT * FROM SUC_USER {0}", string.IsNullOrEmpty(s_name) ? "" : string.Format(@" WHERE NAME LIKE '%{0}%'", s_name));
            sql += string.Format(" )t) tmp where tmp.rowid >={0} and tmp.rowid <= {1}", ((page - 1) * rows) + 1, page * rows);

            DataTable dt = db.GetDataTable(sql);
            HttpContext.Current.Response.Write(JsonHelper.DataTableToJSON(dt));
        }

        public void GetRole()
        {
            DataTable dt = db.GetDataTable(string.Format(@"SELECT * FROM SUC_ROLE ORDER BY ID DESC"));
            HttpContext.Current.Response.Write(JsonHelper.DataTableToJSON(dt));
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