using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;

namespace SucLib.Common
{
    /// <summary>
    /// Common类常用操作
    /// </summary>
    public class CommUtil
    {
        /// <summary>
        /// 去小数点去整数
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static int GetInt(Double num)
        {
            string[] array = num.ToString().Split(new char[]
			{
				'.'
			});
            return int.Parse(array[0]);
        }
        /// <summary>
        /// 筛选安全字符
        /// </summary>
        /// <param name="str"></param>
        /// <param name="exclueStr"></param>
        /// <returns></returns>
        public static string SafeFilter(string str, string exclueStr)
        {
            str = str.Replace(" ", "");
            if (exclueStr.IndexOf("',") < 0)
            {
                str = str.Replace("'", "");
            }
            if (exclueStr.IndexOf(",,") < 0)
            {
                str = str.Replace(",", "");
            }
            if (exclueStr.IndexOf("-,") < 0)
            {
                str = str.Replace("-", "");
            }
            if (exclueStr.IndexOf("+,") < 0)
            {
                str = str.Replace("+", "");
            }
            if (exclueStr.IndexOf("%,") < 0)
            {
                str = str.Replace("%", "");
            }
            if (exclueStr.IndexOf("&,") < 0)
            {
                str = str.Replace("&", "");
            }
            if (exclueStr.IndexOf("$,") < 0)
            {
                str = str.Replace("$", "");
            }
            if (exclueStr.IndexOf("*,") < 0)
            {
                str = str.Replace("*", "");
            }
            if (exclueStr.IndexOf(".,") < 0)
            {
                str = str.Replace(".", "");
            }
            if (exclueStr.IndexOf("=,") < 0)
            {
                str = str.Replace("=", "");
            }
            if (exclueStr.IndexOf("(,") < 0)
            {
                str = str.Replace("(", "");
            }
            if (exclueStr.IndexOf("),") < 0)
            {
                str = str.Replace(")", "");
            }
            if (exclueStr.IndexOf("!,") < 0)
            {
                str = str.Replace("!", "");
            }
            if (exclueStr.IndexOf("@,") < 0)
            {
                str = str.Replace("@", "");
            }
            if (exclueStr.IndexOf("#,") < 0)
            {
                str = str.Replace("#", "");
            }
            if (exclueStr.IndexOf("^,") < 0)
            {
                str = str.Replace("^", "");
            }
            if (exclueStr.IndexOf("|,") < 0)
            {
                str = str.Replace("|", "");
            }
            if (exclueStr.IndexOf(":,") < 0)
            {
                str = str.Replace(":", "");
            }
            if (exclueStr.IndexOf("xp_cmdshell,") < 0)
            {
                str = str.Replace("xp_cmdshell", "");
            }
            if (exclueStr.IndexOf("/add,") < 0)
            {
                str = str.Replace("/add", "");
            }
            str = str.Replace("exec master.dbo.xp_cmdshell", "");
            str = str.Replace("net localgroup administrators", "");
            str = str.Replace("select", "");
            str = str.Replace("insert", "");
            str = str.Replace("delete from", "");
            str = str.Replace("drop table", "");
            str = str.Replace("update", "");
            str = str.Replace("truncate", "");
            str = str.Replace("from", "");
            return str;
        }
        /// <summary>
        /// 去除html标签
        /// </summary>
        /// <param name="Htmlstring"></param>
        /// <returns></returns>
        public static string NoHTML(string Htmlstring)
        {
            Htmlstring = Regex.Replace(Htmlstring, "<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "([\\r\\n])[\\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "<!--.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "&(iexcl|#161);", "¡", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "&(cent|#162);", "¢", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "&(pound|#163);", "£", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "&(copy|#169);", "©", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "&#(\\d+);", "", RegexOptions.IgnoreCase);
            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");
            Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
            return Htmlstring;
        }
        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string EnCrypt(string str)
        {
            int num = int.Parse(ConfigurationSettings.AppSettings["SafeStartIndex"]);
            str = FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5");
            string str2 = CommUtil.Reverse(str.Substring(num, str.Length - num));
            string str3 = CommUtil.Reverse(str.Substring(0, num));
            return str2 + str3;
        }

        private static string Reverse(string str)
        {
            string text = "";
            for (int i = str.Length - 1; i > -1; i--)
            {
                text += str.Substring(i, 1);
            }
            return text;
        }
        public static void Debug(string str)
        {
            HttpContext.Current.Response.Write(str);
            HttpContext.Current.Response.End();
        }
        /// <summary>
        /// 获取程序路径
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetAppPath(int type)
        {
            string result;
            if (type == 1)
            {
                result = HttpContext.Current.Request.ServerVariables["APPL_PHYSICAL_PATH"];
            }
            else
            {
                string text = HttpContext.Current.Request.ServerVariables["path_translated"];
                result = text.Substring(0, text.LastIndexOf("\\") + 1);
            }
            return result;
        }
        /// <summary>
        /// 判断是否为数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNumStr(string str)
        {
            bool result = true;
            for (int i = 0; i < str.Length; i++)
            {
                char c = str[i];
                if (!char.IsNumber(c))
                {
                    result = false;
                    break;
                }
            }
            return result;
        }
        /// <summary>
        /// 获取随机时间的文件名
        /// </summary>
        /// <returns></returns>
        public static string GetDataTimeRandomFileName()
        {
            Random random = new Random(1000);
            return string.Concat(new string[]
			{
				DateTime.Now.Date.Year.ToString(),
				DateTime.Now.Date.Month.ToString(),
				DateTime.Now.Date.Day.ToString(),
				DateTime.Now.Hour.ToString(),
				DateTime.Now.Minute.ToString(),
				DateTime.Now.Second.ToString(),
				DateTime.Now.Millisecond.ToString(),
				random.Next().ToString()
			});
        }
        /// <summary>
        /// 检查ID是否可用
        /// </summary>
        /// <param name="ID"></param>
        public static void ValidID(string ID)
        {
            if (ID.Trim() == "" || !CommUtil.IsNumStr(ID))
            {
                throw new Exception("参数传递错误!");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static int DateDiff(string unit, DateTime startDate, DateTime endDate)
        {
            int result = 0;
            int num = endDate.Year - startDate.Year;
            int num2 = endDate.Month - startDate.Month;
            int num3 = endDate.Day - startDate.Day;
            int num4 = endDate.Hour - startDate.Hour;
            int num5 = endDate.Minute - startDate.Minute;
            int arg_5B_0 = endDate.Second;
            int arg_63_0 = startDate.Second;
            if (unit == "year")
            {
                result = num;
            }
            if (unit == "month")
            {
                result = num * 12 + num2;
            }
            if (unit == "day")
            {
                result = (num * 12 + num2) * 30 + num3;
            }
            if (unit == "hour")
            {
                result = ((num * 12 + num2) * 30 + num3) * 24 + num4;
            }
            if (unit == "minute")
            {
                result = (((num * 12 + num2) * 30 + num3) * 24 + num4) * 60 + num5;
            }
            if (unit == "second")
            {
                result = ((((num * 12 + num2) * 30 + num3) * 24 + num4) * 60 + num5) * 60;
            }
            return result;
        }
        /// <summary>
        /// 获取用户IP
        /// </summary>
        /// <returns></returns>
        public static string GetUserIP()
        {
            string result;
            if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
            {
                result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else
            {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="slen"></param>
        /// <param name="showDot"></param>
        /// <returns></returns>
        public static string GetSpeStr(string str, int slen, bool showDot)
        {
            if (str.Length > slen)
            {
                if (showDot)
                {
                    str = str.Substring(0, slen) + "...";
                }
                else
                {
                    str = str.Substring(0, slen);
                }
            }
            return str;
        }
        /// <summary>
        /// js过滤
        /// </summary>
        /// <param name="str"></param>
        /// <param name="exclueStr"></param>
        /// <returns></returns>
        public static string JsFilter(string str, string exclueStr)
        {
            if (exclueStr.IndexOf("<script>,") < 0)
            {
                str = str.Replace("<script>", "");
            }
            if (exclueStr.IndexOf("</script>,") < 0)
            {
                str = str.Replace("</script>", "");
            }
            if (exclueStr.IndexOf("javascript,") < 0)
            {
                str = str.Replace("javascript", "");
            }
            if (exclueStr.IndexOf("/,") < 0)
            {
                str = str.Replace("/", "");
            }
            if (exclueStr.IndexOf("',") < 0)
            {
                str = str.Replace("'", "");
            }
            if (exclueStr.IndexOf(";,") < 0)
            {
                str = str.Replace(";", "");
            }
            if (exclueStr.IndexOf("&,") < 0)
            {
                str = str.Replace("&", "");
            }
            if (exclueStr.IndexOf("#,") < 0)
            {
                str = str.Replace("#", "");
            }
            return str;
        }
        private static string ToSBC(string input)
        {
            char[] array = input.ToCharArray();
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == ' ')
                {
                    array[i] = '\u3000';
                }
                else
                {
                    if (array[i] < '\u007f')
                    {
                        array[i] += 'ﻠ';
                    }
                }
            }
            return new string(array);
        }
        private static string ToDBC(string input)
        {
            char[] array = input.ToCharArray();
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == '\u3000')
                {
                    array[i] = ' ';
                }
                else
                {
                    if (array[i] > '＀' && array[i] < '｟')
                    {
                        array[i] -= 'ﻠ';
                    }
                }
            }
            return new string(array);
        }
        public static string TSEncode(string input, string exclueStr)
        {
            string text = input;
            if (exclueStr.IndexOf("',") < 0)
            {
                text = text.Replace("'", CommUtil.ToSBC("'"));
            }
            if (exclueStr.IndexOf("%,") < 0)
            {
                text = text.Replace("%", CommUtil.ToSBC("%"));
            }
            if (exclueStr.IndexOf(" ,") < 0)
            {
                text = text.Replace(" ", CommUtil.ToSBC(" "));
            }
            if (exclueStr.IndexOf("-,") < 0)
            {
                text = text.Replace("-", CommUtil.ToSBC("-"));
            }
            if (exclueStr.IndexOf("+,") < 0)
            {
                text = text.Replace("+", CommUtil.ToSBC("+"));
            }
            if (exclueStr.IndexOf("&,") < 0)
            {
                text = text.Replace("&", CommUtil.ToSBC("&"));
            }
            if (exclueStr.IndexOf("$,") < 0)
            {
                text = text.Replace("$", CommUtil.ToSBC("$"));
            }
            if (exclueStr.IndexOf(".,") < 0)
            {
                text = text.Replace(".", CommUtil.ToSBC("."));
            }
            if (exclueStr.IndexOf("=,") < 0)
            {
                text = text.Replace("=", CommUtil.ToSBC("="));
            }
            if (exclueStr.IndexOf("(,") < 0)
            {
                text = text.Replace("(", CommUtil.ToSBC("("));
            }
            if (exclueStr.IndexOf("),") < 0)
            {
                text = text.Replace(")", CommUtil.ToSBC(")"));
            }
            if (exclueStr.IndexOf("<,") < 0)
            {
                text = text.Replace("<", CommUtil.ToSBC("<"));
            }
            if (exclueStr.IndexOf(">,") < 0)
            {
                text = text.Replace(">", CommUtil.ToSBC(">"));
            }
            if (exclueStr.IndexOf("@,") < 0)
            {
                text = text.Replace("@", CommUtil.ToSBC("@"));
            }
            if (exclueStr.IndexOf("#,") < 0)
            {
                text = text.Replace("#", CommUtil.ToSBC("#"));
            }
            if (exclueStr.IndexOf(",,") < 0)
            {
                text = text.Replace(",", CommUtil.ToSBC(","));
            }
            if (exclueStr.IndexOf(":,") < 0)
            {
                text = text.Replace(":", CommUtil.ToSBC(":"));
            }
            if (exclueStr.IndexOf(";,") < 0)
            {
                text = text.Replace(";", CommUtil.ToSBC(";"));
            }
            if (exclueStr.IndexOf("!,") < 0)
            {
                text = text.Replace("!", CommUtil.ToSBC("!"));
            }
            if (exclueStr.IndexOf("|,") < 0)
            {
                text = text.Replace("|", CommUtil.ToSBC("|"));
            }
            return text;
        }
    }
}
