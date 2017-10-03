using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace WindowsFormsApplication1
{
    public class LogHelper
    {
        /// <summary>
        /// 输出日志到Log4net 
        /// </summary>
        /// <param name="t"></param>
        /// <param name="ex"></param>
        public static void Write(Type t, Exception ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Error("Error", ex);
        }

        public static void Write(Type t, string msg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Error(msg);
        }

        public static void Write(Exception ex)
        {
            Type t = typeof(Exception);
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Error(ex);
        }
    }
}
