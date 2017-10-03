using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Text;
using System.Web;
using SucLib.Data.IDal;

namespace SucLib.Data.Dal
{
    public sealed class AccHelp //: IDBHelp
    {
        private string connStr = AccHelp.GetConnStr();
        private bool bDebue = AccHelp.IsDebug();
        private static bool IsDebug()
        {
            string text = ConfigurationSettings.AppSettings["ShowSql"];
            return text != "" && Convert.ToBoolean(text);
        }
        public static string GetConnStr()
        {
            string a = ConfigurationSettings.AppSettings["AccType"];
            string result;
            if (a == "2007")
            {
                result = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + HttpContext.Current.Request.PhysicalApplicationPath + ConfigurationSettings.AppSettings["Xts_AccConnString"];
            }
            else
            {
                result = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + HttpContext.Current.Request.PhysicalApplicationPath + ConfigurationSettings.AppSettings["Xts_AccConnString"];
            }
            return result;
        }
        /// <summary>
        /// 实现了String.Format方法的GetDataTable
        /// </summary>
        /// <param name="commandText">带参数SQL</param>
        /// <param name="args">参数值</param>
        /// <returns></returns>
        public DataTable GetDataTable(string commandText, params string[] args)
        {
            return GetDataTable(string.Format(commandText, args));
        }
        /// <summary>
        /// 通过sql获取datatable
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public DataTable GetDataTable(string commandText)
        {
            DataTable result;
            try
            {
                using (OleDbConnection oleDbConnection = new OleDbConnection(this.connStr))
                {
                    oleDbConnection.Open();
                    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(commandText, oleDbConnection);
                    DataTable dataTable = new DataTable();
                    oleDbDataAdapter.Fill(dataTable);
                    result = dataTable;
                }
            }
            catch (Exception ex)
            {
                string str = "";
                if (this.bDebue)
                {
                    str = commandText;
                }
                throw new Exception(str + " " + ex.Message);
            }
            return result;
        }

        public DataSet GetDataSet(string commandText)
        {
            DataSet result;
            try
            {
                using (OleDbConnection oledbConnection = new OleDbConnection(this.connStr))
                {
                    oledbConnection.Open();
                    OleDbDataAdapter oledbDataAdapter = new OleDbDataAdapter(commandText, oledbConnection);
                    DataSet dataset = new DataSet();
                    oledbDataAdapter.Fill(dataset);
                    result = dataset;
                }
            }
            catch (Exception ex)
            {
                string str = "";
                if (this.bDebue)
                {
                    str = commandText;
                }
                throw new Exception(str + " " + ex.Message);
            }
            return result;
        }
        /// <summary>
        /// 返回数据条数
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public object ExecuteScalar(string commandText)
        {
            object result;
            try
            {
                using (OleDbConnection oleDbConnection = new OleDbConnection(this.connStr))
                {
                    oleDbConnection.Open();
                    OleDbCommand oleDbCommand = new OleDbCommand(commandText, oleDbConnection);
                    result = oleDbCommand.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                string str = "";
                if (this.bDebue)
                {
                    str = commandText;
                }
                throw new Exception(str + " " + ex.Message);
            }
            return result;
        }
        /// <summary>
        /// 实现了String.Format方法的ExecuteNonQuery
        /// </summary>
        /// <param name="commandText">带参数SQL</param>
        /// <param name="args">参数值</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string commandText, params string[] args)
        {
            return ExecuteNonQuery(string.Format(commandText, args));
        }
        /// <summary>
        /// 执行一条数据库操作语句
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string commandText)
        {
            int result;
            try
            {
                using (OleDbConnection oleDbConnection = new OleDbConnection(this.connStr))
                {
                    oleDbConnection.Open();
                    OleDbCommand oleDbCommand = new OleDbCommand(commandText, oleDbConnection);
                    result = oleDbCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                string str = "";
                if (this.bDebue)
                {
                    str = commandText;
                }
                throw new Exception(str + " " + ex.Message);
            }
            return result;
        }
        /// <summary>
        /// 返回数据库是否有值
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public bool IsExists(string commandText)
        {
            bool result = false;
            try
            {
                using (OleDbConnection oleDbConnection = new OleDbConnection(this.connStr))
                {
                    oleDbConnection.Open();
                    OleDbCommand oleDbCommand = new OleDbCommand(commandText, oleDbConnection);
                    if (oleDbCommand.ExecuteReader().Read())
                    {
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                string str = "";
                if (this.bDebue)
                {
                    str = commandText;
                }
                throw new Exception(str + " " + ex.Message);
            }
            return result;
        }
        /// <summary>
        /// 返回记录统计条数
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public int GetRecordCount(string commandText)
        {
            return int.Parse(this.ExecuteScalar(commandText).ToString());
        }
        /// <summary>
        /// 返回单列数据(只查一列)
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public IList<string> GetList(string commandText)
        {
            DataTable dataTable = this.GetDataTable(commandText);
            IList<string> list = new List<string>();
            if (dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows[0].ItemArray.Length; i++)
                {
                    list.Add(dataTable.Rows[0].ItemArray.GetValue(i).ToString());
                }
            }
            return list;
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex">开始页</param>
        /// <param name="pageSize">当页数量</param>
        /// <param name="recordCount">总条数</param>
        /// <param name="fields">数据列(a,b,c)</param>
        /// <param name="tableName">表名</param>
        /// <param name="orderField">排序项</param>
        /// <param name="conditions">条件</param>
        /// <param name="isDesc">是否倒序</param>
        /// <returns></returns>
        public DataTable GetPageDataTable(int pageIndex, int pageSize, int recordCount, string fields, string tableName, string orderField, string conditions, bool isDesc)
        {
            DataTable result;
            try
            {
                using (OleDbConnection oleDbConnection = new OleDbConnection(this.connStr))
                {
                    oleDbConnection.Open();
                    string text;
                    string text2;
                    if (isDesc)
                    {
                        text = "desc";
                        text2 = "asc";
                    }
                    else
                    {
                        text = "asc";
                        text2 = "desc";
                    }
                    int num = (recordCount % pageSize == 0) ? pageSize : (recordCount % pageSize);
                    int num2 = (recordCount % pageSize == 0) ? (recordCount / pageSize) : (AccHelp.GetInt(recordCount / pageSize) + 1);
                    string selectCommandText;
                    if (pageIndex <= 1)
                    {
                        selectCommandText = string.Concat(new object[]
						{
							"select top ",
							pageSize,
							" ",
							fields,
							" from ",
							tableName,
							" where 1=1 ",
							conditions,
							" order by ",
							orderField,
							" ",
							text
						});
                    }
                    else
                    {
                        if (pageIndex >= num2)
                        {
                            selectCommandText = string.Concat(new object[]
							{
								"select * from (select top ",
								num,
								" ",
								fields,
								" from ",
								tableName,
								" where 1=1 ",
								conditions,
								" order by ",
								orderField,
								" ",
								text2,
								")table1 order by ",
								orderField,
								" ",
								text
							});
                        }
                        else
                        {
                            selectCommandText = string.Concat(new object[]
							{
								"select * from (select top ",
								pageSize,
								" * from (select top ",
								pageIndex * pageSize,
								" ",
								fields,
								" from ",
								tableName,
								" where 1=1 ",
								conditions,
								" order by ",
								orderField,
								" ",
								text,
								")table1 order by ",
								orderField,
								" ",
								text2,
								")table2 order by ",
								orderField,
								" ",
								text
							});
                        }
                    }
                    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(selectCommandText, oleDbConnection);
                    DataTable dataTable = new DataTable();
                    oleDbDataAdapter.Fill(dataTable);
                    result = dataTable;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex">开始页</param>
        /// <param name="pageSize">数据条数</param>
        /// <param name="fields">查询项(a,b,c)</param>
        /// <param name="tableName">表名</param>
        /// <param name="conditions">条件</param>
        /// <param name="orderField"></param>
        /// <param name="isDesc"></param>
        /// <returns></returns>
        public DataTable GetMPageDataTable(int pageIndex, int pageSize, string fields, string tableName, string conditions, string orderField, bool isDesc)
        {
            DataTable result;
            try
            {
                int num = 0;
                object obj = this.ExecuteScalar(string.Concat(new string[]
				{
					"select count(1) from ",
					tableName,
					" where 1 > 0 ",
					conditions,
					""
				}));
                if (obj != null)
                {
                    num = int.Parse(obj.ToString());
                }
                int num2 = (num % pageSize == 0) ? pageSize : (num % pageSize);
                int num3 = (num % pageSize == 0) ? (num / pageSize) : (AccHelp.GetInt(num / pageSize) + 1);
                string commandText;
                if (isDesc)
                {
                    if (pageIndex <= 1)
                    {
                        commandText = string.Concat(new object[]
						{
							"select top ",
							pageSize,
							" ",
							fields,
							" from ",
							tableName,
							" where 1 > 0 ",
							conditions,
							" order by ",
							orderField,
							" desc"
						});
                    }
                    else
                    {
                        if (pageIndex >= num3)
                        {
                            commandText = string.Concat(new object[]
							{
								"select * from (select top ",
								num2,
								" ",
								fields,
								" from ",
								tableName,
								" where 1 > 0 ",
								conditions,
								" order by ",
								orderField,
								" asc) T order by ",
								orderField,
								" desc"
							});
                        }
                        else
                        {
                            commandText = string.Concat(new object[]
							{
								"Select Top ",
								pageSize,
								" ",
								fields,
								" From ",
								tableName,
								" Where ",
								orderField,
								"<(Select Min(",
								orderField,
								") From (Select Top ",
								(pageIndex - 1) * pageSize,
								" ",
								orderField,
								" From ",
								tableName,
								" where 1=1 ",
								conditions,
								" Order By ",
								orderField,
								" desc) T) ",
								conditions,
								" Order By ",
								orderField,
								" desc "
							});
                        }
                    }
                }
                else
                {
                    if (pageIndex <= 1)
                    {
                        commandText = string.Concat(new object[]
						{
							"select top ",
							pageSize,
							" ",
							fields,
							" from ",
							tableName,
							" where 1 > 0 ",
							conditions,
							" order by ",
							orderField,
							" asc"
						});
                    }
                    else
                    {
                        if (pageIndex >= num3)
                        {
                            commandText = string.Concat(new object[]
							{
								"select *  from (select top ",
								num2,
								" ",
								fields,
								" from ",
								tableName,
								" where 1 > 0 ",
								conditions,
								" order by ",
								orderField,
								" desc) T order by ",
								orderField,
								" asc"
							});
                        }
                        else
                        {
                            commandText = string.Concat(new object[]
							{
								"Select Top ",
								pageSize,
								" ",
								fields,
								" From ",
								tableName,
								" Where ",
								orderField,
								">=(Select Max(",
								orderField,
								") From (Select Top ",
								(pageIndex - 1) * pageSize,
								" ",
								orderField,
								" From ",
								tableName,
								" where 1=1 ",
								conditions,
								" Order By ",
								orderField,
								" Asc) T) ",
								conditions,
								" Order By ",
								orderField,
								" Asc"
							});
                        }
                    }
                }
                DataTable dataTable = this.GetDataTable(commandText);
                result = dataTable;
            }
            catch (Exception)
            {
                result = null;
            }
            return result;
        }
        /// <summary>
        /// 获取一个数据表
        /// </summary>
        /// <param name="commandText">sql</param>
        /// <param name="showMehtodSql">是否页面执行</param>
        /// <returns></returns>
        public DataTable GetDataTable(string commandText, bool showMehtodSql)
        {
            DataTable result;
            try
            {
                using (OleDbConnection oleDbConnection = new OleDbConnection(this.connStr))
                {
                    oleDbConnection.Open();
                    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(commandText, oleDbConnection);
                    DataTable dataTable = new DataTable();
                    oleDbDataAdapter.Fill(dataTable);
                    if (showMehtodSql)
                    {
                        HttpContext.Current.Response.Write(commandText);
                    }
                    result = dataTable;
                }
            }
            catch (Exception ex)
            {
                string str = "";
                if (this.bDebue)
                {
                    str = commandText;
                }
                throw new Exception(str + " " + ex.Message);
            }
            return result;
        }
        public object ExecuteScalar(string commandText, bool showMehtodSql)
        {
            object result;
            try
            {
                using (OleDbConnection oleDbConnection = new OleDbConnection(this.connStr))
                {
                    oleDbConnection.Open();
                    OleDbCommand oleDbCommand = new OleDbCommand(commandText, oleDbConnection);
                    if (showMehtodSql)
                    {
                        HttpContext.Current.Response.Write(commandText);
                    }
                    result = oleDbCommand.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                string str = "";
                if (this.bDebue)
                {
                    str = commandText;
                }
                throw new Exception(str + " " + ex.Message);
            }
            return result;
        }
        public int ExecuteNonQuery(string commandText, bool showMehtodSql)
        {
            int result;
            try
            {
                using (OleDbConnection oleDbConnection = new OleDbConnection(this.connStr))
                {
                    oleDbConnection.Open();
                    OleDbCommand oleDbCommand = new OleDbCommand(commandText, oleDbConnection);
                    if (showMehtodSql)
                    {
                        HttpContext.Current.Response.Write(commandText);
                    }
                    result = oleDbCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                string str = "";
                if (this.bDebue)
                {
                    str = commandText;
                }
                throw new Exception(str + " " + ex.Message);
            }
            return result;
        }
        public bool IsExists(string commandText, bool showMehtodSql)
        {
            bool result = false;
            try
            {
                using (OleDbConnection oleDbConnection = new OleDbConnection(this.connStr))
                {
                    oleDbConnection.Open();
                    OleDbCommand oleDbCommand = new OleDbCommand(commandText, oleDbConnection);
                    if (oleDbCommand.ExecuteReader().Read())
                    {
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                string str = "";
                if (this.bDebue)
                {
                    str = commandText;
                }
                throw new Exception(str + " " + ex.Message);
            }
            if (showMehtodSql)
            {
                HttpContext.Current.Response.Write(commandText);
            }
            return result;
        }
        public int GetRecordCount(string commandText, bool showMehtodSql)
        {
            if (showMehtodSql)
            {
                HttpContext.Current.Response.Write(commandText);
            }
            return int.Parse(this.ExecuteScalar(commandText).ToString());
        }
        public IList<string> GetList(string commandText, bool showMehtodSql)
        {
            DataTable dataTable = this.GetDataTable(commandText);
            IList<string> list = new List<string>();
            if (dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows[0].ItemArray.Length; i++)
                {
                    list.Add(dataTable.Rows[0].ItemArray.GetValue(i).ToString());
                }
            }
            if (showMehtodSql)
            {
                HttpContext.Current.Response.Write(commandText);
            }
            return list;
        }
        public DataTable GetPageDataTable(int pageIndex, int pageSize, int recordCount, string fields, string tableName, string orderField, string conditions, bool isDesc, bool showMehtodSql)
        {
            DataTable result;
            try
            {
                using (OleDbConnection oleDbConnection = new OleDbConnection(this.connStr))
                {
                    oleDbConnection.Open();
                    string text;
                    string text2;
                    if (isDesc)
                    {
                        text = "desc";
                        text2 = "asc";
                    }
                    else
                    {
                        text = "asc";
                        text2 = "desc";
                    }
                    int num = (recordCount % pageSize == 0) ? pageSize : (recordCount % pageSize);
                    int num2 = (recordCount % pageSize == 0) ? (recordCount / pageSize) : (AccHelp.GetInt(recordCount / pageSize) + 1);
                    string text3;
                    if (pageIndex <= 1)
                    {
                        text3 = string.Concat(new object[]
						{
							"select top ",
							pageSize,
							" ",
							fields,
							" from ",
							tableName,
							" where ",
							orderField,
							">0 ",
							conditions,
							" order by ",
							orderField,
							" ",
							text
						});
                    }
                    else
                    {
                        if (pageIndex >= num2)
                        {
                            text3 = string.Concat(new object[]
							{
								"select * from (select top ",
								num,
								" ",
								fields,
								" from ",
								tableName,
								" where ",
								orderField,
								">0 ",
								conditions,
								" order by ",
								orderField,
								" ",
								text2,
								")table1 order by ",
								orderField,
								" ",
								text
							});
                        }
                        else
                        {
                            text3 = string.Concat(new object[]
							{
								"select * from (select top ",
								pageSize,
								" * from (select top ",
								pageIndex * pageSize,
								" ",
								fields,
								" from ",
								tableName,
								" where ",
								orderField,
								">0 ",
								conditions,
								" order by ",
								orderField,
								" ",
								text,
								")table1 order by ",
								orderField,
								" ",
								text2,
								")table2 order by ",
								orderField,
								" ",
								text
							});
                        }
                    }
                    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(text3, oleDbConnection);
                    DataTable dataTable = new DataTable();
                    oleDbDataAdapter.Fill(dataTable);
                    if (showMehtodSql)
                    {
                        HttpContext.Current.Response.Write(text3);
                    }
                    result = dataTable;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
        public DataTable GetMPageDataTable(int pageIndex, int pageSize, string fields, string tableName, string conditions, string orderField, bool isDesc, bool showMehtodSql)
        {
            DataTable result;
            try
            {
                int num = 0;
                object obj = this.ExecuteScalar(string.Concat(new string[]
				{
					"select count(1) from ",
					tableName,
					" where 1 > 0 ",
					conditions,
					""
				}));
                if (obj != null)
                {
                    num = int.Parse(obj.ToString());
                }
                int num2 = (num % pageSize == 0) ? pageSize : (num % pageSize);
                int num3 = (num % pageSize == 0) ? (num / pageSize) : (AccHelp.GetInt(num / pageSize) + 1);
                string text;
                if (isDesc)
                {
                    if (pageIndex <= 1)
                    {
                        text = string.Concat(new object[]
						{
							"select top ",
							pageSize,
							" ",
							fields,
							" from ",
							tableName,
							" where 1 > 0 ",
							conditions,
							" order by ",
							orderField,
							" desc"
						});
                    }
                    else
                    {
                        if (pageIndex >= num3)
                        {
                            text = string.Concat(new object[]
							{
								"select * from (select top ",
								num2,
								" ",
								fields,
								" from ",
								tableName,
								" where 1 > 0 ",
								conditions,
								" order by ",
								orderField,
								" asc) T order by ",
								orderField,
								" desc"
							});
                        }
                        else
                        {
                            text = string.Concat(new object[]
							{
								"Select Top ",
								pageSize,
								" ",
								fields,
								" From ",
								tableName,
								" Where ",
								orderField,
								"<(Select Min(",
								orderField,
								") From (Select Top ",
								(pageIndex - 1) * pageSize,
								" ",
								orderField,
								" From ",
								tableName,
								" where 1=1 ",
								conditions,
								" Order By ",
								orderField,
								" desc) T) ",
								conditions,
								" Order By ",
								orderField,
								" desc "
							});
                        }
                    }
                }
                else
                {
                    if (pageIndex <= 1)
                    {
                        text = string.Concat(new object[]
						{
							"select top ",
							pageSize,
							" ",
							fields,
							" from ",
							tableName,
							" where 1 > 0 ",
							conditions,
							" order by ",
							orderField,
							" asc"
						});
                    }
                    else
                    {
                        if (pageIndex >= num3)
                        {
                            text = string.Concat(new object[]
							{
								"select *  from (select top ",
								num2,
								" ",
								fields,
								" from ",
								tableName,
								" where 1 > 0 ",
								conditions,
								" order by ",
								orderField,
								" desc) T order by ",
								orderField,
								" asc"
							});
                        }
                        else
                        {
                            text = string.Concat(new object[]
							{
								"Select Top ",
								pageSize,
								" ",
								fields,
								" From ",
								tableName,
								" Where ",
								orderField,
								">=(Select Max(",
								orderField,
								") From (Select Top ",
								(pageIndex - 1) * pageSize,
								" ",
								orderField,
								" From ",
								tableName,
								" where 1=1 ",
								conditions,
								" Order By ",
								orderField,
								" Asc) T) ",
								conditions,
								" Order By ",
								orderField,
								" Asc"
							});
                        }
                    }
                }
                DataTable dataTable = this.GetDataTable(text);
                if (showMehtodSql)
                {
                    HttpContext.Current.Response.Write(text);
                }
                result = dataTable;
            }
            catch (Exception)
            {
                result = null;
            }
            return result;
        }
        public static int GetInt(int num)
        {
            string[] array = num.ToString().Split(new char[]
			{
				'.'
			});
            return int.Parse(array[0]);
        }
    }
}
