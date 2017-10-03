using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace SucLib.Common
{
    /// <summary>
    /// Javascript公共类
    /// </summary>
    public class JsUtil
    {
        /// <summary>
        /// 弹出对话框
        /// </summary>
        /// <param name="msg"></param>
        public static void ShowMsg(string msg)
        {
            string s = "<Script language='JavaScript'>\r\n                    alert('" + msg + "');</Script>";
            HttpContext.Current.Response.Write(s);
        }
        /// <summary>
        /// 弹出对话框并跳转
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="toURL"></param>
        public static void ShowMsg(string msg, string toURL)
        {
            string format = "<script language=javascript>alert('{0}');window.location.replace('{1}')</script>";
            HttpContext.Current.Response.Write(string.Format(format, msg, toURL));
            HttpContext.Current.Response.End();
        }
        /// <summary>
        /// 跳转历史记录(往回跳几个页面)
        /// </summary>
        /// <param name="value"></param>
        public static void GoHistory(int value)
        {
            string format = "<Script language='JavaScript'>\r\n                    history.go({0});  \r\n                  </Script>";
            HttpContext.Current.Response.Write(string.Format(format, value));
        }
        /// <summary>
        /// 关闭窗口
        /// </summary>
        public static void CloseWindow()
        {
            string s = "<Script language='JavaScript'>\r\n                    parent.opener=null;window.close();  \r\n                  </Script>";
            HttpContext.Current.Response.Write(s);
            HttpContext.Current.Response.End();
        }
        /// <summary>
        /// 刷新父页面（在框架页内）
        /// </summary>
        /// <param name="url"></param>
        public static void RefreshParent(string url)
        {
            string s = "<Script language='JavaScript'>\r\n                    window.opener.location.href='" + url + "';window.close();</Script>";
            HttpContext.Current.Response.Write(s);
        }
        /// <summary>
        /// 重新加载
        /// </summary>
        public static void RefreshOpener()
        {
            string s = "<Script language='JavaScript'>\r\n                    opener.location.reload();\r\n                  </Script>";
            HttpContext.Current.Response.Write(s);
        }
        /// <summary>
        /// 打开小窗口
        /// </summary>
        /// <param name="url">路径</param>
        /// <param name="width">宽</param>
        /// <param name="heigth">高</param>
        /// <param name="top">顶边界</param>
        /// <param name="left">左边界</param>
        public static void OpenWindow(string url, int width, int heigth, int top, int left)
        {
            string s = string.Concat(new object[]
			{
				"<Script language='JavaScript'>window.open('",
				url,
				"','','height=",
				heigth,
				",width=",
				width,
				",top=",
				top,
				",left=",
				left,
				",location=no,menubar=no,resizable=yes,scrollbars=yes,status=yes,titlebar=no,toolbar=no,directories=no');</Script>"
			});
            HttpContext.Current.Response.Write(s);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        public static void LocationNewHref(string url)
        {
            string text = "<Script language='JavaScript'>\r\n                    window.location.replace('{0}');\r\n                  </Script>";
            text = string.Format(text, url);
            HttpContext.Current.Response.Write(text);
        }
        /// <summary>
        /// 弹出对话框
        /// </summary>
        /// <param name="url"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="top"></param>
        /// <param name="left"></param>
        public static void ShowModalDialog(string url, int width, int height, int top, int left)
        {
            string text = string.Concat(new string[]
			{
				"dialogWidth:",
				width.ToString(),
				"px;dialogHeight:",
				height.ToString(),
				"px;dialogLeft:",
				left.ToString(),
				"px;dialogTop:",
				top.ToString(),
				"px;center:yes;help=no;resizable:no;status:no;scroll=yes"
			});
            string s = string.Concat(new string[]
			{
				"<script language=javascript>\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\tshowModalDialog('",
				url,
				"','','",
				text,
				"');</script>"
			});
            HttpContext.Current.Response.Write(s);
        }
        public static void TipAndRedirect(string msg, string goUrl, string second)
        {
            HttpContext.Current.Response.Write(string.Concat(new string[]
			{
				"<meta http-equiv='refresh' content='",
				second,
				";url=",
				goUrl,
				"'>"
			}));
            HttpContext.Current.Response.Write("<br/><br/><p align=center><div style=\"size:12px\">&nbsp;&nbsp;&nbsp;&nbsp;" + msg + "</div>");
            HttpContext.Current.Response.End();
        }
    }
}
