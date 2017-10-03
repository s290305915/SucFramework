using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
//using Oracle.DataAccess.Client; //此为32位程序集

namespace OracleFromBase
{
    public interface IDBQuery
    {
        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sql">update/delete语句</param>
        /// <returns></returns>
        void Execute(string sql);
        /// <summary>
        /// 执行查询语句
        /// </summary>
        /// <param name="sql">select</param>
        /// <returns></returns>
        DataSet Query(string sql);

        Hashtable Pagenation(string sql, int rows, int page, string sort, string order);

        object ExecuteScalar(string sql);
    }
    public class QueryManager
    {
        IDBQuery _query = null;

        public QueryManager()
        {
            _query = new ORCLQuery();
        }

        public void Execute(string sql)
        {
            _query.Execute(sql);
        }

        public DataSet Query(string sql)
        {
            return _query.Query(sql);
        }

        public Hashtable Pagenation(string sql, int rows, int page, string sort, string order)
        {
            return _query.Pagenation(sql, rows, page, sort, order);
        }

        public object ExecuteScalar(string sql)
        {
            return _query.ExecuteScalar(sql);
        }
    }
    class ORCLQuery : IDBQuery
    {
        public void Execute(string sql)
        {
            try
            {
                using(OracleConnection connection = new OracleConnection(ConfigurationManager.AppSettings["connection.connection_string"].ToString()))
                {
                    connection.Open();
                    OracleCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    command.ExecuteNonQuery();
                    connection.Close();
                    connection.Dispose();
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public object ExecuteScalar(string sql)
        {
            try
            {
                using(OracleConnection connection = new OracleConnection(ConfigurationManager.AppSettings["connection.connection_string"].ToString()))
                {
                    connection.Open();
                    OracleCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    //  GetParamters(command, parameters);
                    object i = command.ExecuteScalar();
                    connection.Close();
                    connection.Dispose();
                    return i;
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Hashtable Pagenation(string sql, int rows, int page, string sort, string order)
        {
            string where = string.Empty;

            var count = Query("select count(1) from (select  * from (" + sql + ")  mm " + where + ")").Tables[0].Rows[0][0];
            //"            
            //分页语句
            //sql = "select * from (" + sql + ") mm " + where;

            sql = "select * from (select x.*,rownumber() over() as rowid from (" + sql + ") as x )  xx where xx.rowid > " + ((page - 1) * rows).ToString() + " and xx.rowid<=" + (page * rows).ToString();
            if(sort.Length > 0)
                sql += " order by " + sort + " " + order;
            DataSet ds = Query(sql);

            Hashtable hs = new Hashtable();
            hs.Add("total", count);
            hs.Add("rows", ds.Tables[0]);
            return hs;
        }

        public DataSet Query(string sql)
        {
            try
            {
                using(OracleConnection connection = new OracleConnection(ConfigurationManager.AppSettings["connection.connection_string"].ToString()))
                {
                    connection.Open();
                    OracleCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    OracleDataAdapter dataAdapter = new OracleDataAdapter(command);
                    DataSet ds = new DataSet();
                    dataAdapter.Fill(ds);
                    connection.Close();
                    connection.Dispose();
                    return ds;
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
