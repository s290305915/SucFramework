using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
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

    public static void Write(string msg)
    {
        log4net.ILog log = log4net.LogManager.GetLogger(typeof(string));
        log.Error(msg);
    }

    public static void Write(Exception ex)
    {
        Type t = typeof(Exception);
        log4net.ILog log = log4net.LogManager.GetLogger(t);
        log.Error(ex);
    }

    public static void Error(Exception ex)
    {
        Console.Write(ex.Message);
        log4net.ILog log = log4net.LogManager.GetLogger(typeof(string));
        log.Error("Error", ex);
    }

    public static void Info(string info)
    {
        Console.WriteLine(info);
        log4net.ILog log = log4net.LogManager.GetLogger(typeof(string));
        log.Info(info);
    }

    public static void Warn(string info)
    {
        Console.WriteLine(info);
        log4net.ILog log = log4net.LogManager.GetLogger(typeof(string));
        log.Warn(info);
    }
}

