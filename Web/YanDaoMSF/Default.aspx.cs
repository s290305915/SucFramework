using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SucLib.Data.Factory;
using SucLib.Data.IDal;
using SucLib.Common;
using SucLib.Model;
using System.Text.RegularExpressions;


namespace YanDaoMSF
{
    public partial class _Default : System.Web.UI.Page
    {
        IDBHelp db = DBFactory.Create(); //实例化工厂
        protected void Page_Load(object sender, EventArgs e)
        {
            //List<SUC_USER> usr = new SUC_USER().FindAll();
            //Response.Write("<script language='javascript'>alert('用户：" + usr[0].NAME + " 你好，你是：" + usr[].ROLE.NAME + "');</script>");
            //return;

            //DataTable tb = new SUC_USER().FindAllTable().Tables[0];
            //string jstb = JsonHelper.DataTableToJSON(tb);
            //tb = new DataTable();
            //tb = JsonHelper.JsonToDataTable(jstb);
            //
            //return;

            JsUtil.LocationNewHref("/FP/FilePage.aspx");
            return;
            //DataTable dt = db.GetDataTable("select * from suc_files");
            //string jsdt = JsonHelper.DataTableToJSON(dt);
            //Response.Write(jsdt);
            //SucCookie.Add("dt", jsdt, 30);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string cok = SucCookie.Read("dt");
            Response.Write("<script language='javascript'>alert('" + cok + "')</script>");
        }

        protected void tx_password_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 将json转换为DataTable
        /// </summary>
        /// <param name="strJson">得到的json</param>
        /// <returns></returns>
        private DataTable JsonToDataTable(string strJson)
        {
            //转换json格式
            strJson = strJson.Replace(",\"", "*\"").Replace("\":", "\"#").ToString();
            //取出表名   
            var rg = new Regex(@"(?<={)[^:]+(?=:\[)", RegexOptions.IgnoreCase);
            string strName = rg.Match(strJson).Value;
            DataTable tb = null;
            //去除表名   
            strJson = strJson.Substring(strJson.IndexOf("[") + 1);
            strJson = strJson.Substring(0, strJson.IndexOf("]"));

            //获取数据   
            rg = new Regex(@"(?<={)[^}]+(?=})");
            MatchCollection mc = rg.Matches(strJson);
            for (int i = 0; i < mc.Count; i++)
            {
                string strRow = mc[i].Value;
                string[] strRows = strRow.Split('*');

                //创建表   
                if (tb == null)
                {
                    tb = new DataTable();
                    tb.TableName = strName;
                    foreach (string str in strRows)
                    {
                        var dc = new DataColumn();
                        string[] strCell = str.Split('#');

                        if (strCell[0].Substring(0, 1) == "\"")
                        {
                            int a = strCell[0].Length;
                            dc.ColumnName = strCell[0].Substring(1, a - 2);
                        }
                        else
                        {
                            dc.ColumnName = strCell[0];
                        }
                        tb.Columns.Add(dc);
                    }
                    tb.AcceptChanges();
                }

                //增加内容   
                DataRow dr = tb.NewRow();
                for (int r = 0; r < strRows.Length; r++)
                {
                    dr[r] = strRows[r].Split('#')[1].Trim().Replace("，", ",").Replace("：", ":").Replace("\"", "");
                }
                tb.Rows.Add(dr);
                tb.AcceptChanges();
            }

            return tb;
        }
    }
}