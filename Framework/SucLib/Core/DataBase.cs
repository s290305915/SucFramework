using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using SucLib.Data.Factory;
using SucLib.Data.IDal;

namespace SucLib.Core
{
    internal enum OpType
    {
        SELECT, INSERT, UPDATE, DELETE
    }
    public class DataBase<T> where T : class, new()
    {
        #region 属性
        IDBHelp db = DBFactory.Create();
        Type Type_Table = typeof(T);
        // 反射实体的所有属性
        PropertyInfo[] ProList;
        #endregion

        #region 私有方法
        /// 根据类型创建sql
        private string BaseSql(OpType o)
        {
            ProList = Type_Table.GetProperties();
            string sql = "";
            DataMapAttribute table = Type_Table.GetCustomAttributes(typeof(DataMapAttribute), true)[0] as DataMapAttribute;
            switch(o)
            {
                case OpType.SELECT:
                    sql = "SELECT ";
                    //枚举每一个属性比较
                    foreach(PropertyInfo Pro in ProList)
                    {
                        object[] objAttrs = Pro.GetCustomAttributes(typeof(DataMapAttribute), true);
                        if(objAttrs.Length > 0)
                        {
                            DataMapAttribute attr = objAttrs[0] as DataMapAttribute;
                            if(attr.Column != null)
                            {
                                sql += attr.Column + ",";
                            }
                        }
                    }
                    sql = sql.Substring(0, sql.Length - 1);
                    sql += " FROM " + table.TableName + " WHERE 1=1"; //Type_Table.Name;
                    break;
                case OpType.INSERT:
                    sql = "INSERT INTO " + table.TableName + "(";
                    foreach(PropertyInfo Pro in ProList)
                    {
                        object[] objAttrs = Pro.GetCustomAttributes(typeof(DataMapAttribute), true);
                        if(objAttrs.Length > 0)
                        {
                            DataMapAttribute attr = objAttrs[0] as DataMapAttribute;
                            if(attr.Column != null)
                            {
                                if(attr.Column != "ID")    //id自增
                                    sql += attr.Column + ",";
                            }
                        }
                    }
                    sql = sql.Substring(0, sql.Length - 1);
                    sql += ")VALUES(";
                    break;
                case OpType.UPDATE:
                    sql = "UPDATE " + table.TableName + " SET ";
                    break;
                case OpType.DELETE:
                    sql = "DELETE FROM " + table.TableName + " WHERE 1=1 ";
                    break;
            }
            return sql;
        }

        /// 加入子集内容
        private T GetSingle(T t)
        {
            try
            {
                T sg = t; //FindByCondition(t)[0];
                if(sg == null)
                {
                    return null;
                }
                ProList = Type_Table.GetProperties();
                foreach(PropertyInfo Pro in ProList)    //USER
                {
                    object[] objAttrs = Pro.GetCustomAttributes(typeof(DataMapAttribute), true);
                    if(objAttrs.Length > 0)
                    {
                        DataMapAttribute attr = objAttrs[0] as DataMapAttribute;
                        if(attr.BelongsTo != null)
                        {
                            //带出关联实体。。
                            string key = attr.BelongsToKey;
                            PropertyInfo bttpro = Type_Table.GetProperty(key);
                            string tkey = attr.TargetKey;
                            string kvalue = bttpro.GetValue(sg, null).ToString();   //根据key获取值
                            int kv = 0;
                            Type btt = Type.GetType(Pro.GetGetMethod().ReturnType.FullName);
                            DataMapAttribute table = btt.GetCustomAttributes(typeof(DataMapAttribute), true)[0] as DataMapAttribute;
                            PropertyInfo[] bttProList = btt.GetProperties();

                            string sql = "SELECT ";
                            foreach(PropertyInfo sgPro in bttProList)
                            {
                                object[] sgobjAttrs = sgPro.GetCustomAttributes(typeof(DataMapAttribute), true);
                                if(sgobjAttrs.Length > 0)
                                {
                                    DataMapAttribute sgattr = sgobjAttrs[0] as DataMapAttribute;
                                    if(sgattr.Column != null)
                                    {
                                        sql += sgattr.Column + ",";
                                    }
                                }
                            }
                            sql = sql.Substring(0, sql.Length - 1);
                            sql += " FROM " + table.TableName + " WHERE 1=1"; //Type_Table.Name;
                            if(int.TryParse(kvalue, out kv))
                                sql += " AND " + tkey + "=" + kvalue;
                            else
                                sql += " AND " + tkey + "='" + kvalue + "'";

                            DataTable dt = db.GetDataTable(sql);
                            if(dt.Rows.Count < 1)
                                continue;
                            var makeme = System.Type.GetType(btt.FullName);
                            object o = Activator.CreateInstance(makeme);
                            Type oot = o.GetType();
                            PropertyInfo[] ootProList = oot.GetProperties();
                            if(ootProList.Count() > 0)
                            {
                                for(int i = 0;i < ootProList.Count();i++)       //USER-ROLE
                                {
                                    PropertyInfo ootpro = ootProList[i];

                                    if(TypeRead.TRead(ootpro.PropertyType.FullName) == "Int32")
                                    {
                                        ootpro.SetValue(o, Convert.ToInt32(string.IsNullOrEmpty(dt.Rows[0][i].ToString()) ? "0" : dt.Rows[0][i].ToString()), null);
                                    }
                                    else if(TypeRead.TRead(ootpro.PropertyType.FullName) == "Null")
                                    {
                                        //查看是否有下钻或多对多关系
                                        object[] objAttrs_r = ootpro.GetCustomAttributes(typeof(DataMapAttribute), true);
                                        DataMapAttribute attr_r = objAttrs_r[0] as DataMapAttribute;
                                        if(attr_r.HasMany)
                                        {
                                            string t_table = attr_r.ManyTableName;
                                            string self_col = attr_r.ManyCloumnSelf;
                                            string other_col = attr_r.ManyClounmOther;
                                            Type btt_r = Type.GetType(ootProList[i].GetGetMethod().ReturnType.FullName); //大类型

                                            var makeme_r = System.Type.GetType(btt_r.FullName);
                                            object o_r = Activator.CreateInstance(makeme_r);
                                            o_r = GetManyData(o_r.GetType(), t_table, self_col, other_col);
                                            ootpro.SetValue(o, o_r, null);
                                        }
                                    }
                                    else
                                    {
                                        Type type = ootpro.PropertyType;
                                        if(type.FullName.Contains("Nullable"))
                                        {
                                            NullableConverter nullableConverter = new NullableConverter(type);
                                            type = nullableConverter.UnderlyingType;
                                            string v = dt.Rows[0][i].ToString();
                                            if(type.Name.Contains("DateTime"))
                                                v = string.IsNullOrEmpty(v) ? DateTime.MinValue.ToString() : v;
                                            if(type.Name.Contains("Int"))
                                                v = string.IsNullOrEmpty(v) ? int.MinValue.ToString() : v;
                                            ootpro.SetValue(o, Convert.ChangeType(v, type), null);
                                        }
                                        else
                                        {
                                            ootpro.SetValue(o, dt.Rows[0][i].ToString(), null);
                                        }
                                    }
                                }
                            }
                            Pro.SetValue(sg, o, null);
                        }
                        //多对多的关系
                        if(attr.HasMany)
                        {
                            string t_table = attr.ManyTableName;
                            string self_col = attr.ManyCloumnSelf;
                            string other_col = attr.ManyClounmOther;
                            Type btt = Type.GetType(Pro.GetGetMethod().ReturnType.FullName); //大类型
                            var makeme = System.Type.GetType(btt.FullName);
                            object o = Activator.CreateInstance(makeme);
                            o = GetManyData(o.GetType(), t_table, self_col, other_col);
                            Pro.SetValue(sg, o, null);
                        }
                    }
                }
                return sg;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        private object GetManyData(Type oot, string tb_name, string self, string other)
        {
            PropertyInfo[] ootProList = oot.GetProperties();
            //构建一个实体类，再通过 select * from tb_name where self=other来获取数据
            //在将获取的数据放入前面创建的实体类Object中，回传此object
            return null;
        }

        /// 类型转换
        private List<T> GetDataTable(string sql)
        {
            try
            {
                DataTable dt = db.GetDataTable(sql);
                List<T> t = new List<T>();
                t = EntityModel.ConvertTo<T>(dt);
                return t;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        /// 将得到的List加入下级内容
        private List<T> ConvertBy(List<T> ts)
        {
            if(ts == null)
            {
                return null;
            }
            else
            {
                try
                {
                    List<T> nTs = new List<T>();
                    foreach(T t in ts)
                    {
                        nTs.Add(GetSingle(t));
                    }
                    return nTs;
                }
                catch(Exception e)
                {
                    throw e;
                }
            }
        }

        #endregion

        /// <summary>
        /// 根据条件获取一条数据
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public T FindSingleByCondition(T t)
        {
            try
            {
                return GetSingle(FindByCondition(t)[0]);
            }
            catch(Exception e) { throw e; }
        }

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        public List<T> FindAll()
        {
            try
            {
                string sql = BaseSql(OpType.SELECT);
                List<T> Ts = GetDataTable(sql);
                return ConvertBy(Ts);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        public DataSet FindAllTable()//List<T>
        {
            try
            {
                string sql = BaseSql(OpType.SELECT);
                return db.GetDataSet(sql);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 根据条件获取数据
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public List<T> FindByCondition(T t)
        {
            try
            {
                string sql = BaseSql(OpType.SELECT) + " AND ";
                ProList = Type_Table.GetProperties();
                foreach(PropertyInfo pro in ProList)
                {
                    try
                    {
                        DataMapAttribute attr = pro.GetCustomAttributes(typeof(DataMapAttribute), true)[0] as DataMapAttribute;
                        if(attr.Column != null)
                        {
                            if(pro.GetValue(t, null) != null)
                            {
                                if(attr.Column == "ID" && Convert.ToInt32(pro.GetValue(t, null)) == 0)
                                {
                                    continue;
                                }
                                if(TypeRead.TRead(pro.PropertyType.FullName) == "Int32")
                                {
                                    sql += attr.Column + " =" + pro.GetValue(t, null).ToString() + " AND ";
                                }
                                else if(TypeRead.TRead(pro.PropertyType.FullName) == "DateTime")
                                {
                                    if(Convert.ToDateTime(pro.GetValue(t, null).ToString()) != DateTime.MinValue)
                                    {
                                        sql += attr.Column + " =" + pro.GetValue(t, null).ToString() + " AND ";
                                    }
                                }
                                else
                                {
                                    sql += attr.Column + " ='" + pro.GetValue(t, null).ToString() + "' AND ";
                                }
                            }
                        }
                    }
                    catch { continue; }
                }
                sql = sql.Substring(0, sql.Length - 4);
                return ConvertBy(GetDataTable(sql));
            }
            catch(Exception ex) { throw ex; }
        }

        /// <summary>
        /// 更新方法
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool Update(T t)
        {
            try
            {
                string sql = BaseSql(OpType.UPDATE);
                ProList = Type_Table.GetProperties();
                string where = "";
                foreach(PropertyInfo pro in ProList)
                {
                    if(pro.GetValue(t, null) == null)
                        continue;
                    try
                    {
                        DataMapAttribute attr = pro.GetCustomAttributes(typeof(DataMapAttribute), true)[0] as DataMapAttribute;
                        if(attr.Column != null)
                        {
                            if(attr.Column == "ID")
                            {
                                where = string.Format(" WHERE ID={0}", pro.GetValue(t, null));
                                continue;
                            }
                            if(TypeRead.TRead(pro.PropertyType.FullName) == "Int32")
                            {
                                sql += string.Format(" {0}={1},", attr.Column, pro.GetValue(t, null));
                                continue;
                            }
                            else
                            {
                                sql += string.Format(" {0}='{1}',", attr.Column, pro.GetValue(t, null));
                                continue;
                            }
                        }
                    }
                    catch { }
                }
                sql = sql.Substring(0, sql.Length - 1);
                sql += where;
                return db.ExecuteNonQuery(sql) > 0;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool Add(T t)
        {
            try
            {
                string sql = BaseSql(OpType.INSERT);
                ProList = Type_Table.GetProperties();
                foreach(PropertyInfo pro in ProList)
                {
                    try
                    {
                        DataMapAttribute attr = pro.GetCustomAttributes(typeof(DataMapAttribute), true)[0] as DataMapAttribute;
                        if(attr.Column != null)
                        {
                            if(attr.Column == "ID")
                            {
                                continue;
                            }
                            if(TypeRead.TRead(pro.PropertyType.FullName) == "Int32")
                            {
                                if(pro.GetValue(t, null) == null)
                                {
                                    sql += "0,";
                                    continue;
                                }
                                sql += pro.GetValue(t, null) + ",";
                            }
                            else
                            {
                                if(pro.GetValue(t, null) == null)
                                {
                                    sql += "'',";
                                    continue;
                                }
                                sql += "'" + pro.GetValue(t, null).ToString() + "',";
                            }
                        }
                    }
                    catch { continue; }
                }
                sql = sql.Substring(0, sql.Length - 1);
                sql += " )";
                if(db.ExecuteNonQuery(sql) > 0)
                {
                    //查询是否有下级添加数据
                    //级联插入
                    foreach(PropertyInfo pro in ProList)
                    {
                        DataMapAttribute attr = pro.GetCustomAttributes(typeof(DataMapAttribute), true)[0] as DataMapAttribute;
                        //看这个是不是为空的
                        if(attr.BelongsTo != null)
                        {
                            //带出关联实体。。
                            string key = attr.BelongsToKey;
                            PropertyInfo bttpro = Type_Table.GetProperty(key);
                            string tkey = attr.TargetKey;
                            string kvalue = bttpro.GetValue(t, null).ToString();   //根据key获取值
                        }
                    }
                    return true;
                }
                else
                    return false;
            }
            catch(Exception e) { throw e; }
        }

        /// <summary>
        /// 增加多条数据
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public bool AddMutilRow(List<T> ts)
        {
            try
            {
                foreach(T t in ts)
                {
                    Add(t);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 根据id列表删除
        /// </summary>
        /// <param name="IDs"></param>
        /// <returns></returns>
        public bool Delete(List<int> IDs)
        {
            try
            {
                string sql = BaseSql(OpType.DELETE);
                string ids = "";
                foreach(int i in IDs)
                {
                    ids += i + ",";
                }
                ids = ids.Substring(0, ids.Length - 1);
                sql += " AND ID IN(" + ids + ")";
                return db.ExecuteNonQuery(sql) > 0;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 删除单条数据
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool Delete(T t)
        {
            try
            {
                string sql = BaseSql(OpType.DELETE) + " AND ";
                ProList = Type_Table.GetProperties();
                foreach(PropertyInfo pro in ProList)
                {
                    DataMapAttribute attr = pro.GetCustomAttributes(typeof(DataMapAttribute), true)[0] as DataMapAttribute;
                    if(attr.Column != null)
                    {
                        if(pro.GetValue(t, null) != null)
                        {
                            if(attr.Column == "ID" && Convert.ToInt32(pro.GetValue(t, null)) == 0)
                            {
                                continue;
                            }
                            if(TypeRead.TRead(pro.PropertyType.FullName) == "Int32")
                            {
                                sql += attr.Column + " =" + pro.GetValue(t, null).ToString() + " AND ";
                            }
                            else
                            {
                                sql += attr.Column + " ='" + pro.GetValue(t, null).ToString() + "' AND ";
                            }
                        }
                    }
                }
                sql = sql + " 1=1 ";//.Substring(0, sql.Length - 4);
                return db.ExecuteNonQuery(sql) > 0;
            }
            catch(Exception e)
            {
                throw e;
            }
        }


        public int MaxID()
        {
            try
            {
                string sql = "SELECT MAX(ID) FROM ";
                DataMapAttribute table = Type_Table.GetCustomAttributes(typeof(DataMapAttribute), true)[0] as DataMapAttribute;
                sql += table.TableName;
                return Convert.ToInt32(db.GetDataTable(sql).Rows[0][0]);
            }
            catch { return 0; }
        }
    }

    internal static class TypeRead
    {
        public static string TRead(string FullName)
        {
            string tpname = "";
            if(FullName == "System.String")
            {
                tpname = "String";
            }
            else if(FullName == "System.Int32")
            {
                tpname = "Int32";
            }
            else if(FullName == "System.Int64")
            {
                tpname = "Int64";
            }
            else if(FullName == "System.Single")
            {
                tpname = "Single";
            }
            else if(FullName == "System.Double")
            {
                tpname = "Double";
            }
            else if(FullName == "System.Decimal")
            {
                tpname = "Decimal";
            }
            else if(FullName == "System.Char")
            {
                tpname = "Char";
            }
            else if(FullName == "System.Boolean")
            {
                tpname = "Boolean";
            }
            else if(FullName == "System.DateTime")
            {
                tpname = "DateTime";
            }
            //可空类型
            else if(FullName == "System.Nullable`1[[System.DateTime, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]")
            {
                tpname = "DateTime";
            }
            else if(FullName == "System.Nullable`1[[System.DateTime, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]")
            {
                tpname = "DateTime";
            }
            else if(FullName == "System.Nullable`1[[System.Int32, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]")
            {
                tpname = "Int32";
            }
            else if(FullName == "System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]")
            {
                tpname = "Int32";
            }
            else if(FullName == "System.Nullable`1[[System.Int64, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]")
            {
                tpname = "Int64";
            }
            else if(FullName == "System.Nullable`1[[System.Int64, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]")
            {
                tpname = "Int64";
            }
            else if(FullName == "System.Nullable`1[[System.Decimal, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]")
            {
                tpname = "Decimal";
            }
            else if(FullName == "System.Nullable`1[[System.Decimal, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]")
            {
                tpname = "Decimal";
            }
            else if(FullName == "System.Nullable`1[[System.Boolean, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]")
            {
                tpname = "Boolean";
            }
            else if(FullName == "System.Nullable`1[[System.Boolean, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]")
            {
                tpname = "Boolean";
            }
            else
            {
                tpname = "Null";
                //throw new Exception("属性包含不支持的数据类型!");
            }
            return tpname;
        }
    }
}
