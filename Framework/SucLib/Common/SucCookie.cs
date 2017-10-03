using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SucLib.Common
{
    public class SucCookie
    {
        /// <summary>
        /// 添加cookie
        /// </summary>
        /// <param name="strName">cookie名</param>
        /// <param name="strValue">cookie内容</param>
        /// <param name="strMinute">cookie存活分钟</param>
        public static void Add(string strName, string strValue, int strMinute)
        {
            try
            {
                HttpCookie httpCookie = new HttpCookie(strName);
                httpCookie.Expires = DateTime.Now.AddMinutes((double)strMinute);
                httpCookie.Value = strValue;
                HttpContext.Current.Response.Cookies.Add(httpCookie);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 添加cookie 将最大化保存
        /// </summary>
        /// <param name="strName">cookie名</param>
        /// <param name="strValue">cookie内容</param>
        public static void Add(string strName, object strValue)
        {
            try
            {
                HttpCookie httpCookie = new HttpCookie(strName);
                httpCookie.Expires = DateTime.Now.AddMinutes((double)99999);
                httpCookie.Value = strValue.ToString();
                HttpContext.Current.Response.Cookies.Add(httpCookie);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 读取cookie
        /// </summary>
        /// <param name="strName"></param>
        /// <returns></returns>
        public static string Read(string strName)
        {
            HttpCookie httpCookie = HttpContext.Current.Request.Cookies[strName];
            if (httpCookie != null)
            {
                return httpCookie.Value;
            }
            return "";
        }
        /// <summary>
        /// 删除cookie
        /// </summary>
        /// <param name="strName"></param>
        public static void Delete(string strName)
        {
            try
            {
                HttpCookie httpCookie = new HttpCookie(strName);
                httpCookie.Expires = DateTime.Now.AddDays(-1.0);
                HttpContext.Current.Response.Cookies.Add(httpCookie);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 验证cookie是否存在
        /// </summary>
        /// <param name="strName"></param>
        /// <returns></returns>
        public static bool Exists(string strName)
        {
            bool result = false;
            if (SucCookie.Read(strName) != null && SucCookie.Read(strName) != "")
            {
                result = true;
            }
            return result;
        }
    }
}
