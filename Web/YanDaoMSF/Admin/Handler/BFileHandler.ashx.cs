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
    /// FileHandler 的摘要说明
    /// </summary>
    public class FileHandler : IHttpHandler
    {
        IDBHelp db = DBFactory.Create();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            switch (context.Request.QueryString["opt"].ToString())
            {
                case "list":
                    GetList(context);
                    break;
                case "backlist":
                    GetBackList(context);
                    break;
                case "treeList":
                    TreeList();
                    break;
                case "GetFileType":
                    GetFileType();
                    break;
                case "check":
                    CheckFile();
                    break;
                case "save":
                    SaveFile(context);
                    break;
                case "backchecklist":
                    GetBackCheckList(context);
                    break;
                case "chkfile":
                    CheckFile(context);
                    break;
                case "delete":
                    DeleteFile(context);
                    break;
            }
        }

        private void DeleteFile(HttpContext context)
        {
            HttpRequest request = HttpContext.Current.Request;
            string id = request.Form["id"];
            string sql = string.Format(@"DELETE FROM SUC_FILES WHERE ID={0}", id);
            HttpContext.Current.Response.Write("success");
        }


        private void CheckFile(HttpContext context)
        {
            HttpRequest request = HttpContext.Current.Request;
            string id = request.Form["id"];
            int isacc = Convert.ToInt32(request.Form["isacc"].ToString());
            string sql = "";
            if (db.IsExists(string.Format(@"SELECT * FROM SUC_CHECK_FILES WHERE FILE_ID={0}", id)))
            {
                sql = string.Format(@"UPDATE SUC_CHECK_FILES SET CHECK_STATE={0} WHERE FILE_ID={1}", isacc, id);
            }
            else
            {
                sql = string.Format(@"INSERT INTO SUC_CHECK_FILES VALUES({0},{1})", id, isacc);
            }
            db.ExecuteNonQuery(sql);
            HttpContext.Current.Response.Write("success");
        }

        private void GetBackCheckList(HttpContext context)
        {
            int rows = Convert.ToInt32(context.Request.Form["rows"]);
            int page = Convert.ToInt32(context.Request.Form["page"]);
            string sql = " select * from (select t.*,row_number() over(order by ID) as rowid from (";
            sql += string.Format(@"SELECT M.*,CASE WHEN N.CHECK_STATE IS NULL OR N.CHECK_STATE=0 THEN '未审核'
										WHEN N.CHECK_STATE=1 THEN '已通过'
										WHEN N.CHECK_STATE=2 THEN '未通过'
						        END CHECKSTATE FROM (	
							    SELECT X.ID,X.NAME,Y.NAME USER_NAME,X.BROWNUM,X.PUBLISH_DATE,Y.UNIT FROM(
		                            SELECT A.ID,A.NAME,A.USER_ID,A.BROWNUM,A.PUBLISH_DATE,A.TYPE,A.DOWNLOADNUM,B.NAME GRADE_NAME FROM SUC_FILES A
		                            LEFT JOIN (SELECT * FROM SUC_GRADE_CLASS) B
		                            ON A.GRADE_CLASS=B.ID) X
		                            LEFT JOIN (SELECT * FROM SUC_USER) Y
                                    ON X.USER_ID=Y.ID) M 
									LEFT JOIN 
									(SELECT * FROM SUC_CHECK_FILES) N
									ON M.ID=N.FILE_ID WHERE CHECK_STATE IS NULL OR CHECK_STATE=2");//, string.IsNullOrEmpty(id) ? "99999" : id);
            sql += string.Format(" )t) tmp where tmp.rowid >={0} and tmp.rowid <= {1}", ((page - 1) * rows) + 1, page * rows);

            DataTable dt = db.GetDataTable(sql);
            HttpContext.Current.Response.Write(JsonHelper.DataTableToJSON(dt));
        }
        private void CheckFile()
        {
            HttpContext.Current.Response.Write("success");
        }


        private void SaveFile(HttpContext context)
        {
            HttpRequest request = HttpContext.Current.Request;
            string id = request.Form["id"];


            string filename = request.Form["filename"].ToString();
            string filepath = request.Form["filepath"].ToString();
            string grade = request.Form["grade"].ToString();
            string typeid = request.Form["typeid"].ToString();


        }

        private void GetFileType()
        {
            DataTable dt = DBFactory.Create().GetDataTable(string.Format(@"SELECT * FROM SUC_FILETYPE"));
            HttpContext.Current.Response.Write(JsonHelper.DataTableToJSON(dt));
        }

        private void GetList(HttpContext context)
        {
            int rows = Convert.ToInt32(context.Request.Form["rows"]);
            int page = Convert.ToInt32(context.Request.Form["page"]);
            string id = context.Request.QueryString["mid"].ToString();
            string sql = " select * from (select t.*,row_number() over(order by ID) as rowid from (";
            sql += string.Format(@"SELECT X.ID,X.NAME,Y.NAME USER_NAME,X.BROWNUM,X.PUBLISH_DATE,Y.UNIT FROM(
		                            SELECT A.ID,A.NAME,A.USER_ID,A.BROWNUM,A.PUBLISH_DATE,A.TYPE,A.DOWNLOADNUM,B.NAME GRADE_NAME FROM SUC_FILES A
		                            LEFT JOIN (SELECT * FROM SUC_GRADE_CLASS) B
		                            ON A.GRADE_CLASS=B.ID WHERE B.ID={0}) X
		                            LEFT JOIN (SELECT * FROM SUC_USER) Y
                                    ON X.USER_ID=Y.ID", string.IsNullOrEmpty(id) ? "99999" : id);
            sql += string.Format(" )t) tmp where tmp.rowid >={0} and tmp.rowid <= {1}", ((page - 1) * rows) + 1, page * rows);

            DataTable dt = db.GetDataTable(sql);
            HttpContext.Current.Response.Write(JsonHelper.DataTableToJSON(dt));
        }


        private void GetBackList(HttpContext context)
        {
            int rows = Convert.ToInt32(context.Request.Form["rows"]);
            int page = Convert.ToInt32(context.Request.Form["page"]);
            //string id = context.Request.QueryString["mid"].ToString();
            string sql = " select * from (select t.*,row_number() over(order by ID) as rowid from (";
            sql += string.Format(@"SELECT J.*,K.NAME TYPENAME FROM (
                                    SELECT X.ID,X.NAME,Y.NAME USER_NAME,X.BROWNUM,X.PUBLISH_DATE,Y.UNIT,X.TYPE,X.GRADENAME FROM(
		                            SELECT B.NAME GRADENAME,A.ID,A.NAME,A.USER_ID,A.BROWNUM,A.PUBLISH_DATE,A.TYPE,A.DOWNLOADNUM,B.NAME GRADE_NAME FROM SUC_FILES A
		                            LEFT JOIN (SELECT * FROM SUC_GRADE_CLASS) B
		                            ON A.GRADE_CLASS=B.ID) X
		                            LEFT JOIN (SELECT * FROM SUC_USER) Y
                                    ON X.USER_ID=Y.ID
									)J
									LEFT JOIN 
									(SELECT * FROM SUC_FILETYPE) K
									ON J.TYPE=K.ID");
            sql += string.Format(" )t) tmp where tmp.rowid >={0} and tmp.rowid <= {1}", ((page - 1) * rows) + 1, page * rows);
            DataTable dt = db.GetDataTable(sql);//, string.IsNullOrEmpty(id) ? "99999" : id));
            HttpContext.Current.Response.Write(JsonHelper.DataTableToJSON(dt));
        }

        private void TreeList()
        {
            DataTable dt = db.GetDataTable(string.Format(
            @"SELECT M.ID,ISNULL(M.PARENT_ID,0) PARENT_ID,M.NAME,M.SORT
        FROM SUC_GRADE_CLASS M ORDER BY M.ID"));
            HttpContext.Current.Response.Write(JsonHelper.TableToTreeJson(dt, "ID", "NAME", "PARENT_ID", "0", true, "NAME"));
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