using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace SucLib.Common
{
    public class SucConvert
    {
        /// <summary>
        /// 将DataTable转换成list<T> 实体类型转换
        /// </summary>
        /// <typeparam name="T">表类型</typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static IList<T> DataTableToList<T>(DataTable dt)
        {
            if (dt == null)
            {
                return null;
            }
            IList<T> list = new List<T>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                T t = (T)((object)Activator.CreateInstance(typeof(T)));
                PropertyInfo[] properties = t.GetType().GetProperties();
                PropertyInfo[] array = properties;
                for (int j = 0; j < array.Length; j++)
                {
                    PropertyInfo propertyInfo = array[j];
                    int k = 0;
                    while (k < dt.Columns.Count)
                    {
                        if (propertyInfo.Name.Equals(dt.Columns[k].ColumnName))
                        {
                            if (dt.Rows[i][k] == DBNull.Value)
                            {
                                propertyInfo.SetValue(t, null, null);
                                break;
                            }
                            if (propertyInfo.PropertyType.ToString() == "System.Int32")
                            {
                                propertyInfo.SetValue(t, int.Parse(dt.Rows[i][k].ToString()), null);
                            }
                            if (propertyInfo.PropertyType.ToString() == "System.DateTime")
                            {
                                propertyInfo.SetValue(t, Convert.ToDateTime(dt.Rows[i][k].ToString()), null);
                            }
                            if (propertyInfo.PropertyType.ToString() == "System.String")
                            {
                                propertyInfo.SetValue(t, dt.Rows[i][k].ToString(), null);
                            }
                            if (propertyInfo.PropertyType.ToString() == "System.Boolean")
                            {
                                propertyInfo.SetValue(t, Convert.ToBoolean(dt.Rows[i][k].ToString()), null);
                                break;
                            }
                            break;
                        }
                        else
                        {
                            k++;
                        }
                    }
                }
                list.Add(t);
            }
            return list;
        }
    }
}
