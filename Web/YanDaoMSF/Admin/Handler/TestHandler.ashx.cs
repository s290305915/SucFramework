using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Newtonsoft.Json;
using SucLib.Common;
using SucLib.Data.Factory;
using SucLib.Data.IDal;

namespace YanDaoMSF.Admin.Handler
{
    /// <summary>
    /// TestHandler 的摘要说明
    /// </summary>
    public class TestHandler : IHttpHandler
    {
        IDBHelp db = DBFactory.Create();
        public void ProcessRequest(HttpContext context)
        {
            HttpContext.Current.Response.ContentType = "text/plain";
            // HttpContext.Current.Response.Write("Hello World");
            switch (HttpContext.Current.Request.QueryString["opt"].ToString())
            {
                case "list":
                    EntityList(context);
                    break;
                case "Entlist":
                    CmbEntlist();
                    break;
                case "EntClasslist":
                    CmbEntClasslist();
                    break;
                case "SaveEntity":
                    Save(context);
                    break;
                case "ReadFile":
                    ReadFile(context);
                    break;
                case "loadmain":
                    LoadMain(context);
                    break;
                case "loadmainnoex":
                    AllNetUserMain(context);
                    break;
                case "Edt":
                    EditSingle(context);
                    break;
                case "temprows":
                    GetTempRows(context);
                    break;
                case "TREntitylist":
                    GetTREntityList(context);
                    break;
                case "TRWarnlist":
                    GetTRWarnList(context);
                    break;
                case "TRgetuserinfo":
                    GetTRUserInfo(context);
                    break;
                case "TRHandleResult":
                    GetTRHandleResult(context);
                    break;
                case "TRSaveHandle":
                    SaveTRhandle(context);
                    break;
            }
        }


        public void SaveTRhandle(HttpContext context)
        {
            QueryManager query = new QueryManager();
            try
            {
                string phone = context.Request["phone"].ToString();
                string result = context.Request["result"].ToString();
                string id = context.Request["id"].ToString();
                string remark = context.Request["remark"].ToString();
                string entid = context.Request["entid"].ToString();
                int enttype = Convert.ToInt32(query.Query(string.Format(@"SELECT CLASS_ID FROM KG_ENTITY_INFO WHERE ID={0}", entid)).Tables[0].Rows[0][0].ToString());
                string sql = string.Format(@"UPDATE KG_{3}_PHONE_INFO_{4}  SET
                          IS_HANDLE = 1 
                          ,REMARK = '{0}'  
                          ,RESULT = '{1}'
                        WHERE ID={2} ", remark, result, id, enttype == 1 ? "ENTITY" : "WARN", entid);
                query.Execute(sql);
                //插入log
                sql = string.Format(@"INSERT INTO KG_ENTITY_INFO_LOG  VALUES (
                            KG_ENTITY_INFO_LOG_SEQ.NEXTVAL  -- ID - DECIMAL(10)
                          ,{0}   -- ENTITY_ID - DECIMAL(10)
                          ,CURRENT TIMESTAMP  -- OP_TIME - TIMESTAMP
                          ,{1}   -- USER_ID - DECIMAL(10)
                          ,'{2}'  -- REMARK - VARCHAR(4000)
                          ,'{3}'  -- PHONE_NO - VARCHAR(20)
                          ,''  -- HANDE_TYPE - VARCHAR(50)
                          ,'{4}'  -- RESULT - VARCHAR(50)
                        )", id, HttpContext.Current.Session["userId"], remark, phone, result);
                query.Execute(sql);
                HttpContext.Current.Response.Write("success");
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex.Message);
            }
        }


        public void GetTRHandleResult(HttpContext context)
        {
            string phone = context.Request["phone_no"].ToString();
            QueryManager query = new QueryManager();
            try
            {
                DataTable dtents = query.Query(string.Format(@"SELECT ID,CLASS_ID FROM KG_ENTITY_INFO")).Tables[0];
                if (dtents.Rows.Count > 0)
                {
                    string sql = "";// string.Format(@"SELECT A.PHONE_NO PHONE, C.NAME ENTITY_NAME, B.OP_TIME OP_TIME,B.RESULT RESULT
                    //FROM KG_ENTITY_PHONE_INFO A,KG_ENTITY_INFO_LOG B ,KG_ENTITY_INFO C
                    //WHERE A.PHONE_NO=B.PHONE_NO AND A.ID=B.ENTITY_ID AND C.ID=A.ENTITY_ID
                    //AND A.PHONE_NO='{0}'", phone);
                    for (int i = 0; i < dtents.Rows.Count; i++)
                    {
                        int etype = Convert.ToInt32(dtents.Rows[i][1].ToString());
                        sql += string.Format(@" SELECT A.PHONE_NO PHONE, C.NAME ENTITY_NAME,
                                            B.OP_TIME OP_TIME,B.RESULT RESULT
                                            FROM KG_{0}_PHONE_INFO_{1} A,KG_ENTITY_INFO_LOG B ,KG_ENTITY_INFO C
                                            WHERE A.PHONE_NO=B.PHONE_NO AND A.ID=B.ENTITY_ID AND C.ID=A.ENTITY_ID 
                                            AND A.PHONE_NO='{2}'        
                                            ", etype == 1 ? "ENTITY" : "WARN", dtents.Rows[i][0].ToString(), phone);
                        if (i < dtents.Rows.Count - 1)
                            sql += " UNION ALL ";
                    }
                    DataTable dt = query.Query(sql).Tables[0];
                    HttpContext.Current.Response.Write(JsonConvert.SerializeObject(dt));//.ToLower()
                }
                else
                {
                    HttpContext.Current.Response.Write("[]");
                }
            }
            catch (Exception e)
            {
                HttpContext.Current.Response.Write(e.Message);
            }
        }


        public void GetTRUserInfo(HttpContext context)
        {
            QueryManager query = new QueryManager();
            string nowmonth = DateTime.Now.ToString("yyyyMM");//"201602";//
            string phone = context.Request["phone_no"].ToString();//"18384063349";//
            string sql = string.Format(@"SELECT  GROUP_NAME,PHONE_NO,RUN_NAME,MODE_NAME,MEANS_CS_NAME,OPEN_TIME,
           case when IS_KD=1 then '是' else '否' end IS_KD,
           case when IS_KD_SINGLE=1 then '是' else '否' end IS_KD_SINGLE,
           case when IS_KD_FAM=1 then '是' else '否' end IS_KD_FAM,
           case when IS_KD_TV=1 then '是' else '否' end IS_KD_TV,
           case when IS_4G_USER=1 then '是' else '否' end IS_4G_USER,
           case when IS_4G_PRC=1 then '是' else '否' end IS_4G_PRC,
           G4_PRC_FEE,
           case when IS_4G_TERM=1 then '是' else '否' end IS_4G_TERM,
           ARPU         
        FROM KTL_KG001_DC_USERS_{0} WHERE PHONE_NO='{1}'", nowmonth, phone);
            DataTable dt = query.Query(sql).Tables[0];
            HttpContext.Current.Response.Write(JsonConvert.SerializeObject(dt));//.ToLower()
        }


        public void GetTREntityList(HttpContext context)
        {
            QueryManager query = new QueryManager();
            string p_no = string.Empty;
            try
            {
                p_no = context.Request["p_no"].ToString();
                if (string.IsNullOrEmpty(p_no))
                {
                    HttpContext.Current.Response.Write("[]");
                    return;
                }
            }
            catch
            {
                HttpContext.Current.Response.Write("[]");
                return;
            }
            DataTable dtents = query.Query(string.Format(@"SELECT ID FROM KG_ENTITY_INFO WHERE CLASS_ID=1")).Tables[0];
            if (dtents.Rows.Count > 0)
            {
                string sql = "";// string.Format(@"select A.ID ID,C.NAME CLASS_NAME,B.NAME ENTITY_NAME,A.PHONE_NO PHONE_NO,A.YEARMONTH YEARMONTH
                //from KG_ENTITY_PHONE_INFO A, KG_ENTITY_INFO B, KG_ENTITY_CLASS C 
                //WHERE A.ENTITY_ID=B.ID AND B.CLASS_ID=C.ID AND A.PHONE_NO='{0}' AND IS_HANDLE=0 AND C.ID=1
                //ORDER BY A.ID ASC", p_no);
                for (int i = 0; i < dtents.Rows.Count; i++)
                {
                    sql += string.Format(@" SELECT A.ID, A.ENTITY_ID, A.PHONE_NO, A.IS_HANDLE,
                                           A.REMARK, A.OP_TIME, A.USER_ID, A.YEARMONTH, 
                                           A.RESULT, A.HANDE_TYPE, B.NAME, B.STATE,C.NAME CLASS_NAME
                                           FROM KG_ENTITY_PHONE_INFO_{0} A, KG_ENTITY_INFO B,KG_ENTITY_CLASS C
                                           WHERE A.ENTITY_ID=B.ID AND B.CLASS_ID=C.ID  AND PHONE_NO='{1}' 
                                           AND IS_HANDLE=0 
                                            ", dtents.Rows[i][0].ToString(), p_no);
                    if (i < dtents.Rows.Count - 1)
                        sql += " UNION ALL ";
                }
                DataTable dt = query.Query(sql).Tables[0];
                HttpContext.Current.Response.Write(JsonConvert.SerializeObject(dt));//.ToLower()
            }
            else
            { HttpContext.Current.Response.Write("[]"); }
        }


        public void GetTRWarnList(HttpContext context)
        {
            QueryManager query = new QueryManager();
            string p_no = string.Empty;
            try
            {
                p_no = context.Request["p_no"].ToString();
                if (string.IsNullOrEmpty(p_no))
                {
                    HttpContext.Current.Response.Write("[]");
                    return;
                }
            }
            catch
            {
                HttpContext.Current.Response.Write("[]");
                return;
            }
            DataTable dtents = query.Query(string.Format(@"SELECT ID FROM KG_ENTITY_INFO WHERE CLASS_ID=2")).Tables[0];
            if (dtents.Rows.Count > 0)
            {
                string sql = "";// string.Format(@"select A.ID ID,C.NAME CLASS_NAME,B.NAME ENTITY_NAME,A.PHONE_NO PHONE_NO,A.YEARMONTH YEARMONTH
                //from KG_ENTITY_PHONE_INFO A, KG_ENTITY_INFO B, KG_ENTITY_CLASS C 
                //WHERE A.ENTITY_ID=B.ID AND B.CLASS_ID=C.ID AND A.PHONE_NO='{0}' AND IS_HANDLE=0 AND C.ID=1
                //ORDER BY A.ID ASC", p_no);
                for (int i = 0; i < dtents.Rows.Count; i++)
                {
                    sql += string.Format(@" SELECT A.ID, A.ENTITY_ID, A.PHONE_NO, A.IS_HANDLE,
                                           A.REMARK, A.OP_TIME, A.USER_ID, A.YEARMONTH, 
                                           A.RESULT, A.HANDE_TYPE, B.NAME, B.STATE,C.NAME CLASS_NAME
                                           FROM KG_WARN_PHONE_INFO_{0} A, KG_ENTITY_INFO B,KG_ENTITY_CLASS C
                                           WHERE A.ENTITY_ID=B.ID AND B.CLASS_ID=C.ID  AND PHONE_NO='{1}' 
                                           AND IS_HANDLE=0 
                                             ", dtents.Rows[i][0].ToString(), p_no);
                    if (i < dtents.Rows.Count - 1)
                        sql += " UNION ALL ";
                }
                DataTable dt = query.Query(sql).Tables[0];
                HttpContext.Current.Response.Write(JsonConvert.SerializeObject(dt));//.ToLower()
            }
            else
            { HttpContext.Current.Response.Write("[]"); }
        }

        public void AllNetUserMain(HttpContext context)
        {
            QueryManager query = new QueryManager();
            string id = context.Request["et"].ToString();
            string yearmonth = DateTime.Parse(context.Request.Form["txtStart"]).ToString("yyyyMM");
            string tablename = "KG_WARN_PHONE_INFO_" + id;//+ HttpContext.Current.Session["userId"] + "_"
            string seqname = "KG_WARN_PHONE_INFO_" + id + "_SEQ";//HttpContext.Current.Session["userId"] + "_"+
            //删除之前数据
            query.Execute(string.Format(@"ALTER TABLE {0} ACTIVATE NOT LOGGED INITIALLY WITH EMPTY TABLE", tablename));
            //string temptbname = "KTL_KG001_DC_USERS_" + DateTime.Now.ToString("yyyyMM");//"TEMP_ENTITY_IMPROT_PHONE_" + HttpContext.Current.Session["userId"] + "_" + id;// ;
            string temptbname = "KTL_KG001_DC_USERS_201602";//"TEMP_ENTITY_IMPROT_PHONE_" + HttpContext.Current.Session["userId"] + "_" + id;// ;
            string megsql = string.Format(@"MERGE INTO {4} AS A  
            USING {0} AS B  
            ON A.PHONE_NO=B.PHONE_NO AND A.ENTITY_ID={1}
            WHEN NOT MATCHED THEN INSERT VALUES 
            ({5}.NEXTVAL,
              {1},B.PHONE_NO,0,'',
                CURRENT TIMESTAMP,{2},{3},'','')  
            ELSE IGNORE; ", temptbname, id, HttpContext.Current.Session["userId"], yearmonth, tablename, seqname);
            query.Execute(megsql);
            //ALTER TABLE {0} ACTIVATE NOT LOGGED INITIALLY WITH EMPTY TABLE
            //query.Execute(string.Format(@"DROP TABLE {0}", temptbname));
            string efrows = query.Query(string.Format(@"SELECT COUNT(1) FROM {1} WHERE ENTITY_ID={0}", id, tablename)).Tables[0].Rows[0][0].ToString();
            HttpContext.Current.Response.Write(efrows);//.ToLower()efrows
        }


        public void LoadMain(HttpContext context)
        {
            QueryManager query = new QueryManager();
            string id = context.Request["et"].ToString();
            string temptbname = "TEMP_WARN_IMPROT_PHONE_" + HttpContext.Current.Session["userId"] + "_" + id;// ;
            string tablename = "KG_WARN_PHONE_INFO_" + id;//+ HttpContext.Current.Session["userId"]+ "_" 
            string seqname = "KG_WARN_PHONE_INFO_" + id + "_SEQ";// + HttpContext.Current.Session["userId"]+ "_" 
            //删除之前数据
            query.Execute(string.Format(@"ALTER TABLE {0} ACTIVATE NOT LOGGED INITIALLY WITH EMPTY TABLE", tablename));
            string megsql = string.Format(@"MERGE INTO {0} AS A  
            USING {1} AS B  
            ON A.PHONE_NO=B.PHONE_NO AND A.ENTITY_ID=B.ENTITY_ID
            WHEN NOT MATCHED THEN INSERT VALUES 
            ({2}.NEXTVAL,
              B.ENTITY_ID,B.PHONE_NO,0,'',
                B.OP_TIME,B.USER_ID,B.YEARMONTH,'','')  
            ELSE IGNORE; ", tablename, temptbname, seqname);
            query.Execute(megsql);
            //ALTER TABLE {0} ACTIVATE NOT LOGGED INITIALLY WITH EMPTY TABLE
            query.Execute(string.Format(@"DROP TABLE {0}", temptbname));
            //string efrows = query.Query(string.Format(@"SELECT COUNT(1) FROM {0} WHERE ENTITY_ID={1}", tablename, id)).Tables[0].Rows[0][0].ToString();
            HttpContext.Current.Response.Write(megsql);//.ToLower()efrows
        }

        public void GetTempRows(HttpContext context)
        {
            QueryManager query = new QueryManager();
            string id = context.Request["et"].ToString();
            string temptbname = "TEMP_ENTITY_IMPROT_PHONE_" + HttpContext.Current.Session["userId"] + "_" + id;// ;
            string efrows = query.Query(string.Format(@"SELECT COUNT(1) FROM {0} ", temptbname)).Tables[0].Rows[0][0].ToString();
            HttpContext.Current.Response.Write(efrows);//.ToLower()
        }


        public void EditSingle(HttpContext context)
        {
            QueryManager query = new QueryManager();
            string idname = context.Request["id"].ToString();
            DataTable dt = query.Query(string.Format(@"select a.*,b.NAME CLNAME from KG_ENTITY_INFO a LEFT JOIN (SELECT ID,NAME FROM KG_ENTITY_CLASS)b ON a.CLASS_ID=b.ID where a.NAME='{0}'", idname)).Tables[0];
            HttpContext.Current.Response.Write(JsonConvert.SerializeObject(dt));//.ToLower()
        }

        public void ReadFile(HttpContext context)
        {
            HttpFileCollection files = HttpContext.Current.Request.Files;//这里只能用<input type="file" />才能有效果,因为服务器控件是HttpInputFile类型
            string msg = string.Empty;
            string filename = string.Empty;
            if (files.Count > 0)
            {
                try
                {
                    string[] fnames = files[0].FileName.Split('.');
                    if (fnames[1] != "txt")
                    {
                        HttpContext.Current.Response.Write("只能上传.txt类型的文本文件！");
                        return;
                    }
                    string newfname = fnames[0] + DateTime.Now.ToString("yyyyMMddHHmmss") + "." + fnames[1];
                    filename = HttpContext.Current.Server.MapPath("/Files/") + System.IO.Path.GetFileName(newfname); //Server.MapPath("/") + System.IO.Path.GetFileName(files[0].FileName);
                    files[0].SaveAs(filename);

                    QueryManager query = new QueryManager();
                    string content = File.ReadAllText(filename);
                    string id = context.Request["et"].ToString();
                    string[] contents = content.Replace("\r\n", "\r").Split('\r');
                    //写导入数据的方法
                    //HttpContext.Current.Session["userId"] = "123";
                    string yearmonth = query.Query(string.Format(@"SELECT YEARMONTH FROM KG_ENTITY_INFO WHERE ID={0}", id)).Tables[0].Rows[0][0].ToString();
                    string temptbname = "TEMP_ENTITY_IMPROT_PHONE_" + HttpContext.Current.Session["userId"] + "_" + id;// ;
                    if (Convert.ToInt32(query.Query(string.Format(@"select count(1) from syscat.tables where tabname='{0}'", temptbname)).Tables[0].Rows[0][0].ToString()) > 0)
                    {
                        query.Execute(string.Format(@"ALTER TABLE {0} ACTIVATE NOT LOGGED INITIALLY WITH EMPTY TABLE", temptbname));
                    }
                    else
                    {
                        query.Execute(string.Format(@"CREATE TABLE {0}(ENTITY_ID VARCHAR(20),PHONE_NO VARCHAR(20),IS_HANDLE INT,OP_TIME TIMESTAMP,USER_ID DECIMAL(10),YEARMONTH DECIMAL(10))", temptbname));
                    }
                    string sqls = "INSERT INTO " + temptbname + " VALUES ";
                    foreach (string s in contents)
                    {
                        sqls += "(" + "'" + id + "','" + s + "',0,CURRENT TIMESTAMP," + HttpContext.Current.Session["userId"] + "," + yearmonth + "),";
                    }
                    sqls = sqls.Substring(0, sqls.Length - 1) + ";";
                    query.Execute(sqls);
                    int efrows = contents.Length; //
                    msg = " 成功! 导入条数：" + efrows;//文件大小为:" + files[0].ContentLength;//+" Content:"+content;
                    //
                    //File.WriteAllText(filename, sqls, System.Text.Encoding.Default);
                    File.Delete(filename);
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

        public void Save(HttpContext context)
        {
            QueryManager query = new QueryManager();
            try
            {
                string id = context.Request.Form["id"];
                string txtName = context.Request.Form["txtName"];
                string txtCode = context.Request.Form["txtCode"];
                string txtType = context.Request.Form["txtType"];
                string txtStart = context.Request.Form["txtStart"];
                string txtEnd = context.Request.Form["txtEnd"];
                bool txtExport = bool.Parse(context.Request.Form["txtExport"]);
                string txtState = context.Request.Form["txtState"];
                string txtRemark = context.Request.Form["txtRemark"];
                string txtOrder = context.Request.Form["txtOrder"];
                string sql = "";
                string typeid = context.Request.Form["TypeId"];
                string newid = string.Empty;
                string yearmonth = DateTime.Parse(txtStart).ToString("yyyyMM");
                if (string.IsNullOrEmpty(id))
                {
                    //插入
                    //构建sql
                    try
                    {
                        newid = query.Query(string.Format(@"VALUES NEXTVAL FOR KG_ENTITY_INFO_SEQ")).Tables[0].Rows[0][0].ToString();
                        sql = string.Format(@"
                            INSERT INTO KG_ENTITY_INFO 
                             VALUES (
                              " + newid + @"  -- ID - DECIMAL(10) KG_ENTITY_INFO_SEQ.NEXTVAL 
                              ," + yearmonth + @"   -- YEARMONTH - DECIMAL(10)
                              ,'" + txtName + @"'  -- NAME - VARCHAR(100)
                              ,''  -- ENTITY_TYPE - VARCHAR(20)
                              ,current timestamp  -- OPERATE_TIME - TIMESTAMP
                              ,'" + txtStart.Replace("T", " ") + @"'  -- START_TIME - TIMESTAMP
                              ,'" + txtEnd.Replace("T", " ") + @"'  -- END_TIME - TIMESTAMP
                              ," + (string.IsNullOrEmpty(txtState) ? "0" : txtState) + @"   -- STATE - DECIMAL(1)
                              ," + (txtExport ? "1" : "0") + @"   -- IS_IMPORT - DECIMAL(1)
                              ,'" + txtRemark + @"'  -- CALIBRE_REMARK - VARCHAR(1000)
                              ,''  -- POLICY_REMARK - VARCHAR(1000)
                              ,'" + txtCode + @"'  -- CODE - VARCHAR(2000)
                              ," + typeid + @"   -- CLASS_ID - DECIMAL(10)
                              ," + txtOrder + @"   -- ORDER_KEY - DECIMAL(10)
                              ,''  -- STYPE - VARCHAR(50)
                            )");    //难得写，还难得找
                        query.Execute(sql);
                        //插入表名

                        string tablename = "KG_WARN_PHONE_INFO_" + newid;// + HttpContext.Current.Session["userId"] + "_"
                        string tablesql = string.Format(@"CREATE TABLE {0}  (
                                             ID	DECIMAL(11, 0),
                                             ENTITY_ID	DECIMAL(10, 0),
                                             PHONE_NO	VARCHAR(20),
                                             IS_HANDLE	SMALLINT,
                                             REMARK	VARCHAR(5000),
                                             OP_TIME	TIMESTAMP,
                                             USER_ID	DECIMAL(10, 0),
                                             YEARMONTH	DECIMAL(10, 0),
                                             HANDE_TYPE	VARCHAR(50),
                                             RESULT	VARCHAR(50) )", tablename);
                        query.Execute(tablesql);
                        tablesql = "";
                        tablename = "";
                        tablename = "KG_WARN_PHONE_INFO_" + newid + "_SEQ";//HttpContext.Current.Session["userId"] + "_" +
                        tablesql = string.Format(@"CREATE SEQUENCE {0}
                                             AS INTEGER
                                             CACHE 20
                                             MAXVALUE 99999999
                                             ORDER ;", tablename);
                        query.Execute(tablesql);
                        context.Response.Write(newid);//
                    }
                    catch (Exception ex)
                    { context.Response.Write(ex.Message); }
                    return;
                }
                //更新
                newid = query.Query(string.Format("select id from KG_ENTITY_INFO where NAME='{0}'", id)).Tables[0].Rows[0][0].ToString();
                sql = string.Format(@"
                       UPDATE KG_ENTITY_INFO SET
                         YEARMONTH = " + yearmonth + @" -- DECIMAL(10)
                         ,NAME = '" + txtName + @"' -- VARCHAR(100)
                         ,ENTITY_TYPE = '" + @"' -- VARCHAR(20)
                         ,OPERATE_TIME = current timestamp -- TIMESTAMP
                         ,START_TIME = '" + txtStart.Replace("T", " ") + @"' -- TIMESTAMP
                         ,END_TIME = '" + txtEnd.Replace("T", " ") + @"' -- TIMESTAMP
                         ,STATE = " + (string.IsNullOrEmpty(txtState) ? "0" : txtState) + @" -- DECIMAL(1)
                         ,IS_IMPORT = " + (txtExport ? "1" : "0") + @" -- DECIMAL(1)
                         ,CALIBRE_REMARK='" + txtRemark + @"'                         
                         ,CODE = '" + txtCode + @"' -- VARCHAR(2000)
                         ,CLASS_ID = " + typeid + @" -- DECIMAL(10)
                         ,ORDER_KEY = " + txtOrder + @" -- DECIMAL(10)
                         --,STYPE = '' -- VARCHAR(50)
                         WHERE ID=" + newid);
                query.Execute(sql);
                context.Response.Write(newid);//
            }
            catch (Exception ex)
            {//
                context.Response.Write("");
            }
        }

        public void EntityList(HttpContext context)
        {
            int rows = Convert.ToInt32(context.Request.Form["rows"]);
            int page = Convert.ToInt32(context.Request.Form["page"]);

            QueryManager query = new QueryManager();
            string cmbEntity = "";
            string cmbEntityID = "";
            try
            {
                cmbEntityID = context.Request.Form["cmbEntityID"].ToString();
                cmbEntity = context.Request.Form["cmbEntity"].ToString();
            }
            catch { HttpContext.Current.Response.Write("[]"); return; }
            string tablename = "KG_WARN_PHONE_INFO_" + cmbEntityID;//+ HttpContext.Current.Session["userId"] + "_"
            string sql = " select * from (select t.*,row_number() over(order by ID) as rowid from (";
            string msql = string.Format(@"SELECT A.ID ID,B.NAME ENAME,A.PHONE_NO PHONE_NO,
                            A.IS_HANDLE IS_HANDLE,A.YEARMONTH YEARMONTH
                            FROM {1} A
                            LEFT JOIN (SELECT * FROM KG_ENTITY_INFO) B
                            ON A.ENTITY_ID=B.ID
                            WHERE B.NAME='{0}'", cmbEntity, tablename);//KG_ENTITY_PHONE_INFO
            sql += msql + string.Format(" )t) tmp where tmp.rowid >={0} and tmp.rowid <= {1}", ((page <= 0 ? 1 : page - 1) * rows) + 1, page <= 0 ? 1 : page * rows);
            DataTable dt = query.Query(sql).Tables[0];
            string total = query.Query(string.Format(@"SELECT COUNT(1) FROM( {0} ) A", msql)).Tables[0].Rows[0][0].ToString();
            string json = "{\"total\":" + total + ",\"rows\":";
            json += JsonConvert.SerializeObject(dt);
            json += "}";
            HttpContext.Current.Response.Write(json);
        }

        public void CmbEntlist()
        {
            QueryManager query = new QueryManager();
            DataSet ds = query.Query(string.Format(@"select a.ID,a.NAME from KG_ENTITY_INFO a left join(
select * from KG_ENTITY_CLASS) b 
on a.CLASS_ID = b.ID where b.ID=1"));
            HttpContext.Current.Response.Write(JsonConvert.SerializeObject(ds.Tables[0]));//.ToLower()
        }

        public void CmbEntClasslist()
        {
            QueryManager query = new QueryManager();
            DataSet ds = query.Query(string.Format(@"SELECT ID,NAME FROM KG_ENTITY_CLASS"));
            HttpContext.Current.Response.Write(JsonConvert.SerializeObject(ds.Tables[0]));//.ToLower()
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }


    }

    public class QueryManager
    {
        public void Execute(string sql)
        {
            return;
        }

        internal DataSet Query(string p)
        {
            throw new NotImplementedException();
        }
    }
}