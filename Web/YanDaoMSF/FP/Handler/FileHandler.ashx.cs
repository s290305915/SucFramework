using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using SucLib.Common;
using SucLib.Data.Factory;
using SucLib.Data.IDal;

namespace YanDaoMSF.FP.Handler
{
    /// <summary>
    /// FileHandler 的摘要说明
    /// </summary>
    public class FileHandler : IHttpHandler, IRequiresSessionState
    {
        IDBHelp db = DBFactory.Create();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string opt = context.Request.QueryString["opt"].ToString();
            if (opt.IndexOf("?") > 0)
            {
                opt = opt.Substring(0, opt.IndexOf("?"));
            }
            switch (opt)
            {
                case "list":
                    GetList(context);
                    break;
                case "treeList":
                    TreeList();
                    break;
                case "treelazyList":
                    TreeLazyList();
                    break;
                case "treelazyListChild":
                    TreeLazyListChild();
                    break;
                case "FileType":
                    FileType();
                    break;
                //后台
                case "backlist":
                    GetBackList(context);
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
                case "ReadFile":
                    ReadFile(context);
                    break;
                case "SaveInfo":
                    SaveInfo(context);
                    break;
                case "delete":
                    DeleteFile(context);
                    break;
            }
        }

        private void SaveInfo(HttpContext context)
        {
            string msg = string.Empty;
            string q = context.Request["q"].ToString();
            string[] qs = q.Split(',');
            string[] paras = new string[qs.Length];
            for (int i = 0; i < qs.Length; i++)
            {
                string[] ss = qs[i].Split(':');
                paras[i] = ss[1];
            }
            //写导入数据的方法
            //HttpContext.Current.Session["userId"] = "123";
            string usern = paras[6]; //SucCookie.Read("username");// uid; //
            if (!string.IsNullOrEmpty(usern))
            {
                DataTable userDT = db.GetDataTable(string.Format(@"SELECT * FROM SUC_USER WHERE LOGIN_NAME='{0}'", usern));
                if (userDT.Rows.Count > 0)
                {
                    DateTime publicdate = DateTime.Now;
                    string type = paras[0];
                    string fromwhere = userDT.Rows[0]["Unit"].ToString();
                    string userid = userDT.Rows[0]["ID"].ToString();
                    string filesize = paras[2];
                    string filepath = paras[1];
                    string gradeclass = paras[5];
                    db.ExecuteNonQuery(string.Format(@"INSERT INTO SUC_FILES (NAME, USER_ID,BROWNUM,TYPE,FROMWHERE,DOWNLOADNUM,FILETYPE,FILEPATH,FILESIZE,GRADE_CLASS,PUBLISH_DATE)
                        VALUES ('" + paras[3] + "'," + userid + ",0,'" + type + "','" + fromwhere + "',0," + paras[4] + ",'" + filepath + "','" + filesize + "'," + gradeclass + ",GETDATE())"));
                    msg = "上传成功！";//Default
                    HttpContext.Current.Response.Write(msg);
                    return;
                }
                else
                {
                    msg = "该用户不存在！";
                    HttpContext.Current.Response.Write(msg);
                    return;
                }
            }
            msg = " 成功!";//文件大小为:" + files[0].ContentLength;//+" Content:"+content;
            //
        }


        public void ReadFile(HttpContext context)
        {
            //HttpFileCollection files = HttpContext.Current.Request.Files;//这里只能用<input type="file" />才能有效果,因为服务器控件是HttpInputFile类型
            HttpPostedFile files = context.Request.Files["Filedata"];//
            string msg = string.Empty;
            string filename = string.Empty;
            if (files != null)
            {
                try
                {
                    string[] fnames = files.FileName.Split('.');
                    string newfname = fnames[0] + DateTime.Now.ToString("yyyyMMddHHmmss") + "." + fnames[1];
                    filename = HttpContext.Current.Server.MapPath("/Files/") + System.IO.Path.GetFileName(newfname); //Server.MapPath("/") + System.IO.Path.GetFileName(files[0].FileName);
                    files.SaveAs(filename);
                    string path = "~/Files/" + newfname;
                    msg += "." + fnames[1] + ",filename:" + path + ",length:" + (files.ContentLength / 1000).ToString() + "kb";
                    //File.WriteAllText(filename, sqls, System.Text.Encoding.Default);
                    HttpContext.Current.Response.Write(msg);
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                    HttpContext.Current.Response.Write(msg);
                    return;
                }
            }
            else
            {
                msg = "错误，未选择文件！";
                HttpContext.Current.Response.Write(msg);
            }
            //HttpContext.Current.Response.Write("success");
        }

        private void DeleteFile(HttpContext context)
        {
            HttpRequest request = HttpContext.Current.Request;
            string id = request.Form["id"];
            string sql = string.Format(@"DELETE FROM SUC_FILES WHERE ID={0}", id);
            db.ExecuteNonQuery(sql);
            HttpContext.Current.Response.Write("success");
        }

        private void GetBackList(HttpContext context)
        {
            int rows = Convert.ToInt32(context.Request.Form["rows"]);
            int page = Convert.ToInt32(context.Request.Form["page"]);
            string s_name = context.Request.Form["s_name"];
            //string id = context.Request.QueryString["mid"].ToString();
            string sql = " select * from (select t.*,row_number() over(order by ID) as rowid from (";
            string msql = string.Format(@"SELECT J.*,K.NAME TYPENAME FROM (
                                    SELECT X.ID,X.NAME,Y.NAME USER_NAME,X.BROWNUM,X.PUBLISH_DATE,Y.UNIT,X.TYPE,X.GRADENAME FROM(
		                            SELECT B.NAME GRADENAME,A.ID,A.NAME,A.USER_ID,A.BROWNUM,A.PUBLISH_DATE,A.FILETYPE TYPE,A.DOWNLOADNUM,B.NAME GRADE_NAME FROM SUC_FILES A
		                            LEFT JOIN (SELECT * FROM SUC_GRADE_CLASS) B
		                            ON A.GRADE_CLASS=B.ID) X
		                            LEFT JOIN (SELECT * FROM SUC_USER) Y
                                    ON X.USER_ID=Y.ID
									)J
									LEFT JOIN 
									(SELECT * FROM SUC_FILETYPE) K
									ON J.TYPE=K.ID
                                        ");
            msql += string.IsNullOrEmpty(s_name) ? "" : string.Format(" WHERE K.NAME LIKE '%{0}%' OR J.USER_NAME LIKE '%{0}%' OR K.NAME LIKE '%{0}%' OR J.UNIT LIKE '%{0}%' OR J.GRADENAME LIKE '%{0}%'", s_name);
            sql += msql + string.Format(" )t) tmp where tmp.rowid >={0} and tmp.rowid <= {1}", ((page <= 0 ? 1 : page - 1) * rows) + 1, page <= 0 ? 1 : page * rows);
            DataTable dt = db.GetDataTable(sql);//, string.IsNullOrEmpty(id) ? "99999" : id));

            string total = db.GetList(string.Format(@"SELECT COUNT(1) FROM( {0} ) A", msql))[0];
            string json = "{\"total\":" + total + ",\"rows\":";
            json += JsonHelper.DataTableToJSON(dt);
            json += "}";
            //JsonHelper.DataTableToJSON(dt)
            HttpContext.Current.Response.Write(json);
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
            string msql = string.Format(@"SELECT M.*,CASE WHEN N.CHECK_STATE IS NULL OR N.CHECK_STATE=0 THEN '未审核'
										WHEN N.CHECK_STATE=1 THEN '已通过'
										WHEN N.CHECK_STATE=2 THEN '未通过'
						        END CHECKSTATE FROM (	
							    SELECT X.*,Y.NAME USERNAME,Y.UNIT FROM(
		                            SELECT A.ID,A.NAME,A.USER_ID,A.BROWNUM,A.PUBLISH_DATE,A.TYPE,A.DOWNLOADNUM,B.NAME GRADE_NAME FROM SUC_FILES A
		                            LEFT JOIN (SELECT * FROM SUC_GRADE_CLASS) B
		                            ON A.GRADE_CLASS=B.ID) X
		                            LEFT JOIN (SELECT * FROM SUC_USER) Y
                                    ON X.USER_ID=Y.ID) M 
									LEFT JOIN 
									(SELECT * FROM SUC_CHECK_FILES) N
									ON M.ID=N.FILE_ID WHERE CHECK_STATE IS NULL OR CHECK_STATE=2");//, string.IsNullOrEmpty(id) ? "99999" : id);

            sql += msql + string.Format(" )t) tmp where tmp.rowid >={0} and tmp.rowid <= {1}", ((page - 1) * rows) + 1, (page + 1) * rows);

            DataTable dt = db.GetDataTable(sql);
            string total = db.GetList(string.Format(@"SELECT COUNT(1) FROM( {0} ) A", msql))[0];
            string json = "{\"total\":" + total + ",\"rows\":";
            json += JsonHelper.DataTableToJSON(dt);
            json += "}";
            //JsonHelper.DataTableToJSON(dt)
            HttpContext.Current.Response.Write(json);
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

        private string GetLowLev(string all, IList<string> ids)
        {
            try
            {
                string allids = all;
                string idss = "";
                foreach (string id in ids)
                {
                    idss += id + ",";
                }
                idss = idss.Substring(0, idss.Length - 1);
                if (db.GetList(string.Format(@"SELECT HASCHILD FROM SUC_GRADE_CLASS WHERE ID IN ({0})", idss)).Contains("0"))
                { return idss; }
                ids = db.GetList(string.Format(@"SELECT ID FROM SUC_GRADE_CLASS WHERE PARENT_ID in ({0})", idss));
                if (ids.Count > 0)
                {
                    IList<string> ch_levels = db.GetList(string.Format(@"SELECT LEVEL FROM SUC_GRADE_CLASS WHERE PARENT_ID in ({0})", idss));
                    if (ch_levels.Contains("5"))
                    {
                        //到了倒数第二级
                        string pid5s = "";
                        foreach (string pid5 in ids)
                        {
                            pid5s += pid5 + ",";
                        }
                        pid5s = pid5s.Substring(0, pid5s.Length - 1);
                        IList<string> l6ids = db.GetList(string.Format(@"SELECT ID FROM SUC_GRADE_CLASS WHERE PARENT_ID in ({0})", pid5s));
                        string l6idss = "";
                        foreach (string id in l6ids)
                        {
                            l6idss += id + ",";
                        }
                        l6idss = l6idss.Substring(0, l6idss.Length - 1);
                        allids += l6idss;
                        return allids;
                    }
                    else
                    {
                        allids += idss + ",";
                        return GetLowLev(allids, ids);
                    }
                }
                else
                {
                    return "";
                }
            }
            catch { return ""; }
        }

        private void GetList(HttpContext context)
        {
            string id = context.Request.QueryString["mid"].ToString();
            //System.Diagnostics.EventLog.WriteEntry("GetList id" + DateTime.Now.ToString("yyyyMMddHH24mmss"), id, System.Diagnostics.EventLogEntryType.Information);

            string ftype = context.Request.QueryString["ftype"].ToString();
            int stype = Convert.ToInt32(context.Request.QueryString["stype"].ToString());
            string key = context.Request.QueryString["key"].ToString();
            string sql = "";
            string searchcon = "";
            int rows = Convert.ToInt32(context.Request.Form["rows"]);
            int page = Convert.ToInt32(context.Request.Form["page"]);
            if (!string.IsNullOrEmpty(key))
            {
                switch (stype)
                {
                    case 0:
                        searchcon = string.Format(@" AND NAME LIKE '%{0}%'", key);
                        break;
                    case 1:
                        searchcon = string.Format(@" AND UNIT LIKE '%{0}%'", key);
                        break;
                    case 2:
                        searchcon = string.Format(@" AND USER_NAME LIKE '%{0}%'", key);
                        break;
                }
            }
            DataTable dt;
            string total;
            IList<string> ids;
            string json;
            string msql;
            sql = " select * from (select t.*,row_number() over(order by ID) as rowid from (";
            if (id == "1")
            {
                msql = string.Format(@"SELECT J.*,K.CHECK_STATE FROM(
                                    SELECT X.ID,X.NAME,Y.NAME USER_NAME,X.BROWNUM,X.PUBLISH_DATE,Y.UNIT,X.FILETYPE,X.GRADEID,X.TYPE  FROM(
		                            SELECT A.ID,A.NAME,A.USER_ID,A.BROWNUM,A.PUBLISH_DATE,A.TYPE,A.DOWNLOADNUM,B.NAME GRADE_NAME,A.FILETYPE,B.ID GRADEID  
									FROM SUC_FILES A
		                            LEFT JOIN (SELECT * FROM SUC_GRADE_CLASS) B
		                            ON A.GRADE_CLASS=B.ID) X
		                            LEFT JOIN (SELECT * FROM SUC_USER) Y
                                    ON X.USER_ID=Y.ID
									)J LEFT JOIN (
									SELECT * FROM SUC_CHECK_FILES)K
									ON J.ID=K.FILE_ID  WHERE CHECK_STATE=1 {0}", ftype == "0" ? "" : string.Format(@"AND FILETYPE={0}", ftype));// AND FILETYPE=3
                msql += searchcon;

                ids = new List<string>() { id };//db.GetList(string.Format(@"SELECT PARENT_ID FROM SUC_GRADE_CLASS WHERE ID={0}", id))[0]
                msql += GetLowLev("", ids).Length > 0 ? string.Format(@" AND GRADEID IN({0})", GetLowLev("", ids)) : " AND GRADEID IN (99999)";
                sql += msql;
                sql += string.Format(" )t) tmp where tmp.rowid >={0} and tmp.rowid <= {1}", ((page - 1) * rows) + 1, page * rows);
                //System.Diagnostics.EventLog.WriteEntry("GetList sql" + DateTime.Now.ToString("yyyyMMddHH24mmss"), sql, System.Diagnostics.EventLogEntryType.Information);
                LogHelper.Write("获取文件的sql:" + sql);
                dt = db.GetDataTable(sql);
                LogHelper.Write(string.Format(@"SELECT COUNT(1) FROM( {0} ) A", msql));
                total = db.GetList(string.Format(@"SELECT COUNT(1) FROM( {0} ) A", msql))[0];
                json = "{\"total\":" + total + ",\"rows\":";
                json += JsonHelper.DataTableToJSON(dt);
                json += "}";
                HttpContext.Current.Response.Write(json);
                //HttpContext.Current.Response.Write("{\"total\":\"" + total + "\",\"rows\":[" + JsonHelper.DataTableToJSON(dt) + "]}");
                return;
            }
            msql = string.Format(@"SELECT J.*,K.CHECK_STATE FROM(
                                    SELECT X.ID,X.NAME,Y.NAME USER_NAME,X.BROWNUM,X.PUBLISH_DATE,Y.UNIT,X.FILETYPE,X.GRADEID,X.TYPE  FROM(
		                            SELECT A.ID,A.NAME,A.USER_ID,A.BROWNUM,A.PUBLISH_DATE,A.TYPE,A.DOWNLOADNUM,B.NAME GRADE_NAME,A.FILETYPE,B.ID GRADEID  
									FROM SUC_FILES A
		                            LEFT JOIN (SELECT * FROM SUC_GRADE_CLASS) B
		                            ON A.GRADE_CLASS=B.ID) X
		                            LEFT JOIN (SELECT * FROM SUC_USER) Y
                                    ON X.USER_ID=Y.ID
									)J LEFT JOIN (
									SELECT * FROM SUC_CHECK_FILES)K
									ON J.ID=K.FILE_ID  WHERE CHECK_STATE=1 {0}",
                //string.IsNullOrEmpty(id) ? "" : "AND J.GRADEID=" + id,
                                    ftype == "0" ? "" : string.Format(@"AND FILETYPE={0}", ftype));
            msql += searchcon;
            ids = new List<string>() { string.IsNullOrEmpty(id) ? "1" : id };//.Count > 0 ? ids : ids = ids.Add("1")  db.GetList(string.Format(@"SELECT PARENT_ID FROM SUC_GRADE_CLASS WHERE ID={0}", id))[0]
            msql += GetLowLev("", ids).Length > 0 ? string.Format(@" AND GRADEID IN({0})", GetLowLev("", ids)) : " AND GRADEID IN (99999)";
            sql += msql;
            sql += string.Format(" )t) tmp where tmp.rowid >={0} and tmp.rowid <= {1}", ((page - 1) * rows) + 1, page * rows);
            //LogHelper.Write(sql);
            dt = db.GetDataTable("统计的sql:" + sql);
            //LogHelper.Write(string.Format(@"SELECT COUNT(1) FROM( {0} ) A", msql));
            total = db.GetList(string.Format(@"SELECT COUNT(1) FROM( {0} ) A", msql))[0];
            json = "{\"total\":" + total + ",\"rows\":";
            json += JsonHelper.DataTableToJSON(dt);
            json += "}";
            HttpContext.Current.Response.Write(json);
            //HttpContext.Current.Response.Write("{\"total\":\"" + total + "\",\"rows\":[" + JsonHelper.DataTableToJSON(dt) + "]}");
        }

        private void TreeList()
        {
            DataTable dt = db.GetDataTable(string.Format(
               @"SELECT M.ID,ISNULL(M.PARENT_ID,0) PARENT_ID,M.NAME,M.SORT,M.LEVEL
        FROM SUC_GRADE_CLASS M ORDER BY M.ID"));// WHERE PARENT_ID<>0
            HttpContext.Current.Response.Write(JsonHelper.TableToTreeJson(dt, "ID", "NAME", "PARENT_ID", "0", true, "NAME", "LEVEL"));
        }

        /// <summary>
        /// 异步树
        /// </summary>
        private void TreeLazyListChild()
        {
            HttpRequest request = HttpContext.Current.Request;
            string pid = request.Form["id"];
            DataTable dt = db.GetDataTable(string.Format(
               @"SELECT M.ID,ISNULL(M.PARENT_ID,0) PARENT_ID,M.NAME,M.SORT,M.LEVEL,HASCHILD
        FROM SUC_GRADE_CLASS M where M.PARENT_ID={0} ORDER BY M.ID ", pid));// WHERE PARENT_ID<>0
            //HttpContext.Current.Response.Write(JsonHelper.TableToTreeJsons(dt, "ID", "NAME", "PARENT_ID", "0", true, "NAME", "LEVEL",pid));
            string resultStr = "";
            resultStr += "[";
            foreach (DataRow item in dt.Rows)
            {
                resultStr += "{";
                if (item[5].ToString().Equals("1"))
                    resultStr += string.Format("\"id\": \"{0}\", \"text\": \"{1}\", \"iconCls\": \"icon-folder\", \"state\": \"closed\"", item["ID"], item["NAME"]);
                else
                    resultStr += string.Format("\"id\": \"{0}\", \"text\": \"{1}\"", item["ID"], item["NAME"]);
                resultStr += "},";
            }
            resultStr = resultStr.Substring(0, resultStr.Length - 1);
            resultStr += "]";
            HttpContext.Current.Response.Write(resultStr);
        }

        private void TreeLazyList()
        {
            DataTable dt = db.GetDataTable(string.Format(
            @"SELECT M.ID,ISNULL(M.PARENT_ID,0) PARENT_ID,M.NAME,M.SORT,M.LEVEL,'CLOSED' STATE,HASCHILD
            FROM SUC_GRADE_CLASS M  WHERE M.PARENT_ID=1 ORDER BY M.ID DESC"));// WHERE PARENT_ID<>0 
            sb = new System.Text.StringBuilder();
            sb.Append("[");
            sb.Append("{\"id\":\"1\",");
            sb.Append("\"text\":\"课程目录\",");
            sb.Append("\"attributes\":{\"sort\":\"0\"},");
            sb.Append("\"children\":");
            //string moduleListJson = JsonHelper.DataTableToJSON(dt, "id", "name", "parent_id", int.Parse(strParentId));
            //string moduleListJson =
            GetJsonInfo(dt, "id", "name", "parent_id", 1).ToString();
            sb.Append("}]");
            //HttpContext.Current.Response.Write(JsonHelper.TableToTreeJson(dt, "ID", "NAME", "PARENT_ID", "0", true, "NAME", "LEVEL"));
            HttpContext.Current.Response.Write(sb.ToString());
        }


        System.Text.StringBuilder sb;
        public System.Text.StringBuilder GetJsonInfo(DataTable dt, string value_filed, string text_filed, string parent_filed, int parentid)
        {
            sb.Append("[");
            DataRow[] _drlist = dt.Select(parent_filed + "=" + parentid + " AND " + value_filed + "<>" + parentid);
            // DataRow[] _drlist = dt.Select(parentid == 0 ? (parent_filed + " IS NULL OR " + parent_filed + " = 0") : (parent_filed + " = " + parentid + ""));//dt.Select(parent_filed + "=" + parentid); 
            for (int i = 0; i < _drlist.Length; i++)
            {
                DataRow dr = _drlist[i];
                sb.Append("{\"id\":\"" + dr[value_filed].ToString() + "\",");
                sb.Append("\"text\":\"" + dr[text_filed].ToString() + "\",");
                //sb.Append("\"state\":\"closed\",");
                sb.Append("\"attributes\":{\"sort\":\"" + dr["sort"].ToString() + "\"}");

                if (dr[value_filed].ToString() != parentid.ToString())
                {
                    // DataRow[] _drlist_other = dt.Select(parent_filed + "=" + dr[value_filed].ToString() + " and " + value_filed + "<>" + parentid);
                    //if (_drlist_other.Length > 0)

                    //{
                    string haschild = Convert.ToString(dt.Rows[i]["HASCHILD"]) ?? "";
                    if (haschild.Equals("1") && !string.IsNullOrEmpty(haschild))
                    {
                        sb.Append(",\"state\":\"closed\"");//closed
                        sb.Append(",\"children\":");
                        GetJsonInfo(dt, value_filed, text_filed, parent_filed, int.Parse(dr[value_filed].ToString()));
                    }
                    //SS  }
                }
                if (i == _drlist.Length - 1)
                {
                    sb.Append(",\"checked\":false}");
                }
                else
                {
                    sb.Append(",\"checked\":false},");
                }
            }
            sb.Append("]");
            return sb;
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }


        public void FileType()
        {
            DataTable dt = DBFactory.Create().GetDataTable(string.Format(@"SELECT * FROM SUC_FILETYPE"));
            HttpContext.Current.Response.Write(JsonHelper.DataTableToJSON(dt));
        }
    }
}