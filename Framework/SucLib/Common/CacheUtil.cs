using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;

namespace SucLib.Common
{
    /// <summary>
    /// 缓存辅助类
    /// </summary>
    public class CacheUtil
    {
        public static bool Insert(string strKey, object valueObj, double duration)
        {
            try
            {
                if (strKey != null && strKey.Length != 0 && valueObj != null)
                {
                    CacheItemRemovedCallback onRemoveCallback = new CacheItemRemovedCallback(CacheUtil.onRemove);
                    HttpContext.Current.Cache.Insert(strKey, valueObj, null, DateTime.Now.AddSeconds(duration), Cache.NoSlidingExpiration, CacheItemPriority.Normal, onRemoveCallback);
                    return true;
                }
                return false;
            }
            catch { return false; }
        }
        /// <summary>
        /// 判断缓存是否已存在
        /// </summary>
        /// <param name="strKey">cachename</param>
        /// <returns></returns>
        public static bool IsExist(string strKey)
        {
            try
            {
                return HttpContext.Current.Cache[strKey] != null;
            }
            catch { return true; }
        }
        /// <summary>
        /// 读取一个cache
        /// </summary>
        /// <param name="strKey"></param>
        /// <returns></returns>
        public static object Read(string strKey)
        {
            try
            {
                if (HttpContext.Current.Cache[strKey] == null)
                {
                    return null;
                }
                object obj = HttpContext.Current.Cache[strKey];
                if (obj == null)
                {
                    return null;
                }
                return obj;
            }
            catch { return null; }
        }
        /// <summary>
        /// 删除一个cache
        /// </summary>
        /// <param name="strKey"></param>
        public static void Remove(string strKey)
        {
            try
            {
                if (HttpContext.Current.Cache[strKey] != null)
                {
                    HttpContext.Current.Cache.Remove(strKey);
                }
            }
            catch { }
        }
        /// <summary>
        /// 根据规则删除cache
        /// </summary>
        /// <param name="pattern"></param>
        public static void RemoveByRegexp(string pattern)
        {
            if (pattern != "")
            {
                IDictionaryEnumerator enumerator = HttpContext.Current.Cache.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    string text = enumerator.Key.ToString();
                    if (Regex.IsMatch(text, pattern))
                    {
                        CacheUtil.Remove(text);
                    }
                }
            }
        }
        /// <summary>
        /// 清空cache
        /// </summary>
        public static void Clear()
        {
            IDictionaryEnumerator enumerator = HttpContext.Current.Cache.GetEnumerator();
            while (enumerator.MoveNext())
            {
                CacheUtil.Remove(enumerator.Key.ToString());
            }
        }
        /// <summary>
        /// 还未实现
        /// </summary>
        /// <param name="strKey"></param>
        /// <param name="obj"></param>
        /// <param name="reason"></param>
        private static void onRemove(string strKey, object obj, CacheItemRemovedReason reason)
        { }
    }
}
