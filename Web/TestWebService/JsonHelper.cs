using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;


/// <summary>
/// JSON帮助类
/// </summary>
public class JsonHelper
{
    #region 对象转JSON
    /// <summary>
    /// 对象转JSON
    /// </summary>
    /// <param name="obj">对象</param>
    /// <returns>JSON格式的字符串</returns>
    public static string ObjectToJSON(object obj)
    {
        JavaScriptSerializer jss = new JavaScriptSerializer();
        try
        {
            return jss.Serialize(obj);
        }
        catch (Exception ex)
        {

            throw new Exception("JSONHelper.ObjectToJSON(): " + ex.Message);
        }
    }
    #endregion

    #region 数据表转键值对集合,把DataTable转成 List集合, 存每一行,集合中放的是键值对字典,存每一列
    /// <summary>
    /// 数据表转键值对集合
    /// 把DataTable转成 List集合, 存每一行
    /// 集合中放的是键值对字典,存每一列
    /// </summary>
    /// <param name="dt">数据表</param>
    /// <returns>哈希表数组</returns>
    public static List<Dictionary<string, object>> DataTableToList(DataTable dt)
    {
        List<Dictionary<string, object>> list
             = new List<Dictionary<string, object>>();

        foreach (DataRow dr in dt.Rows)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            foreach (DataColumn dc in dt.Columns)
            {
                dic.Add(dc.ColumnName, dr[dc.ColumnName]);
            }
            list.Add(dic);
        }
        return list;
    }
    #endregion

    #region 数据集转键值对数组字典
    /// <summary>
    /// 数据集转键值对数组字典
    /// </summary>
    /// <param name="dataSet">数据集</param>
    /// <returns>键值对数组字典</returns>
    public static Dictionary<string, List<Dictionary<string, object>>> DataSetToDic(DataSet ds)
    {
        Dictionary<string, List<Dictionary<string, object>>> result = new Dictionary<string, List<Dictionary<string, object>>>();

        foreach (DataTable dt in ds.Tables)
            result.Add(dt.TableName, DataTableToList(dt));

        return result;
    }
    #endregion

    #region 数据表转JSON
    /// <summary>
    /// 数据表转JSON
    /// </summary>
    /// <param name="dataTable">数据表</param>
    /// <returns>JSON字符串</returns>
    public static string DataTableToJSON(DataTable dt)
    {
        return ObjectToJSON(DataTableToList(dt));
    }
    #endregion

    #region 数据表转JSON，无限级分类形式的数据表，用于绑定树型下拉列表的JSON格式
    public static System.Text.StringBuilder sb = new System.Text.StringBuilder();

    /// <summary>
    /// 数据表转JSON，无限级分类形式的数据表，用于绑定树型下拉列表的JSON格式
    /// </summary>
    /// <param name="dt">数据表</param>
    /// <param name="value_filed">数据表中KEY字段名</param>
    /// <param name="text_filed">数据表中TEXT字段名</param>
    /// <param name="parent_filed">数据表中父类字段名</param>
    /// <param name="parentid">数据表中最高级的ID值</param>
    /// <returns>JSON字符串</returns>
    public static string DataTableToJSON(DataTable dt, string value_filed, string text_filed, string columns, string parent_filed, int parentid)
    {
        sb = new System.Text.StringBuilder();
        sb = JsonHelper.GetJsonInfo(dt, value_filed, text_filed, columns, parent_filed, parentid);
        return sb.ToString();
    }
    //public static string DataTableToJSON(DataTable dt, string value_filed, string text_filed, string parent_filed, string parentid)
    //{
    //    sb = new System.Text.StringBuilder();
    //    sb = JsonHelper.GetJsonInfo(dt, value_filed, text_filed, parent_filed, parentid);
    //    return sb.ToString();
    //}
    //public static System.Text.StringBuilder GetJsonInfo(DataTable dt, string value_filed, string text_filed, string parent_filed, string parentid)
    //{
    //    sb.Append("[");
    //    DataRow[] _drlist = dt.Select(parent_filed + "=" + parentid);
    //    for (int i = 0; i < _drlist.Length; i++)
    //    {
    //        DataRow dr = _drlist[i];
    //        sb.Append("{\"" + value_filed + "\":\"" + dr[value_filed].ToString() + "\",");
    //        sb.Append("\"" + text_filed + "\":\"" + dr[text_filed].ToString() + "\"");
    //        sb.Append("\"attributes\":{\"sort\":\"" + dr["sort"].ToString() + "\"}");
    //        if (dr[value_filed].ToString() != parentid.ToString())
    //        {
    //            DataRow[] _drlist_other = dt.Select(parent_filed + "=" + dr[value_filed].ToString() + " and " + value_filed + "<>" + parentid);
    //            if (_drlist_other.Length > 0)
    //            {
    //                sb.Append(",\"children\":");
    //                GetJsonInfo(dt, value_filed, text_filed, parent_filed, int.Parse(dr[value_filed].ToString()));
    //            }
    //        }
    //        if (i == _drlist.Length - 1)
    //        {
    //            sb.Append(",\"checked\":false}");
    //        }
    //        else
    //        {
    //            sb.Append(",\"checked\":false},");
    //        }
    //    }
    //    sb.Append("]");
    //    return sb;
    //}

    public static System.Text.StringBuilder GetJsonInfo(DataTable dt, string value_filed, string text_filed, string columns, string parent_filed, int parentid)
    {
        sb.Append("[");
        IList<DataRow> _drlist = null;
        _drlist = dt.Select(parentid == 0 ? (parent_filed + " IS NULL OR " + parent_filed + " = 0 ") : (parent_filed + " = " + parentid)).OrderBy(m => m["sort"]).ToList<DataRow>();
        for (int i = 0; i < _drlist.Count; i++)
        {
            DataRow dr = _drlist[i];

            sb.Append("{\"id\":\"" + dr[value_filed].ToString() + "\",");
            if (columns.Length > 0)
            {
                foreach (string s in columns.Split(','))
                {
                    sb.Append("\"" + s + "\":\"" + dr[s].ToString() + "\",");
                }
            }
            sb.Append("\"text\":\"" + dr[text_filed].ToString() + "\"");
            //sb.Append("\"manager\":\"" + dr["login_name"].ToString() + "\",");
            //sb.Append("\"state\":\"closed\",");
            //sb.Append("\"attributes\":{\"sort\":\"" + dr["sort"].ToString() + "\"}");
            //sb.Append("\"attributes\":{\"sort\":\"1\"}");
            if (dr[value_filed].ToString() != parentid.ToString())
            {
                DataRow[] _drlist_other = dt.Select(parent_filed + "=" + dr[value_filed].ToString() + " and " + value_filed + "<>" + parentid);
                if (_drlist_other.Length > 0)
                {
                    sb.Append(",\"children\":");
                    GetJsonInfo(dt, value_filed, text_filed, columns, parent_filed, int.Parse(dr[value_filed].ToString()));
                }
            }
            //if (i == _drlist.Length - 1)
            //{
            //    sb.Append(",\"checked\":false}");
            //}
            //else
            //{
            //    sb.Append(",\"checked\":false},");
            //}
            if (i == _drlist.Count - 1)
            {
                sb.Append("}");
            }
            else
            {
                sb.Append("},");
            }
        }
        sb.Append("]");
        return sb;
    }
    #endregion

    #region DataSet数据集转Json
    /// <summary>
    /// DataSet数据集转Json
    /// </summary>
    /// <param name="ds">数据集</param>
    /// <returns></returns>
    public static string DataSetToJson(DataSet ds)
    {
        try
        {
            string json = "[";
            foreach (DataRow i in ds.Tables[0].Rows)
            {
                json += "{";
                foreach (DataColumn column in ds.Tables[0].Columns)
                {
                    json += '"' + column.ColumnName + '"' + ":";
                    json += '"' + i[column.ColumnName].ToString() + '"' + ",";

                    //json += '"' + column.ColumnName + '":';
                    //if (column.DataType == typeof(DateTime) || column.DataType == typeof(string))
                    //{
                    //    json += '"' + i[column.ColumnName].ToString() + '"' + ",";
                    //    //json += "'" + i[column.ColumnName].ToString() + "',";
                    //}
                    //else
                    //{
                    //    json += "" + i[column.ColumnName].ToString() + ",";
                    //}
                }
                json = json.Substring(0, json.LastIndexOf(",")) + "},";
            }
            json = json.Substring(0, json.LastIndexOf(","));
            return json + "]";
        }
        catch
        {
            return "";
        }
    }
    #endregion

    #region TableToTreeJson
    /// <summary>
    /// 把传入的表解析为Tree格式的JSON字符串，Tree可以是无限级的,用户表自身的主从关系
    /// </summary>
    /// <param name="data">需解析的表数据</param>
    /// <param name="idField">节点的ID字段名称</param>
    /// <param name="valueField">节点的Text字段名称</param>
    /// <param name="parentField">表示主从关系的字段名称</param>
    /// <returns></returns>
    public static string TableToTreeJson(DataTable data, string idField, string valueField, string parentField, string parentId, bool nodeState, string fields)
    {
        System.Text.StringBuilder ret = new System.Text.StringBuilder();
        try
        {
            if (data != null && data.Rows.Count > 0)
            {
                ret.Append(TableTreeJsonStr(data, idField, valueField, parentField, parentId, nodeState, fields));
            }
            if (ret.Length <= 0)
            {
                ret.Append("[{\"id\":0,");
                ret.Append("\"text\":\"暂无\",");
                ret.Append("\"state\":\"closed\"]");
            }
        }
        catch
        { }
        return ret.ToString();
    }


    private static string TableTreeJsonStr(DataTable data, string idField, string valueField, string parentField, string parentId, bool nodeState, string fields)
    {
        System.Text.StringBuilder ret = new System.Text.StringBuilder();
        ret.Append("[");
        string expr = "";
        DataRow[] drzero = data.Select(parentField + "=0");
        if (drzero != null && drzero.Length > 0)
            expr = string.IsNullOrEmpty(parentId) ? (parentField + " = 0") : (parentField + " = " + parentId + "");
        else
            expr = string.IsNullOrEmpty(parentId) ? (parentField + " IS NULL ") : (parentField + " = " + parentId + "");
        DataRow[] root = data.Select(expr);
        int i = 1;
        foreach (DataRow row in root)
        {
            ret.Append("{\"id\":" + row[idField].ToString() + ",");
            ret.Append("\"text\":\"" + row[valueField].ToString() + "\",");
            if (!string.IsNullOrEmpty(fields))
            {
                string[] fieldArray = fields.Split(',');
                for (int j = 0; j < fieldArray.Count(); j++)
                {
                    ret.Append("\"" + fieldArray[j] + "\":\"" + row[fieldArray[j]].ToString() + "\",");
                }
            }
            ret.Append("\"state\":\"" + (nodeState ? "open" : "closed") + "\"");
            DataRow[] child = data.Select(parentField + "=" + row[idField].ToString());
            if (child.Length > 0)
            {
                ret.Append(",\"children\":");
                ret.Append(TableToTreeJson(data, idField, valueField, parentField, row[idField].ToString(), nodeState, fields));
                if (i == root.Length)
                    ret.Append("}");
                else
                    ret.Append("},");
            }
            else
            {
                if (i == root.Length)
                    ret.Append("}");
                else
                    ret.Append("},");
            }
            i++;
        }

        ret.Append("]");
        return ret.ToString();
    }

    //ttt
    private static string TableTreeJsonStr(DataTable data, string idField, string valueField, string parentField, string parentId, bool nodeState, string fields, string level)
    {
        System.Text.StringBuilder ret = new System.Text.StringBuilder();
        ret.Append("[");
        string expr = "";
        DataRow[] drzero = data.Select(parentField + "=0");
        if (drzero != null && drzero.Length > 0)
            expr = string.IsNullOrEmpty(parentId) ? (parentField + " = 0 OR ID=") : (parentField + " = " + parentId + "");
        else
            expr = string.IsNullOrEmpty(parentId) ? (parentField + " IS NULL ") : (parentField + " = " + parentId + "");
        DataRow[] root = data.Select(expr);
        int i = 1;
        foreach (DataRow row in root)
        {
            ret.Append("{\"id\":" + row[idField].ToString() + ",");
            ret.Append("\"text\":\"" + row[valueField].ToString() + "\",");
            ret.Append("\"level\":\"" + row[level].ToString() + "\",");
            if (!string.IsNullOrEmpty(fields))
            {
                string[] fieldArray = fields.Split(',');
                for (int j = 0; j < fieldArray.Count(); j++)
                {
                    ret.Append("\"" + fieldArray[j] + "\":\"" + row[fieldArray[j]].ToString() + "\",");
                }
            }
            ret.Append("\"state\":\"" + (nodeState ? "open" : "closed") + "\"");
            DataRow[] child = data.Select(parentField + "=" + row[idField].ToString());
            if (child.Length > 0)
            {
                ret.Append(",\"children\":");
                ret.Append(TableToTreeJson(data, idField, valueField, parentField, row[idField].ToString(), nodeState, fields, level));
                if (i == root.Length)
                    ret.Append("}");
                else
                    ret.Append("},");
            }
            else
            {
                if (i == root.Length)
                    ret.Append("}");
                else
                    ret.Append("},");
            }
            i++;
        }

        ret.Append("]");
        return ret.ToString();
    }

    //ttt
    public static string TableToTreeJson(DataTable data, string idField, string valueField, string parentField, string parentId, bool nodeState, string fields, string level)
    {
        System.Text.StringBuilder ret = new System.Text.StringBuilder();
        try
        {
            if (data != null && data.Rows.Count > 0)
            {
                ret.Append(TableTreeJsonStr(data, idField, valueField, parentField, parentId, nodeState, fields, level));
            }
            if (ret.Length <= 0)
            {
                ret.Append("[{\"id\":0,");
                ret.Append("\"text\":\"暂无\",");
                ret.Append("\"state\":\"closed\"}]");
            }
        }
        catch
        { }
        return ret.ToString();
    }

    #endregion

    #region TableToTreeJsonString
    /// <summary>
    /// 把传入的表解析为Tree格式的JSON字符串，Tree可以是无限级的,用户表自身的主从关系
    /// </summary>
    /// <param name="data">需解析的表数据</param>
    /// <param name="idField">节点的ID字段名称</param>
    /// <param name="valueField">节点的Text字段名称</param>
    /// <param name="parentField">表示主从关系的字段名称</param>
    /// <returns></returns>
    public static string TableToTreeJsonString(DataTable data, string idField, string valueField, string parentField, string parentId, bool nodeState, string icon, bool showtextId)
    {
        System.Text.StringBuilder ret = new System.Text.StringBuilder();
        try
        {
            if (data != null && data.Rows.Count > 0)
            {
                ret.Append(TableTreeJsonStrStringValue(data, idField, valueField, parentField, parentId, nodeState, icon, showtextId));
            }
            if (ret.Length <= 0)
            {
                ret.Append("[{\"id\":0,");
                ret.Append("\"text\":\"暂无\",");
                ret.Append("\"state\":\"closed\",\"iconCls\":\"" + icon + "}]");
            }
        }
        catch
        { }
        return ret.ToString();
    }

    private static string TableTreeJsonStrStringValue(DataTable data, string idField, string valueField, string parentField, string parentId, bool nodeState, string icon, bool showtextId)
    {
        System.Text.StringBuilder ret = new System.Text.StringBuilder();
        ret.Append("[");
        string expr = "";
        DataRow[] drzero = data.Select(parentField + "=0");
        if (drzero != null && drzero.Length > 0)
            expr = string.IsNullOrEmpty(parentId) ? (parentField + " = 0") : (parentField + " = '" + parentId + "'");
        else
            expr = string.IsNullOrEmpty(parentId) ? (parentField + " IS NULL ") : (parentField + " = '" + parentId + "'");
        DataRow[] root = data.Select(expr);
        int i = 1;
        foreach (DataRow row in root)
        {
            ret.Append("{\"id\":\"" + row[idField].ToString() + "\",");
            ret.Append("\"text\":\"" + (showtextId ? row[valueField].ToString() + " " + row[idField].ToString() : row[valueField].ToString()) + "\",");
            //ret.Append("\"state\":\"" + (nodeState ? "open" : "closed") + "\"" + (string.IsNullOrEmpty(icon) ? ",\"iconCls\":\"" + icon + "\"" : ""));
            //easyui tree特性，自定义树节点属性,level用于标识机构是区县还是片区或是渠道
            ret.Append("\"attributes\":{\"sort\":\"" + row["sort"].ToString() + "\"},");
            if (row["parent_id"].ToString() == "0")
            {
                ret.Append("\"state\":\"open\"" + (string.IsNullOrEmpty(icon) ? ",\"iconCls\":\"" + icon + "\"" : ""));
            }
            else
            {
                ret.Append("\"state\":\"" + row["state"].ToString() + "\"" + (string.IsNullOrEmpty(icon) ? ",\"iconCls\":\"" + icon + "\"" : ""));
            }
            DataRow[] child = data.Select(parentField + "='" + row[idField].ToString() + "'");
            if (child.Length != null && child.Length > 0)
            {
                ret.Append(",\"children\":");
                ret.Append(TableTreeJsonStrStringValue(data, idField, valueField, parentField, row[idField].ToString(), nodeState, icon, showtextId));
                if (i == root.Length)
                    ret.Append("}");
                else
                    ret.Append("},");
            }
            else
            {
                if (i == root.Length)
                    ret.Append("}");
                else
                    ret.Append("},");
            }
            i++;
        }

        ret.Append("]");
        return ret.ToString();
    }
    #endregion
    public static string DataTableToJSON(DataTable dt, string value_filed, string text_filed, string parent_filed, int parentid)
    {
        sb = new System.Text.StringBuilder();
        sb = JsonHelper.GetJsonInfo(dt, value_filed, text_filed, parent_filed, parentid);
        return sb.ToString();
    }

    public static string DataTableToJSON(DataTable dt, string value_filed, string text_filed, string parent_filed, string parentid)
    {
        sb = new System.Text.StringBuilder();
        sb = JsonHelper.GetJsonInfo(dt, value_filed, text_filed, parent_filed, parentid);
        return sb.ToString();
    }

    public static System.Text.StringBuilder GetJsonInfo(DataTable dt, string value_filed, string text_filed, string parent_filed, string parentid)
    {
        sb.Append("[");
        DataRow[] _drlist = dt.Select(parent_filed + "=" + parentid);
        for (int i = 0; i < _drlist.Length; i++)
        {
            DataRow dr = _drlist[i];
            sb.Append("{\"" + value_filed + "\":\"" + dr[value_filed].ToString() + "\",");
            sb.Append("\"" + text_filed + "\":\"" + dr[text_filed].ToString() + "\"");
            sb.Append("\"attributes\":{\"sort\":\"" + dr["sort"].ToString() + "\"}");
            if (dr[value_filed].ToString() != parentid.ToString())
            {
                DataRow[] _drlist_other = dt.Select(parent_filed + "=" + dr[value_filed].ToString() + " and " + value_filed + "<>" + parentid);
                if (_drlist_other.Length > 0)
                {
                    sb.Append(",\"children\":");
                    GetJsonInfo(dt, value_filed, text_filed, parent_filed, int.Parse(dr[value_filed].ToString()));
                }
            }
            if (i == _drlist.Length - 1)
            {
                sb.Append(",\"checked\":false}");
            }
            else
            {
                sb.Append(",\"checked\":false},");
            }
        }
        sb.Append("]");
        return sb;
    }

    public static System.Text.StringBuilder GetJsonInfo(DataTable dt, string value_filed, string text_filed, string parent_filed, int parentid)
    {
        sb.Append("[");
        DataRow[] _drlist = dt.Select(parentid == 0 ? (parent_filed + " IS NULL OR " + parent_filed + " = 0") : (parent_filed + " = " + parentid + ""));//dt.Select(parent_filed + "=" + parentid); 
        for (int i = 0; i < _drlist.Length; i++)
        {
            DataRow dr = _drlist[i];

            sb.Append("{\"id\":\"" + dr[value_filed].ToString() + "\",");
            sb.Append("\"text\":\"" + dr[text_filed].ToString() + "\",");
            //sb.Append("\"state\":\"closed\",");
            sb.Append("\"attributes\":{\"sort\":\"" + dr["sort"].ToString() + "\"}");

            if (dr[value_filed].ToString() != parentid.ToString())
            {
                DataRow[] _drlist_other = dt.Select(parent_filed + "=" + dr[value_filed].ToString() + " and " + value_filed + "<>" + parentid);
                if (_drlist_other.Length > 0)
                {
                    sb.Append(",\"state\":\"closed\"");
                    sb.Append(",\"children\":");
                    GetJsonInfo(dt, value_filed, text_filed, parent_filed, int.Parse(dr[value_filed].ToString()));
                }
            }
            if (i == _drlist.Length - 1)
            {
                sb.Append(",\"checked\":false}");
            }
            else
            {
                sb.Append(",\"checked\":false},");
            }
        }
        sb.Append("]");
        return sb;
    }
    #region GetStringIdJson
    /// <summary>
    /// 得到列表ID为字符串的主键的json格式
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="value_filed"></param>
    /// <param name="text_filed"></param>
    /// <param name="parent_filed"></param>
    /// <param name="parentid"></param>
    /// <returns></returns>
    public static string GetStringIdJson(DataTable dt, string value_filed, string text_filed, string parent_filed, string parentid)
    {
        sb = new System.Text.StringBuilder();
        sb = JsonHelper.GetStringIdJsonInfo(dt, value_filed, text_filed, parent_filed, parentid);
        return sb.ToString();
    }

    /// <summary>
    /// 得到列表ID为字符串的主键的json格式
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="value_filed"></param>
    /// <param name="text_filed"></param>
    /// <param name="parent_filed"></param>
    /// <param name="parentid"></param>
    /// <returns></returns>
    public static System.Text.StringBuilder GetStringIdJsonInfo(DataTable dt, string value_filed, string text_filed, string parent_filed, string parentid)
    {
        System.Text.StringBuilder builder = new System.Text.StringBuilder();
        sb.Append("[");
        DataRow[] _drlist = dt.Select(parent_filed + " ='" + parentid + "'");
        for (int i = 0; i < _drlist.Length; i++)
        {
            DataRow dr = _drlist[i];
            sb.Append("{\"" + value_filed + "\":\"" + dr[value_filed].ToString() + "\",");
            sb.Append("\"" + text_filed + "\":\"" + dr[text_filed].ToString() + "\"");
            if (dr[value_filed].ToString() != parentid.ToString())
            {
                DataRow[] _drlist_other = dt.Select(parent_filed + "='" + dr[value_filed].ToString() + "' and " + value_filed + "<>'" + parentid + "'");
                if (_drlist_other.Length > 0)
                {
                    sb.Append(",\"children\":");
                    GetStringIdJsonInfo(dt, value_filed, text_filed, parent_filed, dr[value_filed].ToString());
                }
            }
            if (i == _drlist.Length - 1)
            {
                sb.Append(",\"checked\":false}");
            }
            else
            {
                sb.Append(",\"checked\":false},");
            }
        }
        sb.Append("]");
        return sb;
    }
    #endregion

    #region EnumToJSON
    /// <summary>
    /// 将枚举类型转换Json格式字符串
    /// </summary>
    /// <param name="enumType">枚举类型</param>
    /// <returns></returns>
    public static string EnumToJSON(Type enumType)
    {
        System.Text.StringBuilder json = new System.Text.StringBuilder();
        json.Append("[");
        Type typeDescription = typeof(System.ComponentModel.DescriptionAttribute);
        System.Reflection.FieldInfo[] fields = enumType.GetFields();
        string strText = string.Empty;
        string strValue = string.Empty;
        int i = 0;
        foreach (System.Reflection.FieldInfo field in fields)
        {
            if (field.IsSpecialName) continue;
            strValue = field.GetRawConstantValue().ToString();
            object[] arr = field.GetCustomAttributes(typeDescription, true);
            if (arr.Length > 0)
            {
                strText = (arr[0] as System.ComponentModel.DescriptionAttribute).Description;
            }
            else
            {
                strText = field.Name;
            }
            json.Append("{\"ID\":" + strValue + ",\"TEXT\":\"" + strText + "\"}");
            i++;
            if (i < fields.Length - 1)
                json.Append(",");
        }
        json.Append("]");
        return json.ToString();
    }
    #endregion

    #region JSON文本转对象,泛型方法
    /// <summary>
    /// JSON文本转对象,泛型方法
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="jsonText">JSON文本</param>
    /// <returns>指定类型的对象</returns>
    public static T JSONToObject<T>(string jsonText)
    {
        JavaScriptSerializer jss = new JavaScriptSerializer();
        try
        {
            return jss.Deserialize<T>(jsonText);
        }
        catch (Exception ex)
        {
            throw new Exception("JSONHelper.JSONToObject(): " + ex.Message);
        }
    }
    #endregion

    #region 将JSON文本转换为数据表数据
    /// <summary>
    /// 将JSON文本转换为数据表数据
    /// </summary>
    /// <param name="jsonText">JSON文本</param>
    /// <returns>数据表字典</returns>
    public static Dictionary<string, List<Dictionary<string, object>>> TablesDataFromJSON(string jsonText)
    {
        return JSONToObject<Dictionary<string, List<Dictionary<string, object>>>>(jsonText);
    }
    #endregion

    #region 将JSON文本转换成数据行
    /// <summary>
    /// 将JSON文本转换成数据行
    /// </summary>
    /// <param name="jsonText">JSON文本</param>
    /// <returns>数据行的字典</returns>
    public static Dictionary<string, object> DataRowFromJSON(string jsonText)
    {
        return JSONToObject<Dictionary<string, object>>(jsonText);
    }
    #endregion



    #region

    //bk
    //ttt
    public static string TableToTreeJsons(DataTable data, string idField, string valueField, string parentField, string parentId, bool nodeState, string fields, string level, string pid)
    {
        System.Text.StringBuilder ret = new System.Text.StringBuilder();
        try
        {
            if (data != null && data.Rows.Count > 0)
            {
                ret.Append(TableTreeJsonStrs(data, idField, valueField, parentField, parentId, nodeState, fields, level, pid));
            }
            if (ret.Length <= 0)
            {
                ret.Append("[{\"id\":0,");
                ret.Append("\"text\":\"暂无\",");
                ret.Append("\"state\":\"closed\"]");
            }
        }
        catch
        { }
        return ret.ToString();
    }

    private static string TableTreeJsonStrs(DataTable data, string idField, string valueField, string parentField, string parentId, bool nodeState, string fields, string level, string pid)
    {
        System.Text.StringBuilder ret = new System.Text.StringBuilder();
        ret.Append("[");
        string expr = "";
        DataRow[] drzero = data.Select("PARENT_ID=" + pid + " or " + parentField + " = 0");
        if (drzero != null && drzero.Length > 0)
            expr = "PARENT_ID=" + pid + " or " + parentField + " = 0";
        else
            expr = string.IsNullOrEmpty(parentId) ? (parentField + " IS NULL ") : (parentField + " = " + parentId + "");
        DataRow[] root = data.Select(expr);
        int i = 1;
        foreach (DataRow row in root)
        {
            ret.Append("{\"id\":" + row[idField].ToString() + ",");
            ret.Append("\"text\":\"" + row[valueField].ToString() + "\",");
            ret.Append("\"level\":\"" + row[level].ToString() + "\",");
            ret.Append("\"children\":[],");
            if (!string.IsNullOrEmpty(fields))
            {
                string[] fieldArray = fields.Split(',');
                for (int j = 0; j < fieldArray.Count(); j++)
                {

                    ret.Append("\"" + fieldArray[j] + "\":\"" + row[fieldArray[j]].ToString() + "\",");
                }
            }
            ret.Append("\"state\":\"" + (nodeState ? "open" : "closed") + "\"");

            DataRow[] child = data.Select(idField + "=" + pid);
            if (child.Length > 0)
            {
                //string haschild = Convert.ToString(row["HASCHILD"]) ?? "";
                // if (haschild.Equals("1") && !string.IsNullOrEmpty(haschild)&&i<=root.Count())
                //{

                ret.Append(TableToTreeJsons(data, idField, valueField, parentField, row[idField].ToString(), nodeState, fields, level, pid));
                if (i == root.Length)
                    ret.Append("}");
                else
                    ret.Append("},");

            }
            //}
            else
            {
                //  ret.Append("]");

                if (i == root.Length)
                    ret.Append("}");
                else
                    ret.Append("},");
            }
            ret.Append("]");
            i++;
        }

        ret.Append("]");
        return ret.ToString();
    }
    #endregion


    /// <summary>
    /// 将json转换为DataTable
    /// </summary>
    /// <param name="strJson">得到的json</param>
    /// <returns></returns>
    public static DataTable JsonToDataTable(string strJson)
    {
        //转换json格式
        strJson = strJson.Replace(",\"", "*\"").Replace("\":", "\"#").ToString();
        //取出表名   
        var rg = new Regex(@"(?<={)[^:]+(?=:\[)", RegexOptions.IgnoreCase);
        string strName = rg.Match(strJson).Value;
        DataTable tb = null;
        //去除表名   
        strJson = strJson.Substring(strJson.IndexOf("[") + 1);
        strJson = strJson.Substring(0, strJson.IndexOf("]"));

        //获取数据   
        rg = new Regex(@"(?<={)[^}]+(?=})");
        MatchCollection mc = rg.Matches(strJson);
        for (int i = 0; i < mc.Count; i++)
        {
            string strRow = mc[i].Value;
            string[] strRows = strRow.Split('*');

            //创建表   
            if (tb == null)
            {
                tb = new DataTable();
                tb.TableName = strName;
                foreach (string str in strRows)
                {
                    var dc = new DataColumn();
                    string[] strCell = str.Split('#');

                    if (strCell[0].Substring(0, 1) == "\"")
                    {
                        int a = strCell[0].Length;
                        dc.ColumnName = strCell[0].Substring(1, a - 2);
                    }
                    else
                    {
                        dc.ColumnName = strCell[0];
                    }
                    tb.Columns.Add(dc);
                }
                tb.AcceptChanges();
            }

            //增加内容   
            DataRow dr = tb.NewRow();
            for (int r = 0; r < strRows.Length; r++)
            {
                dr[r] = strRows[r].Split('#')[1].Trim().Replace("，", ",").Replace("：", ":").Replace("\"", "");
            }
            tb.Rows.Add(dr);
            tb.AcceptChanges();
        }

        return tb;
    }

}
