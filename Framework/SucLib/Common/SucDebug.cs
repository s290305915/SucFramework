using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace SucLib.Common
{
    public class SucDebug
    {
        public static void Write(string msg)
        {
            HttpContext.Current.Response.Write(msg);
        }
        public static void WriteEnd(string msg)
        {
            HttpContext.Current.Response.Write(msg);
            HttpContext.Current.Response.End();
        }
    }
}
