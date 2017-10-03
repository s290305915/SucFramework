using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using SucLib.Data.IDal;
using SucLib.Data.Factory;
using SucLib.Model;
using System.Collections;
using Newtonsoft.Json;

namespace WebApplication1.Handler
{
    /// <summary>
    /// EntityHandler 的摘要说明
    /// </summary>
    public class EntityHandler : IHttpHandler
    {
        IDBHelp db = DBFactory.Create();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
            switch(HttpContext.Current.Request.QueryString["opt"].ToString())
            {
                case "ReadFile":
                    ReadFile(context);
                    break;
                case "list":
                    List(context);
                    break;
            }
        }

        private void List(HttpContext context)
        {
            //int page = request.Form["page"]=="0"?1:Convert.ToInt32(request.Form["page"]);
            int page = Convert.ToInt32(context.Request.Form["page"]);
            int rows = Convert.ToInt32(context.Request.Form["rows"]);
            string strsql = string.Format(@"SELECT * FROM SUC_USER");
            IDBHelp sc1 = DBFactory.Create(DataBaseType.SqlServer, ".", "SUCMSF1", "sa", "suchi12345");
            List<SUC_USER> us = new SUC_USER().FindAll();
            //List<SUC_ROLE> ur = new SUC_ROLE().FindAll();
            DataTable ht = db.GetDataTable(strsql);
            string ret = JsonConvert.SerializeObject(us).ToLower();
            HttpContext.Current.Response.Write(ret);
        }

        public void ReadFile(HttpContext context)
        {
            HttpFileCollection files = HttpContext.Current.Request.Files;//这里只能用<input type="file" />才能有效果,因为服务器控件是HttpInputFile类型
            string msg = string.Empty;
            string filename = string.Empty;
            if(files.Count > 0)
            {
                for(int ii = 0;ii < files.Count;ii++)
                {
                    try
                    {
                        string[] fnames = files[ii].FileName.Split('.');
                        fnames[0] = fnames[0].Replace("/", "").Replace("\\", "");
                        string newfname = fnames[0] + DateTime.Now.ToString("yyyyMMddHHmmss") + "." + fnames[1];
                        filename = HttpContext.Current.Server.MapPath("/Files/") + System.IO.Path.GetFileName(newfname); //Server.MapPath("/") + System.IO.Path.GetFileName(files[ii].FileName);
                        files[ii].SaveAs(filename);
                        continue;
                        QueryManager query = new QueryManager();
                        string content = File.ReadAllText(filename);
                        string[] contents = content.Replace("\r\n", "\r").Split('\r');
                        string sql = "";
                        string id = context.Request["et"].ToString();
                        foreach(string s in contents)
                        {
                            if(s.Length > 10)
                            {
                                string[] ss = s.Split(',');
                                string v = "";
                                sql += "(";
                                //v = string.Join(",", ss.ToList().Select(x => "'" + x + "'"));
                                for(int i = 0;i < ss.Length;i++)
                                {
                                    ss[i] = "'" + ss[i] + "'";
                                }
                                v = string.Join(",", ss);
                                sql += v;
                                sql = sql + "),";
                            }
                        }
                        string asql = "insert into xx values" + sql;
                        asql = asql.Substring(0, asql.Length - 1);
                        File.Delete(filename);
                        HttpContext.Current.Response.Write(asql);
                        return;

                        //写导入数据的方法
                        //HttpContext.Current.Session["userId"] = "123";
                        string yearmonth = query.Query(string.Format(@"SELECT YEARMONTH FROM KG_ENTITY_INFO WHERE ID={0}", id)).Tables[0].Rows[0][0].ToString();
                        string temptbname = "TEMP_ENTITY_IMPROT_PHONE_" + HttpContext.Current.Session["userId"] + "_" + id;// ;
                        if(Convert.ToInt32(query.Query(string.Format(@"select count(1) from syscat.tables where tabname='{0}'", temptbname)).Tables[0].Rows[0][0].ToString()) > 0)
                        {
                            query.Execute(string.Format(@"ALTER TABLE {0} ACTIVATE NOT LOGGED INITIALLY WITH EMPTY TABLE", temptbname));
                        }
                        else
                        {
                            query.Execute(string.Format(@"CREATE TABLE {0}(ENTITY_ID VARCHAR(20),PHONE_NO VARCHAR(20),IS_HANDLE INT,OP_TIME TIMESTAMP,USER_ID DECIMAL(10),YEARMONTH DECIMAL(10))", temptbname));
                        }
                        string sqls = "INSERT INTO " + temptbname + " VALUES ";
                        foreach(string s in contents)
                        {
                            sqls += "(" + "'" + id + "','" + s + "',0,CURRENT TIMESTAMP," + HttpContext.Current.Session["userId"] + "," + yearmonth + "),";
                        }
                        sqls = sqls.Substring(0, sqls.Length - 1) + ";";
                        query.Execute(sqls);
                        int efrows = contents.Length;
                        msg = " 成功! 导入条数：" + efrows;//文件大小为:" + files[0].ContentLength;//+" Content:"+content;
                                                    //
                                                    //File.WriteAllText(filename, sqls, System.Text.Encoding.Default);
                        File.Delete(filename);
                        HttpContext.Current.Response.Write(msg);
                    }
                    catch(Exception ex)
                    {
                        msg = ex.Message;
                        HttpContext.Current.Response.Write(msg);
                        return;
                    }
                }
            }
            else
            {
                msg = "错误，未选择文件！";
                HttpContext.Current.Response.Write(msg);
            }
            //HttpContext.Current.Response.Write("success");
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