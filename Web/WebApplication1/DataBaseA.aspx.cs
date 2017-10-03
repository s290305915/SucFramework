using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SucLib.Model;
using SucLib.Data.Factory;
using SucLib.Data.IDal;
using System.Data;
using SucLib.Core;
using SucLib.Model;

namespace WebApplication1
{
    public partial class DataBaseA : System.Web.UI.Page
    {
        IDBHelp db = DBFactory.Create();
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = db.GetDataTable("SELECT ID,NAME FROM SUC_USER");
            tid.Value = dt.Rows[0][0].ToString();
            List<SUC_USER> U = new SucLib.Model.SUC_USER().FindAll();
            IEnumerable<SUC_USER> eu = U.AsEnumerable();
            IEnumerable<string> su = eu.Select((d,i) => "A" + i.ToString());
            //U.AsEnumerable()).Select((d, i) => "A" + i.ToString()
            var t = string.Join(",", su);
            var a = dt.Rows.Cast<DataRow>().Skip(0).Take(2).CopyToDataTable();
            //var t = SplitDT(dt, 2);
            rp_list.DataSource = dt;
            rp_list.DataBind();
        }

        private DataTable[] SplitDT(DataTable dt, int p)
        {
            var totalRows = dt.Rows.Count; //总行数

            if (totalRows == 0) //一行没有就直接返回空的
            {
                return new[]
                       {
                           dt.Clone()
                       };
            }

            var totalTables = (totalRows - 1) / p + 1; //要返回的表的个数
            var result = new DataTable[totalTables]; //要返回的结果

            for (var i = 0; i < totalTables; i++)
            {
                var thisDT = result[i] = dt.Clone();
                thisDT.BeginLoadData();
                var end = Math.Max(i + i * p, totalRows);
                for (var j = i * p; j < end; j++)
                {
                    thisDT.Rows.Add(dt.Rows[j].ItemArray);
                }
                thisDT.EndLoadData();
            }

            return result;
        }
    }
}