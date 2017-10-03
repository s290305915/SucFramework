using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace SucLib.Common
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
            if (isOpen())
            {
                log4net.ILog log = log4net.LogManager.GetLogger(t);
                log.Error("Error", ex);
            }
        }

        public static void Write(Type t, string msg)
        {
            if (isOpen())
            {
                log4net.ILog log = log4net.LogManager.GetLogger(t);
                log.Error(msg);
            }
        }

        public static void Write(string msg)
        {
            if (isOpen())
            {
                Type t = typeof(String);
                log4net.ILog log = log4net.LogManager.GetLogger(t);
                log.Error(msg);
            }
        }

        private static bool isOpen()
        {
            return ConfigUtil.ConfigHelper.GetConfigBool("OpenLog");
        }


        //public static void Error(string msg)
        //{if (isOpen())
        //  {
        //    Type t = typeof(Exception);
        //    Write(t, msg);
        //}}
        //
        //public static void Debug(string msg)
        //{if (isOpen())
        //   {
        //    Type t = typeof(String);
        //    log4net.ILog log = log4net.LogManager.GetLogger(t);
        //    log.Debug(msg);
        //}}
        //
        //public static void Message(string msg)
        //{if (isOpen())
        // {
        //    Type t = typeof(String);
        //    log4net.ILog log = log4net.LogManager.GetLogger(t);
        //    log.Info(msg);
        //}}

        public static void Write(Exception ex)
        {
            if (isOpen())
            {
                Type t = typeof(Exception);
                log4net.ILog log = log4net.LogManager.GetLogger(t);
                log.Error(ex);
            }
        }
    }
}
