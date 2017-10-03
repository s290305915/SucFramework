using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using System.Text;
using SucLib.Data.IDal;
using SucLib.Common;

namespace SucLib.Data.Factory
{
    public class DBFactory
    {
        /// <summary>
        /// 创建实例工厂
        /// </summary>
        /// <returns></returns>
        public static IDBHelp Create()
        {
            string typeName = "";
            string text = "SucLib";
            //string a = ConfigurationSettings.AppSettings["DBType"];
            string a = "SqlServer"; //ConfigUtil.ConfigHelper.GetConfigString("DBType");
            if(a == "Access")
            {
                typeName = text + ".Data.Dal.AccHelp";
            }
            else if(a == "SqlServer" || string.IsNullOrEmpty(a))
            {
                typeName = text + ".Data.Dal.SQLHelp";
            }
            else if(a == "MySql")
            {
            }
            else if(a == "DB2")
            {
            }
            else if(a == "Oracle")
            {
            }
            else if(a == "SqlLite")
            {
            }
            return (IDBHelp)Assembly.Load(text).CreateInstance(typeName);
        }

        /// <summary>
        /// 创建带链接的实例
        /// </summary>
        /// <param name="dbt">数据库类型</param>
        /// <param name="conn">连接字符串</param>
        /// <returns></returns>
        public static IDBHelp Create(DataBaseType dbt, string conn)
        {
            string typeName = "";
            string text = "SucLib";
            //string a = ConfigurationSettings.AppSettings["DBType"];
            string a = dbt.ToString();//"SqlServer"; //ConfigUtil.ConfigHelper.GetConfigString("DBType");
            if(a == "Access")
            {
                typeName = text + ".Data.Dal.AccHelp";
            }
            else if(a == "SqlServer" || string.IsNullOrEmpty(a))
            {
                typeName = text + ".Data.Dal.SQLHelp";
            }
            else if(a == "MySql")
            {
            }
            else if(a == "DB2")
            {
            }
            else if(a == "Oracle")
            {
            }
            else if(a == "SqlLite")
            {
            }
            Type t = Type.GetType(typeName);
            return (IDBHelp)Activator.CreateInstance(t, new object[] { conn });
        }

        /// <summary>
        /// 创建带链接的实例
        /// </summary>
        /// <param name="dbt">数据库类型</param>
        /// <param name="host">地址</param>
        /// <param name="database">数据库名</param>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public static IDBHelp Create(DataBaseType dbt, string host, string database, string username, string password)
        {
            string conn = "";
            string typeName = "";
            string text = "SucLib";
            //string a = ConfigurationSettings.AppSettings["DBType"];
            string a = dbt.ToString();//"SqlServer"; //ConfigUtil.ConfigHelper.GetConfigString("DBType");
            if(a == "Access")
            {
                typeName = text + ".Data.Dal.AccHelp";
            }
            else if(a == "SqlServer" || string.IsNullOrEmpty(a))
            {
                typeName = text + ".Data.Dal.SQLHelp";
                conn = string.Format(@"Data Source={0};Initial Catalog={1};User ID={2};Pwd={3}", host, database, username, password);
            }
            else if(a == "MySql")
            {
            }
            else if(a == "DB2")
            {
            }
            else if(a == "Oracle")
            {
            }
            else if(a == "SqlLite")
            {
            }
            Type t = Type.GetType(typeName);
            return (IDBHelp)Activator.CreateInstance(t, new object[] { conn });
        }
    }

    public enum DataBaseType
    {
        Access, SqlServer, MySql, DB2, Oracle, SqlLite
    }
}
