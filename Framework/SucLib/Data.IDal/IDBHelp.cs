using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SucLib.Data.IDal
{
    public interface IDBHelp
    {
        DataTable GetDataTable(string commandText, params string[] args);
        DataTable GetDataTable(string commandText);
        DataSet GetDataSet(string commandText);
        object ExecuteScalar(string commandText);
        int ExecuteNonQuery(string commandText, params string[] args);
        int ExecuteNonQuery(string commandText);
        bool IsExists(string commandText);
        int GetRecordCount(string commandText);
        IList<string> GetList(string commandText);
        DataTable GetPageDataTable(int pageIndex, int pageSize, int recordCount, string fields, string tableName, string orderField, string conditions, bool isDesc);
        DataTable GetMPageDataTable(int pageIndex, int pageSize, string fields, string tableName, string conditions, string orderField, bool isDesc);
        DataTable GetDataTable(string commandText, bool showMethodSql);
        object ExecuteScalar(string commandText, bool showMethodSql);
        int ExecuteNonQuery(string commandText, bool showMethodSql);
        bool IsExists(string commandText, bool showMethodSql);
        int GetRecordCount(string commandText, bool showMethodSql);
        IList<string> GetList(string commandText, bool showMethodSql);
        DataTable GetPageDataTable(int pageIndex, int pageSize, int recordCount, string fields, string tableName, string orderField, string conditions, bool isDesc, bool showMethodSql);
        DataTable GetMPageDataTable(int pageIndex, int pageSize, string fields, string tableName, string conditions, string orderField, bool isDesc, bool showMethodSql);
    }
}
