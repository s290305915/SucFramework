using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using SucLib.Common;
using SucLib.Data.IDal;

namespace SucLib.Data.Dal
{
    public sealed class SQLHelp : IDBHelp
    {
        private string connStr = "";
        public SQLHelp()
        {
            connStr = ConfigUtil.ConfigHelper.GetConfigString("SqlConnString");
        }
        public SQLHelp(string conn)
        {
            connStr = conn;
        }
        private bool bDebue = SQLHelp.IsDebug();
        private static bool IsDebug()
        {
            //string text = ConfigurationSettings.AppSettings["ShowSql"];
            try
            {
                string text = ConfigUtil.ConfigHelper.GetConfigString("ShowSql");
                return text != "" && Convert.ToBoolean(text);
            }
            catch { return false; } //未配置
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
        public DataTable GetDataTable(string commandText)
        {
            DataTable result;
            try
            {
                using(SqlConnection sqlConnection = new SqlConnection(this.connStr))
                {
                    sqlConnection.Open();
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(commandText, sqlConnection);
                    DataTable dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);
                    result = dataTable;
                }
            }
            catch(Exception ex)
            {
                string str = "";
                if(this.bDebue)
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
                using(SqlConnection sqlConnection = new SqlConnection(this.connStr))
                {
                    sqlConnection.Open();
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(commandText, sqlConnection);
                    DataSet dataset = new DataSet();
                    sqlDataAdapter.Fill(dataset);
                    result = dataset;
                }
            }
            catch(Exception ex)
            {
                string str = "";
                if(this.bDebue)
                {
                    str = commandText;
                }
                throw new Exception(str + " " + ex.Message);
            }
            return result;
        }
        public object ExecuteScalar(string commandText)
        {
            object result;
            try
            {
                using(SqlConnection sqlConnection = new SqlConnection(this.connStr))
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand(commandText, sqlConnection);
                    result = sqlCommand.ExecuteScalar();
                }
            }
            catch(Exception ex)
            {
                string str = "";
                if(this.bDebue)
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
        public int ExecuteNonQuery(string commandText)
        {
            int result;
            try
            {
                using(SqlConnection sqlConnection = new SqlConnection(this.connStr))
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand(commandText, sqlConnection);
                    result = sqlCommand.ExecuteNonQuery();
                }
            }
            catch(Exception ex)
            {
                string str = "";
                if(this.bDebue)
                {
                    str = commandText;
                }
                throw new Exception(str + " " + ex.Message);
            }
            return result;
        }
        public bool IsExists(string commandText)
        {
            bool result = false;
            try
            {
                using(SqlConnection sqlConnection = new SqlConnection(this.connStr))
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand(commandText, sqlConnection);
                    if(sqlCommand.ExecuteReader().Read())
                    {
                        result = true;
                    }
                }
            }
            catch(Exception ex)
            {
                string str = "";
                if(this.bDebue)
                {
                    str = commandText;
                }
                throw new Exception(str + " " + ex.Message);
            }
            return result;
        }
        public int GetRecordCount(string commandText)
        {
            return int.Parse(this.ExecuteScalar(commandText).ToString());
        }
        public IList<string> GetSingleList(string commandText)
        {
            DataTable dataTable = this.GetDataTable(commandText);
            IList<string> list = new List<string>();
            if(dataTable.Rows.Count > 0)
            {
                for(int i = 0;i < dataTable.Rows[0].ItemArray.Length;i++)
                {
                    list.Add(dataTable.Rows[0].ItemArray.GetValue(i).ToString());
                }
            }
            return list;
        }
        public IList<string> GetList(string commandText)
        {
            DataTable dataTable = this.GetDataTable(commandText);
            IList<string> list = new List<string>();
            if(dataTable.Rows.Count > 0)
            {
                for(int i = 0;i < dataTable.Rows.Count;i++)
                {
                    list.Add(dataTable.Rows[i][0].ToString());
                }
            }
            return list;
        }
        public DataTable GetPageDataTable(int pageIndex, int pageSize, int recordCount, string fields, string tableName, string orderField, string conditions, bool isDesc)
        {
            DataTable result;
            try
            {
                using(SqlConnection sqlConnection = new SqlConnection(this.connStr))
                {
                    sqlConnection.Open();
                    string text;
                    string text2;
                    if(isDesc)
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
                    int num2 = (recordCount % pageSize == 0) ? (recordCount / pageSize) : (SQLHelp.GetInt(recordCount / pageSize) + 1);
                    string selectCommandText;
                    if(pageIndex <= 1)
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
                        if(pageIndex >= num2)
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
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommandText, sqlConnection);
                    DataTable dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);
                    result = dataTable;
                }
            }
            catch(Exception ex)
            {
                throw new Exception(" " + ex.Message);
            }
            return result;
        }
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
                if(obj != null)
                {
                    num = int.Parse(obj.ToString());
                }
                int num2 = (num % pageSize == 0) ? pageSize : (num % pageSize);
                int num3 = (num % pageSize == 0) ? (num / pageSize) : (SQLHelp.GetInt(num / pageSize) + 1);
                string commandText;
                if(isDesc)
                {
                    if(pageIndex <= 1)
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
                        if(pageIndex >= num3)
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
                    if(pageIndex <= 1)
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
                        if(pageIndex >= num3)
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
            catch(Exception)
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
        public DataTable GetDataTable(string commandText, bool showMehtodSql)
        {
            DataTable result;
            try
            {
                using(SqlConnection sqlConnection = new SqlConnection(this.connStr))
                {
                    sqlConnection.Open();
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(commandText, sqlConnection);
                    DataTable dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);
                    if(showMehtodSql)
                    {
                        HttpContext.Current.Response.Write(commandText);
                    }
                    result = dataTable;
                }
            }
            catch(Exception ex)
            {
                string str = "";
                if(this.bDebue)
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
                using(SqlConnection sqlConnection = new SqlConnection(this.connStr))
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand(commandText, sqlConnection);
                    if(showMehtodSql)
                    {
                        HttpContext.Current.Response.Write(commandText);
                    }
                    result = sqlCommand.ExecuteScalar();
                }
            }
            catch(Exception ex)
            {
                string str = "";
                if(this.bDebue)
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
                using(SqlConnection sqlConnection = new SqlConnection(this.connStr))
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand(commandText, sqlConnection);
                    if(showMehtodSql)
                    {
                        HttpContext.Current.Response.Write(commandText);
                    }
                    result = sqlCommand.ExecuteNonQuery();
                }
            }
            catch(Exception ex)
            {
                string str = "";
                if(this.bDebue)
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
                using(SqlConnection sqlConnection = new SqlConnection(this.connStr))
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand(commandText, sqlConnection);
                    if(sqlCommand.ExecuteReader().Read())
                    {
                        result = true;
                    }
                }
            }
            catch(Exception ex)
            {
                string str = "";
                if(this.bDebue)
                {
                    str = commandText;
                }
                throw new Exception(str + " " + ex.Message);
            }
            if(showMehtodSql)
            {
                HttpContext.Current.Response.Write(commandText);
            }
            return result;
        }
        public int GetRecordCount(string commandText, bool showMehtodSql)
        {
            if(showMehtodSql)
            {
                HttpContext.Current.Response.Write(commandText);
            }
            return int.Parse(this.ExecuteScalar(commandText).ToString());
        }
        public IList<string> GetList(string commandText, bool showMehtodSql)
        {
            DataTable dataTable = this.GetDataTable(commandText);
            IList<string> list = new List<string>();
            if(dataTable.Rows.Count > 0)
            {
                for(int i = 0;i < dataTable.Rows[0].ItemArray.Length;i++)
                {
                    list.Add(dataTable.Rows[0].ItemArray.GetValue(i).ToString());
                }
            }
            if(showMehtodSql)
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
                using(SqlConnection sqlConnection = new SqlConnection(this.connStr))
                {
                    sqlConnection.Open();
                    string text;
                    string text2;
                    if(isDesc)
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
                    int num2 = (recordCount % pageSize == 0) ? (recordCount / pageSize) : (SQLHelp.GetInt(recordCount / pageSize) + 1);
                    string text3;
                    if(pageIndex <= 1)
                    {
                        text3 = string.Concat(new object[]
                        {
                            "select top ",
                            pageSize,
                            " ",
                            fields,
                            " from ",
                            tableName,
                            " order by ",
                            orderField,
                            " ",
                            text
                        });
                    }
                    else
                    {
                        if(pageIndex >= num2)
                        {
                            text3 = string.Concat(new object[]
                            {
                                "select * from (select top ",
                                num,
                                " ",
                                fields,
                                " from ",
                                tableName,
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
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(text3, sqlConnection);
                    DataTable dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);
                    if(showMehtodSql)
                    {
                        HttpContext.Current.Response.Write(text3);
                    }
                    result = dataTable;
                }
            }
            catch(Exception ex)
            {
                throw new Exception(" " + ex.Message);
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
                if(obj != null)
                {
                    num = int.Parse(obj.ToString());
                }
                int num2 = (num % pageSize == 0) ? pageSize : (num % pageSize);
                int num3 = (num % pageSize == 0) ? (num / pageSize) : (SQLHelp.GetInt(num / pageSize) + 1);
                string text;
                if(isDesc)
                {
                    if(pageIndex <= 1)
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
                        if(pageIndex >= num3)
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
                    if(pageIndex <= 1)
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
                        if(pageIndex >= num3)
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
                if(showMehtodSql)
                {
                    HttpContext.Current.Response.Write(text);
                }
                DataTable dataTable = this.GetDataTable(text);
                result = dataTable;
            }
            catch(Exception)
            {
                result = null;
            }
            return result;
        }
    }
}
