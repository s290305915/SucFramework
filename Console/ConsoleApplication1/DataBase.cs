using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ConsoleApplication1
{
    public class DataBase<T> where T : class
    {
        public string Query()//List<T>
        {
            Type type1 = typeof(T);

            // 反射字体的所有属性
            PropertyInfo[] ProList = type1.GetProperties();
            string sql = "SELECT ";
            //枚举每一个属性比较
            foreach (PropertyInfo Pro in ProList)
            {
                sql += Pro.Name + ",";
            }
            sql = sql.Substring(0, sql.Length - 1);
            sql += " FROM " + type1.Name;
            return sql;
        }
    }
}
