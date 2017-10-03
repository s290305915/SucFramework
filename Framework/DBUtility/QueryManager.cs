using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace QuerManager
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

        Hashtable Pagenation(string sql, int rows, int page, string sort, string order, string seq);

        object ExecuteScalar(string sql);
    }
    public class QueryManager
    {
        IDBQuery _query = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public QueryManager()
        {
            _query = new SqlQuery();
        }

        /// <summary>
        /// 执行sql
        /// </summary>
        /// <param name="sql">sql</param>
        public void Execute(string sql)
        {
            _query.Execute(sql);
        }

        /// <summary>
        /// 执行sql并返回DataSet
        /// </summary>
        /// <param name="sql">sql</param>
        /// <returns>数据集</returns>
        public DataSet Query(string sql)
        {
            return _query.Query(sql);
        }


        /// <summary>
        /// 分页语句
        /// </summary>
        /// <param name="sql">主要sql</param>
        /// <param name="rows">单页行数</param>
        /// <param name="page">当前页</param>
        /// <param name="sort">排序列</param>
        /// <param name="order">排序类型（desc,asc）</param>
        /// <param name="seq">取条数的咧（ID，name等）</param>
        /// <returns></returns>
        public Hashtable Pagenation(string sql, int rows, int page, string sort, string order, string seq)
        {
            return _query.Pagenation(sql, rows, page, sort, order, seq);
        }

        /// <summary>
        /// 执行sql返回查询条数
        /// </summary>
        /// <param name="sql">sql</param>
        /// <returns>条数</returns>
        public object ExecuteScalar(string sql)
        {
            return _query.ExecuteScalar(sql);
        }
    }
    class SqlQuery : IDBQuery
    {
        public void Execute(string sql)
        {
            try
            {
                using(SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connection_string"].ToString()))
                {
                    connection.Open();
                    SqlCommand command = connection.CreateCommand();
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
                using(SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connection_string"].ToString()))
                {
                    connection.Open();
                    SqlCommand command = connection.CreateCommand();
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

        public Hashtable Pagenation(string sql, int rows, int page, string sort, string order, string seq)
        {
            string where = string.Empty;

            var count = Query("select count(1) from (select  * from (" + sql + ")  mm " + where + ")").Tables[0].Rows[0][0];
            //"            
            //分页语句
            //sql = "select * from (" + sql + ") mm " + where;

            sql = "select * from (select x.*,rownumber() over(order by " + seq + ") as rowid from (" + sql + ") as x )  xx where xx.rowid > " + ((page - 1) * rows).ToString() + " and xx.rowid<=" + (page * rows).ToString();
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
                using(SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connection_string"].ToString()))
                {
                    connection.Open();
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
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
