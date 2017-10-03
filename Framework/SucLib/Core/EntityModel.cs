using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SucLib.Core
{
    public static class EntityModel
    {
        public static List<T> ConvertTo<T>(DataTable dt) where T : new()
        {
            if (dt == null) return null;
            if (dt.Rows.Count <= 0) return null;

            List<T> list = new List<T>();
            Type type = typeof(T);
            PropertyInfo[] propertyInfos = type.GetProperties();  //获取泛型的属性
            List<DataColumn> listColumns = dt.Columns.Cast<DataColumn>().ToList();  //获取数据集的表头，以便于匹配
            T t;
            foreach (DataRow dr in dt.Rows)
            {
                t = new T();
                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    try
                    {
                        DataColumn dColumn = listColumns.Find(name => name.ToString().ToUpper() == propertyInfo.Name.ToUpper());  //查看是否存在对应的列名

                        if (dColumn != null)
                            if (dr[propertyInfo.Name] != DBNull.Value)
                                propertyInfo.SetValue(t, dr[propertyInfo.Name], null);  //赋值
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
                list.Add(t);
            }
            return list;

        }
    }
}
