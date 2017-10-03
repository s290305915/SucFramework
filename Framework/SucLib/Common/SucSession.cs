using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace SucLib.Common
{
    public class SucSession
    {
        /// <summary>
        /// 添加Session
        /// </summary>
        /// <param name="Key">Key</param>
        /// <param name="Value">Value</param>
        public static void Add(string Key, string Value)
        {
            HttpContext.Current.Session[Key] = Value;
        }
        /// <summary>
        /// 判断Session是否存在
        /// </summary>
        /// <param name="Key">Key</param>
        /// <returns></returns>
        public static bool Exists(string Key)
        {
            bool result = true;
            if (HttpContext.Current.Session[Key] == null || HttpContext.Current.Session[Key].ToString().Trim() == "")
            {
                result = false;
            }
            return result;
        }
        /// <summary>
        /// 去除相应Session
        /// </summary>
        /// <param name="Key">Key</param>
        public static void Delete(string Key)
        {
            HttpContext.Current.Session[Key] = "";
        }
        /// <summary>
        /// 读取
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static string Read(string Key)
        {
            if (SucSession.Exists(Key))
            {
                return HttpContext.Current.Session[Key].ToString();
            }
            return "";
        }
    }
}
